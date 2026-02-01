#include "include/Fast_Math_Library.hlsl"

float2 warp(float2 inuv)
{
    float2 uv  = inuv;
    float time = LIL_TIME * _WarpAnimSpeed;
    float x    = uv.x;
    float y    = uv.y;

    // Normalize each sin() phase argument to 0-2π to prevent discontinuities
    x += SIN_NORMALIZED(y * _WarpBigFreqY + time * _WarpBigSpeedX) * _WarpBigAmp;
    y += SIN_NORMALIZED(x * _WarpBigFreqX + time * _WarpBigSpeedY) * _WarpBigAmp;
    x += SIN_NORMALIZED(y * _WarpSmallFreqY + time * _WarpSmallSpeedX) * _WarpSmallAmp;
    y += SIN_NORMALIZED(x * _WarpSmallFreqX + time * _WarpSmallSpeedY) * _WarpSmallAmp;

    uv.x = x;
    uv.y = y;

    return inuv + (uv - inuv) * _WarpIntensity;
}

// Background Warping
#if defined(LIL_REFRACTION) && !defined(LIL_LITE)
    void lilBGWarp(inout lilFragData fd LIL_SAMP_IN_FUNC(samp))
    {
        float2 warpUV = fd.uvScn;
        warpUV = warp(warpUV);
        float3 warpedBG = LIL_GET_BG_TEX(warpUV, 0).rgb;

        fd.col.rgb = lerp(warpedBG, fd.col.rgb, fd.col.a);
    }

