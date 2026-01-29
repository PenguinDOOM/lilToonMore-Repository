//----------------------------------------------------------------------------------------------------------------------
// Macro

/*
// Why can't I use lilbool and TEXTURE2D(tex)?
#if !defined(lilBool)
    #define lilBool uint
#endif

#if defined(SHADER_API_D3D11)
    #if !defined(TEXTURE2D)
        #define TEXTURE2D(tex) Texture2D tex
    #endif
#endif
*/

// Custom variables
//#define LIL_CUSTOM_PROPERTIES \
//    float _CustomVariable;
#define LIL_CUSTOM_PROPERTIES \
    float4  _Color4th; \
    float4  _Main4thTex_ST; \
    float4  _Main4thTex_ScrollRotate; \
    float4  _Main4thDistanceFade; \
    float4  _Main4thTexDecalAnimation; \
    float4  _Main4thTexDecalSubParam; \
    float   _Main4thTexAngle; \
    float   _Main4thEnableLighting; \
    float   _Main4thDissolveNoiseStrength; \
    uint    _Main4thTexBlendMode; \
    uint    _Main4thTexAlphaMode; \
    uint    _Main4thTex_UVMode; \
    uint    _Main4thTex_Cull; \
    bool    _UseMain4thTex; \
    bool    _Main4thTexIsMSDF; \
    bool    _Main4thTexIsDecal; \
    bool    _Main4thTexIsLeftOnly; \
    bool    _Main4thTexIsRightOnly; \
    bool    _Main4thTexShouldCopy; \
    bool    _Main4thTexShouldFlipMirror; \
    bool    _Main4thTexShouldFlipCopy; \
    bool    _AudioLink2Main4th; \
    float4  _Color5th; \
    float4  _Main5thTex_ST; \
    float4  _Main5thTex_ScrollRotate; \
    float4  _Main5thDistanceFade; \
    float4  _Main5thTexDecalAnimation; \
    float4  _Main5thTexDecalSubParam; \
    float   _Main5thTexAngle; \
    float   _Main5thEnableLighting; \
    float   _Main5thDissolveNoiseStrength; \
    uint    _Main5thTexBlendMode; \
    uint    _Main5thTexAlphaMode; \
    uint    _Main5thTex_UVMode; \
    uint    _Main5thTex_Cull; \
    bool    _UseMain5thTex; \
    bool    _Main5thTexIsMSDF; \
    bool    _Main5thTexIsDecal; \
    bool    _Main5thTexIsLeftOnly; \
    bool    _Main5thTexIsRightOnly; \
    bool    _Main5thTexShouldCopy; \
    bool    _Main5thTexShouldFlipMirror; \
    bool    _Main5thTexShouldFlipCopy; \
    bool    _AudioLink2Main5th; \
    float4  _Color6th; \
    float4  _Main6thTex_ST; \
    float4  _Main6thTex_ScrollRotate; \
    float4  _Main6thDistanceFade; \
    float4  _Main6thTexDecalAnimation; \
    float4  _Main6thTexDecalSubParam; \
    float   _Main6thTexAngle; \
    float   _Main6thEnableLighting; \
    float   _Main6thDissolveNoiseStrength; \
    uint    _Main6thTexBlendMode; \
    uint    _Main6thTexAlphaMode; \
    uint    _Main6thTex_UVMode; \
    uint    _Main6thTex_Cull; \
    bool    _UseMain6thTex; \
    bool    _Main6thTexIsMSDF; \
    bool    _Main6thTexIsDecal; \
    bool    _Main6thTexIsLeftOnly; \
    bool    _Main6thTexIsRightOnly; \
    bool    _Main6thTexShouldCopy; \
    bool    _Main6thTexShouldFlipMirror; \
    bool    _Main6thTexShouldFlipCopy; \
    bool    _AudioLink2Main6th; \
    float4  _Bump3rdMap_ST; \
    float4  _Bump3rdScaleMask_ST; \
    float   _Bump3rdScale; \
    uint    _Bump3rdMap_UVMode; \
    bool    _UseBump3rdMap; \
    float4  _MatCap3rdColor; \
    float4  _MatCap3rdTex_ST; \
    float4  _MatCap3rdBlendMask_ST; \
    float4  _MatCap3rdBlendUV1; \
    float   _MatCap3rdBlend; \
    float   _MatCap3rdEnableLighting; \
    float   _MatCap3rdShadowMask; \
    float   _MatCap3rdVRParallaxStrength; \
    float   _MatCap3rdBackfaceMask; \
    float   _MatCap3rdLod; \
    float   _MatCap3rdNormalStrength; \
    float   _MatCap3rdMainStrength; \
    uint    _MatCap3rdBlendMode; \
    bool    _UseMatCap3rd; \
    bool    _MatCap3rdApplyTransparency; \
    bool    _MatCap3rdPerspective; \
    bool    _MatCap3rdZRotCancel; \
    bool    _Anisotropy2MatCap3rd; \
    float4  _MatCap4thColor; \
    float4  _MatCap4thTex_ST; \
    float4  _MatCap4thBlendMask_ST; \
    float4  _MatCap4thBlendUV1; \
    float   _MatCap4thBlend; \
    float   _MatCap4thEnableLighting; \
    float   _MatCap4thShadowMask; \
    float   _MatCap4thVRParallaxStrength; \
    float   _MatCap4thBackfaceMask; \
    float   _MatCap4thLod; \
    float   _MatCap4thNormalStrength; \
    float   _MatCap4thMainStrength; \
    uint    _MatCap4thBlendMode; \
    bool    _UseMatCap4th; \
    bool    _MatCap4thApplyTransparency; \
    bool    _MatCap4thPerspective; \
    bool    _MatCap4thZRotCancel; \
    bool    _Anisotropy2MatCap4th; \
    float4  _Glitter2ndColor; \
    float4  _Glitter2ndColorTex_ST; \
    float4  _Glitter2ndParams1; \
    float4  _Glitter2ndParams2; \
    float4  _Glitter2ndShapeTex_ST; \
    float4  _Glitter2ndAtras; \
    float   _Glitter2ndMainStrength; \
    float   _Glitter2ndPostContrast; \
    float   _Glitter2ndSensitivity; \
    float   _Glitter2ndNormalStrength; \
    float   _Glitter2ndEnableLighting; \
    float   _Glitter2ndShadowMask; \
    float   _Glitter2ndVRParallaxStrength; \
    float   _Glitter2ndBackfaceMask; \
    float   _Glitter2ndScaleRandomize; \
    uint    _Glitter2ndUVMode; \
    uint    _Glitter2ndColorTex_UVMode; \
    bool    _UseGlitter2nd; \
    bool    _Glitter2ndApplyTransparency; \
    bool    _Glitter2ndApplyShape; \
    bool    _Glitter2ndAngleRandomize;\
    float   _WarpAnimSpeed; \
    float   _WarpIntensity; \
    float   _WarpBigAmp; \
    float   _WarpBigFreqX; \
    float   _WarpBigFreqY; \
    float   _WarpBigSpeedX; \
    float   _WarpBigSpeedY; \
    float   _WarpSmallAmp; \
    float   _WarpSmallFreqX; \
    float   _WarpSmallFreqY; \
    float   _WarpSmallSpeedX; \
    float   _WarpSmallSpeedY; \
    bool    _UseWarp; \
    bool    _UseWarpUVMain; \
    bool    _UseWarpUV0; \
    bool    _UseWarpUV1; \
    bool    _UseWarpUV2; \
    bool    _UseWarpUV3; \
    bool    _UseWarpUVMat; \
    bool    _UseWarpUVRim; \
    bool    _UseWarpMain1st; \
    bool    _UseWarpMain2nd; \
    bool    _UseWarpMain3rd; \
    bool    _UseWarpMain4th; \
    bool    _UseWarpMain5th; \
    bool    _UseWarpMain6th; \
    bool    _WarpReplaceRefract;\
    float4  _Emission3rdColor; \
    float4  _Emission3rdMap_ST; \
    float4  _Emission3rdMap_ScrollRotate; \
    float4  _Emission3rdBlendMask_ST; \
    float4  _Emission3rdBlendMask_ScrollRotate; \
    float4  _Emission3rdBlink; \
    float   _Emission3rdMainStrength; \
    float   _Emission3rdBlend; \
    float   _Emission3rdParallaxDepth; \
    float   _Emission3rdFluorescence; \
    uint    _Emission3rdMap_UVMode; \
    uint    _Emission3rdBlendMode; \
    bool    _UseEmission3rd; \
    bool    _AudioLink2Emission3rd; \
    float4  _MainTex_TexelSize; \
    float4  _MoleColor; \
    float2  _Mole1stPos; \
    float2  _Mole2ndPos; \
    float2  _Mole3rdPos; \
    float2  _Mole4thPos; \
    float2  _Mole5thPos; \
    float2  _Mole6thPos; \
    float2  _Mole7thPos; \
    float2  _Mole8thPos; \
    float2  _Mole9thPos; \
    float2  _Mole10thPos; \
    float   _Mole1stRadius; \
    float   _Mole2ndRadius; \
    float   _Mole3rdRadius; \
    float   _Mole4thRadius; \
    float   _Mole5thRadius; \
    float   _Mole6thRadius; \
    float   _Mole7thRadius; \
    float   _Mole8thRadius; \
    float   _Mole9thRadius; \
    float   _Mole10thRadius; \
    float   _Mole1stRadiusMultiplier; \
    float   _Mole2ndRadiusMultiplier; \
    float   _Mole3rdRadiusMultiplier; \
    float   _Mole4thRadiusMultiplier; \
    float   _Mole5thRadiusMultiplier; \
    float   _Mole6thRadiusMultiplier; \
    float   _Mole7thRadiusMultiplier; \
    float   _Mole8thRadiusMultiplier; \
    float   _Mole9thRadiusMultiplier; \
    float   _Mole10thRadiusMultiplier; \
    float   _Mole1stBlur; \
    float   _Mole2ndBlur; \
    float   _Mole3rdBlur; \
    float   _Mole4thBlur; \
    float   _Mole5thBlur; \
    float   _Mole6thBlur; \
    float   _Mole7thBlur; \
    float   _Mole8thBlur; \
    float   _Mole9thBlur; \
    float   _Mole10thBlur; \
    float   _Mole1stRotation; \
    float   _Mole2ndRotation; \
    float   _Mole3rdRotation; \
    float   _Mole4thRotation; \
    float   _Mole5thRotation; \
    float   _Mole6thRotation; \
    float   _Mole7thRotation; \
    float   _Mole8thRotation; \
    float   _Mole9thRotation; \
    float   _Mole10thRotation; \
    uint    _MoleBlendMode; \
    uint    _Mole1stShape; \
    uint    _Mole2ndShape; \
    uint    _Mole3rdShape; \
    uint    _Mole4thShape; \
    uint    _Mole5thShape; \
    uint    _Mole6thShape; \
    uint    _Mole7thShape; \
    uint    _Mole8thShape; \
    uint    _Mole9thShape; \
    uint    _Mole10thShape; \
    bool    _MoleAspectFix; \
    bool    _UseMole; \
    bool    _UseMole1st; \
    bool    _UseMole2nd; \
    bool    _UseMole3rd; \
    bool    _UseMole4th; \
    bool    _UseMole5th; \
    bool    _UseMole6th; \
    bool    _UseMole7th; \
    bool    _UseMole8th; \
    bool    _UseMole9th; \
    bool    _UseMole10th; \
    float4  _ParallaxMap_ST; \
    float   _LightBasedAlphaMaskScale; \
    float   _LightBasedAlphaMaskValue; \
    float   _OverrideMinTransparency; \
    float   _OverrideMaxTransparency; \
    float   _LowestLightThreshold; \
    float   _MiddleLightThreshold; \
    float   _HighestLightThreshold; \
    float   _SharpnessLightThreshold; \
    float   _LightThreshold; \
    float   _MinTransparency; \
    float   _MaxTransparency; \
    uint    _LightBasedAlphaLoadType; \
    uint    _LightBasedAlphaValueType; \
    uint    _LightBasedAlphaMode; \
    uint    _LightBasedAlphaApplyMode; \
    bool    _UseLightBasedAlpha; \
    bool    _UseAlphaMaskStyle; \
    bool    _OverrideMin; \
    bool    _OverrideMax; \
    bool    _UseMiddleLight; \
    bool    _UseClamp; \
    bool    _LightBasedAlphaInvert; \
    bool    _LightBasedAlphaPrePost; \
    uint    _RefractionType; \
    uint    _RefractionSGMBQuality;



