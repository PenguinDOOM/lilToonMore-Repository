#include "custom_function.hlsl"

#define BEFORE_PARALLAX \
    if(_UseWarp && _UseWarpUVMain) warp(fd.uvMain); \
    if(_UseWarp && _UseWarpUV0) warp(fd.uv0); \
    if(_UseWarp && _UseWarpUV1) warp(fd.uv1); \
    if(_UseWarp && _UseWarpUV2) warp(fd.uv2); \
    if(_UseWarp && _UseWarpUV3) warp(fd.uv3); \
    if(_UseWarp && _UseWarpUVMat) warp(fd.uvMat); \
    if(_UseWarp && _UseWarpUVRim) warp(fd.uvRim);

#if !defined(OVERRIDE_MAIN)
    #define OVERRIDE_MAIN \
        float2 bkuvMain = fd.uvMain; \
        if(_UseWarp && _UseWarpMain1st) warp(fd.uvMain); \
        LIL_GET_MAIN_TEX \
        LIL_APPLY_MAIN_TONECORRECTION \
        fd.col *= _Color; \
        fd.uvMain = bkuvMain;
#endif

#if !defined(OVERRIDE_NORMAL_2ND)
    #if defined(LIL_FEATURE_Bump2ndScaleMask)
        #define LIL_SAMPLE_Bump2ndScaleMask bump2ndScale *= LIL_SAMPLE_2D_ST(_Bump2ndScaleMask, sampler_MainTex, fd.uvMain).r
        #define LIL_SAMPLE_Bump3rdScaleMask bump3rdScale *= LIL_SAMPLE_2D_ST(_Bump3rdScaleMask, sampler_MainTex, fd.uvMain).r
    #else
        #define LIL_SAMPLE_Bump2ndScaleMask
        #define LIL_SAMPLE_Bump3rdScaleMask
    #endif

    #if defined(LIL_FEATURE_Bump2ndMap)
        #define OVERRIDE_NORMAL_2ND \
            if(_UseBump2ndMap) \
                { \
                    float2 uvBump2nd = fd.uv0; \
                    if(_Bump2ndMap_UVMode == 1) uvBump2nd = fd.uv1; \
                    if(_Bump2ndMap_UVMode == 2) uvBump2nd = fd.uv2; \
                    if(_Bump2ndMap_UVMode == 3) uvBump2nd = fd.uv3; \
                    float4 normal2ndTex = LIL_SAMPLE_2D_ST(_Bump2ndMap, lil_sampler_linear_repeat, uvBump2nd); \
                    float bump2ndScale = _Bump2ndScale; \
                    LIL_SAMPLE_Bump2ndScaleMask; \
                    normalmap = lilBlendNormal(normalmap, lilUnpackNormalScale(normal2ndTex, bump2ndScale)); \
            } \
            if(_UseBump3rdMap) \
            { \
                float2 uvBump3rd = fd.uv0; \
                if(_Bump3rdMap_UVMode == 1) uvBump3rd = fd.uv1; \
                if(_Bump3rdMap_UVMode == 2) uvBump3rd = fd.uv2; \
                if(_Bump3rdMap_UVMode == 3) uvBump3rd = fd.uv3; \
                float4 normal3rdTex = LIL_SAMPLE_2D_ST(_Bump3rdMap, lil_sampler_linear_repeat, uvBump3rd); \
                float bump3rdScale = _Bump3rdScale; \
                LIL_SAMPLE_Bump3rdScaleMask; \
                normalmap = lilBlendNormal(normalmap, lilUnpackNormalScale(normal3rdTex, bump3rdScale)); \
            }
    #else
        #define OVERRIDE_NORMAL_2ND
    #endif
#endif

#if !defined(OVERRIDE_MAIN2ND)
    #define OVERRIDE_MAIN2ND \
        lilGetMain2ndMore(fd, color2nd, main2ndDissolveAlpha LIL_SAMP_IN(sampler_MainTex));
#endif

#if !defined(OVERRIDE_MAIN3RD)
    #define OVERRIDE_MAIN3RD \
        lilGetMain3rdMore(fd, color3rd, main3rdDissolveAlpha LIL_SAMP_IN(sampler_MainTex));
#endif