//--------------------------------------------------------------
// Refraction Blur - SGMB Implementation
// (Single-pass Gaussian-weighted Multi-ring Blur)
//
// Inspired by Kawase Blur's diagonal sampling approach,
// adapted for single-pass rendering (Mainly VRChat)
//--------------------------------------------------------------
    void lilRefractionSGMB(inout lilFragData fd LIL_SAMP_IN_FUNC(samp))
    {
        float2 refractUV = fd.uvScn + (pow(1.0 - fd.nv, _RefractionFresnelPower) * _RefractionStrength) * mul((float3x3)LIL_MATRIX_V, fd.N).xy;

        #if defined(LIL_REFRACTION_BLUR2)
            #if defined(LIL_BRP)
                float3 refractCol = 0;

                // 元のぼかし強度計算を完全に流用
                float baseBlurOffset = fd.perceptualRoughness / sqrt(fd.positionSS.w) * (0.03 / LIL_REFRACTION_SAMPNUM);

                if(_RefractionType == 1)
                {
                    // === Bilinear Interpolation：4タップ（バイリニア）===
                    // GPUのバイリニア補間機能を活用した最速ブラー
                    float2 blurOffset = float2(
                        baseBlurOffset * LIL_MATRIX_P._m00 * 0.5,
                        baseBlurOffset * LIL_MATRIX_P._m11 * 0.5
                    );

                    // 対角4点をバイリニア補間でサンプリング
                    refractCol += LIL_GET_GRAB_TEX(refractUV + float2(blurOffset.x, blurOffset.y), 0).rgb;
                    refractCol += LIL_GET_GRAB_TEX(refractUV + float2(blurOffset.x, -blurOffset.y), 0).rgb;
                    refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-blurOffset.x, blurOffset.y), 0).rgb;
                    refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-blurOffset.x, -blurOffset.y), 0).rgb;

                    refractCol /= 4.0;
                }
                else if(_RefractionType == 2)
                {
                    if(_RefractionSGMBQuality == 3)
                    {
                        // === Ultra：25タップ（ガウス重み付き）===
                        float sum = 0;
                        // X方向も追加（元はY方向のみ）
                        float2 blurOffset = float2(
                            baseBlurOffset * LIL_MATRIX_P._m00,
                            baseBlurOffset * LIL_MATRIX_P._m11
                        );

                        // ガウス分布パラメータ（元の式と同じ）
                        // sigma^2 = LIL_REFRACTION_SAMPNUM^2 / 2 = 8^2 / 2 = 32
                        float sigmaSq = (LIL_REFRACTION_SAMPNUM * LIL_REFRACTION_SAMPNUM) / 2.0;

                        // 中央
                        float dist0 = 0;
                        float weight0 = exp(-dist0 * dist0 / sigmaSq);
                        refractCol += LIL_GET_GRAB_TEX(refractUV, 0).rgb * weight0;
                        sum += weight0;

                        // リング1（0.5）- 対角4点
                        float2 offset1 = blurOffset * 0.5;
                        float dist1 = 0.5;
                        float weight1 = exp(-dist1 * dist1 / sigmaSq);
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset1.x, offset1.y), 0).rgb * weight1;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset1.x, -offset1.y), 0).rgb * weight1;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset1.x, offset1.y), 0).rgb * weight1;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset1.x, -offset1.y), 0).rgb * weight1;
                        sum += weight1 * 4;

                        // リング2（1.5）- 対角4点 + 十字4点
                        float2 offset2 = blurOffset * 1.5;
                        float dist2 = 1.5;
                        float weight2 = exp(-dist2 * dist2 / sigmaSq);
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset2.x, offset2.y), 0).rgb * weight2;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset2.x, -offset2.y), 0).rgb * weight2;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset2.x, offset2.y), 0).rgb * weight2;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset2.x, -offset2.y), 0).rgb * weight2;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset2.x, 0), 0).rgb * weight2;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset2.x, 0), 0).rgb * weight2;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(0, offset2.y), 0).rgb * weight2;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(0, -offset2.y), 0).rgb * weight2;
                        sum += weight2 * 8;

                        // リング3（2.5）- 対角4点 + 十字4点
                        float2 offset3 = blurOffset * 2.5;
                        float dist3 = 2.5;
                        float weight3 = exp(-dist3 * dist3 / sigmaSq);
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset3.x, offset3.y), 0).rgb * weight3;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset3.x, -offset3.y), 0).rgb * weight3;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset3.x, offset3.y), 0).rgb * weight3;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset3.x, -offset3.y), 0).rgb * weight3;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset3.x, 0), 0).rgb * weight3;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset3.x, 0), 0).rgb * weight3;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(0, offset3.y), 0).rgb * weight3;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(0, -offset3.y), 0).rgb * weight3;
                        sum += weight3 * 8;

                        // リング4（3.5）- 対角4点
                        float2 offset4 = blurOffset * 3.5;
                        float dist4 = 3.5;
                        float weight4 = exp(-dist4 * dist4 / sigmaSq);
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset4.x, offset4.y), 0).rgb * weight4;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset4.x, -offset4.y), 0).rgb * weight4;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset4.x, offset4.y), 0).rgb * weight4;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset4.x, -offset4.y), 0).rgb * weight4;
                        sum += weight4 * 4;

                        refractCol /= sum;
                    }
                    else if(_RefractionSGMBQuality == 2)
                    {
                        // === High：17タップ（ガウス重み付き）===
                        float sum = 0;
                        float2 blurOffset = float2(
                            baseBlurOffset * LIL_MATRIX_P._m00,
                            baseBlurOffset * LIL_MATRIX_P._m11
                        );

                        float sigmaSq = (LIL_REFRACTION_SAMPNUM * LIL_REFRACTION_SAMPNUM) / 2.0;

                        // 中央
                        float dist0 = 0;
                        float weight0 = exp(-dist0 * dist0 / sigmaSq);
                        refractCol += LIL_GET_GRAB_TEX(refractUV, 0).rgb * weight0;
                        sum += weight0;

                        // リング1（0.5）- 対角4点
                        float2 offset1 = blurOffset * 0.5;
                        float dist1 = 0.5;
                        float weight1 = exp(-dist1 * dist1 / sigmaSq);
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset1.x, offset1.y), 0).rgb * weight1;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset1.x, -offset1.y), 0).rgb * weight1;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset1.x, offset1.y), 0).rgb * weight1;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset1.x, -offset1.y), 0).rgb * weight1;
                        sum += weight1 * 4;

                        // リング2（1.5）- 対角4点
                        float2 offset2 = blurOffset * 1.5;
                        float dist2 = 1.5;
                        float weight2 = exp(-dist2 * dist2 / sigmaSq);
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset2.x, offset2.y), 0).rgb * weight2;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset2.x, -offset2.y), 0).rgb * weight2;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset2.x, offset2.y), 0).rgb * weight2;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset2.x, -offset2.y), 0).rgb * weight2;
                        sum += weight2 * 4;

                        // リング3（2.5）- 対角4点 + 十字4点
                        float2 offset3 = blurOffset * 2.5;
                        float dist3 = 2.5;
                        float weight3 = exp(-dist3 * dist3 / sigmaSq);
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset3.x, offset3.y), 0).rgb * weight3;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset3.x, -offset3.y), 0).rgb * weight3;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset3.x, offset3.y), 0).rgb * weight3;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset3.x, -offset3.y), 0).rgb * weight3;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset3.x, 0), 0).rgb * weight3;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset3.x, 0), 0).rgb * weight3;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(0, offset3.y), 0).rgb * weight3;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(0, -offset3.y), 0).rgb * weight3;
                        sum += weight3 * 8;

                        refractCol /= sum;
                    }
                    else if(_RefractionSGMBQuality == 1)
                    {
                        // === Mid：13タップ（ガウス重み付き）===
                        float sum = 0;
                        float2 blurOffset = float2(
                            baseBlurOffset * LIL_MATRIX_P._m00,
                            baseBlurOffset * LIL_MATRIX_P._m11
                        );

                        float sigmaSq = (LIL_REFRACTION_SAMPNUM * LIL_REFRACTION_SAMPNUM) / 2.0;

                        // 中央
                        float dist0 = 0;
                        float weight0 = exp(-dist0 * dist0 / sigmaSq);
                        refractCol += LIL_GET_GRAB_TEX(refractUV, 0).rgb * weight0;
                        sum += weight0;

                        // リング1（0.5）- 対角4点
                        float2 offset1 = blurOffset * 0.5;
                        float dist1 = 0.5;
                        float weight1 = exp(-dist1 * dist1 / sigmaSq);
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset1.x, offset1.y), 0).rgb * weight1;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset1.x, -offset1.y), 0).rgb * weight1;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset1.x, offset1.y), 0).rgb * weight1;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset1.x, -offset1.y), 0).rgb * weight1;
                        sum += weight1 * 4;

                        // リング2（1.5）- 十字4点
                        float2 offset2 = blurOffset * 1.5;
                        float dist2 = 1.5;
                        float weight2 = exp(-dist2 * dist2 / sigmaSq);
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset2.x, 0), 0).rgb * weight2;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset2.x, 0), 0).rgb * weight2;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(0, offset2.y), 0).rgb * weight2;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(0, -offset2.y), 0).rgb * weight2;
                        sum += weight2 * 4;

                        // リング3（2.5）- 対角4点
                        float2 offset3 = blurOffset * 2.5;
                        float dist3 = 2.5;
                        float weight3 = exp(-dist3 * dist3 / sigmaSq);
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset3.x, offset3.y), 0).rgb * weight3;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset3.x, -offset3.y), 0).rgb * weight3;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset3.x, offset3.y), 0).rgb * weight3;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset3.x, -offset3.y), 0).rgb * weight3;
                        sum += weight3 * 4;

                        refractCol /= sum;
                    }
                    else
                    {
                        // === Low：8タップ（ガウス重み付き）===
                        float sum = 0;
                        float2 blurOffset = float2(
                            baseBlurOffset * LIL_MATRIX_P._m00,
                            baseBlurOffset * LIL_MATRIX_P._m11
                        );

                        float sigmaSq = (LIL_REFRACTION_SAMPNUM * LIL_REFRACTION_SAMPNUM) / 2.0;

                        // リング1（0.5）- 対角4点
                        float2 offset1 = blurOffset * 0.5;
                        float dist1 = 0.5;
                        float weight1 = exp(-dist1 * dist1 / sigmaSq);
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset1.x, offset1.y), 0).rgb * weight1;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset1.x, -offset1.y), 0).rgb * weight1;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset1.x, offset1.y), 0).rgb * weight1;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset1.x, -offset1.y), 0).rgb * weight1;
                        sum += weight1 * 4;

                        // リング2（1.5）- 十字4点
                        float2 offset2 = blurOffset * 1.5;
                        float dist2 = 1.5;
                        float weight2 = exp(-dist2 * dist2 / sigmaSq);
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(offset2.x, 0), 0).rgb * weight2;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(-offset2.x, 0), 0).rgb * weight2;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(0, offset2.y), 0).rgb * weight2;
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(0, -offset2.y), 0).rgb * weight2;
                        sum += weight2 * 4;

                        refractCol /= sum;
                    }
                }
                else
                {
                    // === デフォルト：元の33タップ垂直ガウシアン ===
                    float sum = 0;
                    float blurOffset = baseBlurOffset * LIL_MATRIX_P._m11;
                    for(int j = -16; j <= 16; j++)
                    {
                        refractCol += LIL_GET_GRAB_TEX(refractUV + float2(0, j * blurOffset), 0).rgb * LIL_REFRACTION_GAUSDIST(j);
                        sum += LIL_REFRACTION_GAUSDIST(j);
                    }
                    refractCol /= sum;
                }

                refractCol *= _RefractionColor.rgb;
            #else
                // URP/HDRP: ミップマップ使用
                float refractLod = min(sqrt(fd.perceptualRoughness / sqrt(fd.positionSS.w) * 5.0), 10);
                float3 refractCol = LIL_GET_GRAB_TEX(refractUV, refractLod).rgb * _RefractionColor.rgb;
            #endif
        #else
            // ブラーなし
            float3 refractCol = LIL_GET_BG_TEX(refractUV, 0).rgb * _RefractionColor.rgb;
        #endif

        if(_RefractionColorFromMain) refractCol *= fd.albedo;
        fd.col.rgb = lerp(refractCol, fd.col.rgb, fd.col.a);
    }
#endif