// Custom textures
#define LIL_CUSTOM_TEXTURES \
    Texture2D _Main4thTex; \
    Texture2D _Main4thBlendMask; \
    SamplerState sampler_Main4thTex; \
    Texture2D _Main5thTex; \
    Texture2D _Main5thBlendMask; \
    SamplerState sampler_Main5thTex; \
    Texture2D _Main6thTex; \
    Texture2D _Main6thBlendMask; \
    SamplerState sampler_Main6thTex; \
    Texture2D _Bump3rdMap; \
    Texture2D _Bump3rdScaleMask; \
    Texture2D _MatCap3rdTex; \
    Texture2D _MatCap3rdBlendMask; \
    Texture2D _MatCap4thTex; \
    Texture2D _MatCap4thBlendMask; \
    Texture2D _Glitter2ndColorTex; \
    Texture2D _Glitter2ndShapeTex; \
    Texture2D _Emission3rdMap; \
    Texture2D _Emission3rdBlendMask; \
    SamplerState sampler_Emission3rdMap;




// Add vertex shader input
//#define LIL_REQUIRE_APP_POSITION
//#define LIL_REQUIRE_APP_TEXCOORD0
//#define LIL_REQUIRE_APP_TEXCOORD1
//#define LIL_REQUIRE_APP_TEXCOORD2
//#define LIL_REQUIRE_APP_TEXCOORD3
//#define LIL_REQUIRE_APP_TEXCOORD4
//#define LIL_REQUIRE_APP_TEXCOORD5
//#define LIL_REQUIRE_APP_TEXCOORD6
//#define LIL_REQUIRE_APP_TEXCOORD7
//#define LIL_REQUIRE_APP_COLOR
//#define LIL_REQUIRE_APP_NORMAL
//#define LIL_REQUIRE_APP_TANGENT
//#define LIL_REQUIRE_APP_VERTEXID

