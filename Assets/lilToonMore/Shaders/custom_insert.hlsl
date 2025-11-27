#include "custom_function.hlsl"

#if !defined(OVERRIDE_NORMAL_2ND)
    #define LIL_SAMPLE_Bump3rdScaleMask bump3rdScale *= LIL_SAMPLE_2D_ST(_Bump3rdScaleMask, sampler_MainTex, fd.uvMain).r

    #if defined(LIL_FEATURE_Bump3rdMap)
        #define OVERRIDE_NORMAL_2ND \
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

#if !defined(OVERRIDE_MAIN3RD)
    #define OVERRIDE_MAIN3RD \
        float4 color4th = 1.0; \
        float4 color5th = 1.0; \
        float4 color6th = 1.0; \
        float main4thDissolveAlpha = 1.0; \
        float main5thDissolveAlpha = 1.0; \
        float main6thDissolveAlpha = 1.0; \
        lilGetMain3rd(fd, color3rd, main3rdDissolveAlpha LIL_SAMP_IN(sampler_MainTex)); \
        lilGetMain4th(fd, color4th, main4thDissolveAlpha LIL_SAMP_IN(sampler_MainTex)); \
        lilGetMain5th(fd, color5th, main5thDissolveAlpha LIL_SAMP_IN(sampler_MainTex)); \
        lilGetMain6th(fd, color6th, main6thDissolveAlpha LIL_SAMP_IN(sampler_MainTex));
#endif

#if !defined(LIL_OUTLINE)
    #if !defined(LIL_PASS_FORWARDADD)
        #define BEFORE_RIMSHADE \
            if(_UseMain4thTex) fd.col.rgb = lilBlendColor(fd.col.rgb, color4th.rgb, color4th.a - color4th.a * _Main4thEnableLighting, _Main4thTexBlendMode); \
            if(_UseMain5thTex) fd.col.rgb = lilBlendColor(fd.col.rgb, color5th.rgb, color5th.a - color5th.a * _Main5thEnableLighting, _Main5thTexBlendMode); \
            if(_UseMain6thTex) fd.col.rgb = lilBlendColor(fd.col.rgb, color6th.rgb, color6th.a - color6th.a * _Main6thEnableLighting, _Main6thTexBlendMode);
    #else
        #define BEFORE_RIMSHADE \
            if(_UseMain4thTex) fd.col.rgb = lerp(fd.col.rgb, 0, color4th.a - color4th.a * _Main4thEnableLighting); \
            if(_UseMain5thTex) fd.col.rgb = lerp(fd.col.rgb, 0, color5th.a - color5th.a * _Main5thEnableLighting); \
            if(_UseMain6thTex) fd.col.rgb = lerp(fd.col.rgb, 0, color6th.a - color6th.a * _Main6thEnableLighting);
    #endif
#endif

#if !defined(OVERRIDE_MATCAP_2ND)
    #define OVERRIDE_MATCAP_2ND \
        lilGetMatCap2nd(fd LIL_SAMP_IN(sampler_MainTex)); \
        lilGetMatCap3rd(fd LIL_SAMP_IN(sampler_MainTex)); \
        lilGetMatCap4th(fd LIL_SAMP_IN(sampler_MainTex));
#endif

#if !defined(OVERRIDE_GLITTER)
    #define OVERRIDE_GLITTER \
        lilGlitter(fd LIL_SAMP_IN(sampler_MainTex)); \
        lilGlitter2nd(fd LIL_SAMP_IN(sampler_MainTex));
#endif