// Main2nd
#if defined(LIL_FEATURE_MAIN2ND) && !defined(LIL_LITE)
    void lilGetMain2ndMore(inout lilFragData fd, inout float4 color2nd, inout float main2ndDissolveAlpha LIL_SAMP_IN_FUNC(samp))
    {
        #if !(defined(LIL_FEATURE_DECAL) && defined(LIL_FEATURE_ANIMATE_DECAL))
            float4 _Main2ndTexDecalAnimation = 0.0;
            float4 _Main2ndTexDecalSubParam  = 0.0;
        #endif
        #if !defined(LIL_FEATURE_DECAL)
            bool _Main2ndTexIsDecal          = false;
            bool _Main2ndTexIsLeftOnly       = false;
            bool _Main2ndTexIsRightOnly      = false;
            bool _Main2ndTexShouldCopy       = false;
            bool _Main2ndTexShouldFlipMirror = false;
            bool _Main2ndTexShouldFlipCopy   = false;
        #endif
        color2nd = _Color2nd;
        if(!_UseMain2ndTex) return;
        float2 uv2nd = fd.uv0;
        if(_Main2ndTex_UVMode == 1) uv2nd = fd.uv1;
        if(_Main2ndTex_UVMode == 2) uv2nd = fd.uv2;
        if(_Main2ndTex_UVMode == 3) uv2nd = fd.uv3;
        if(_Main2ndTex_UVMode == 4) uv2nd = fd.uvMat;
        if(_UseWarp && _UseWarpMain2nd) uv2nd = warp(uv2nd);
        #if defined(LIL_FEATURE_Main2ndTex)
            color2nd *= LIL_GET_SUBTEX(_Main2ndTex, uv2nd);
        #endif
        #if defined(LIL_FEATURE_Main2ndBlendMask)
            if(_UseWarp && _UseWarpMain2nd) fd.uvMain = warp(fd.uvMain);
            color2nd.a *= LIL_SAMPLE_2D(_Main2ndBlendMask, samp, fd.uvMain).r;
        #endif
        #if defined(LIL_FEATURE_AUDIOLINK)
            if(_AudioLink2Main2nd) color2nd.a *= fd.audioLinkValue;
        #endif
        color2nd.a = lerp(color2nd.a,
                            color2nd.a * saturate((fd.depth - _Main2ndDistanceFade.x) / (_Main2ndDistanceFade.y - _Main2ndDistanceFade.x)),
                            _Main2ndDistanceFade.z);
        if(_Main2ndTex_Cull == 1 && fd.facing > 0 || _Main2ndTex_Cull == 2 && fd.facing < 0) color2nd.a = 0;
        #if LIL_RENDER != 0
            if(_Main2ndTexAlphaMode != 0)
            {
                if(_Main2ndTexAlphaMode == 1) fd.col.a = color2nd.a;
                if(_Main2ndTexAlphaMode == 2) fd.col.a = fd.col.a * color2nd.a;
                if(_Main2ndTexAlphaMode == 3) fd.col.a = saturate(fd.col.a + color2nd.a);
                if(_Main2ndTexAlphaMode == 4) fd.col.a = saturate(fd.col.a - color2nd.a);
                color2nd.a = 1;
            }
        #endif
        fd.col.rgb = lilBlendColor(fd.col.rgb, color2nd.rgb, color2nd.a * _Main2ndEnableLighting, _Main2ndTexBlendMode);
    }
#endif

// Main3rd
#if defined(LIL_FEATURE_MAIN3RD) && !defined(LIL_LITE)
    void lilGetMain3rdMore(inout lilFragData fd, inout float4 color3rd, inout float main3rdDissolveAlpha LIL_SAMP_IN_FUNC(samp))
    {
        #if !(defined(LIL_FEATURE_DECAL) && defined(LIL_FEATURE_ANIMATE_DECAL))
            float4 _Main3rdTexDecalAnimation = 0.0;
            float4 _Main3rdTexDecalSubParam  = 0.0;
        #endif
        #if !defined(LIL_FEATURE_DECAL)
            bool _Main3rdTexIsDecal          = false;
            bool _Main3rdTexIsLeftOnly       = false;
            bool _Main3rdTexIsRightOnly      = false;
            bool _Main3rdTexShouldCopy       = false;
            bool _Main3rdTexShouldFlipMirror = false;
            bool _Main3rdTexShouldFlipCopy   = false;
        #endif
        color3rd = _Color3rd;
        if(!_UseMain3rdTex) return;

        float2 uv3rd = fd.uv0;
        if(_Main3rdTex_UVMode == 1) uv3rd = fd.uv1;
        if(_Main3rdTex_UVMode == 2) uv3rd = fd.uv2;
        if(_Main3rdTex_UVMode == 3) uv3rd = fd.uv3;
        if(_Main3rdTex_UVMode == 4) uv3rd = fd.uvMat;
        if(_UseWarp && _UseWarpMain3rd) uv3rd = warp(uv3rd);
        #if defined(LIL_FEATURE_Main3rdTex)
            color3rd *= LIL_GET_SUBTEX(_Main3rdTex, uv3rd);
        #endif
        #if defined(LIL_FEATURE_Main3rdBlendMask)
            if(_UseWarp && _UseWarpMain3rd) fd.uvMain = warp(fd.uvMain);
            color3rd.a *= LIL_SAMPLE_2D(_Main3rdBlendMask, samp, fd.uvMain).r;
        #endif
        #if defined(LIL_FEATURE_AUDIOLINK)
            if(_AudioLink2Main3rd) color3rd.a *= fd.audioLinkValue;
        #endif
        color3rd.a = lerp(color3rd.a,
                            color3rd.a * saturate((fd.depth - _Main3rdDistanceFade.x) / (_Main3rdDistanceFade.y - _Main3rdDistanceFade.x)),
                            _Main3rdDistanceFade.z);
        if(_Main3rdTex_Cull == 1 && fd.facing > 0 || _Main3rdTex_Cull == 2 && fd.facing < 0) color3rd.a = 0;
        #if LIL_RENDER != 0
            if(_Main3rdTexAlphaMode != 0)
            {
                if(_Main3rdTexAlphaMode == 1) fd.col.a = color3rd.a;
                if(_Main3rdTexAlphaMode == 2) fd.col.a = fd.col.a * color3rd.a;
                if(_Main3rdTexAlphaMode == 3) fd.col.a = saturate(fd.col.a + color3rd.a);
                if(_Main3rdTexAlphaMode == 4) fd.col.a = saturate(fd.col.a - color3rd.a);
                color3rd.a = 1;
            }
        #endif
        fd.col.rgb = lilBlendColor(fd.col.rgb, color3rd.rgb, color3rd.a * _Main3rdEnableLighting, _Main3rdTexBlendMode);
    }
#endif

// Main4th
void lilGetMain4th(inout lilFragData fd, inout float4 color4th LIL_SAMP_IN_FUNC(samp))
{
    color4th = _Color4th;
    if(!_UseMain4thTex) return;

    float2 uv4th  = fd.uv0;
    if(_Main4thTex_UVMode == 1) uv4th = fd.uv1;
    if(_Main4thTex_UVMode == 2) uv4th = fd.uv2;
    if(_Main4thTex_UVMode == 3) uv4th = fd.uv3;
    if(_Main4thTex_UVMode == 4) uv4th = fd.uvMat;
    if(_UseWarp && _UseWarpMain4th)
    {
        uv4th = warp(uv4th);
        fd.uvMain = warp(fd.uvMain);
    }

    color4th *= LIL_GET_SUBTEX(_Main4thTex, uv4th);
    color4th.a *= LIL_SAMPLE_2D(_Main4thBlendMask, samp, fd.uvMain).r;
    if(_AudioLink2Main4th) color4th.a *= fd.audioLinkValue;
    color4th.a = lerp(color4th.a,
                        color4th.a * saturate((fd.depth - _Main4thDistanceFade.x) / (_Main4thDistanceFade.y - _Main4thDistanceFade.x)),
                        _Main4thDistanceFade.z);
    if(_Main4thTex_Cull == 1 && fd.facing > 0 || _Main4thTex_Cull == 2 && fd.facing < 0) color4th.a = 0;

    #if LIL_RENDER != 0
        if(_Main4thTexAlphaMode != 0)
        {
            if(_Main4thTexAlphaMode == 1) fd.col.a = color4th.a;
            if(_Main4thTexAlphaMode == 2) fd.col.a = fd.col.a * color4th.a;
            if(_Main4thTexAlphaMode == 3) fd.col.a = saturate(fd.col.a + color4th.a);
            if(_Main4thTexAlphaMode == 4) fd.col.a = saturate(fd.col.a - color4th.a);
            color4th.a = 1;
        }
    #endif

    fd.col.rgb = lilBlendColor(fd.col.rgb, color4th.rgb, color4th.a * _Main4thEnableLighting, _Main4thTexBlendMode);
}