// Add vertex shader output
//#define LIL_V2F_FORCE_TEXCOORD0
//#define LIL_V2F_FORCE_TEXCOORD1
//#define LIL_V2F_FORCE_POSITION_OS
//#define LIL_V2F_FORCE_POSITION_WS
//#define LIL_V2F_FORCE_POSITION_SS
//#define LIL_V2F_FORCE_NORMAL
//#define LIL_V2F_FORCE_TANGENT
//#define LIL_V2F_FORCE_BITANGENT
//#define LIL_CUSTOM_V2F_MEMBER(id0,id1,id2,id3,id4,id5,id6,id7)

// Add vertex copy
#define LIL_CUSTOM_VERT_COPY

// Inserting a process into the vertex shader
//#define LIL_CUSTOM_VERTEX_OS
//#define LIL_CUSTOM_VERTEX_WS

// Inserting a process into pixel shader
//#define BEFORE_xx
//#define OVERRIDE_xx

//----------------------------------------------------------------------------------------------------------------------
// Information about variables
//----------------------------------------------------------------------------------------------------------------------

//----------------------------------------------------------------------------------------------------------------------
// Vertex shader inputs (appdata structure)
//
// Type     Name                    Description
// -------- ----------------------- --------------------------------------------------------------------
// float4   input.positionOS        POSITION
// float2   input.uv0               TEXCOORD0
// float2   input.uv1               TEXCOORD1
// float2   input.uv2               TEXCOORD2
// float2   input.uv3               TEXCOORD3
// float2   input.uv4               TEXCOORD4
// float2   input.uv5               TEXCOORD5
// float2   input.uv6               TEXCOORD6
// float2   input.uv7               TEXCOORD7
// float4   input.color             COLOR
// float3   input.normalOS          NORMAL
// float4   input.tangentOS         TANGENT
// uint     vertexID                SV_VertexID