#if LIL_RENDER != 0
    #define BEFORE_ALPHAMASK \
        float4 color4th = 1.0; \
        float4 color5th = 1.0; \
        lilGetMain4th(fd, color4th LIL_SAMP_IN(sampler_MainTex)); \
        lilGetMain5th(fd, color5th LIL_SAMP_IN(sampler_MainTex)); \
        lilMoleDrower(fd LIL_SAMP_IN(sampler_MainTex)); \
        float valueFactor = 1.0; \
        if(_UseLightAlpha) \
        { \
                float value = max(fd.lightColor.r, max(fd.lightColor.g, fd.lightColor.b)); \
                float lLT = min(_LowestLightThreshold, _HighestLightThreshold - 1e-5); \
                float hLT = max(_HighestLightThreshold, _LowestLightThreshold + 1e-5); \
                valueFactor = smoothstep(lLT, hLT, value); \
                if(_LightAlphaInvert) valueFactor = 1.0 - valueFactor; \
                fd.col.a *= valueFactor; \
        }
#else
    #define BEFORE_ALPHAMASK \
        float4 color4th = 1.0; \
        float4 color5th = 1.0; \
        float4 color6th = 1.0; \
        lilGetMain4th(fd, color4th LIL_SAMP_IN(sampler_MainTex)); \
        lilGetMain5th(fd, color5th LIL_SAMP_IN(sampler_MainTex)); \
        lilGetMain6th(fd, color6th LIL_SAMP_IN(sampler_MainTex)); \
        lilMoleDrower(fd LIL_SAMP_IN(sampler_MainTex));
#endif

#if !defined(OVERRIDE_ALPHAMASK)
    #if defined(LIL_FEATURE_AlphaMask)
        #define LIL_SAMPLE_AlphaMask alphaMask = LIL_SAMPLE_2D_ST(_AlphaMask, sampler_MainTex, fd.uvMain).r
    #else
        #define LIL_SAMPLE_AlphaMask
    #endif

    #define OVERRIDE_ALPHAMASK \
        if(_AlphaMaskMode) \
        { \
            float alphaMask = 1.0; \
            LIL_SAMPLE_AlphaMask; \
            alphaMask = saturate(alphaMask * _AlphaMaskScale + _AlphaMaskValue); \
            if(_UseLightAlpha && _LightAlphaMask) alphaMask *= valueFactor; \
            if(_AlphaMaskMode == 1) fd.col.a = alphaMask; \
            if(_AlphaMaskMode == 2) fd.col.a = fd.col.a * alphaMask; \
            if(_AlphaMaskMode == 3) fd.col.a = saturate(fd.col.a + alphaMask); \
            if(_AlphaMaskMode == 4) fd.col.a = saturate(fd.col.a - alphaMask); \
        }
#endif

#define BEFORE_ANISOTROPY \
    float3 matcap3rdN = 0.0; \
    float3 matcap4thN = 0.0;

#if !defined(OVERRIDE_ANISOTROPY)
    #if defined(LIL_FEATURE_AnisotropyTangentMap)
        #define LIL_SAMPLE_AnisotropyTangentMap anisoTangentMap = LIL_SAMPLE_2D_ST(_AnisotropyTangentMap, sampler_MainTex, fd.uvMain)
    #else
        #define LIL_SAMPLE_AnisotropyTangentMap
    #endif

    #if defined(LIL_FEATURE_AnisotropyScaleMask)
        #define LIL_SAMPLE_AnisotropyScaleMask fd.anisotropy *= LIL_SAMPLE_2D_ST(_AnisotropyScaleMask, sampler_MainTex, fd.uvMain).r
    #else
        #define LIL_SAMPLE_AnisotropyScaleMask
    #endif
    
    #define OVERRIDE_ANISOTROPY \
        if(_UseAnisotropy) \
        { \
            float4 anisoTangentMap = float4(0.5,0.5,1.0,0.5); \
            LIL_SAMPLE_AnisotropyTangentMap; \
            float3 anisoTangent = lilUnpackNormalScale(anisoTangentMap, 1.0); \
            fd.T = lilOrthoNormalize(normalize(mul(anisoTangent, fd.TBN)), fd.N); \
            fd.B = cross(fd.N, fd.T); \
            fd.anisotropy = _AnisotropyScale; \
            LIL_SAMPLE_AnisotropyScaleMask; \
            float3 anisoNormalWS = lilGetAnisotropyNormalWS(fd.N, fd.T, fd.B, fd.V, fd.anisotropy); \
            if(_Anisotropy2Reflection)  fd.reflectionN  = anisoNormalWS; \
            if(_Anisotropy2MatCap)      fd.matcapN      = anisoNormalWS; \
            if(_Anisotropy2MatCap2nd)   fd.matcap2ndN   = anisoNormalWS; \
            if(_Anisotropy2MatCap3rd)   matcap3rdN   = anisoNormalWS; \
            if(_Anisotropy2MatCap4th)   matcap4thN   = anisoNormalWS; \
            if(_Anisotropy2Reflection)  fd.perceptualRoughness = saturate(1.2 - abs(fd.anisotropy)); \
        }