// Main5th
void lilGetMain5th(inout lilFragData fd, inout float4 color5th LIL_SAMP_IN_FUNC(samp))
{
    color5th = _Color5th;
    if(!_UseMain5thTex) return;

    float2 uv5th  = fd.uv0;
    if(_Main5thTex_UVMode == 1) uv5th = fd.uv1;
    if(_Main5thTex_UVMode == 2) uv5th = fd.uv2;
    if(_Main5thTex_UVMode == 3) uv5th = fd.uv3;
    if(_Main5thTex_UVMode == 4) uv5th = fd.uvMat;
    if(_UseWarp && _UseWarpMain5th)
    {
        uv5th = warp(uv5th);
        fd.uvMain = warp(fd.uvMain);
    }

    color5th *= LIL_GET_SUBTEX(_Main5thTex, uv5th);
    color5th.a *= LIL_SAMPLE_2D(_Main5thBlendMask, samp, fd.uvMain).r;
    if(_AudioLink2Main5th) color5th.a *= fd.audioLinkValue;
    color5th.a = lerp(color5th.a,
                        color5th.a * saturate((fd.depth - _Main5thDistanceFade.x) / (_Main5thDistanceFade.y - _Main5thDistanceFade.x)),
                        _Main5thDistanceFade.z);
    if(_Main5thTex_Cull == 1 && fd.facing > 0 || _Main5thTex_Cull == 2 && fd.facing < 0) color5th.a = 0;

    #if LIL_RENDER != 0
        if(_Main5thTexAlphaMode != 0)
        {
            if(_Main5thTexAlphaMode == 1) fd.col.a = color5th.a;
            if(_Main5thTexAlphaMode == 2) fd.col.a = fd.col.a * color5th.a;
            if(_Main5thTexAlphaMode == 3) fd.col.a = saturate(fd.col.a + color5th.a);
            if(_Main5thTexAlphaMode == 4) fd.col.a = saturate(fd.col.a - color5th.a);
            color5th.a = 1;
        }
    #endif

    fd.col.rgb = lilBlendColor(fd.col.rgb, color5th.rgb, color5th.a * _Main5thEnableLighting, _Main5thTexBlendMode);
}

// Main6th
void lilGetMain6th(inout lilFragData fd, inout float4 color6th LIL_SAMP_IN_FUNC(samp))
{
    color6th = _Color6th;
    if(!_UseMain6thTex) return;

    float2 uv6th  = fd.uv0;
    if(_Main6thTex_UVMode == 1) uv6th = fd.uv1;
    if(_Main6thTex_UVMode == 2) uv6th = fd.uv2;
    if(_Main6thTex_UVMode == 3) uv6th = fd.uv3;
    if(_Main6thTex_UVMode == 4) uv6th = fd.uvMat;
    if(_UseWarp && _UseWarpMain6th)
    {
        uv6th = warp(uv6th);
        fd.uvMain = warp(fd.uvMain);
    }

    color6th *= LIL_GET_SUBTEX(_Main6thTex, uv6th);
    color6th.a *= LIL_SAMPLE_2D(_Main6thBlendMask, samp, fd.uvMain).r;
    if(_AudioLink2Main6th) color6th.a *= fd.audioLinkValue;
    color6th.a = lerp(color6th.a,
                        color6th.a * saturate((fd.depth - _Main6thDistanceFade.x) / (_Main6thDistanceFade.y - _Main6thDistanceFade.x)),
                        _Main6thDistanceFade.z);
    if(_Main6thTex_Cull == 1 && fd.facing > 0 || _Main6thTex_Cull == 2 && fd.facing < 0) color6th.a = 0;

    #if LIL_RENDER != 0
        if(_Main6thTexAlphaMode != 0)
        {
            if(_Main6thTexAlphaMode == 1) fd.col.a = color6th.a;
            if(_Main6thTexAlphaMode == 2) fd.col.a = fd.col.a * color6th.a;
            if(_Main6thTexAlphaMode == 3) fd.col.a = saturate(fd.col.a + color6th.a);
            if(_Main6thTexAlphaMode == 4) fd.col.a = saturate(fd.col.a - color6th.a);
            color6th.a = 1;
        }
    #endif

    fd.col.rgb = lilBlendColor(fd.col.rgb, color6th.rgb, color6th.a * _Main6thEnableLighting, _Main6thTexBlendMode);
}

// MatCap3rd
void lilGetMatCap3rd(inout lilFragData fd, in float3 matcap3rdN LIL_SAMP_IN_FUNC(samp))
{
    if(!_UseMatCap3rd) return;

    // --- Normal ---
    float3 N = matcap3rdN;
    N = lerp(fd.origN, matcap3rdN, _MatCap3rdNormalStrength);

    // --- UV ---
    float2 mat3rdUV   = lilCalcMatCapUV(fd.uv1, N, fd.V, fd.headV,
                                        _MatCap3rdTex_ST, _MatCap3rdBlendUV1.xy,
                                        _MatCap3rdZRotCancel, _MatCap3rdPerspective,
                                        _MatCap3rdVRParallaxStrength);

    // --- Color ---
    float4 matCap3rdColor = _MatCap3rdColor;
    matCap3rdColor *= LIL_SAMPLE_2D_LOD(_MatCap3rdTex, lil_sampler_linear_repeat, mat3rdUV, _MatCap3rdLod);

    #if !defined(LIL_PASS_FORWARDADD)
        matCap3rdColor.rgb = lerp(matCap3rdColor.rgb, matCap3rdColor.rgb * fd.lightColor, _MatCap3rdEnableLighting);
        matCap3rdColor.a = lerp(matCap3rdColor.a, matCap3rdColor.a * fd.shadowmix, _MatCap3rdShadowMask);
    #else
        if(_MatCap3rdBlendMode < 3) matCap3rdColor.rgb *= fd.lightColor * _MatCap3rdEnableLighting;
        matCap3rdColor.a = lerp(matCap3rdColor.a, matCap3rdColor.a * fd.shadowmix, _MatCap3rdShadowMask);
    #endif

    #if LIL_RENDER == 2 && !defined(LIL_REFRACTION)
        if(_MatCap3rdApplyTransparency) matCap3rdColor.a *= fd.col.a;
    #endif

    matCap3rdColor.a = fd.facing < (_MatCap3rdBackfaceMask - 1.0) ? 0.0 : matCap3rdColor.a;
    float3 matCapMask = LIL_SAMPLE_2D_ST(_MatCap3rdBlendMask, samp, fd.uvMain).rgb;

    // --- Blend ---
    matCap3rdColor.rgb = lerp(matCap3rdColor.rgb, matCap3rdColor.rgb * fd.albedo,
                                _MatCap3rdMainStrength);
    fd.col.rgb = lilBlendColor(fd.col.rgb, matCap3rdColor.rgb,
                                _MatCap3rdBlend * matCap3rdColor.a * matCapMask,
                                _MatCap3rdBlendMode);
}