//----------------------------------------------------------------------------------------------------------------------
// Vertex shader outputs or pixel shader inputs (v2f structure)
//
// The structure depends on the pass.
// Please check lil_pass_xx.hlsl for details.
//
// Type     Name                    Description
// -------- ----------------------- --------------------------------------------------------------------
// float4   output.positionCS       SV_POSITION
// float2   output.uv01             TEXCOORD0 TEXCOORD1
// float2   output.uv23             TEXCOORD2 TEXCOORD3
// float3   output.positionOS       object space position
// float3   output.positionWS       world space position
// float3   output.normalWS         world space normal
// float4   output.tangentWS        world space tangent

//----------------------------------------------------------------------------------------------------------------------
// Variables commonly used in the forward pass
//
// These are members of `lilFragData fd`
//
// Type     Name                    Description
// -------- ----------------------- --------------------------------------------------------------------
// float4   col                     lit color
// float3   albedo                  unlit color
// float3   emissionColor           color of emission
// -------- ----------------------- --------------------------------------------------------------------
// float3   lightColor              color of light
// float3   indLightColor           color of indirectional light
// float3   addLightColor           color of additional light
// float    attenuation             attenuation of light
// float3   invLighting             saturate((1.0 - lightColor) * sqrt(lightColor));
// -------- ----------------------- --------------------------------------------------------------------
// float2   uv0                     TEXCOORD0
// float2   uv1                     TEXCOORD1
// float2   uv2                     TEXCOORD2
// float2   uv3                     TEXCOORD3
// float2   uvMain                  Main UV
// float2   uvMat                   MatCap UV
// float2   uvRim                   Rim Light UV
// float2   uvPanorama              Panorama UV
// float2   uvScn                   Screen UV
// bool     isRightHand             input.tangentWS.w > 0.0;
// -------- ----------------------- --------------------------------------------------------------------
// float3   positionOS              object space position
// float3   positionWS              world space position
// float4   positionCS              clip space position
// float4   positionSS              screen space position
// float    depth                   distance from camera
// -------- ----------------------- --------------------------------------------------------------------
// float3x3 TBN                     tangent / bitangent / normal matrix
// float3   T                       tangent direction
// float3   B                       bitangent direction
// float3   N                       normal direction
// float3   V                       view direction
// float3   L                       light direction
// float3   origN                   normal direction without normal map
// float3   origL                   light direction without sh light
// float3   headV                   middle view direction of 2 cameras
// float3   reflectionN             normal direction for reflection
// float3   matcapN                 normal direction for reflection for MatCap
// float3   matcap4thN              normal direction for reflection for MatCap 4th
// float    facing                  VFACE
// -------- ----------------------- --------------------------------------------------------------------
// float    vl                      dot(viewDirection, lightDirection);
// float    hl                      dot(headDirection, lightDirection);
// float    ln                      dot(lightDirection, normalDirection);
// float    nv                      saturate(dot(normalDirection, viewDirection));
// float    nvabs                   abs(dot(normalDirection, viewDirection));
// -------- ----------------------- --------------------------------------------------------------------
// float4   triMask                 TriMask (for lite version)
// float3   parallaxViewDirection   mul(tbnWS, viewDirection);
// float2   parallaxOffset          parallaxViewDirection.xy / (parallaxViewDirection.z+0.5);
// float    anisotropy              strength of anisotropy
// float    smoothness              smoothness
// float    roughness               roughness
// float    perceptualRoughness     perceptual roughness
// float    shadowmix               this variable is 0 in the shadow area
// float    audioLinkValue          volume acquired by AudioLink
// -------- ----------------------- --------------------------------------------------------------------
// uint     renderingLayers         light layer of object (for URP / HDRP)
// uint     featureFlags            feature flags (for HDRP)
// uint2    tileIndex               tile index (for HDRP)