#endif

#if !defined(LIL_OUTLINE)
    #if !defined(LIL_PASS_FORWARDADD)
        #if LIL_RENDER != 0
            #define BEFORE_RIMSHADE \
                if(_UseMain4thTex) fd.col.rgb = lilBlendColor(fd.col.rgb, color4th.rgb, color4th.a - color4th.a * _Main4thEnableLighting, _Main4thTexBlendMode); \
                if(_UseMain5thTex) fd.col.rgb = lilBlendColor(fd.col.rgb, color5th.rgb, color5th.a - color5th.a * _Main5thEnableLighting, _Main5thTexBlendMode); 
        #else
            #define BEFORE_RIMSHADE \
                if(_UseMain4thTex) fd.col.rgb = lilBlendColor(fd.col.rgb, color4th.rgb, color4th.a - color4th.a * _Main4thEnableLighting, _Main4thTexBlendMode); \
                if(_UseMain5thTex) fd.col.rgb = lilBlendColor(fd.col.rgb, color5th.rgb, color5th.a - color5th.a * _Main5thEnableLighting, _Main5thTexBlendMode); \
                if(_UseMain6thTex) fd.col.rgb = lilBlendColor(fd.col.rgb, color6th.rgb, color6th.a - color6th.a * _Main6thEnableLighting, _Main6thTexBlendMode);
        #endif
    #else
        #if LIL_RENDER != 0
            #define BEFORE_RIMSHADE \
                if(_UseMain4thTex) fd.col.rgb = lerp(fd.col.rgb, 0, color4th.a - color4th.a * _Main4thEnableLighting); \
                if(_UseMain5thTex) fd.col.rgb = lerp(fd.col.rgb, 0, color5th.a - color5th.a * _Main5thEnableLighting);
        #else
            #define BEFORE_RIMSHADE \
                if(_UseMain4thTex) fd.col.rgb = lerp(fd.col.rgb, 0, color4th.a - color4th.a * _Main4thEnableLighting); \
                if(_UseMain5thTex) fd.col.rgb = lerp(fd.col.rgb, 0, color5th.a - color5th.a * _Main5thEnableLighting); \
                if(_UseMain6thTex) fd.col.rgb = lerp(fd.col.rgb, 0, color6th.a - color6th.a * _Main6thEnableLighting);
        #endif
    #endif
#endif

#define OVERRIDE_REFRACTION \
    if(_UseWarp && _WarpReplaceRefract) \
        lilBGWarp(fd LIL_SAMP_IN(sampler_MainTex)); \
    else \
        lilRefraction(fd LIL_SAMP_IN(sampler_MainTex));

#if !defined(BEFORE_RIMLIGHT)
    #if LIL_RENDER != 0
        #define BEFORE_RIMLIGHT \
            lilGetMatCap3rd(fd, matcap3rdN LIL_SAMP_IN(sampler_MainTex));
    #else
        #define BEFORE_RIMLIGHT \
            lilGetMatCap3rd(fd, matcap3rdN LIL_SAMP_IN(sampler_MainTex)); \
            lilGetMatCap4th(fd, matcap4thN LIL_SAMP_IN(sampler_MainTex));
    #endif
#endif

#if !defined(OVERRIDE_GLITTER)
    #define OVERRIDE_GLITTER \
        lilGlitter(fd LIL_SAMP_IN(sampler_MainTex)); \
        lilGlitter2nd(fd LIL_SAMP_IN(sampler_MainTex));
#endif

#ifndef LIL_PASS_FORWARDADD
    #if !defined(OVERRIDE_EMISSION_2ND)
        #define OVERRIDE_EMISSION_2ND \
            lilEmission2nd(fd LIL_SAMP_IN(sampler_MainTex)); \
            lilEmission3rd(fd LIL_SAMP_IN(sampler_MainTex));
    #endif
#endif