// MatCap4th
void lilGetMatCap4th(inout lilFragData fd, in float3 matcap4thN LIL_SAMP_IN_FUNC(samp))
{
    if(!_UseMatCap4th) return;

    // --- Normal ---
    float3 N = matcap4thN;
    N = lerp(fd.origN, matcap4thN, _MatCap4thNormalStrength);

    // --- UV ---
    float2 mat4thUV   = lilCalcMatCapUV(fd.uv1, N, fd.V, fd.headV,
                                        _MatCap4thTex_ST, _MatCap4thBlendUV1.xy,
                                        _MatCap4thZRotCancel, _MatCap4thPerspective,
                                        _MatCap4thVRParallaxStrength);

    // --- Color ---
    float4 matCap4thColor = _MatCap4thColor;
    matCap4thColor *= LIL_SAMPLE_2D_LOD(_MatCap4thTex, lil_sampler_linear_repeat, mat4thUV, _MatCap4thLod);

    #if !defined(LIL_PASS_FORWARDADD)
        matCap4thColor.rgb = lerp(matCap4thColor.rgb, matCap4thColor.rgb * fd.lightColor, _MatCap4thEnableLighting);
        matCap4thColor.a = lerp(matCap4thColor.a, matCap4thColor.a * fd.shadowmix, _MatCap4thShadowMask);
    #else
        if(_MatCap4thBlendMode < 3) matCap4thColor.rgb *= fd.lightColor * _MatCap4thEnableLighting;
        matCap4thColor.a = lerp(matCap4thColor.a, matCap4thColor.a * fd.shadowmix, _MatCap4thShadowMask);
    #endif

    #if LIL_RENDER == 2 && !defined(LIL_REFRACTION)
        if(_MatCap4thApplyTransparency) matCap4thColor.a *= fd.col.a;
    #endif

    matCap4thColor.a = fd.facing < (_MatCap4thBackfaceMask - 1.0) ? 0.0 : matCap4thColor.a;
    float3 matCapMask = LIL_SAMPLE_2D_ST(_MatCap4thBlendMask, samp, fd.uvMain).rgb;

    // --- Blend ---
    matCap4thColor.rgb = lerp(matCap4thColor.rgb, matCap4thColor.rgb * fd.albedo,
                                _MatCap4thMainStrength);
    fd.col.rgb = lilBlendColor(fd.col.rgb, matCap4thColor.rgb,
                                _MatCap4thBlend * matCap4thColor.a * matCapMask,
                                _MatCap4thBlendMode);
}

// Glitter2nd
void lilGlitter2nd(inout lilFragData fd LIL_SAMP_IN_FUNC(samp))
{
    if(!_UseGlitter2nd) return;

    // --- View Direction ---
    float3 glitter2ndViewDirection   = lilBlendVRParallax(fd.headV, fd.V, _Glitter2ndVRParallaxStrength);
    float3 glitter2ndCameraDirection = lerp(fd.cameraFront, fd.V, _Glitter2ndVRParallaxStrength);

    // --- Normal ---
    float3 N = fd.N;
    N = lerp(fd.origN, fd.N, _Glitter2ndNormalStrength);

    // --- Color ---
    float4 glitter2ndColor    = _Glitter2ndColor;
    float2 uvGlitter2ndColor  = fd.uvMain;
    if(_Glitter2ndColorTex_UVMode == 1) uvGlitter2ndColor = fd.uv1;
    if(_Glitter2ndColorTex_UVMode == 2) uvGlitter2ndColor = fd.uv2;
    if(_Glitter2ndColorTex_UVMode == 3) uvGlitter2ndColor = fd.uv3;
    glitter2ndColor *= LIL_SAMPLE_2D_ST(_Glitter2ndColorTex, samp, uvGlitter2ndColor);

    float2 glitter2ndPos   = _Glitter2ndUVMode ? fd.uv1 : fd.uv0;
    glitter2ndColor.rgb *= lilCalcGlitter(glitter2ndPos, N, glitter2ndViewDirection, glitter2ndCameraDirection, fd.L,
                                            _Glitter2ndParams1, _Glitter2ndParams2, _Glitter2ndPostContrast, _Glitter2ndSensitivity,
                                            _Glitter2ndScaleRandomize, _Glitter2ndAngleRandomize, _Glitter2ndApplyShape,
                                            _Glitter2ndShapeTex, _Glitter2ndShapeTex_ST, _Glitter2ndAtras);
    glitter2ndColor.rgb *= lilCalcGlitter(glitter2ndPos, N, glitter2ndViewDirection, glitter2ndCameraDirection, fd.L,
                                            _Glitter2ndParams1, _Glitter2ndParams2, _Glitter2ndPostContrast, _Glitter2ndSensitivity,
                                            _Glitter2ndScaleRandomize, 0, false, _Glitter2ndShapeTex, float4(0, 0, 0, 0), float4(1, 1, 0, 0));
    glitter2ndColor.rgb = lerp(glitter2ndColor.rgb, glitter2ndColor.rgb * fd.albedo, _Glitter2ndMainStrength);

    #if LIL_RENDER == 2 && !defined(LIL_REFRACTION)
        if(_Glitter2ndApplyTransparency) glitter2ndColor.a *= fd.col.a;
    #endif

    glitter2ndColor.a = fd.facing < (_Glitter2ndBackfaceMask - 1.0) ? 0.0 : glitter2ndColor.a;

    // --- Blend ---
    #if !defined(LIL_PASS_FORWARDADD)
        glitter2ndColor.a = lerp(glitter2ndColor.a, glitter2ndColor.a * fd.shadowmix, _Glitter2ndShadowMask);
        glitter2ndColor.rgb = lerp(glitter2ndColor.rgb, glitter2ndColor.rgb * fd.lightColor, _Glitter2ndEnableLighting);
        fd.col.rgb += glitter2ndColor.rgb * glitter2ndColor.a;
    #else
        glitter2ndColor.a = lerp(glitter2ndColor.a, glitter2ndColor.a * fd.shadowmix, _Glitter2ndShadowMask);
        fd.col.rgb += glitter2ndColor.a * _Glitter2ndEnableLighting * glitter2ndColor.rgb * fd.lightColor;
    #endif
}

