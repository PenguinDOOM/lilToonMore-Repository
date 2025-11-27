//4th
void lilGetMain4th(inout lilFragData fd, inout float4 color4th, inout float main4thDissolveAlpha LIL_SAMP_IN_FUNC(samp))
{
    color4th = _Color4th;
    if(_UseMain4thTex)
    {
        float2 uv4th = fd.uv0;
        if(_Main4thTex_UVMode == 1) uv4th = fd.uv1;
        if(_Main4thTex_UVMode == 2) uv4th = fd.uv2;
        if(_Main4thTex_UVMode == 3) uv4th = fd.uv3;
        if(_Main4thTex_UVMode == 4) uv4th = fd.uvMat;
        color4th *= LIL_GET_SUBTEX(_Main4thTex, uv4th);
        color4th.a *= LIL_SAMPLE_2D(_Main4thBlendMask, samp, fd.uvMain).r;
        /*#define _Main4thDissolveMaskEnabled true
        lilCalcDissolveWithNoise(
            color4th.a,
            main4thDissolveAlpha,
            fd.uv0,
            fd.positionOS,
            _Main4thDissolveParams,
            _Main4thDissolvePos,
            _Main4thDissolveMask,
            _Main4thDissolveMask_ST,
            _Main4thDissolveMaskEnabled,
            _Main4thDissolveNoiseMask,
            _Main4thDissolveNoiseMask_ST,
            _Main4thDissolveNoiseMask_ScrollRotate,
            _Main4thDissolveNoiseStrength,
            samp
        );*/
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

void lilGetMain5th(inout lilFragData fd, inout float4 color5th, inout float main5thDissolveAlpha LIL_SAMP_IN_FUNC(samp))
{
    color5th = _Color5th;
    if(_UseMain5thTex)
    {
        float2 uv5th = fd.uv0;
        if(_Main5thTex_UVMode == 1) uv5th = fd.uv1;
        if(_Main5thTex_UVMode == 2) uv5th = fd.uv2;
        if(_Main5thTex_UVMode == 3) uv5th = fd.uv3;
        if(_Main5thTex_UVMode == 4) uv5th = fd.uvMat;
        color5th *= LIL_GET_SUBTEX(_Main5thTex, uv5th);
        color5th.a *= LIL_SAMPLE_2D(_Main5thBlendMask, samp, fd.uvMain).r;
        /*#define _Main5thDissolveMaskEnabled true
        lilCalcDissolveWithNoise(
            color5th.a,
            main5thDissolveAlpha,
            fd.uv0,
            fd.positionOS,
            _Main5thDissolveParams,
            _Main5thDissolvePos,
            _Main5thDissolveMask,
            _Main5thDissolveMask_ST,
            _Main5thDissolveMaskEnabled,
            _Main5thDissolveNoiseMask,
            _Main5thDissolveNoiseMask_ST,
            _Main5thDissolveNoiseMask_ScrollRotate,
            _Main5thDissolveNoiseStrength,
            samp
        );*/
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

void lilGetMain6th(inout lilFragData fd, inout float4 color6th, inout float main6thDissolveAlpha LIL_SAMP_IN_FUNC(samp))
{
    color6th = _Color6th;
    if(_UseMain6thTex)
    {
        float2 uv6th = fd.uv0;
        if(_Main6thTex_UVMode == 1) uv6th = fd.uv1;
        if(_Main6thTex_UVMode == 2) uv6th = fd.uv2;
        if(_Main6thTex_UVMode == 3) uv6th = fd.uv3;
        if(_Main6thTex_UVMode == 4) uv6th = fd.uvMat;
        color6th *= LIL_GET_SUBTEX(_Main6thTex, uv6th);
        color6th.a *= LIL_SAMPLE_2D(_Main6thBlendMask, samp, fd.uvMain).r;
        /*#define _Main6thDissolveMaskEnabled true
        lilCalcDissolveWithNoise(
            color6th.a,
            main6thDissolveAlpha,
            fd.uv0,
            fd.positionOS,
            _Main6thDissolveParams,
            _Main6thDissolvePos,
            _Main6thDissolveMask,
            _Main6thDissolveMask_ST,
            _Main6thDissolveMaskEnabled,
            _Main6thDissolveNoiseMask,
            _Main6thDissolveNoiseMask_ST,
            _Main6thDissolveNoiseMask_ScrollRotate,
            _Main6thDissolveNoiseStrength,
            samp
        );*/
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

float3 matcap3rdN = 0.0;


void lilGetMatCap3rd(inout lilFragData fd LIL_SAMP_IN_FUNC(samp))
{
    if(_UseMatCap3rd)
    {
        // Normal
        float3 N = 0.0;
        // if(_Anisotropy2MatCap3rd)   matcap3rdN   = anisoNormalWS;
        N = lerp(fd.origN, matcap3rdN, _MatCap3rdNormalStrength);
        if(_MatCap3rdCustomNormal)
        {
            float4 normalTex = LIL_SAMPLE_2D_ST(_MatCap3rdBumpMap, samp, fd.uvMain);
            float3 normalmap = lilUnpackNormalScale(normalTex, _MatCap3rdBumpScale);
            N = normalize(mul(normalmap, fd.TBN));
            N = fd.facing < (_FlipNormal-1.0) ? -N : N;
        }

        // UV
        float2 mat3rdUV = lilCalcMatCapUV(fd.uv1, N, fd.V, fd.headV, _MatCap3rdTex_ST, _MatCap3rdBlendUV1.xy, _MatCap3rdZRotCancel, _MatCap3rdPerspective, _MatCap3rdVRParallaxStrength);

        // Color
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
        matCap3rdColor.a = fd.facing < (_MatCap3rdBackfaceMask-1.0) ? 0.0 : matCap3rdColor.a;
        float3 matCapMask = 1.0;
        matCapMask = LIL_SAMPLE_2D_ST(_MatCap3rdBlendMask, samp, fd.uvMain).rgb;

        // Blend
        matCap3rdColor.rgb = lerp(matCap3rdColor.rgb, matCap3rdColor.rgb * fd.albedo, _MatCap3rdMainStrength);
        fd.col.rgb = lilBlendColor(fd.col.rgb, matCap3rdColor.rgb, _MatCap3rdBlend * matCap3rdColor.a * matCapMask, _MatCap3rdBlendMode);
    }
}

void lilGetMatCap4th(inout lilFragData fd LIL_SAMP_IN_FUNC(samp))
{
    if(_UseMatCap4th)
    {
        // Normal
        float3 N = 0.0;
        // if(_Anisotropy2MatCap4th)   matcap3rdN   = anisoNormalWS;
        N = lerp(fd.origN, matcap3rdN, _MatCap4thNormalStrength);
        if(_MatCap4thCustomNormal)
        {
            float4 normalTex = LIL_SAMPLE_2D_ST(_MatCap4thBumpMap, samp, fd.uvMain);
            float3 normalmap = lilUnpackNormalScale(normalTex, _MatCap4thBumpScale);
            N = normalize(mul(normalmap, fd.TBN));
            N = fd.facing < (_FlipNormal-1.0) ? -N : N;
        }

        // UV
        float2 mat4thUV = lilCalcMatCapUV(fd.uv1, N, fd.V, fd.headV, _MatCap4thTex_ST, _MatCap4thBlendUV1.xy, _MatCap4thZRotCancel, _MatCap4thPerspective, _MatCap4thVRParallaxStrength);

        // Color
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
        matCap4thColor.a = fd.facing < (_MatCap4thBackfaceMask-1.0) ? 0.0 : matCap4thColor.a;
        float3 matCapMask = 1.0;
        matCapMask = LIL_SAMPLE_2D_ST(_MatCap4thBlendMask, samp, fd.uvMain).rgb;

        // Blend
        matCap4thColor.rgb = lerp(matCap4thColor.rgb, matCap4thColor.rgb * fd.albedo, _MatCap4thMainStrength);
        fd.col.rgb = lilBlendColor(fd.col.rgb, matCap4thColor.rgb, _MatCap4thBlend * matCap4thColor.a * matCapMask, _MatCap4thBlendMode);
    }
}

void lilGlitter2nd(inout lilFragData fd LIL_SAMP_IN_FUNC(samp))
{
    if(_UseGlitter2nd)
    {
        // View direction
        float3 glitter2ndViewDirection = lilBlendVRParallax(fd.headV, fd.V, _Glitter2ndVRParallaxStrength);
        float3 glitter2ndCameraDirection = lerp(fd.cameraFront, fd.V, _Glitter2ndVRParallaxStrength);

        // Normal
        float3 N = fd.N;
        N = lerp(fd.origN, fd.N, _Glitter2ndNormalStrength);

        // Color
        float4 glitter2ndColor = _Glitter2ndColor;
        float2 uvGlitter2ndColor = fd.uvMain; //fd.uv0;
        if(_Glitter2ndColorTex_UVMode == 1) uvGlitter2ndColor = fd.uv1;
        if(_Glitter2ndColorTex_UVMode == 2) uvGlitter2ndColor = fd.uv2;
        if(_Glitter2ndColorTex_UVMode == 3) uvGlitter2ndColor = fd.uv3;
        glitter2ndColor *= LIL_SAMPLE_2D_ST(_Glitter2ndColorTex, samp, uvGlitter2ndColor);
        float2 glitter2ndPos = _Glitter2ndUVMode ? fd.uv1 : fd.uv0;
        glitter2ndColor.rgb *= lilCalcGlitter(glitter2ndPos, N, glitter2ndViewDirection, glitter2ndCameraDirection, fd.L, _Glitter2ndParams1, _Glitter2ndParams2, _Glitter2ndPostContrast, _Glitter2ndSensitivity, _Glitter2ndScaleRandomize, _Glitter2ndAngleRandomize, _Glitter2ndApplyShape, _Glitter2ndShapeTex, _Glitter2ndShapeTex_ST, _Glitter2ndAtras);
        glitter2ndColor.rgb *= lilCalcGlitter(glitter2ndPos, N, glitter2ndViewDirection, glitter2ndCameraDirection, fd.L, _Glitter2ndParams1, _Glitter2ndParams2, _Glitter2ndPostContrast, _Glitter2ndSensitivity, _Glitter2ndScaleRandomize, 0, false, _Glitter2ndShapeTex, float4(0,0,0,0), float4(1,1,0,0));
        glitter2ndColor.rgb = lerp(glitter2ndColor.rgb, glitter2ndColor.rgb * fd.albedo, _Glitter2ndMainStrength);
        #if LIL_RENDER == 2 && !defined(LIL_REFRACTION)
            if(_Glitter2ndApplyTransparency) glitter2ndColor.a *= fd.col.a;
        #endif
        glitter2ndColor.a = fd.facing < (_Glitter2ndBackfaceMask-1.0) ? 0.0 : glitter2ndColor.a;

        // Blend
        #if !defined(LIL_PASS_FORWARDADD)
            glitter2ndColor.a = lerp(glitter2ndColor.a, glitter2ndColor.a * fd.shadowmix, _Glitter2ndShadowMask);
            glitter2ndColor.rgb = lerp(glitter2ndColor.rgb, glitter2ndColor.rgb * fd.lightColor, _Glitter2ndEnableLighting);
            fd.col.rgb += glitter2ndColor.rgb * glitter2ndColor.a;
        #else
            glitter2ndColor.a = lerp(glitter2ndColor.a, glitter2ndColor.a * fd.shadowmix, _Glitter2ndShadowMask);
            fd.col.rgb += glitter2ndColor.a * _Glitter2ndEnableLighting * glitter2ndColor.rgb * fd.lightColor;
        #endif
    }
}