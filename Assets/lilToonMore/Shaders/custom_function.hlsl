float fastsin3(float x)
{
    x = fmod(x + LIL_PI, 2.0 * LIL_PI) - LIL_PI;
    return x * (1.27323954 - 0.405284735 * abs(x));
}

void warp(inout float2 inuv)
{
    float2 uv   = inuv;
    float  time = LIL_TIME * _WarpAnimSpeed;
    float  x    = uv.x;
    float  y    = uv.y;

    x += fastsin3(y * _WarpBigFreqY + time * _WarpBigSpeedX) * _WarpBigAmp;
    y += fastsin3(x * _WarpBigFreqX + time * _WarpBigSpeedY) * _WarpBigAmp;
    x += fastsin3(y * _WarpSmallFreqY + time * _WarpSmallSpeedX) * _WarpSmallAmp;
    y += fastsin3(x * _WarpSmallFreqX + time * _WarpSmallSpeedY) * _WarpSmallAmp;
    
    uv.x = x;
    uv.y = y;

    inuv = inuv + (uv - inuv) * _WarpIntensity;
}

// Background Warping
#if defined(LIL_REFRACTION) && !defined(LIL_LITE)
    void lilBGWarp(inout lilFragData fd LIL_SAMP_IN_FUNC(samp))
    {
        float2 warpUV = fd.uvScn;
        warp(warpUV);
        float3 warpedBG = LIL_GET_BG_TEX(warpUV, 0).rgb;

        fd.col.rgb = lerp(warpedBG, fd.col.rgb, fd.col.a);
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
        if(_UseMain2ndTex)
        {
            float2 uv2nd = fd.uv0;
            if(_Main2ndTex_UVMode == 1) uv2nd = fd.uv1;
            if(_Main2ndTex_UVMode == 2) uv2nd = fd.uv2;
            if(_Main2ndTex_UVMode == 3) uv2nd = fd.uv3;
            if(_Main2ndTex_UVMode == 4) uv2nd = fd.uvMat;
            if(_UseWarp && _UseWarpMain2nd) warp(uv2nd);
            #if defined(LIL_FEATURE_Main2ndTex)
                color2nd *= LIL_GET_SUBTEX(_Main2ndTex, uv2nd);
            #endif
            #if defined(LIL_FEATURE_Main2ndBlendMask)
                float2 uvBM = fd.uvMain;
                if(_UseWarp && _UseWarpMain2nd) warp(uvBM);
                color2nd.a *= LIL_SAMPLE_2D(_Main2ndBlendMask, samp, uvBM).r;
            #endif
            #if defined(LIL_FEATURE_AUDIOLINK)
                if(_AudioLink2Main2nd) color2nd.a *= fd.audioLinkValue;
            #endif
            color2nd.a = lerp(color2nd.a, color2nd.a * saturate((fd.depth - _Main2ndDistanceFade.x) / (_Main2ndDistanceFade.y - _Main2ndDistanceFade.x)), _Main2ndDistanceFade.z);
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
        if(_UseMain3rdTex)
        {
            float2 uv3rd = fd.uv0;
            if(_Main3rdTex_UVMode == 1) uv3rd = fd.uv1;
            if(_Main3rdTex_UVMode == 2) uv3rd = fd.uv2;
            if(_Main3rdTex_UVMode == 3) uv3rd = fd.uv3;
            if(_Main3rdTex_UVMode == 4) uv3rd = fd.uvMat;
            if(_UseWarp && _UseWarpMain3rd) warp(uv3rd);
            #if defined(LIL_FEATURE_Main3rdTex)
                color3rd *= LIL_GET_SUBTEX(_Main3rdTex, uv3rd);
            #endif
            #if defined(LIL_FEATURE_Main3rdBlendMask)
                float2 uvBM = fd.uvMain;
                if(_UseWarp && _UseWarpMain3rd) warp(uvBM);
                color3rd.a *= LIL_SAMPLE_2D(_Main3rdBlendMask, samp, uvBM).r;
            #endif
            #if defined(LIL_FEATURE_AUDIOLINK)
                if(_AudioLink2Main3rd) color3rd.a *= fd.audioLinkValue;
            #endif
            color3rd.a = lerp(color3rd.a, color3rd.a * saturate((fd.depth - _Main3rdDistanceFade.x) / (_Main3rdDistanceFade.y - _Main3rdDistanceFade.x)), _Main3rdDistanceFade.z);
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
    }
#endif

// Main4th
void lilGetMain4th(inout lilFragData fd, inout float4 color4th LIL_SAMP_IN_FUNC(samp))
{
    color4th = _Color4th;
    if(_UseMain4thTex)
    {
        float2 uv4th = fd.uv0;
        float2 uvBM = fd.uvMain;
        if(_Main4thTex_UVMode == 1) uv4th = fd.uv1;
        if(_Main4thTex_UVMode == 2) uv4th = fd.uv2;
        if(_Main4thTex_UVMode == 3) uv4th = fd.uv3;
        if(_Main4thTex_UVMode == 4) uv4th = fd.uvMat;
        if(_UseWarp && _UseWarpMain4th)
        {
            warp(uv4th);
            warp(uvBM);
        }
        color4th *= LIL_GET_SUBTEX(_Main4thTex, uv4th);
        color4th.a *= LIL_SAMPLE_2D(_Main4thBlendMask, samp, uvBM).r;
        if(_AudioLink2Main4th) color4th.a *= fd.audioLinkValue;
        color4th.a = lerp(color4th.a, color4th.a * saturate((fd.depth - _Main4thDistanceFade.x) / (_Main4thDistanceFade.y - _Main4thDistanceFade.x)), _Main4thDistanceFade.z);
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
}

// Main5th
void lilGetMain5th(inout lilFragData fd, inout float4 color5th LIL_SAMP_IN_FUNC(samp))
{
    color5th = _Color5th;
    if(_UseMain5thTex)
    {
        float2 uv5th = fd.uv0;
        float2 uvBM = fd.uvMain;
        if(_Main5thTex_UVMode == 1) uv5th = fd.uv1;
        if(_Main5thTex_UVMode == 2) uv5th = fd.uv2;
        if(_Main5thTex_UVMode == 3) uv5th = fd.uv3;
        if(_Main5thTex_UVMode == 4) uv5th = fd.uvMat;
        if(_UseWarp && _UseWarpMain5th)
        {
            warp(uv5th);
            warp(uvBM);
        }
        color5th *= LIL_GET_SUBTEX(_Main5thTex, uv5th);
        color5th.a *= LIL_SAMPLE_2D(_Main5thBlendMask, samp, uvBM).r;
        if(_AudioLink2Main5th) color5th.a *= fd.audioLinkValue;
        color5th.a = lerp(color5th.a, color5th.a * saturate((fd.depth - _Main5thDistanceFade.x) / (_Main5thDistanceFade.y - _Main5thDistanceFade.x)), _Main5thDistanceFade.z);
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
}

// Main6th
void lilGetMain6th(inout lilFragData fd, inout float4 color6th LIL_SAMP_IN_FUNC(samp))
{
    color6th = _Color6th;
    if(_UseMain6thTex)
    {
        float2 uv6th = fd.uv0;
        float2 uvBM = fd.uvMain;
        if(_Main6thTex_UVMode == 1) uv6th = fd.uv1;
        if(_Main6thTex_UVMode == 2) uv6th = fd.uv2;
        if(_Main6thTex_UVMode == 3) uv6th = fd.uv3;
        if(_Main6thTex_UVMode == 4) uv6th = fd.uvMat;
        if(_UseWarp && _UseWarpMain6th)
        {
            warp(uv6th);
            warp(uvBM);
        }
        color6th *= LIL_GET_SUBTEX(_Main6thTex, uv6th);
        color6th.a *= LIL_SAMPLE_2D(_Main6thBlendMask, samp, uvBM).r;
        if(_AudioLink2Main6th) color6th.a *= fd.audioLinkValue;
        color6th.a = lerp(color6th.a, color6th.a * saturate((fd.depth - _Main6thDistanceFade.x) / (_Main6thDistanceFade.y - _Main6thDistanceFade.x)), _Main6thDistanceFade.z);
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
}

//MatCap3rd
void lilGetMatCap3rd(inout lilFragData fd, in float3 matcap3rdN LIL_SAMP_IN_FUNC(samp))
{
    if(_UseMatCap3rd)
    {
        // Normal
        float3 N = matcap3rdN;
        
        N = lerp(fd.origN, matcap3rdN, _MatCap3rdNormalStrength);

        // UV
        float2 mat3rdUV = lilCalcMatCapUV(fd.uv1, N, fd.V, fd.headV, _MatCap3rdTex_ST, _MatCap3rdBlendUV1.xy, _MatCap3rdZRotCancel, _MatCap3rdPerspective, _MatCap3rdVRParallaxStrength);

        // Color
        float4 matCap3rdColor = _MatCap3rdColor;
        matCap3rdColor *= LIL_SAMPLE_2D_LOD(_MatCap3rdTex, lil_sampler_linear_repeat, mat3rdUV, _MatCap3rdLod);
        #if !defined(LIL_PASS_FORWARDADD)
            matCap3rdColor.rgb = lerp(matCap3rdColor.rgb, matCap3rdColor.rgb * fd.lightColor, _MatCap3rdEnableLighting);
            matCap3rdColor.a   = lerp(matCap3rdColor.a, matCap3rdColor.a * fd.shadowmix, _MatCap3rdShadowMask);
        #else
            if(_MatCap3rdBlendMode < 3) matCap3rdColor.rgb *= fd.lightColor * _MatCap3rdEnableLighting;
            matCap3rdColor.a = lerp(matCap3rdColor.a, matCap3rdColor.a * fd.shadowmix, _MatCap3rdShadowMask);
        #endif
        #if LIL_RENDER == 2 && !defined(LIL_REFRACTION)
            if(_MatCap3rdApplyTransparency) matCap3rdColor.a *= fd.col.a;
        #endif
        matCap3rdColor.a = fd.facing < (_MatCap3rdBackfaceMask-1.0) ? 0.0 : matCap3rdColor.a;
        float3 matCapMask = 1.0;
        matCapMask = LIL_SAMPLE_2D_ST(_MatCap3rdBlendMask, samp, fd.uvMain).rgb;

        // Blend
        matCap3rdColor.rgb = lerp(matCap3rdColor.rgb, matCap3rdColor.rgb * fd.albedo, _MatCap3rdMainStrength);
        fd.col.rgb         = lilBlendColor(fd.col.rgb, matCap3rdColor.rgb, _MatCap3rdBlend * matCap3rdColor.a * matCapMask, _MatCap3rdBlendMode);
    }
}

// MatCap4th
void lilGetMatCap4th(inout lilFragData fd, in float3 matcap4thN LIL_SAMP_IN_FUNC(samp)) // Opaque only
{
    if(_UseMatCap4th)
    {
        // Normal
        float3 N = matcap4thN;
        
        N = lerp(fd.origN, matcap4thN, _MatCap4thNormalStrength);

        // UV
        float2 mat4thUV = lilCalcMatCapUV(fd.uv1, N, fd.V, fd.headV, _MatCap4thTex_ST, _MatCap4thBlendUV1.xy, _MatCap4thZRotCancel, _MatCap4thPerspective, _MatCap4thVRParallaxStrength);

        // Color
        float4 matCap4thColor = _MatCap4thColor;
        matCap4thColor *= LIL_SAMPLE_2D_LOD(_MatCap4thTex, lil_sampler_linear_repeat, mat4thUV, _MatCap4thLod);
        #if !defined(LIL_PASS_FORWARDADD)
            matCap4thColor.rgb = lerp(matCap4thColor.rgb, matCap4thColor.rgb * fd.lightColor, _MatCap4thEnableLighting);
            matCap4thColor.a   = lerp(matCap4thColor.a, matCap4thColor.a * fd.shadowmix, _MatCap4thShadowMask);
        #else
            if(_MatCap4thBlendMode < 3) matCap4thColor.rgb *= fd.lightColor * _MatCap4thEnableLighting;
            matCap4thColor.a = lerp(matCap4thColor.a, matCap4thColor.a * fd.shadowmix, _MatCap4thShadowMask);
        #endif
        #if LIL_RENDER == 2 && !defined(LIL_REFRACTION)
            if(_MatCap4thApplyTransparency) matCap4thColor.a *= fd.col.a;
        #endif
        matCap4thColor.a = fd.facing < (_MatCap4thBackfaceMask-1.0) ? 0.0 : matCap4thColor.a;
        float3 matCapMask = 1.0;
        matCapMask = LIL_SAMPLE_2D_ST(_MatCap4thBlendMask, samp, fd.uvMain).rgb;

        // Blend
        matCap4thColor.rgb = lerp(matCap4thColor.rgb, matCap4thColor.rgb * fd.albedo, _MatCap4thMainStrength);
        fd.col.rgb         = lilBlendColor(fd.col.rgb, matCap4thColor.rgb, _MatCap4thBlend * matCap4thColor.a * matCapMask, _MatCap4thBlendMode);
    }
}

// Glitter2nd
void lilGlitter2nd(inout lilFragData fd LIL_SAMP_IN_FUNC(samp))
{
    if(_UseGlitter2nd)
    {
        // View direction
        float3 glitter2ndViewDirection   = lilBlendVRParallax(fd.headV, fd.V, _Glitter2ndVRParallaxStrength);
        float3 glitter2ndCameraDirection = lerp(fd.cameraFront, fd.V, _Glitter2ndVRParallaxStrength);

        // Normal
        float3 N = fd.N;
        N = lerp(fd.origN, fd.N, _Glitter2ndNormalStrength);

        // Color
        float4 glitter2ndColor   = _Glitter2ndColor;
        float2 uvGlitter2ndColor = fd.uvMain; //fd.uv0;
        if(_Glitter2ndColorTex_UVMode == 1) uvGlitter2ndColor = fd.uv1;
        if(_Glitter2ndColorTex_UVMode == 2) uvGlitter2ndColor = fd.uv2;
        if(_Glitter2ndColorTex_UVMode == 3) uvGlitter2ndColor = fd.uv3;
        glitter2ndColor      *=  LIL_SAMPLE_2D_ST(_Glitter2ndColorTex, samp, uvGlitter2ndColor);
        float2 glitter2ndPos =   _Glitter2ndUVMode ? fd.uv1 : fd.uv0;
        glitter2ndColor.rgb  *=  lilCalcGlitter(glitter2ndPos, N, glitter2ndViewDirection, glitter2ndCameraDirection, fd.L, _Glitter2ndParams1, _Glitter2ndParams2, _Glitter2ndPostContrast, _Glitter2ndSensitivity, _Glitter2ndScaleRandomize, _Glitter2ndAngleRandomize, _Glitter2ndApplyShape, _Glitter2ndShapeTex, _Glitter2ndShapeTex_ST, _Glitter2ndAtras);
        glitter2ndColor.rgb  *=  lilCalcGlitter(glitter2ndPos, N, glitter2ndViewDirection, glitter2ndCameraDirection, fd.L, _Glitter2ndParams1, _Glitter2ndParams2, _Glitter2ndPostContrast, _Glitter2ndSensitivity, _Glitter2ndScaleRandomize, 0, false, _Glitter2ndShapeTex, float4(0,0,0,0), float4(1,1,0,0));
        glitter2ndColor.rgb  =   lerp(glitter2ndColor.rgb, glitter2ndColor.rgb * fd.albedo, _Glitter2ndMainStrength);
        #if LIL_RENDER == 2 && !defined(LIL_REFRACTION)
            if(_Glitter2ndApplyTransparency) glitter2ndColor.a *= fd.col.a;
        #endif
        glitter2ndColor.a = fd.facing < (_Glitter2ndBackfaceMask-1.0) ? 0.0 : glitter2ndColor.a;

        // Blend
        #if !defined(LIL_PASS_FORWARDADD)
            glitter2ndColor.a   =   lerp(glitter2ndColor.a, glitter2ndColor.a * fd.shadowmix, _Glitter2ndShadowMask);
            glitter2ndColor.rgb =   lerp(glitter2ndColor.rgb, glitter2ndColor.rgb * fd.lightColor, _Glitter2ndEnableLighting);
            fd.col.rgb          +=  glitter2ndColor.rgb * glitter2ndColor.a;
        #else
            glitter2ndColor.a  =   lerp(glitter2ndColor.a, glitter2ndColor.a * fd.shadowmix, _Glitter2ndShadowMask);
            fd.col.rgb         +=  glitter2ndColor.a * _Glitter2ndEnableLighting * glitter2ndColor.rgb * fd.lightColor;
        #endif
    }
}

// Emission3rd
void lilEmission3rd(inout lilFragData fd LIL_SAMP_IN_FUNC(samp))
{
    if(_UseEmission3rd)
    {
        float4 emission3rdColor = _Emission3rdColor;
        // UV
        float2 emission3rdUV = fd.uv0;
        if(_Emission3rdMap_UVMode == 1) emission3rdUV = fd.uv1;
        if(_Emission3rdMap_UVMode == 2) emission3rdUV = fd.uv2;
        if(_Emission3rdMap_UVMode == 3) emission3rdUV = fd.uv3;
        if(_Emission3rdMap_UVMode == 4) emission3rdUV = fd.uvRim;
        //if(_Emission3rdMap_UVMode == 4) emission3rdUV = fd.uvPanorama;
        float2 _Emission3rdMapParaTex = emission3rdUV + _Emission3rdParallaxDepth * fd.parallaxOffset;
        // Texture
        #if defined(LIL_FEATURE_ANIMATE_EMISSION_UV)
            emission3rdColor *= LIL_GET_EMITEX(_Emission3rdMap, _Emission3rdMapParaTex);
        #else
            emission3rdColor *= LIL_SAMPLE_2D_ST(_Emission3rdMap, sampler_Emission3rdMap, _Emission3rdMapParaTex);
        #endif
        // Mask
        #if defined(LIL_FEATURE_ANIMATE_EMISSION_MASK_UV)
            emission3rdColor *= LIL_GET_EMIMASK(_Emission3rdBlendMask, fd.uv0);
        #else
            emission3rdColor *= LIL_SAMPLE_2D_ST(_Emission3rdBlendMask, samp, fd.uvMain);
        #endif
        #if defined(LIL_FEATURE_AUDIOLINK)
            if(_AudioLink2Emission3rd) emission3rdColor.a *= fd.audioLinkValue;
        #endif
        emission3rdColor.rgb   = lerp(emission3rdColor.rgb, emission3rdColor.rgb * fd.invLighting, _Emission3rdFluorescence);
        emission3rdColor.rgb   = lerp(emission3rdColor.rgb, emission3rdColor.rgb * fd.albedo, _Emission3rdMainStrength);
        float emission3rdBlend = _Emission3rdBlend * lilCalcBlink(_Emission3rdBlink) * emission3rdColor.a;
        #if LIL_RENDER == 2 && !defined(LIL_REFRACTION)
            emission3rdBlend *= fd.col.a;
        #endif
        fd.col.rgb = lilBlendColor(fd.col.rgb, emission3rdColor.rgb, emission3rdBlend, _Emission3rdBlendMode);
    }
}

float MoleCalc(float2 uv, float2 pos, float radius, float blur)
{
    float   aspectFix = 1.0;
    if(_MoleAspectFix) aspectFix = _MainTex_TexelSize.w / _MainTex_TexelSize.z;
    float2 afuv = (uv - pos) * float2(aspectFix, 1);
    float  dist = length(afuv);
    
    return smoothstep(radius + blur, radius, dist);
}

// Mole
void lilMoleDrower(inout lilFragData fd LIL_SAMP_IN_FUNC(samp))
{
    if(_UseMole)
    {
        float  mole      = 0;
        float2 uv        = fd.uvMain;
        float4 moleColor = _MoleColor;
        
        if(_UseMole1st)  mole += MoleCalc(uv, _Mole1stPos, _Mole1stRadius, _Mole1stBlur);
        if(_UseMole2nd)  mole += MoleCalc(uv, _Mole2ndPos, _Mole2ndRadius, _Mole2ndBlur);
        if(_UseMole3rd)  mole += MoleCalc(uv, _Mole3rdPos, _Mole3rdRadius, _Mole3rdBlur);
        if(_UseMole4th)  mole += MoleCalc(uv, _Mole4thPos, _Mole4thRadius, _Mole4thBlur);
        if(_UseMole5th)  mole += MoleCalc(uv, _Mole5thPos, _Mole5thRadius, _Mole5thBlur);
        if(_UseMole6th)  mole += MoleCalc(uv, _Mole6thPos, _Mole6thRadius, _Mole6thBlur);
        if(_UseMole7th)  mole += MoleCalc(uv, _Mole7thPos, _Mole7thRadius, _Mole7thBlur);
        if(_UseMole8th)  mole += MoleCalc(uv, _Mole8thPos, _Mole8thRadius, _Mole8thBlur);
        if(_UseMole9th)  mole += MoleCalc(uv, _Mole9thPos, _Mole9thRadius, _Mole9thBlur);
        if(_UseMole10th) mole += MoleCalc(uv, _Mole10thPos, _Mole10thRadius, _Mole10thBlur);

        mole       = saturate(mole);
        fd.col.rgb = lilBlendColor(fd.col.rgb, moleColor.rgb, mole * moleColor.a, _MoleBlendMode);
    }
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

void lilLightBasedAlpha(inout lilFragData fd, uint _LightBasedAlphaLoadType, float alphaMask, float mainTexAlpha LIL_SAMP_IN_FUNC(samp))
{
    if(_UseLightBasedAlpha && !_UseParallax)
    {
        float4  lightBasedAlphaMask = 1.0;
        lightBasedAlphaMask         = saturate(LIL_SAMPLE_2D_ST(_ParallaxMap, sampler_MainTex, fd.uvMain));
        bool    isOff               = lightBasedAlphaMask.a < 0.25;
        bool    isOn                = lightBasedAlphaMask.a > 0.75;
        bool    isInvert            = (!isOff && !isOn) ^ _LightBasedAlphaInvert;
        if(!isOff)
        {
            if( _LightBasedAlphaLoadType == 0 && _UseAlphaMaskStyle) lightBasedAlphaMask.r = saturate(lightBasedAlphaMask.r * _LightBasedAlphaMaskScale + _LightBasedAlphaMaskValue);
            if(_LightBasedAlphaLoadType == 1) lightBasedAlphaMask.r = mainTexAlpha;
            if(_LightBasedAlphaLoadType == 2) lightBasedAlphaMask.r = alphaMask;
            float valueFactor       = 1.0;
            float maskedValueFactor = 1.0;
            float minTransparency   = max(lightBasedAlphaMask.g, lightBasedAlphaMask.r);
            float maxTransparency   = min(lightBasedAlphaMask.g, lightBasedAlphaMask.r);
            float sharpness         = 1.0 - lightBasedAlphaMask.b;
            float value             = GetLightValue(fd.lightColor, _LightBasedAlphaValueType);
            float L                 = _LowestLightThreshold;
            float M                 = _MiddleLightThreshold;
            float H                 = _HighestLightThreshold;
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
                float hard   = step(0.5, smooth);
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
        }
    }
}