// Emission3rd
void lilEmission3rd(inout lilFragData fd LIL_SAMP_IN_FUNC(samp))
{
    if(!_UseEmission3rd) return;

    float4 emission3rdColor = _Emission3rdColor;

    // --- UV ---
    float2 emission3rdUV = fd.uv0;
    if(_Emission3rdMap_UVMode == 1) emission3rdUV = fd.uv1;
    if(_Emission3rdMap_UVMode == 2) emission3rdUV = fd.uv2;
    if(_Emission3rdMap_UVMode == 3) emission3rdUV = fd.uv3;
    if(_Emission3rdMap_UVMode == 4) emission3rdUV = fd.uvRim;
    float2 _Emission3rdMapParaTex = emission3rdUV + _Emission3rdParallaxDepth * fd.parallaxOffset;

    // --- Texture ---
    #if defined(LIL_FEATURE_ANIMATE_EMISSION_UV)
        emission3rdColor *= LIL_GET_EMITEX(_Emission3rdMap, _Emission3rdMapParaTex);
    #else
        emission3rdColor *= LIL_SAMPLE_2D_ST(_Emission3rdMap, sampler_Emission3rdMap, _Emission3rdMapParaTex);
    #endif

    // --- Mask ---
    #if defined(LIL_FEATURE_ANIMATE_EMISSION_MASK_UV)
        emission3rdColor *= LIL_GET_EMIMASK(_Emission3rdBlendMask, fd.uv0);
    #else
        emission3rdColor *= LIL_SAMPLE_2D_ST(_Emission3rdBlendMask, samp, fd.uvMain);
    #endif

    #if defined(LIL_FEATURE_AUDIOLINK)
        if(_AudioLink2Emission3rd) emission3rdColor.a *= fd.audioLinkValue;
    #endif

    emission3rdColor.rgb = lerp(emission3rdColor.rgb, emission3rdColor.rgb * fd.invLighting, _Emission3rdFluorescence);
    emission3rdColor.rgb = lerp(emission3rdColor.rgb, emission3rdColor.rgb * fd.albedo, _Emission3rdMainStrength);
    float emission3rdBlend  = _Emission3rdBlend * lilCalcBlink(_Emission3rdBlink) * emission3rdColor.a;

    #if LIL_RENDER == 2 && !defined(LIL_REFRACTION)
        emission3rdBlend *= fd.col.a;
    #endif

    fd.col.rgb = lilBlendColor(fd.col.rgb, emission3rdColor.rgb, emission3rdBlend, _Emission3rdBlendMode);
}

float ndot(float2 a, float2 b)
{
    return a.x * b.x - a.y * b.y;
}

float dot2(float2 v)
{
    return dot(v, v);
}

float opUnion(float d1, float d2)
{
    return min(d1, d2);
}
float opSubtraction(float d1, float d2)
{
    return max(-d1, d2);
}

float opIntersection(float d1, float d2)
{
    return max(d1, d2);
}

float opXor(float d1, float d2)
{
    return max(min(d1, d2), -max(d1, d2));
}

float opSmoothUnion(float d1, float d2, float k)
{
    k *= 4.0;
    float h = opIntersection(k - abs(d1 - d2), 0.0);
    return opUnion(d1, d2) - h * h * 0.25 / k;
}

float opSmoothSubtraction(float d1, float d2, float k)
{
    return -opSmoothUnion(d1, -d2, k);
}

float opSmoothIntersection(float d1, float d2, float k)
{
    return -opSmoothUnion(-d1, -d2, k);
}

float opRound(float2 p, float r)
{
    return p - r;
}

float opRoundInside(float2 p, float r)
{
    return length(p) - r;
}

// ==========================================================
// SDF (Signed Distance Functions)
// ==========================================================

// 1. Heart
float sdHeart(float2 p)
{
    p.x = abs(p.x);

    if(p.y + p.x > 1.0)
        return sqrt(dot2(p - float2(0.25, 0.75))) - sqrt(2.0) / 4.0;

    return sqrt(opUnion(dot2(p - float2(0.00, 1.00)),
                        dot2(p - 0.5 * opIntersection(p.x + p.y, 0.0)))) *
                        sign(p.x - p.y);
}

// 2. Star
float sdStar(float2 p, float r, float rf)
{
    const float2 k1 = float2(0.80901699437, -0.58778525229);
    const float2 k2 = float2(-k1.x, k1.y);

    p.x = abs(p.x);
    p -= 2.0 * opIntersection(dot(k1, p), 0.0) * k1;
    p -= 2.0 * opIntersection(dot(k2, p), 0.0) * k2;
    p.x = abs(p.x);
    p.y -= r;

    float2 ba = rf * float2(-k1.y, k1.x) - float2(0, r);
    float h = clamp(dot(p, ba) / dot(ba, ba), 0.0, r);

    return length(p - ba * h) *
           sign(p.y * ba.x - p.x * ba.y);
}

// 3. Cross
float sdCross(float2 p, float size)
{
    float2 b  = float2(size, size * 0.25); // Width
    p = abs(p);
    p = (p.y > p.x) ? p.yx : p.xy;
    float2 q = p - b;
    float k = opIntersection(q.y, q.x);
    float2 w = (k > 0.0) ? q : float2(b.y - p.x, -k);

    return sign(k) * length(max(w, 0.0));
}

// 4. Rounded X
float sdRoundedX(float2 p, float w, float r)
{
    p = abs(p);

    return length(p - opUnion(p.x + p.y, w) * 0.5) - r;
}

// 5. Rhombus
float sdRhombus(float2 p, float2 b)
{
    p = abs(p);
    float h  = clamp(0.5 + 0.5 * ndot(b - 2.0 * p, b) / dot(b, b), 0.0, 1.0);

    return length(p - 0.5 * b * float2(1.0 - h, 1.0 + h)) *
           sign(p.x * b.y + p.y * b.x - b.x * b.y);
}

// 6. Rounded Box
float sdRoundedBox(float2 p, float2 b, float r)
{
    float2 q = abs(p) - b + r;

    return opUnion(opIntersection(q.x, q.y), 0.0) +
                    length(max(q, 0.0)) - r;
}

// 7. Moon
float sdMoon(float2 p, float d, float ra, float rb)
{
    p.y = abs(p.y);
    float a = (ra * ra - rb * rb + d * d) / (2.0 * d);
    float b = sqrt(opIntersection(ra * ra - a * a, 0.0));

    if(d * (p.x * b - p.y * a) > d * d * opIntersection(b - p.y, 0.0))
        return length(p - float2(a, b));

    return opIntersection((length(p) - ra), -(length(p - float2(d, 0)) - rb));
}

float sdEquilateralTriangle(float2 p, float r)
{
    const float k = sqrt(3.0);
    p.x = abs(p.x) - r;
    p.y = p.y + r / k;
    if(p.x + k * p.y > 0.0) p = float2(p.x - k * p.y, -k * p.x - p.y) / 2.0;
    p.x -= clamp(p.x, -2.0 * r, 0.0);
    return -length(p) * sign(p.y);
}

// 8. Cat Face
float sdCatFace(float2 p)
{
    // --- Face (Ellipse) ---
    float2 facePos  = p - float2(0.0, -0.1);
    float faceShape = length(facePos * float2(0.65, 1.0)) - 0.5;

    // --- Ears (Rotated) ---
    float2 q = p;
    q.x = abs(q.x); // Symmetry

    float2 earPivot = float2(0.32, 0.2);
    float2 earUV = q - earPivot;

    float angle = 0.4;
    float s     = sin(angle);
    float c     = cos(angle);

    earUV  = float2(earUV.x * c - earUV.y * s,
                    earUV.x * s + earUV.y * c);
    earUV -= float2(0.0, 0.18);

    float earShape = opRound(sdEquilateralTriangle(earUV, 0.15), 0.1);

    // --- Blend ---
    return opSmoothUnion(faceShape, earShape, 0.01);
}

float removeOverlap(float d1, float d2)
{
    // 両方の内側なら「強制的に外側」にする
    if(d1 < 0 && d2 < 0)
        return opIntersection(d1, d2); // 共通部分を削る

    return opUnion(d1, d2);
}

float removeOverlapSmooth(float d1, float d2, float k)
{
    // 両方の内側なら「強制的に外側」にする
    if(d1 < 0 && d2 < 0)
        return opSmoothSubtraction(d1, d2, k); // 共通部分を削る

    return opUnion(d1, d2);
}

// 9. Cat Paw
float sdCatPaw(float2 p)
{
    p.x = abs(p.x);

    // Main pads
    float d1 = length(p - float2(0.00, -0.15)) - 0.2;
    float d2 = length(p - float2(0.22, -0.28)) - 0.2;
    float k  = 0.045;

    float pad = opSmoothUnion(d1, d2, k);

    // Inner toe (center)
    float toe_inner = length((p - float2(0.18, 0.32)) * float2(1.25, 1.0)) - 0.1875;

    // Outer toe
    float toe_outer = length(p - float2(0.45, 0.12)) - 0.14;

    // Combine
    float paw = opUnion(pad, opUnion(toe_inner, toe_outer));

    float lp   = 0.44;
    float d3   = length(p - float2(0.00, -0.15 - lp)) - 0.2;
    float d4   = length(p - float2(0.30, -0.24 - lp)) - 0.2;
    float k2   = 0.045;
    float dent = opSmoothUnion(d3, d4, k2);

    paw = opSmoothSubtraction(dent, paw, 0.01);

    return paw;
}

    // circle = length(p * float2(height, width) - float2(x, y)) - radius;
    // p ピクセルのUV座標
    // * float2(height, width) 円の高さと幅
    // - float2(x, y) pおける円の中心位置
    // length(...) 現在のピクセルから中心までの距離
    // - radius 距離から半径を減算
    // radius > 1 大きくなる
    // radius < 1 小さくなる

// ==========================================================
// 計算 & 描画処理
// ==========================================================

float MoleCalc(float2 uv, float2 pos, float radius, float rotation, int shapeType)
{
    // --- Aspect Ratio Correction ---
    float aspectFix = 1.0;
    if(_MoleAspectFix)
        aspectFix = _MainTex_TexelSize.w / _MainTex_TexelSize.z;

    float2 p = (uv - pos) * float2(aspectFix, 1.0);

    // --- Rotation ---
    float rotRad = radians(rotation);
    float s      = sin(rotRad);
    float c      = cos(rotRad);

    p = mul(p, float2x2(c, -s,
                        s, c));

    // --- Normalized Space ---
    float2 sp = p / max(radius, 1e-5);
    float d;

    if(shapeType == 1)      d = sdHeart(sp + float2(0, 0.5));
    else if(shapeType == 2) d = sdStar(sp, 1.0, 0.45);
    else if(shapeType == 3) d = sdCross(sp, 0.8);
    else if(shapeType == 4) d = sdRoundedX(sp, 0.5, 0.1);
    else if(shapeType == 5) d = sdRhombus(sp, float2(0.8, 1.0));
    else if(shapeType == 6) d = sdRoundedBox(sp, 0.7, 0.2);
    else if(shapeType == 7) d = sdMoon(sp, 0.4, 1.0, 0.85);
    else if(shapeType == 8) d = sdCatFace(sp);
    else if(shapeType == 9) d = sdCatPaw(sp);
    else                    d = length(sp) - 1.0; // Circle

    // Distance scale back to UV space
    return d * radius;
}

float MoleCalc_WithBlur(float2 uv, float2 pos, float radius, float blur, float rotation, int shapeType)
{
    float d = MoleCalc(uv, pos, radius, rotation, shapeType);

    // d == 0: Edge
    // d < 0: Interior / d > 0: Exterior

    // Normalize blur to 1.0 unit
    return d / max(blur, 1e-5);
}

// Mole
void lilMoleDrower(inout lilFragData fd LIL_SAMP_IN_FUNC(samp))
{
    if(!_UseMole) return;

    float4 moleColor = _MoleColor;
    float2 uv        = fd.uvMain;
    float d          = 1e6;

    if(_UseMole1st)  d = min(d, MoleCalc_WithBlur(uv, _Mole1stPos,  _Mole1stRadius * _Mole1stRadiusMultiplier,
                                                    _Mole1stBlur,  _Mole1stRotation,  _Mole1stShape));
    if(_UseMole2nd)  d = min(d, MoleCalc_WithBlur(uv, _Mole2ndPos,  _Mole2ndRadius * _Mole2ndRadiusMultiplier,
                                                    _Mole2ndBlur,  _Mole2ndRotation,  _Mole2ndShape));
    if(_UseMole3rd)  d = min(d, MoleCalc_WithBlur(uv, _Mole3rdPos,  _Mole3rdRadius * _Mole3rdRadiusMultiplier,
                                                    _Mole3rdBlur,  _Mole3rdRotation,  _Mole3rdShape));
    if(_UseMole4th)  d = min(d, MoleCalc_WithBlur(uv, _Mole4thPos,  _Mole4thRadius * _Mole4thRadiusMultiplier,
                                                    _Mole4thBlur,  _Mole4thRotation,  _Mole4thShape));
    if(_UseMole5th)  d = min(d, MoleCalc_WithBlur(uv, _Mole5thPos,  _Mole5thRadius * _Mole5thRadiusMultiplier,
                                                    _Mole5thBlur,  _Mole5thRotation,  _Mole5thShape));
    if(_UseMole6th)  d = min(d, MoleCalc_WithBlur(uv, _Mole6thPos,  _Mole6thRadius * _Mole6thRadiusMultiplier,
                                                    _Mole6thBlur,  _Mole6thRotation,  _Mole6thShape));
    if(_UseMole7th)  d = min(d, MoleCalc_WithBlur(uv, _Mole7thPos,  _Mole7thRadius * _Mole7thRadiusMultiplier,
                                                    _Mole7thBlur,  _Mole7thRotation,  _Mole7thShape));
    if(_UseMole8th)  d = min(d, MoleCalc_WithBlur(uv, _Mole8thPos,  _Mole8thRadius * _Mole8thRadiusMultiplier,
                                                    _Mole8thBlur,  _Mole8thRotation,  _Mole8thShape));
    if(_UseMole9th)  d = min(d, MoleCalc_WithBlur(uv, _Mole9thPos,  _Mole9thRadius * _Mole9thRadiusMultiplier,
                                                    _Mole9thBlur,  _Mole9thRotation,  _Mole9thShape));
    if(_UseMole10th) d = min(d, MoleCalc_WithBlur(uv, _Mole10thPos, _Mole10thRadius * _Mole10thRadiusMultiplier,
                                                    _Mole10thBlur, _Mole10thRotation, _Mole10thShape));

    // --- SDF Distance → Mask ---
    float mole = smoothstep(1.0, 0.0, d);

    fd.col.rgb = lilBlendColor(fd.col.rgb, moleColor.rgb, mole * moleColor.a, _MoleBlendMode);
}

float GetLightValue(float3 lightColor, int type)
{
    if(type == 0)
    {
        // Max RGB
        return max(lightColor.r, max(lightColor.g, lightColor.b));
    }
    else if(type == 1)
    {
        // Luminance (Rec.709)
        return dot(lightColor, float3(0.2126, 0.7152, 0.0722));
    }
    else
    {
        // HSV V (same as Max RGB, but semantic separation for future expansion)
        return max(lightColor.r, max(lightColor.g, lightColor.b));
    }
}

// Light Based Alpha
// DO NOT USE WITH "LI MaterialOptimizer" AND "LNU lilToonMaterialOptimizer"
void lilLightBasedAlpha(inout lilFragData fd, uint _LightBasedAlphaLoadType, float alphaMask, float mainTexAlpha LIL_SAMP_IN_FUNC(samp))
{
    #ifndef LIL_FEATURE_PARALLAX // VRChat with optimization and no parallax
        if(!_UseLightBasedAlpha)  return;

        float4  lightBasedAlphaMask = 1.0;
        lightBasedAlphaMask         = saturate(LIL_SAMPLE_2D_ST(_ParallaxMap, sampler_MainTex, fd.uvMain));
        bool    isOff               = lightBasedAlphaMask.a < 0.25;
        bool    isOn                = lightBasedAlphaMask.a > 0.75;
        bool    isInvert            = (!isOff && !isOn) ^ _LightBasedAlphaInvert;

        if(isOff) return;

        if(_LightBasedAlphaLoadType == 0 && _UseAlphaMaskStyle)
            lightBasedAlphaMask.r = saturate(lightBasedAlphaMask.r * _LightBasedAlphaMaskScale + _LightBasedAlphaMaskValue);
        if(_LightBasedAlphaLoadType == 1)  lightBasedAlphaMask.r = mainTexAlpha;
        if(_LightBasedAlphaLoadType == 2)  lightBasedAlphaMask.r = alphaMask;

        float valueFactor            = 1.0;
        float maskedValueFactor      = 1.0;
        float minTransparency        = max(lightBasedAlphaMask.g, lightBasedAlphaMask.r);
        float maxTransparency        = min(lightBasedAlphaMask.g, lightBasedAlphaMask.r);
        float sharpness              = 1.0 - lightBasedAlphaMask.b;
        float value                  = GetLightValue(fd.lightColor, _LightBasedAlphaValueType);
        float L                      = _LowestLightThreshold;
        float M                      = _MiddleLightThreshold;
        float H                      = _HighestLightThreshold;

        if(_OverrideMin) minTransparency = 1.0 - min(_OverrideMinTransparency, _OverrideMaxTransparency);
        if(_OverrideMax) maxTransparency = 1.0 - max(_OverrideMinTransparency, _OverrideMaxTransparency);
        if(_LightBasedAlphaMode == 0)
        {
            if(_UseMiddleLight)
            {
                L            = min(L, M - 1e-5);
                H            = max(H, M + 1e-5);
                float up     = smoothstep(L, M, value);
                float down   = 1.0 - smoothstep(M, H, value);
                valueFactor  = up * down;
            }
            else
            {
                L           = min(L, H - 1e-5);
                H           = max(H, L + 1e-5);
                valueFactor = smoothstep(L, H, value);
            }
            float smooth = valueFactor;
            float hard   = step(_SharpnessLightThreshold, smooth);
            valueFactor  = lerp(smooth, hard, sharpness);
        }
        else if(_LightBasedAlphaMode == 1)
        {
            valueFactor = step(_LightThreshold, value);
        }
        else
        {
            valueFactor = step(L, value) * step(value, H);
        }
        if(isInvert) valueFactor = 1.0 - valueFactor;
        maskedValueFactor = lerp(minTransparency, maxTransparency, valueFactor);
        if(_LightBasedAlphaApplyMode == 0) fd.col.a = maskedValueFactor;
        if(_LightBasedAlphaApplyMode == 1) fd.col.a = fd.col.a * maskedValueFactor;
        if(_LightBasedAlphaApplyMode == 2) fd.col.a = saturate(fd.col.a + maskedValueFactor);
        if(_LightBasedAlphaApplyMode == 3) fd.col.a = saturate(fd.col.a - maskedValueFactor);
        if(_UseClamp)
        {
            float minT = 1.0 - max(_MinTransparency, _MaxTransparency);
            float maxT = 1.0 - min(_MinTransparency, _MaxTransparency);
            fd.col.a   = clamp(fd.col.a, minT, maxT);
        }
    #else // On Editor and VRChat with optimization and parallax
        if(!_UseLightBasedAlpha && _UseParallax) return;

        float4  lightBasedAlphaMask = 1.0;
        lightBasedAlphaMask         = saturate(LIL_SAMPLE_2D_ST(_ParallaxMap, sampler_MainTex, fd.uvMain));
        bool    isOff               = lightBasedAlphaMask.a < 0.25;
        bool    isOn                = lightBasedAlphaMask.a > 0.75;
        bool    isInvert            = (!isOff && !isOn) ^ _LightBasedAlphaInvert;

        if(isOff) return;

        // --- Value Load ---
        if(_LightBasedAlphaLoadType == 0 && _UseAlphaMaskStyle)
            lightBasedAlphaMask.r = saturate(lightBasedAlphaMask.r * _LightBasedAlphaMaskScale + _LightBasedAlphaMaskValue);
        if(_LightBasedAlphaLoadType == 1)  lightBasedAlphaMask.r = mainTexAlpha;
        if(_LightBasedAlphaLoadType == 2)  lightBasedAlphaMask.r = alphaMask;

        // --- Variable Setup ---
        float valueFactor            = 1.0;
        float maskedValueFactor      = 1.0;
        float minTransparency        = max(lightBasedAlphaMask.g, lightBasedAlphaMask.r);
        float maxTransparency        = min(lightBasedAlphaMask.g, lightBasedAlphaMask.r);
        float sharpness              = 1.0 - lightBasedAlphaMask.b;
        float value                  = GetLightValue(fd.lightColor, _LightBasedAlphaValueType);
        float L                      = _LowestLightThreshold;
        float M                      = _MiddleLightThreshold;
        float H                      = _HighestLightThreshold;

        // --- Threshold Override ---
        if(_OverrideMin) minTransparency = 1.0 - min(_OverrideMinTransparency, _OverrideMaxTransparency);
        if(_OverrideMax) maxTransparency = 1.0 - max(_OverrideMinTransparency, _OverrideMaxTransparency);

        // --- Alpha Mode ---
        if(_LightBasedAlphaMode == 0)
        {
            if(_UseMiddleLight)
            {
                L            = min(L, M - 1e-5);
                H            = max(H, M + 1e-5);
                float up     = smoothstep(L, M, value);
                float down   = 1.0 - smoothstep(M, H, value);
                valueFactor  = up * down;
            }
            else
            {
                L           = min(L, H - 1e-5);
                H           = max(H, L + 1e-5);
                valueFactor = smoothstep(L, H, value);
            }

            float smooth = valueFactor;
            float hard   = step(_SharpnessLightThreshold, smooth);
            valueFactor  = lerp(smooth, hard, sharpness);
        }
        else if(_LightBasedAlphaMode == 1)
        {
            valueFactor = step(_LightThreshold, value);
        }
        else
        {
            valueFactor = step(L, value) * step(value, H);
        }

        // --- Apply ---
        if(isInvert) valueFactor = 1.0 - valueFactor;
        maskedValueFactor = lerp(minTransparency, maxTransparency, valueFactor);

        if(_LightBasedAlphaApplyMode == 0) fd.col.a = maskedValueFactor;
        if(_LightBasedAlphaApplyMode == 1) fd.col.a = fd.col.a * maskedValueFactor;
        if(_LightBasedAlphaApplyMode == 2) fd.col.a = saturate(fd.col.a + maskedValueFactor);
        if(_LightBasedAlphaApplyMode == 3) fd.col.a = saturate(fd.col.a - maskedValueFactor);

        if(_UseClamp)
        {
            float minT = 1.0 - max(_MinTransparency, _MaxTransparency);
            float maxT = 1.0 - min(_MinTransparency, _MaxTransparency);
            fd.col.a   = clamp(fd.col.a, minT, maxT);
        }
    #endif
}
