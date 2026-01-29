#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;

namespace lilToon
{
    /// <summary>
    /// lilToon More Inspector
    /// </summary>
    public class lilToonMoreInspector : lilToonInspector
    {
        // Custom properties
        //private static bool isShowCustomProperties;
        private const string shaderName = "lilToonMore";
        internal static lilToonMoreEditorSetting ltmedSet
        {
            get { return lilToonMoreEditorSetting.instance; }
        }

        readonly string[] mainColor4thCategory = new string[]
        {
            "_Color4th",
            "_Main4thTex",
            "_Main4thTexAngle",
            "_Main4thTex_ScrollRotate",
            "_Main4thTex_UVMode",
            "_Main4thTex_Cull",
            "_Main4thTexDecalAnimation",
            "_Main4thTexDecalSubParam",
            "_Main4thTexIsDecal",
            "_Main4thTexIsLeftOnly",
            "_Main4thTexIsRightOnly",
            "_Main4thTexShouldCopy",
            "_Main4thTexShouldFlipMirror",
            "_Main4thTexShouldFlipCopy",
            "_Main4thTexIsMSDF",
            "_Main4thBlendMask",
            "_Main4thTexBlendMode",
            "_Main4thTexAlphaMode",
            "_Main4thEnableLighting",
            "_Main4thDistanceFade",
            "_AudioLink2Main4th",
        };

        readonly string[] mainColor5thCategory = new string[]
        {
            "_Color5th",
            "_Main5thTex",
            "_Main5thTexAngle",
            "_Main5thTex_ScrollRotate",
            "_Main5thTex_UVMode",
            "_Main5thTex_Cull",
            "_Main5thTexDecalAnimation",
            "_Main5thTexDecalSubParam",
            "_Main5thTexIsDecal",
            "_Main5thTexIsLeftOnly",
            "_Main5thTexIsRightOnly",
            "_Main5thTexShouldCopy",
            "_Main5thTexShouldFlipMirror",
            "_Main5thTexShouldFlipCopy",
            "_Main5thTexIsMSDF",
            "_Main5thBlendMask",
            "_Main5thTexBlendMode",
            "_Main5thTexAlphaMode",
            "_Main5thEnableLighting",
            "_Main5thDistanceFade",
            "_AudioLink2Main5th",
        };

        readonly string[] mainColor6thCategory = new string[]
        {
            "_Color6th",
            "_Main6thTex",
            "_Main6thTexAngle",
            "_Main6thTex_ScrollRotate",
            "_Main6thTex_UVMode",
            "_Main6thTex_Cull",
            "_Main6thTexDecalAnimation",
            "_Main6thTexDecalSubParam",
            "_Main6thTexIsDecal",
            "_Main6thTexIsLeftOnly",
            "_Main6thTexIsRightOnly",
            "_Main6thTexShouldCopy",
            "_Main6thTexShouldFlipMirror",
            "_Main6thTexShouldFlipCopy",
            "_Main6thTexIsMSDF",
            "_Main6thBlendMask",
            "_Main6thTexBlendMode",
            "_Main6thTexAlphaMode",
            "_Main6thEnableLighting",
            "_Main6thDistanceFade",
            "_AudioLink2Main6th",
        };

        readonly string[] bump3rdMapCategory = new string[]
        {
            "_Bump3rdMap",
            "_Bump3rdMap_UVMode",
            "_Bump3rdScale",
            "_Bump3rdScaleMask",
        };

        readonly string[] matCap3rdCategory = new string[]
        {
            "_MatCap3rdColor",
            "_MatCap3rdTex",
            "_MatCap3rdMainStrength",
            "_MatCap3rdBlendUV1",
            "_MatCap3rdZRotCancel",
            "_MatCap3rdPerspective",
            "_MatCap3rdVRParallaxStrength",
            "_MatCap3rdBlend",
            "_MatCap3rdBlendMask",
            "_MatCap3rdEnableLighting",
            "_MatCap3rdShadowMask",
            "_MatCap3rdBackfaceMask",
            "_MatCap3rdLod",
            "_MatCap3rdBlendMode",
            "_MatCap3rdApplyTransparency",
            "_MatCap3rdNormalStrength",
            "_Anisotropy2MatCap3rd",
        };

        readonly string[] matCap4thCategory = new string[]
        {
            "_MatCap4thColor",
            "_MatCap4thTex",
            "_MatCap4thMainStrength",
            "_MatCap4thBlendUV1",
            "_MatCap4thZRotCancel",
            "_MatCap4thPerspective",
            "_MatCap4thVRParallaxStrength",
            "_MatCap4thBlend",
            "_MatCap4thBlendMask",
            "_MatCap4thEnableLighting",
            "_MatCap4thShadowMask",
            "_MatCap4thBackfaceMask",
            "_MatCap4thLod",
            "_MatCap4thBlendMode",
            "_MatCap4thApplyTransparency",
            "_MatCap4thNormalStrength",
            "_Anisotropy2MatCap4th",
        };

        readonly string[] glitter2ndCategory = new string[]
        {
            "_Glitter2ndUVMode",
            "_Glitter2ndColor",
            "_Glitter2ndColorTex",
            "_Glitter2ndColorTex_UVMode",
            "_Glitter2ndMainStrength",
            "_Glitter2ndNormalStrength",
            "_Glitter2ndScaleRandomize",
            "_Glitter2ndApplyShape",
            "_Glitter2ndShapeTex",
            "_Glitter2ndAtras",
            "_Glitter2ndAngleRandomize",
            "_Glitter2ndParams1",
            "_Glitter2ndParams2",
            "_Glitter2ndPostContrast",
            "_Glitter2ndSensitivity",
            "_Glitter2ndEnableLighting",
            "_Glitter2ndShadowMask",
            "_Glitter2ndBackfaceMask",
            "_Glitter2ndApplyTransparency",
            "_Glitter2ndVRParallaxStrength",
        };

        readonly string[] warpCategory = new string[]
        {
            "_WarpAnimSpeed",
            "_WarpIntensity",
            "_WarpBigAmp",
            "_WarpBigFreqX",
            "_WarpBigFreqY",
            "_WarpBigSpeedX",
            "_WarpBigSpeedY",
            "_WarpSmallAmp",
            "_WarpSmallFreqX",
            "_WarpSmallFreqY",
            "_WarpSmallSpeedX",
            "_WarpSmallSpeedY",
            "_UseWarpUVMain",
            "_UseWarpUV0",
            "_UseWarpUV1",
            "_UseWarpUV2",
            "_UseWarpUV3",
            "_UseWarpUVMat",
            "_UseWarpUVRim",
            "_UseWarpMain1st",
            "_UseWarpMain2nd",
            "_UseWarpMain3rd",
            "_UseWarpMain4th",
            "_UseWarpMain5th",
            "_UseWarpMain6th",
            "_WarpReplaceRefract",
        };

        readonly string[] emission3rdCategory = new string[]
        {
            "_UseEmission3rd",
            "_Emission3rdColor",
            "_Emission3rdMap",
            "_Emission3rdMap_ScrollRotate",
            "_Emission3rdMap_UVMode",
            "_Emission3rdMainStrength",
            "_Emission3rdBlend",
            "_Emission3rdBlendMask",
            "_Emission3rdBlendMask_ScrollRotate",
            "_Emission3rdBlendMode",
            "_Emission3rdBlink",
            "_Emission3rdParallaxDepth",
            "_Emission3rdFluorescence",
            "_AudioLink2Emission3rd",
        };

        readonly string[] moleCategory = new string[]
        {
            "_MoleColor",
            "_MoleBlendMode",
            "_MoleAspectFix",
            "_UseMole1st",
            "_Mole1stPos",
            "_Mole1stRadius",
            "_Mole1stRadiusMultiplier",
            "_Mole1stBlur",
            "_Mole1stShape",
            "_Mole1stRotation",
            "_UseMole2nd",
            "_Mole2ndPos",
            "_Mole2ndRadius",
            "_Mole2ndRadiusMultiplier",
            "_Mole2ndBlur",
            "_Mole2ndShape",
            "_Mole2ndRotation",
            "_UseMole3rd",
            "_Mole3rdPos",
            "_Mole3rdRadius",
            "_Mole3rdRadiusMultiplier",
            "_Mole3rdBlur",
            "_Mole3rdShape",
            "_Mole3rdRotation",
            "_UseMole4th",
            "_Mole4thPos",
            "_Mole4thRadius",
            "_Mole4thRadiusMultiplier",
            "_Mole4thBlur",
            "_Mole4thShape",
            "_Mole4thRotation",
            "_UseMole5th",
            "_Mole5thPos",
            "_Mole5thRadius",
            "_Mole5thRadiusMultiplier",
            "_Mole5thBlur",
            "_Mole5thShape",
            "_Mole5thRotation",
            "_UseMole6th",
            "_Mole6thPos",
            "_Mole6thRadius",
            "_Mole6thRadiusMultiplier",
            "_Mole6thBlur",
            "_Mole6thShape",
            "_Mole6thRotation",
            "_UseMole7th",
            "_Mole7thPos",
            "_Mole7thRadius",
            "_Mole7thRadiusMultiplier",
            "_Mole7thBlur",
            "_Mole7thShape",
            "_Mole7thRotation",
            "_UseMole8th",
            "_Mole8thPos",
            "_Mole8thRadius",
            "_Mole8thRadiusMultiplier",
            "_Mole8thBlur",
            "_Mole8thShape",
            "_Mole8thRotation",
            "_UseMole9th",
            "_Mole9thPos",
            "_Mole9thRadius",
            "_Mole9thRadiusMultiplier",
            "_Mole9thBlur",
            "_Mole9thShape",
            "_Mole9thRotation",
            "_UseMole10th",
            "_Mole10thPos",
            "_Mole10thRadius",
            "_Mole10thRadiusMultiplier",
            "_Mole10thBlur",
            "_Mole10thShape",
            "_Mole10thRotation",
        };

        readonly string[] lightAlphaCategory = new string[]
        {
            "_UseAlphaMaskStyle",
            "_LightBasedAlphaMaskScale",
            "_LightBasedAlphaMaskValue",
            "_LightBasedAlphaLoadType",
            "_OverrideMin",
            "_OverrideMinTransparency",
            "_OverrideMax",
            "_OverrideMaxTransparency",
            "_LightBasedAlphaValueType",
            "_LightBasedAlphaMode",
            "_UseMiddleLight",
            "_LowestLightThreshold",
            "_MiddleLightThreshold",
            "_HighestLightThreshold",
            "_SharpnessLightThreshold",
            "_LightThreshold",
            "_LightBasedAlphaApplyMode",
            "_UseClamp",
            "_MinTransparency",
            "_MaxTransparency",
            "_LightBasedAlphaInvert",
            "_LightBasedAlphaPrePost",
        };

        readonly string[] refractionCategory = new string[]
        {
            "_RefractionType",
            "_RefractionSGMBQuality",
        };

        // Material Properties
        private MaterialProperty useMain4thTex;
        private MaterialProperty color4th;
        private MaterialProperty main4thTex;
        private MaterialProperty main4thTexAngle;
        private MaterialProperty main4thTex_ScrollRotate;
        private MaterialProperty main4thTex_UVMode;
        private MaterialProperty main4thTex_Cull;
        private MaterialProperty main4thTexDecalAnimation;
        private MaterialProperty main4thTexDecalSubParam;
        private MaterialProperty main4thTexIsDecal;
        private MaterialProperty main4thTexIsLeftOnly;
        private MaterialProperty main4thTexIsRightOnly;
        private MaterialProperty main4thTexShouldCopy;
        private MaterialProperty main4thTexShouldFlipMirror;
        private MaterialProperty main4thTexShouldFlipCopy;
        private MaterialProperty main4thTexIsMSDF;
        private MaterialProperty main4thBlendMask;
        private MaterialProperty main4thTexBlendMode;
        private MaterialProperty main4thTexAlphaMode;
        private MaterialProperty main4thEnableLighting;
        private MaterialProperty main4thDistanceFade;
        private MaterialProperty audioLink2Main4th;

        private MaterialProperty useMain5thTex;
        private MaterialProperty color5th;
        private MaterialProperty main5thTex;
        private MaterialProperty main5thTexAngle;
        private MaterialProperty main5thTex_ScrollRotate;
        private MaterialProperty main5thTex_UVMode;
        private MaterialProperty main5thTex_Cull;
        private MaterialProperty main5thTexDecalAnimation;
        private MaterialProperty main5thTexDecalSubParam;
        private MaterialProperty main5thTexIsDecal;
        private MaterialProperty main5thTexIsLeftOnly;
        private MaterialProperty main5thTexIsRightOnly;
        private MaterialProperty main5thTexShouldCopy;
        private MaterialProperty main5thTexShouldFlipMirror;
        private MaterialProperty main5thTexShouldFlipCopy;
        private MaterialProperty main5thTexIsMSDF;
        private MaterialProperty main5thBlendMask;
        private MaterialProperty main5thTexBlendMode;
        private MaterialProperty main5thTexAlphaMode;
        private MaterialProperty main5thEnableLighting;
        private MaterialProperty main5thDistanceFade;
        private MaterialProperty audioLink2Main5th;

        private MaterialProperty useMain6thTex;
        private MaterialProperty color6th;
        private MaterialProperty main6thTex;
        private MaterialProperty main6thTexAngle;
        private MaterialProperty main6thTex_ScrollRotate;
        private MaterialProperty main6thTex_UVMode;
        private MaterialProperty main6thTex_Cull;
        private MaterialProperty main6thTexDecalAnimation;
        private MaterialProperty main6thTexDecalSubParam;
        private MaterialProperty main6thTexIsDecal;
        private MaterialProperty main6thTexIsLeftOnly;
        private MaterialProperty main6thTexIsRightOnly;
        private MaterialProperty main6thTexShouldCopy;
        private MaterialProperty main6thTexShouldFlipMirror;
        private MaterialProperty main6thTexShouldFlipCopy;
        private MaterialProperty main6thTexIsMSDF;
        private MaterialProperty main6thBlendMask;
        private MaterialProperty main6thTexBlendMode;
        private MaterialProperty main6thTexAlphaMode;
        private MaterialProperty main6thEnableLighting;
        private MaterialProperty main6thDistanceFade;
        private MaterialProperty audioLink2Main6th;

        private MaterialProperty useBump3rdMap;
        private MaterialProperty bump3rdMap;
        private MaterialProperty bump3rdMap_UVMode;
        private MaterialProperty bump3rdScale;
        private MaterialProperty bump3rdScaleMask;

        private MaterialProperty useMatCap3rd;
        private MaterialProperty matCap3rdColor;
        private MaterialProperty matCap3rdTex;
        private MaterialProperty matCap3rdMainStrength;
        private MaterialProperty matCap3rdBlendUV1;
        private MaterialProperty matCap3rdZRotCancel;
        private MaterialProperty matCap3rdPerspective;
        private MaterialProperty matCap3rdVRParallaxStrength;
        private MaterialProperty matCap3rdBlend;
        private MaterialProperty matCap3rdBlendMask;
        private MaterialProperty matCap3rdEnableLighting;
        private MaterialProperty matCap3rdShadowMask;
        private MaterialProperty matCap3rdBackfaceMask;
        private MaterialProperty matCap3rdLod;
        private MaterialProperty matCap3rdBlendMode;
        private MaterialProperty matCap3rdApplyTransparency;
        private MaterialProperty matCap3rdNormalStrength;
        private MaterialProperty anisotropy2MatCap3rd;

        private MaterialProperty useMatCap4th;
        private MaterialProperty matCap4thColor;
        private MaterialProperty matCap4thTex;
        private MaterialProperty matCap4thMainStrength;
        private MaterialProperty matCap4thBlendUV1;
        private MaterialProperty matCap4thZRotCancel;
        private MaterialProperty matCap4thPerspective;
        private MaterialProperty matCap4thVRParallaxStrength;
        private MaterialProperty matCap4thBlend;
        private MaterialProperty matCap4thBlendMask;
        private MaterialProperty matCap4thEnableLighting;
        private MaterialProperty matCap4thShadowMask;
        private MaterialProperty matCap4thBackfaceMask;
        private MaterialProperty matCap4thLod;
        private MaterialProperty matCap4thBlendMode;
        private MaterialProperty matCap4thApplyTransparency;
        private MaterialProperty matCap4thNormalStrength;
        private MaterialProperty anisotropy2MatCap4th;

        private MaterialProperty useGlitter2nd;
        private MaterialProperty glitter2ndUVMode;
        private MaterialProperty glitter2ndColor;
        private MaterialProperty glitter2ndColorTex;
        private MaterialProperty glitter2ndColorTex_UVMode;
        private MaterialProperty glitter2ndMainStrength;
        private MaterialProperty glitter2ndNormalStrength;
        private MaterialProperty glitter2ndScaleRandomize;
        private MaterialProperty glitter2ndApplyShape;
        private MaterialProperty glitter2ndShapeTex;
        private MaterialProperty glitter2ndAtras;
        private MaterialProperty glitter2ndAngleRandomize;
        private MaterialProperty glitter2ndParams1;
        private MaterialProperty glitter2ndParams2;
        private MaterialProperty glitter2ndPostContrast;
        private MaterialProperty glitter2ndSensitivity;
        private MaterialProperty glitter2ndEnableLighting;
        private MaterialProperty glitter2ndShadowMask;
        private MaterialProperty glitter2ndBackfaceMask;
        private MaterialProperty glitter2ndApplyTransparency;
        private MaterialProperty glitter2ndVRParallaxStrength;

        private MaterialProperty useWarp;
        private MaterialProperty warpAnimSpeed;
        private MaterialProperty warpIntensity;
        private MaterialProperty warpBigAmp;
        private MaterialProperty warpBigFreqX;
        private MaterialProperty warpBigFreqY;
        private MaterialProperty warpBigSpeedX;
        private MaterialProperty warpBigSpeedY;
        private MaterialProperty warpSmallAmp;
        private MaterialProperty warpSmallFreqX;
        private MaterialProperty warpSmallFreqY;
        private MaterialProperty warpSmallSpeedX;
        private MaterialProperty warpSmallSpeedY;
        private MaterialProperty useWarpUVMain;
        private MaterialProperty useWarpUV0;
        private MaterialProperty useWarpUV1;
        private MaterialProperty useWarpUV2;
        private MaterialProperty useWarpUV3;
        private MaterialProperty useWarpUVMat;
        private MaterialProperty useWarpUVRim;
        private MaterialProperty useWarpMain1st;
        private MaterialProperty useWarpMain2nd;
        private MaterialProperty useWarpMain3rd;
        private MaterialProperty useWarpMain4th;
        private MaterialProperty useWarpMain5th;
        private MaterialProperty useWarpMain6th;
        private MaterialProperty warpReplaceRefract;

        private MaterialProperty useEmission3rd;
        private MaterialProperty emission3rdColor;
        private MaterialProperty emission3rdMap;
        private MaterialProperty emission3rdMap_ScrollRotate;
        private MaterialProperty emission3rdMap_UVMode;
        private MaterialProperty emission3rdMainStrength;
        private MaterialProperty emission3rdBlend;
        private MaterialProperty emission3rdBlendMask;
        private MaterialProperty emission3rdBlendMask_ScrollRotate;
        private MaterialProperty emission3rdBlendMode;
        private MaterialProperty emission3rdBlink;
        private MaterialProperty emission3rdParallaxDepth;
        private MaterialProperty emission3rdFluorescence;
        private MaterialProperty audioLink2Emission3rd;

        private MaterialProperty useMole;
        private MaterialProperty moleColor;
        private MaterialProperty moleBlendMode;
        private MaterialProperty moleAspectFix;
        private MaterialProperty useMole1st;
        private MaterialProperty mole1stPos;
        private MaterialProperty mole1stRadius;
        private MaterialProperty mole1stRadiusMultiplier;
        private MaterialProperty mole1stBlur;
        private MaterialProperty mole1stShape;
        private MaterialProperty mole1stRotation;
        private MaterialProperty useMole2nd;
        private MaterialProperty mole2ndPos;
        private MaterialProperty mole2ndRadius;
        private MaterialProperty mole2ndRadiusMultiplier;
        private MaterialProperty mole2ndBlur;
        private MaterialProperty mole2ndShape;
        private MaterialProperty mole2ndRotation;
        private MaterialProperty useMole3rd;
        private MaterialProperty mole3rdPos;
        private MaterialProperty mole3rdRadius;
        private MaterialProperty mole3rdRadiusMultiplier;
        private MaterialProperty mole3rdBlur;
        private MaterialProperty mole3rdShape;
        private MaterialProperty mole3rdRotation;
        private MaterialProperty useMole4th;
        private MaterialProperty mole4thPos;
        private MaterialProperty mole4thRadius;
        private MaterialProperty mole4thRadiusMultiplier;
        private MaterialProperty mole4thBlur;
        private MaterialProperty mole4thShape;
        private MaterialProperty mole4thRotation;
        private MaterialProperty useMole5th;
        private MaterialProperty mole5thPos;
        private MaterialProperty mole5thRadius;
        private MaterialProperty mole5thRadiusMultiplier;
        private MaterialProperty mole5thBlur;
        private MaterialProperty mole5thShape;
        private MaterialProperty mole5thRotation;
        private MaterialProperty useMole6th;
        private MaterialProperty mole6thPos;
        private MaterialProperty mole6thRadius;
        private MaterialProperty mole6thRadiusMultiplier;
        private MaterialProperty mole6thBlur;
        private MaterialProperty mole6thShape;
        private MaterialProperty mole6thRotation;
        private MaterialProperty useMole7th;
        private MaterialProperty mole7thPos;
        private MaterialProperty mole7thRadius;
        private MaterialProperty mole7thRadiusMultiplier;
        private MaterialProperty mole7thBlur;
        private MaterialProperty mole7thShape;
        private MaterialProperty mole7thRotation;
        private MaterialProperty useMole8th;
        private MaterialProperty mole8thPos;
        private MaterialProperty mole8thRadius;
        private MaterialProperty mole8thRadiusMultiplier;
        private MaterialProperty mole8thBlur;
        private MaterialProperty mole8thShape;
        private MaterialProperty mole8thRotation;
        private MaterialProperty useMole9th;
        private MaterialProperty mole9thPos;
        private MaterialProperty mole9thRadius;
        private MaterialProperty mole9thRadiusMultiplier;
        private MaterialProperty mole9thBlur;
        private MaterialProperty mole9thShape;
        private MaterialProperty mole9thRotation;
        private MaterialProperty useMole10th;
        private MaterialProperty mole10thPos;
        private MaterialProperty mole10thRadius;
        private MaterialProperty mole10thRadiusMultiplier;
        private MaterialProperty mole10thBlur;
        private MaterialProperty mole10thShape;
        private MaterialProperty mole10thRotation;

        private MaterialProperty useLightBasedAlpha;
        private MaterialProperty useAlphaMaskStyle;
        private MaterialProperty lightBasedAlphaMaskScale;
        private MaterialProperty lightBasedAlphaMaskValue;
        private MaterialProperty lightBasedAlphaLoadType;
        private MaterialProperty overrideMin;
        private MaterialProperty overrideMinTransparency;
        private MaterialProperty overrideMax;
        private MaterialProperty overrideMaxTransparency;
        private MaterialProperty lightBasedAlphaValueType;
        private MaterialProperty lightBasedAlphaMode;
        private MaterialProperty useMiddleLight;
        private MaterialProperty lowestLightThreshold;
        private MaterialProperty middleLightThreshold;
        private MaterialProperty highestLightThreshold;
        private MaterialProperty sharpnessLightThreshold;
        private MaterialProperty lightThreshold;
        private MaterialProperty lightBasedAlphaApplyMode;
        private MaterialProperty useClamp;
        private MaterialProperty minTransparency;
        private MaterialProperty maxTransparency;
        private MaterialProperty lightBasedAlphaInvert;
        private MaterialProperty lightBasedAlphaPrePost;

        private MaterialProperty refractionType;
        private MaterialProperty refractionSGMBQuality;

        private MaterialProperty useBump2ndMap;
        private MaterialProperty useGlitter;
        private MaterialProperty useEmission2nd;
        private MaterialProperty alphaMask;
        private MaterialProperty alphaMaskScale;
        private MaterialProperty alphaMaskValue;
        private MaterialProperty useParallax;
        private MaterialProperty parallaxMap;
        private MaterialProperty cutoff;

        // Copy-Paste Buffer
        static Dictionary<string, object> copyBuffer = new Dictionary<string, object>();
        static string copySourceMaterialName = "";
        static string copySourceShaderName = "";

        /// <summary>
        /// Copy Category
        /// </summary>
        /// <param name="props"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        void CopyCategory(string[] props, Material material)
        {
            copyBuffer.Clear();
            copySourceMaterialName = material.name;
            copySourceShaderName = material.shader.name;

            foreach (var prop in props)
            {
                if (!material.HasProperty(prop))
                    continue;
                int idx = material.shader.FindPropertyIndex(prop);
                if (idx < 0)
                    continue;

                var type = material.shader.GetPropertyType(idx);

                switch (type)
                {
                    case ShaderPropertyType.Color:
                        copyBuffer[prop] = material.GetColor(prop);
                        break;

                    case ShaderPropertyType.Float:
                    case ShaderPropertyType.Range:
                        copyBuffer[prop] = material.GetFloat(prop);
                        break;

                    case ShaderPropertyType.Vector:
                        copyBuffer[prop] = material.GetVector(prop);
                        break;

                    case ShaderPropertyType.Texture:
                        copyBuffer[prop] = new TexturePack(
                            material.GetTexture(prop),
                            material.GetTextureOffset(prop),
                            material.GetTextureScale(prop)
                        );
                        break;
                }
            }

            EditorUtility.DisplayDialog(
                "Copy Custom Property",
                $"Copied {copyBuffer.Count} properties\n"
                    + $"From: {copySourceMaterialName}\n"
                    + $"Shader: {copySourceShaderName}",
                "OK"
            );
        }

        /// <summary>
        /// Texture Pack
        /// </summary>
        /// <remarks>
        /// Used for copying texture along with offset and scale
        /// </remarks>
        class TexturePack
        {
            public Texture tex;
            public Vector2 offset;
            public Vector2 scale;

            public TexturePack(Texture t, Vector2 o, Vector2 s)
            {
                tex = t;
                offset = o;
                scale = s;
            }
        }

        /// <summary>
        /// Paste Category
        /// </summary>
        /// <param name="props"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        void PasteCategory(string[] props, Material material)
        {
            if (copyBuffer.Count == 0)
            {
                EditorUtility.DisplayDialog("Paste Custom Property", "Please copy it first.", "OK");
                return;
            }

            bool differentShader = material.shader.name != copySourceShaderName;
            string warningMsg = differentShader
                ? $"⚠️ Warning: Different shader!\n"
                    + $"Source: {copySourceShaderName}\n"
                    + $"Target: {material.shader.name}\n\n"
                    + $"Some properties may not be compatible.\n\n"
                : "";

            if (
                !EditorUtility.DisplayDialog(
                    "Paste Confirmation",
                    $"{warningMsg}"
                        + $"Paste {copyBuffer.Count} properties\n"
                        + $"From: {copySourceMaterialName}\n"
                        + $"To: {material.name}\n\n"
                        + $"よろしいですか？",
                    "Paste",
                    "Cancel"
                )
            )
            {
                return;
            }

            Undo.RecordObject(material, "Paste Custom Property");

            int pastedCount = 0;
            int skippedCount = 0;

            foreach (var prop in props)
            {
                if (!material.HasProperty(prop))
                {
                    skippedCount++;
                    continue;
                }
                if (!copyBuffer.ContainsKey(prop))
                {
                    continue;
                }

                var value = copyBuffer[prop];

                if (value is Color col)
                    material.SetColor(prop, col);
                else if (value is float f)
                    material.SetFloat(prop, f);
                else if (value is Vector4 v)
                    material.SetVector(prop, v);
                else if (value is TexturePack pack)
                {
                    material.SetTexture(prop, pack.tex);
                    material.SetTextureOffset(prop, pack.offset);
                    material.SetTextureScale(prop, pack.scale);
                }

                pastedCount++;
            }

            EditorUtility.SetDirty(material);

            if (skippedCount > 0)
            {
                EditorUtility.DisplayDialog(
                    "Paste Result",
                    $"Pasted: {pastedCount}\nSkipped: {skippedCount} (property not found)",
                    "OK"
                );
            }
        }

        /// <summary>
        /// Reset Category
        /// </summary>
        /// <param name="props"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        void ResetCategory(string[] props, Material material)
        {
            if (
                !EditorUtility.DisplayDialog(
                    "Reset Confirmation",
                    $"Reset properties to default values for:\n{material.name}\n\nよろしいですか？",
                    "Reset",
                    "Cancel"
                )
            )
            {
                return;
            }

            Undo.RecordObject(material, "Reset Custom Property");

            Material defaultMat = material.shader != null ? new Material(material.shader) : null;

            if (defaultMat == null)
                return;

            foreach (var prop in props)
            {
                if (!material.HasProperty(prop))
                    continue;
                int idx = material.shader.FindPropertyIndex(prop);
                if (idx < 0)
                    continue;

                var type = material.shader.GetPropertyType(idx);

                switch (type)
                {
                    case ShaderPropertyType.Color:
                        material.SetColor(prop, defaultMat.GetColor(prop));
                        break;

                    case ShaderPropertyType.Float:
                    case ShaderPropertyType.Range:
                        material.SetFloat(prop, defaultMat.GetFloat(prop));
                        break;

                    case ShaderPropertyType.Vector:
                        material.SetVector(prop, defaultMat.GetVector(prop));
                        break;

                    case ShaderPropertyType.Texture:
                        material.SetTexture(prop, defaultMat.GetTexture(prop));
                        material.SetTextureOffset(prop, defaultMat.GetTextureOffset(prop));
                        material.SetTextureScale(prop, defaultMat.GetTextureScale(prop));
                        break;
                }
            }

            EditorUtility.SetDirty(material);
            UnityEngine.Object.DestroyImmediate(defaultMat);
        }

        /// <summary>
        /// Load Custom Properties
        /// </summary>
        /// <param name="props"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        protected override void LoadCustomProperties(MaterialProperty[] props, Material material)
        {
            isCustomShader = true;

            // If you want to change rendering modes in the editor, specify the shader here
            ReplaceToCustomShaders();
            isShowRenderMode = !material.shader.name.Contains("Optional");

            // If not, set isShowRenderMode to false
            //isShowRenderMode = false;

            //LoadCustomLanguage("");
            //customVariable = FindProperty("_CustomVariable", props);
            useMain4thTex = FindProperty("_UseMain4thTex", props);
            color4th = FindProperty("_Color4th", props);
            main4thTex = FindProperty("_Main4thTex", props);
            main4thTexAngle = FindProperty("_Main4thTexAngle", props);
            main4thTex_ScrollRotate = FindProperty("_Main4thTex_ScrollRotate", props);
            main4thTex_UVMode = FindProperty("_Main4thTex_UVMode", props);
            main4thTex_Cull = FindProperty("_Main4thTex_Cull", props);
            main4thTexDecalAnimation = FindProperty("_Main4thTexDecalAnimation", props);
            main4thTexDecalSubParam = FindProperty("_Main4thTexDecalSubParam", props);
            main4thTexIsDecal = FindProperty("_Main4thTexIsDecal", props);
            main4thTexIsLeftOnly = FindProperty("_Main4thTexIsLeftOnly", props);
            main4thTexIsRightOnly = FindProperty("_Main4thTexIsRightOnly", props);
            main4thTexShouldCopy = FindProperty("_Main4thTexShouldCopy", props);
            main4thTexShouldFlipMirror = FindProperty("_Main4thTexShouldFlipMirror", props);
            main4thTexShouldFlipCopy = FindProperty("_Main4thTexShouldFlipCopy", props);
            main4thTexIsMSDF = FindProperty("_Main4thTexIsMSDF", props);
            main4thBlendMask = FindProperty("_Main4thBlendMask", props);
            main4thTexBlendMode = FindProperty("_Main4thTexBlendMode", props);
            main4thTexAlphaMode = FindProperty("_Main4thTexAlphaMode", props);
            main4thEnableLighting = FindProperty("_Main4thEnableLighting", props);
            main4thDistanceFade = FindProperty("_Main4thDistanceFade", props);
            audioLink2Main4th = FindProperty("_AudioLink2Main4th", props);

            useMain5thTex = FindProperty("_UseMain5thTex", props);
            color5th = FindProperty("_Color5th", props);
            main5thTex = FindProperty("_Main5thTex", props);
            main5thTexAngle = FindProperty("_Main5thTexAngle", props);
            main5thTex_ScrollRotate = FindProperty("_Main5thTex_ScrollRotate", props);
            main5thTex_UVMode = FindProperty("_Main5thTex_UVMode", props);
            main5thTex_Cull = FindProperty("_Main5thTex_Cull", props);
            main5thTexDecalAnimation = FindProperty("_Main5thTexDecalAnimation", props);
            main5thTexDecalSubParam = FindProperty("_Main5thTexDecalSubParam", props);
            main5thTexIsDecal = FindProperty("_Main5thTexIsDecal", props);
            main5thTexIsLeftOnly = FindProperty("_Main5thTexIsLeftOnly", props);
            main5thTexIsRightOnly = FindProperty("_Main5thTexIsRightOnly", props);
            main5thTexShouldCopy = FindProperty("_Main5thTexShouldCopy", props);
            main5thTexShouldFlipMirror = FindProperty("_Main5thTexShouldFlipMirror", props);
            main5thTexShouldFlipCopy = FindProperty("_Main5thTexShouldFlipCopy", props);
            main5thTexIsMSDF = FindProperty("_Main5thTexIsMSDF", props);
            main5thBlendMask = FindProperty("_Main5thBlendMask", props);
            main5thTexBlendMode = FindProperty("_Main5thTexBlendMode", props);
            main5thTexAlphaMode = FindProperty("_Main5thTexAlphaMode", props);
            main5thEnableLighting = FindProperty("_Main5thEnableLighting", props);
            main5thDistanceFade = FindProperty("_Main5thDistanceFade", props);
            audioLink2Main5th = FindProperty("_AudioLink2Main5th", props);

            useMain6thTex = FindProperty("_UseMain6thTex", props);
            color6th = FindProperty("_Color6th", props);
            main6thTex = FindProperty("_Main6thTex", props);
            main6thTexAngle = FindProperty("_Main6thTexAngle", props);
            main6thTex_ScrollRotate = FindProperty("_Main6thTex_ScrollRotate", props);
            main6thTex_UVMode = FindProperty("_Main6thTex_UVMode", props);
            main6thTex_Cull = FindProperty("_Main6thTex_Cull", props);
            main6thTexDecalAnimation = FindProperty("_Main6thTexDecalAnimation", props);
            main6thTexDecalSubParam = FindProperty("_Main6thTexDecalSubParam", props);
            main6thTexIsDecal = FindProperty("_Main6thTexIsDecal", props);
            main6thTexIsLeftOnly = FindProperty("_Main6thTexIsLeftOnly", props);
            main6thTexIsRightOnly = FindProperty("_Main6thTexIsRightOnly", props);
            main6thTexShouldCopy = FindProperty("_Main6thTexShouldCopy", props);
            main6thTexShouldFlipMirror = FindProperty("_Main6thTexShouldFlipMirror", props);
            main6thTexShouldFlipCopy = FindProperty("_Main6thTexShouldFlipCopy", props);
            main6thTexIsMSDF = FindProperty("_Main6thTexIsMSDF", props);
            main6thBlendMask = FindProperty("_Main6thBlendMask", props);
            main6thTexBlendMode = FindProperty("_Main6thTexBlendMode", props);
            main6thTexAlphaMode = FindProperty("_Main6thTexAlphaMode", props);
            main6thEnableLighting = FindProperty("_Main6thEnableLighting", props);
            main6thDistanceFade = FindProperty("_Main6thDistanceFade", props);
            audioLink2Main6th = FindProperty("_AudioLink2Main6th", props);

            useBump3rdMap = FindProperty("_UseBump3rdMap", props);
            bump3rdMap = FindProperty("_Bump3rdMap", props);
            bump3rdMap_UVMode = FindProperty("_Bump3rdMap_UVMode", props);
            bump3rdScale = FindProperty("_Bump3rdScale", props);
            bump3rdScaleMask = FindProperty("_Bump3rdScaleMask", props);

            useMatCap3rd = FindProperty("_UseMatCap3rd", props);
            matCap3rdColor = FindProperty("_MatCap3rdColor", props);
            matCap3rdTex = FindProperty("_MatCap3rdTex", props);
            matCap3rdMainStrength = FindProperty("_MatCap3rdMainStrength", props);
            matCap3rdBlendUV1 = FindProperty("_MatCap3rdBlendUV1", props);
            matCap3rdZRotCancel = FindProperty("_MatCap3rdZRotCancel", props);
            matCap3rdPerspective = FindProperty("_MatCap3rdPerspective", props);
            matCap3rdVRParallaxStrength = FindProperty("_MatCap3rdVRParallaxStrength", props);
            matCap3rdBlend = FindProperty("_MatCap3rdBlend", props);
            matCap3rdBlendMask = FindProperty("_MatCap3rdBlendMask", props);
            matCap3rdEnableLighting = FindProperty("_MatCap3rdEnableLighting", props);
            matCap3rdShadowMask = FindProperty("_MatCap3rdShadowMask", props);
            matCap3rdBackfaceMask = FindProperty("_MatCap3rdBackfaceMask", props);
            matCap3rdLod = FindProperty("_MatCap3rdLod", props);
            matCap3rdBlendMode = FindProperty("_MatCap3rdBlendMode", props);
            matCap3rdApplyTransparency = FindProperty("_MatCap3rdApplyTransparency", props);
            matCap3rdNormalStrength = FindProperty("_MatCap3rdNormalStrength", props);
            anisotropy2MatCap3rd = FindProperty("_Anisotropy2MatCap3rd", props);

            useMatCap4th = FindProperty("_UseMatCap4th", props);
            matCap4thColor = FindProperty("_MatCap4thColor", props);
            matCap4thTex = FindProperty("_MatCap4thTex", props);
            matCap4thMainStrength = FindProperty("_MatCap4thMainStrength", props);
            matCap4thBlendUV1 = FindProperty("_MatCap4thBlendUV1", props);
            matCap4thZRotCancel = FindProperty("_MatCap4thZRotCancel", props);
            matCap4thPerspective = FindProperty("_MatCap4thPerspective", props);
            matCap4thVRParallaxStrength = FindProperty("_MatCap4thVRParallaxStrength", props);
            matCap4thBlend = FindProperty("_MatCap4thBlend", props);
            matCap4thBlendMask = FindProperty("_MatCap4thBlendMask", props);
            matCap4thEnableLighting = FindProperty("_MatCap4thEnableLighting", props);
            matCap4thShadowMask = FindProperty("_MatCap4thShadowMask", props);
            matCap4thBackfaceMask = FindProperty("_MatCap4thBackfaceMask", props);
            matCap4thLod = FindProperty("_MatCap4thLod", props);
            matCap4thBlendMode = FindProperty("_MatCap4thBlendMode", props);
            matCap4thApplyTransparency = FindProperty("_MatCap4thApplyTransparency", props);
            matCap4thNormalStrength = FindProperty("_MatCap4thNormalStrength", props);
            anisotropy2MatCap4th = FindProperty("_Anisotropy2MatCap4th", props);

            useGlitter2nd = FindProperty("_UseGlitter2nd", props);
            glitter2ndUVMode = FindProperty("_Glitter2ndUVMode", props);
            glitter2ndColor = FindProperty("_Glitter2ndColor", props);
            glitter2ndColorTex = FindProperty("_Glitter2ndColorTex", props);
            glitter2ndColorTex_UVMode = FindProperty("_Glitter2ndColorTex_UVMode", props);
            glitter2ndMainStrength = FindProperty("_Glitter2ndMainStrength", props);
            glitter2ndNormalStrength = FindProperty("_Glitter2ndNormalStrength", props);
            glitter2ndScaleRandomize = FindProperty("_Glitter2ndScaleRandomize", props);
            glitter2ndApplyShape = FindProperty("_Glitter2ndApplyShape", props);
            glitter2ndShapeTex = FindProperty("_Glitter2ndShapeTex", props);
            glitter2ndAtras = FindProperty("_Glitter2ndAtras", props);
            glitter2ndAngleRandomize = FindProperty("_Glitter2ndAngleRandomize", props);
            glitter2ndParams1 = FindProperty("_Glitter2ndParams1", props);
            glitter2ndParams2 = FindProperty("_Glitter2ndParams2", props);
            glitter2ndPostContrast = FindProperty("_Glitter2ndPostContrast", props);
            glitter2ndSensitivity = FindProperty("_Glitter2ndSensitivity", props);
            glitter2ndEnableLighting = FindProperty("_Glitter2ndEnableLighting", props);
            glitter2ndShadowMask = FindProperty("_Glitter2ndShadowMask", props);
            glitter2ndBackfaceMask = FindProperty("_Glitter2ndBackfaceMask", props);
            glitter2ndApplyTransparency = FindProperty("_Glitter2ndApplyTransparency", props);
            glitter2ndVRParallaxStrength = FindProperty("_Glitter2ndVRParallaxStrength", props);

            useWarp = FindProperty("_UseWarp", props);
            warpAnimSpeed = FindProperty("_WarpAnimSpeed", props);
            warpIntensity = FindProperty("_WarpIntensity", props);
            warpBigAmp = FindProperty("_WarpBigAmp", props);
            warpBigFreqX = FindProperty("_WarpBigFreqX", props);
            warpBigFreqY = FindProperty("_WarpBigFreqY", props);
            warpBigSpeedX = FindProperty("_WarpBigSpeedX", props);
            warpBigSpeedY = FindProperty("_WarpBigSpeedY", props);
            warpSmallAmp = FindProperty("_WarpSmallAmp", props);
            warpSmallFreqX = FindProperty("_WarpSmallFreqX", props);
            warpSmallFreqY = FindProperty("_WarpSmallFreqY", props);
            warpSmallSpeedX = FindProperty("_WarpSmallSpeedX", props);
            warpSmallSpeedY = FindProperty("_WarpSmallSpeedY", props);
            useWarpUVMain = FindProperty("_UseWarpUVMain", props);
            useWarpUV0 = FindProperty("_UseWarpUV0", props);
            useWarpUV1 = FindProperty("_UseWarpUV1", props);
            useWarpUV2 = FindProperty("_UseWarpUV2", props);
            useWarpUV3 = FindProperty("_UseWarpUV3", props);
            useWarpUVMat = FindProperty("_UseWarpUVMat", props);
            useWarpUVRim = FindProperty("_UseWarpUVRim", props);
            useWarpMain1st = FindProperty("_UseWarpMain1st", props);
            useWarpMain2nd = FindProperty("_UseWarpMain2nd", props);
            useWarpMain3rd = FindProperty("_UseWarpMain3rd", props);
            useWarpMain4th = FindProperty("_UseWarpMain4th", props);
            useWarpMain5th = FindProperty("_UseWarpMain5th", props);
            useWarpMain6th = FindProperty("_UseWarpMain6th", props);
            warpReplaceRefract = FindProperty("_WarpReplaceRefract", props);

            useEmission3rd = FindProperty("_UseEmission3rd", props);
            emission3rdColor = FindProperty("_Emission3rdColor", props);
            emission3rdMap = FindProperty("_Emission3rdMap", props);
            emission3rdMap_ScrollRotate = FindProperty("_Emission3rdMap_ScrollRotate", props);
            emission3rdMap_UVMode = FindProperty("_Emission3rdMap_UVMode", props);
            emission3rdMainStrength = FindProperty("_Emission3rdMainStrength", props);
            emission3rdBlend = FindProperty("_Emission3rdBlend", props);
            emission3rdBlendMask = FindProperty("_Emission3rdBlendMask", props);
            emission3rdBlendMask_ScrollRotate = FindProperty(
                "_Emission3rdBlendMask_ScrollRotate",
                props
            );
            emission3rdBlendMode = FindProperty("_Emission3rdBlendMode", props);
            emission3rdBlink = FindProperty("_Emission3rdBlink", props);
            emission3rdParallaxDepth = FindProperty("_Emission3rdParallaxDepth", props);
            emission3rdFluorescence = FindProperty("_Emission3rdFluorescence", props);
            audioLink2Emission3rd = FindProperty("_AudioLink2Emission3rd", props);

            useMole = FindProperty("_UseMole", props);
            moleColor = FindProperty("_MoleColor", props);
            moleBlendMode = FindProperty("_MoleBlendMode", props);
            moleAspectFix = FindProperty("_MoleAspectFix", props);
            useMole1st = FindProperty("_UseMole1st", props);
            mole1stPos = FindProperty("_Mole1stPos", props);
            mole1stRadius = FindProperty("_Mole1stRadius", props);
            mole1stRadiusMultiplier = FindProperty("_Mole1stRadiusMultiplier", props);
            mole1stBlur = FindProperty("_Mole1stBlur", props);
            mole1stShape = FindProperty("_Mole1stShape", props);
            mole1stRotation = FindProperty("_Mole1stRotation", props);
            useMole2nd = FindProperty("_UseMole2nd", props);
            mole2ndPos = FindProperty("_Mole2ndPos", props);
            mole2ndRadius = FindProperty("_Mole2ndRadius", props);
            mole2ndRadiusMultiplier = FindProperty("_Mole2ndRadiusMultiplier", props);
            mole2ndBlur = FindProperty("_Mole2ndBlur", props);
            mole2ndShape = FindProperty("_Mole2ndShape", props);
            mole2ndRotation = FindProperty("_Mole2ndRotation", props);
            useMole3rd = FindProperty("_UseMole3rd", props);
            mole3rdPos = FindProperty("_Mole3rdPos", props);
            mole3rdRadius = FindProperty("_Mole3rdRadius", props);
            mole3rdRadiusMultiplier = FindProperty("_Mole3rdRadiusMultiplier", props);
            mole3rdBlur = FindProperty("_Mole3rdBlur", props);
            mole3rdShape = FindProperty("_Mole3rdShape", props);
            mole3rdRotation = FindProperty("_Mole3rdRotation", props);
            useMole4th = FindProperty("_UseMole4th", props);
            mole4thPos = FindProperty("_Mole4thPos", props);
            mole4thRadius = FindProperty("_Mole4thRadius", props);
            mole4thRadiusMultiplier = FindProperty("_Mole4thRadiusMultiplier", props);
            mole4thBlur = FindProperty("_Mole4thBlur", props);
            mole4thShape = FindProperty("_Mole4thShape", props);
            mole4thRotation = FindProperty("_Mole4thRotation", props);
            useMole5th = FindProperty("_UseMole5th", props);
            mole5thPos = FindProperty("_Mole5thPos", props);
            mole5thRadius = FindProperty("_Mole5thRadius", props);
            mole5thRadiusMultiplier = FindProperty("_Mole5thRadiusMultiplier", props);
            mole5thBlur = FindProperty("_Mole5thBlur", props);
            mole5thShape = FindProperty("_Mole5thShape", props);
            mole5thRotation = FindProperty("_Mole5thRotation", props);
            useMole6th = FindProperty("_UseMole6th", props);
            mole6thPos = FindProperty("_Mole6thPos", props);
            mole6thRadius = FindProperty("_Mole6thRadius", props);
            mole6thRadiusMultiplier = FindProperty("_Mole6thRadiusMultiplier", props);
            mole6thBlur = FindProperty("_Mole6thBlur", props);
            mole6thShape = FindProperty("_Mole6thShape", props);
            mole6thRotation = FindProperty("_Mole6thRotation", props);
            useMole7th = FindProperty("_UseMole7th", props);
            mole7thPos = FindProperty("_Mole7thPos", props);
            mole7thRadius = FindProperty("_Mole7thRadius", props);
            mole7thRadiusMultiplier = FindProperty("_Mole7thRadiusMultiplier", props);
            mole7thBlur = FindProperty("_Mole7thBlur", props);
            mole7thShape = FindProperty("_Mole7thShape", props);
            mole7thRotation = FindProperty("_Mole7thRotation", props);
            useMole8th = FindProperty("_UseMole8th", props);
            mole8thPos = FindProperty("_Mole8thPos", props);
            mole8thRadius = FindProperty("_Mole8thRadius", props);
            mole8thRadiusMultiplier = FindProperty("_Mole8thRadiusMultiplier", props);
            mole8thBlur = FindProperty("_Mole8thBlur", props);
            mole8thShape = FindProperty("_Mole8thShape", props);
            mole8thRotation = FindProperty("_Mole8thRotation", props);
            useMole9th = FindProperty("_UseMole9th", props);
            mole9thPos = FindProperty("_Mole9thPos", props);
            mole9thRadius = FindProperty("_Mole9thRadius", props);
            mole9thRadiusMultiplier = FindProperty("_Mole9thRadiusMultiplier", props);
            mole9thBlur = FindProperty("_Mole9thBlur", props);
            mole9thShape = FindProperty("_Mole9thShape", props);
            mole9thRotation = FindProperty("_Mole9thRotation", props);
            useMole10th = FindProperty("_UseMole10th", props);
            mole10thPos = FindProperty("_Mole10thPos", props);
            mole10thRadius = FindProperty("_Mole10thRadius", props);
            mole10thRadiusMultiplier = FindProperty("_Mole10thRadiusMultiplier", props);
            mole10thBlur = FindProperty("_Mole10thBlur", props);
            mole10thShape = FindProperty("_Mole10thShape", props);
            mole10thRotation = FindProperty("_Mole10thRotation", props);

            useLightBasedAlpha = FindProperty("_UseLightBasedAlpha", props);
            useAlphaMaskStyle = FindProperty("_UseAlphaMaskStyle", props);
            lightBasedAlphaMaskScale = FindProperty("_LightBasedAlphaMaskScale", props);
            lightBasedAlphaMaskValue = FindProperty("_LightBasedAlphaMaskValue", props);
            lightBasedAlphaLoadType = FindProperty("_LightBasedAlphaLoadType", props);
            overrideMin = FindProperty("_OverrideMin", props);
            overrideMinTransparency = FindProperty("_OverrideMinTransparency", props);
            overrideMax = FindProperty("_OverrideMax", props);
            overrideMaxTransparency = FindProperty("_OverrideMaxTransparency", props);
            lightBasedAlphaValueType = FindProperty("_LightBasedAlphaValueType", props);
            lightBasedAlphaMode = FindProperty("_LightBasedAlphaMode", props);
            useMiddleLight = FindProperty("_UseMiddleLight", props);
            lowestLightThreshold = FindProperty("_LowestLightThreshold", props);
            middleLightThreshold = FindProperty("_MiddleLightThreshold", props);
            highestLightThreshold = FindProperty("_HighestLightThreshold", props);
            sharpnessLightThreshold = FindProperty("_SharpnessLightThreshold", props);
            lightThreshold = FindProperty("_LightThreshold", props);
            lightBasedAlphaApplyMode = FindProperty("_LightBasedAlphaApplyMode", props);
            useClamp = FindProperty("_UseClamp", props);
            minTransparency = FindProperty("_MinTransparency", props);
            maxTransparency = FindProperty("_MaxTransparency", props);
            lightBasedAlphaInvert = FindProperty("_LightBasedAlphaInvert", props);
            lightBasedAlphaPrePost = FindProperty("_LightBasedAlphaPrePost", props);

            refractionType = FindProperty("_RefractionType", props);
            refractionSGMBQuality = FindProperty("_RefractionSGMBQuality", props);

            useBump2ndMap = FindProperty("_UseBump2ndMap", props);
            useGlitter = FindProperty("_UseGlitter", props);
            useEmission2nd = FindProperty("_UseEmission2nd", props);
            alphaMask = FindProperty("_AlphaMask", props);
            alphaMaskScale = FindProperty("_AlphaMaskScale", props);
            alphaMaskValue = FindProperty("_AlphaMaskValue", props);
            useParallax = FindProperty("_UseParallax", props);
            parallaxMap = FindProperty("_ParallaxMap", props);
            cutoff = FindProperty("_Cutoff", props);
        }

        /// <summary>
        /// Draw Custom Properties
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        protected override void DrawCustomProperties(Material material)
        {
            // GUIStyles Name   Description
            // ---------------- ------------------------------------
            // boxOuter         outer box
            // boxInnerHalf     inner box
            // boxInner         inner box without label
            // customBox        box (similar to unity default box)
            // customToggleFont label for box

            ltmedSet.isShowMain = lilEditorGUI.Foldout(
                GetLoc("sMainColorSetting"),
                ltmedSet.isShowMain
            );
            if (ltmedSet.isShowMain)
            {
                EditorGUILayout.BeginVertical(boxOuter);
                lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMain4thTex, false);
                if (useMain4thTex.floatValue == 1)
                {
                    EditorGUILayout.BeginVertical(boxInnerHalf);
                    lilEditorGUI.LocalizedPropertyTexture(
                        m_MaterialEditor,
                        colorRGBAContent,
                        main4thTex,
                        color4th
                    );
                    EditorGUI.indentLevel += 2;
                    lilEditorGUI.LocalizedPropertyAlpha(color4th);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, main4thTexIsMSDF);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, main4thTex_Cull);
                    EditorGUI.indentLevel -= 2;
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, main4thEnableLighting);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, main4thTexBlendMode);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, main4thTexAlphaMode);
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.UV4Decal(
                        m_MaterialEditor,
                        main4thTexIsDecal,
                        main4thTexIsLeftOnly,
                        main4thTexIsRightOnly,
                        main4thTexShouldCopy,
                        main4thTexShouldFlipMirror,
                        main4thTexShouldFlipCopy,
                        main4thTex,
                        main4thTex_ScrollRotate,
                        main4thTexAngle,
                        main4thTexDecalAnimation,
                        main4thTexDecalSubParam,
                        main4thTex_UVMode
                    );
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.LocalizedPropertyTexture(
                        m_MaterialEditor,
                        maskBlendContent,
                        main4thBlendMask
                    );
                    EditorGUILayout.LabelField(GetLoc("sDistanceFade"));
                    EditorGUI.indentLevel++;
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, main4thDistanceFade);
                    EditorGUI.indentLevel--;
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, audioLink2Main4th);
                    lilEditorGUI.DrawLine();
                    if (GUILayout.Button("Copy MainColor 4th"))
                    {
                        CopyCategory(mainColor4thCategory, material);
                    }
                    lilEditorGUI.DrawLine();
                    if (GUILayout.Button("Paste MainColor 4th"))
                    {
                        PasteCategory(mainColor4thCategory, material);
                    }
                    lilEditorGUI.DrawLine();
                    if (GUILayout.Button("Reset MainColor 4th"))
                    {
                        if (
                            EditorUtility.DisplayDialog(
                                "Reset Confirmation",
                                "MainColor 4th will be reset to their default values. \nAre you sure?",
                                "Reset",
                                "Cancel"
                            )
                        )
                        {
                            ResetCategory(mainColor4thCategory, material);
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.BeginVertical(boxOuter);
                lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMain5thTex, false);
                if (useMain5thTex.floatValue == 1)
                {
                    EditorGUILayout.BeginVertical(boxInnerHalf);
                    lilEditorGUI.LocalizedPropertyTexture(
                        m_MaterialEditor,
                        colorRGBAContent,
                        main5thTex,
                        color5th
                    );
                    EditorGUI.indentLevel += 2;
                    lilEditorGUI.LocalizedPropertyAlpha(color5th);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, main5thTexIsMSDF);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, main5thTex_Cull);
                    EditorGUI.indentLevel -= 2;
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, main5thEnableLighting);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, main5thTexBlendMode);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, main5thTexAlphaMode);
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.UV4Decal(
                        m_MaterialEditor,
                        main5thTexIsDecal,
                        main5thTexIsLeftOnly,
                        main5thTexIsRightOnly,
                        main5thTexShouldCopy,
                        main5thTexShouldFlipMirror,
                        main5thTexShouldFlipCopy,
                        main5thTex,
                        main5thTex_ScrollRotate,
                        main5thTexAngle,
                        main5thTexDecalAnimation,
                        main5thTexDecalSubParam,
                        main5thTex_UVMode
                    );
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.LocalizedPropertyTexture(
                        m_MaterialEditor,
                        maskBlendContent,
                        main5thBlendMask
                    );
                    EditorGUILayout.LabelField(GetLoc("sDistanceFade"));
                    EditorGUI.indentLevel++;
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, main5thDistanceFade);
                    EditorGUI.indentLevel--;
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, audioLink2Main5th);
                    lilEditorGUI.DrawLine();
                    if (GUILayout.Button("Copy MainColor 5th"))
                    {
                        CopyCategory(mainColor5thCategory, material);
                    }
                    lilEditorGUI.DrawLine();
                    if (GUILayout.Button("Paste MainColor 5th"))
                    {
                        PasteCategory(mainColor5thCategory, material);
                    }
                    lilEditorGUI.DrawLine();
                    if (GUILayout.Button("Reset MainColor 5th"))
                    {
                        if (
                            EditorUtility.DisplayDialog(
                                "Reset Confirmation",
                                "MainColor 5th will be reset to their default values. \nAre you sure?",
                                "Reset",
                                "Cancel"
                            )
                        )
                        {
                            ResetCategory(mainColor5thCategory, material);
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndVertical();

                if (renderingModeBuf != RenderingMode.Opaque)
                {
                    GUILayout.Label(
                        GetLoc("When using MainColor 6th, the rendering mode must be opaque"),
                        wrapLabel
                    );
                }
                else
                {
                    EditorGUILayout.BeginVertical(boxOuter);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMain6thTex, false);
                    if (useMain6thTex.floatValue == 1)
                    {
                        EditorGUILayout.BeginVertical(boxInnerHalf);
                        lilEditorGUI.LocalizedPropertyTexture(
                            m_MaterialEditor,
                            colorRGBAContent,
                            main6thTex,
                            color6th
                        );
                        EditorGUI.indentLevel += 2;
                        lilEditorGUI.LocalizedPropertyAlpha(color6th);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, main6thTexIsMSDF);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, main6thTex_Cull);
                        EditorGUI.indentLevel -= 2;
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, main6thEnableLighting);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, main6thTexBlendMode);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, main6thTexAlphaMode);
                        lilEditorGUI.DrawLine();
                        lilEditorGUI.UV4Decal(
                            m_MaterialEditor,
                            main6thTexIsDecal,
                            main6thTexIsLeftOnly,
                            main6thTexIsRightOnly,
                            main6thTexShouldCopy,
                            main6thTexShouldFlipMirror,
                            main6thTexShouldFlipCopy,
                            main6thTex,
                            main6thTex_ScrollRotate,
                            main6thTexAngle,
                            main6thTexDecalAnimation,
                            main6thTexDecalSubParam,
                            main6thTex_UVMode
                        );
                        lilEditorGUI.DrawLine();
                        lilEditorGUI.LocalizedPropertyTexture(
                            m_MaterialEditor,
                            maskBlendContent,
                            main6thBlendMask
                        );
                        EditorGUILayout.LabelField(GetLoc("sDistanceFade"));
                        EditorGUI.indentLevel++;
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, main6thDistanceFade);
                        EditorGUI.indentLevel--;
                        lilEditorGUI.DrawLine();
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, audioLink2Main6th);
                        lilEditorGUI.DrawLine();
                        if (GUILayout.Button("Copy MainColor 6th"))
                        {
                            CopyCategory(mainColor6thCategory, material);
                        }
                        lilEditorGUI.DrawLine();
                        if (GUILayout.Button("Paste MainColor 6th"))
                        {
                            PasteCategory(mainColor6thCategory, material);
                        }
                        lilEditorGUI.DrawLine();
                        if (GUILayout.Button("Reset MainColor 6th"))
                        {
                            if (
                                EditorUtility.DisplayDialog(
                                    "Reset Confirmation",
                                    "MainColor 6th will be reset to their default values. \nAre you sure?",
                                    "Reset",
                                    "Cancel"
                                )
                            )
                            {
                                ResetCategory(mainColor6thCategory, material);
                            }
                        }
                        EditorGUILayout.EndVertical();
                    }
                    EditorGUILayout.EndVertical();
                }

                if (
                    renderingModeBuf == RenderingMode.Opaque
                    || renderingModeBuf == RenderingMode.Cutout
                )
                {
                    GUILayout.Label(
                        GetLoc(
                            "When using Light Based Alpha, the rendering mode must be transparent"
                        ),
                        wrapLabel
                    );
                }
                else if (useParallax.floatValue == 1)
                {
                    GUILayout.Label(
                        GetLoc("When using Light Based Alpha, turn off Parallax"),
                        wrapLabel
                    );
                }
                else
                {
                    EditorGUILayout.BeginVertical(boxOuter);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useLightBasedAlpha, false);
                    if (useLightBasedAlpha.floatValue == 1)
                    {
                        EditorGUILayout.BeginVertical(boxInnerHalf);
                        lilEditorGUI.LocalizedPropertyTexture(
                            m_MaterialEditor,
                            customMaskContent,
                            parallaxMap
                        );
                        if (parallaxMap.textureValue == null)
                            parallaxMap.textureValue = Texture2D.whiteTexture;
                        lilEditorGUI.UVSettingGUI(m_MaterialEditor, parallaxMap);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, lightBasedAlphaLoadType);
                        if (lightBasedAlphaLoadType.floatValue == 2)
                        {
                            lilEditorGUI.DrawLine();
                            lilEditorGUI.LocalizedPropertyTexture(
                                m_MaterialEditor,
                                lilLanguageManager.alphaMaskContent,
                                alphaMask
                            );
                            lilEditorGUI.UVSettingGUI(m_MaterialEditor, alphaMask);

                            bool invertAlphaMask = alphaMaskScale.floatValue < 0;
                            float transparency =
                                alphaMaskValue.floatValue - (invertAlphaMask ? 1.0f : 0.0f);

                            EditorGUI.BeginChangeCheck();
                            EditorGUI.showMixedValue =
                                alphaMaskScale.hasMixedValue || alphaMaskValue.hasMixedValue;
                            invertAlphaMask = lilEditorGUI.Toggle(
                                Event.current.alt ? alphaMaskScale.name : "Invert",
                                invertAlphaMask
                            );
                            transparency = lilEditorGUI.Slider(
                                Event.current.alt
                                    ? alphaMaskScale.name + ", " + alphaMaskValue.name
                                    : "Transparency",
                                transparency,
                                -1.0f,
                                1.0f
                            );
                            EditorGUI.showMixedValue = false;

                            if (EditorGUI.EndChangeCheck())
                            {
                                alphaMaskScale.floatValue = invertAlphaMask ? -1.0f : 1.0f;
                                alphaMaskValue.floatValue =
                                    transparency + (invertAlphaMask ? 1.0f : 0.0f);
                            }
                            lilEditorGUI.LocalizedProperty(m_MaterialEditor, cutoff);

                            ltmedSet.isAlphaMaskModeAdvanced = EditorGUILayout.Toggle(
                                "Show advanced editor",
                                ltmedSet.isAlphaMaskModeAdvanced
                            );
                            if (ltmedSet.isAlphaMaskModeAdvanced)
                            {
                                EditorGUI.indentLevel++;
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, alphaMaskScale);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, alphaMaskValue);
                                EditorGUI.indentLevel--;
                            }
                            lilEditorGUI.DrawLine();
                        }
                        if (lightBasedAlphaLoadType.floatValue == 0)
                        {
                            lilEditorGUI.LocalizedProperty(m_MaterialEditor, useAlphaMaskStyle);
                            if (useAlphaMaskStyle.floatValue == 1)
                            {
                                bool invertAlphaMask = lightBasedAlphaMaskScale.floatValue < 0;
                                float transparency =
                                    lightBasedAlphaMaskValue.floatValue
                                    - (invertAlphaMask ? 1.0f : 0.0f);

                                EditorGUI.BeginChangeCheck();
                                EditorGUI.showMixedValue =
                                    lightBasedAlphaMaskScale.hasMixedValue
                                    || lightBasedAlphaMaskValue.hasMixedValue;
                                invertAlphaMask = lilEditorGUI.Toggle(
                                    Event.current.alt ? lightBasedAlphaMaskScale.name : "Invert",
                                    invertAlphaMask
                                );
                                transparency = lilEditorGUI.Slider(
                                    Event.current.alt
                                        ? lightBasedAlphaMaskScale.name
                                            + ", "
                                            + lightBasedAlphaMaskValue.name
                                        : "Transparency",
                                    transparency,
                                    -1.0f,
                                    1.0f
                                );
                                EditorGUI.showMixedValue = false;

                                if (EditorGUI.EndChangeCheck())
                                {
                                    lightBasedAlphaMaskScale.floatValue = invertAlphaMask
                                        ? -1.0f
                                        : 1.0f;
                                    lightBasedAlphaMaskValue.floatValue =
                                        transparency + (invertAlphaMask ? 1.0f : 0.0f);
                                }
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, cutoff);
                                ltmedSet.isLightBasedAlphaMaskAdvanced = EditorGUILayout.Toggle(
                                    "Show advanced editor",
                                    ltmedSet.isLightBasedAlphaMaskAdvanced
                                );
                                if (ltmedSet.isLightBasedAlphaMaskAdvanced)
                                {
                                    EditorGUI.indentLevel++;
                                    lilEditorGUI.LocalizedProperty(
                                        m_MaterialEditor,
                                        lightBasedAlphaMaskScale
                                    );
                                    lilEditorGUI.LocalizedProperty(
                                        m_MaterialEditor,
                                        lightBasedAlphaMaskValue
                                    );
                                    EditorGUI.indentLevel--;
                                }
                            }
                        }
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, overrideMin);
                        if (overrideMin.floatValue == 1)
                            lilEditorGUI.LocalizedProperty(
                                m_MaterialEditor,
                                overrideMinTransparency
                            );
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, overrideMax);
                        if (overrideMax.floatValue == 1)
                            lilEditorGUI.LocalizedProperty(
                                m_MaterialEditor,
                                overrideMaxTransparency
                            );
                        lilEditorGUI.DrawLine();
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, lightBasedAlphaValueType);
                        lilEditorGUI.DrawLine();
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, lightBasedAlphaMode);
                        if (lightBasedAlphaMode.floatValue == 0)
                        {
                            lilEditorGUI.LocalizedProperty(m_MaterialEditor, lowestLightThreshold);
                            lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMiddleLight);
                            if (useMiddleLight.floatValue == 1)
                            {
                                lilEditorGUI.LocalizedProperty(
                                    m_MaterialEditor,
                                    middleLightThreshold
                                );
                            }
                            lilEditorGUI.LocalizedProperty(m_MaterialEditor, highestLightThreshold);
                            lilEditorGUI.LocalizedProperty(
                                m_MaterialEditor,
                                sharpnessLightThreshold
                            );
                        }
                        else if (lightBasedAlphaMode.floatValue == 1)
                        {
                            lilEditorGUI.LocalizedProperty(m_MaterialEditor, lightThreshold);
                        }
                        else
                        {
                            lilEditorGUI.LocalizedProperty(m_MaterialEditor, lowestLightThreshold);
                            lilEditorGUI.LocalizedProperty(m_MaterialEditor, highestLightThreshold);
                        }
                        lilEditorGUI.DrawLine();
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, lightBasedAlphaApplyMode);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, useClamp);
                        if (useClamp.floatValue == 1)
                        {
                            lilEditorGUI.LocalizedProperty(m_MaterialEditor, minTransparency);
                            lilEditorGUI.LocalizedProperty(m_MaterialEditor, maxTransparency);
                        }
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, lightBasedAlphaInvert);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, lightBasedAlphaPrePost);
                        lilEditorGUI.DrawLine();
                        if (GUILayout.Button("Copy Light Based Alpha"))
                        {
                            CopyCategory(lightAlphaCategory, material);
                        }
                        lilEditorGUI.DrawLine();
                        if (GUILayout.Button("Paste Light Based Alpha"))
                        {
                            PasteCategory(lightAlphaCategory, material);
                        }
                        lilEditorGUI.DrawLine();
                        if (GUILayout.Button("Reset Light Based Alpha"))
                        {
                            if (
                                EditorUtility.DisplayDialog(
                                    "Reset Confirmation",
                                    "Light Based Alpha will be reset to their default values. \nAre you sure?",
                                    "Reset",
                                    "Cancel"
                                )
                            )
                            {
                                ResetCategory(lightAlphaCategory, material);
                            }
                        }
                        EditorGUILayout.EndVertical();
                    }
                    EditorGUILayout.EndVertical();
                }
            }

            ltmedSet.isShowEmission = lilEditorGUI.Foldout(
                GetLoc("sEmissionSetting"),
                ltmedSet.isShowEmission
            );
            if (ltmedSet.isShowEmission)
            {
                if (useEmission2nd.floatValue == 0)
                {
                    GUILayout.Label(
                        GetLoc("If you use Emission 3rd, you must use Emission 2nd"),
                        wrapLabel
                    );
                    useEmission3rd.floatValue = 0;
                }
                else
                {
                    EditorGUILayout.BeginVertical(boxOuter);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useEmission3rd, false);
                    if (useEmission3rd.floatValue == 1)
                    {
                        EditorGUILayout.BeginVertical(boxInnerHalf);
                        lilEditorGUI.TextureGUI(
                            m_MaterialEditor,
                            false,
                            ref ltmedSet.isShowEmission3rdMap,
                            colorMaskRGBAContent,
                            emission3rdMap,
                            emission3rdColor,
                            emission3rdMap_ScrollRotate,
                            emission3rdMap_UVMode,
                            true,
                            true
                        );
                        lilEditorGUI.LocalizedPropertyAlpha(emission3rdColor);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, emission3rdMainStrength);
                        lilEditorGUI.DrawLine();
                        lilEditorGUI.TextureGUI(
                            m_MaterialEditor,
                            false,
                            ref ltmedSet.isShowEmission3rdBlendMask,
                            maskBlendRGBAContent,
                            emission3rdBlendMask,
                            emission3rdBlend,
                            emission3rdBlendMask_ScrollRotate,
                            true,
                            true
                        );
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, emission3rdBlendMode);
                        lilEditorGUI.DrawLine();
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, emission3rdBlink);
                        lilEditorGUI.DrawLine();
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, emission3rdParallaxDepth);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, emission3rdFluorescence);
                        lilEditorGUI.DrawLine();
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, audioLink2Emission3rd);
                        lilEditorGUI.DrawLine();
                        if (GUILayout.Button("Copy Emission3rd"))
                        {
                            CopyCategory(emission3rdCategory, material);
                        }
                        lilEditorGUI.DrawLine();
                        if (GUILayout.Button("Paste Emission3rd"))
                        {
                            PasteCategory(emission3rdCategory, material);
                        }
                        lilEditorGUI.DrawLine();
                        if (GUILayout.Button("Reset Emission3rd"))
                        {
                            if (
                                EditorUtility.DisplayDialog(
                                    "Reset Confirmation",
                                    "Emission3rd will be reset to their default values. \nAre you sure?",
                                    "Reset",
                                    "Cancel"
                                )
                            )
                            {
                                ResetCategory(emission3rdCategory, material);
                            }
                        }
                        EditorGUILayout.EndVertical();
                    }
                    EditorGUILayout.EndVertical();
                }
            }

            ltmedSet.isShowNormal = lilEditorGUI.Foldout(
                GetLoc("sNormalMapSetting"),
                ltmedSet.isShowNormal
            );
            if (ltmedSet.isShowNormal)
            {
                if (useBump2ndMap.floatValue == 0)
                {
                    GUILayout.Label(
                        GetLoc("If you use NormalMap 3rd, you must use NormalMap 2nd"),
                        wrapLabel
                    );
                    useBump3rdMap.floatValue = 0;
                }
                else
                {
                    EditorGUILayout.BeginVertical(boxOuter);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useBump3rdMap, false);
                    if (useBump3rdMap.floatValue == 1)
                    {
                        EditorGUILayout.BeginVertical(boxInnerHalf);
                        lilEditorGUI.TextureGUI(
                            m_MaterialEditor,
                            false,
                            ref ltmedSet.isShowBump3rdMap,
                            normalMapContent,
                            bump3rdMap,
                            bump3rdScale,
                            bump3rdMap_UVMode,
                            "UV Mode|UV0|UV1|UV2|UV3"
                        );
                        lilEditorGUI.DrawLine();
                        lilEditorGUI.TextureGUI(
                            m_MaterialEditor,
                            false,
                            ref ltmedSet.isShowBump3rdScaleMask,
                            maskStrengthContent,
                            bump3rdScaleMask
                        );
                        lilEditorGUI.DrawLine();
                        if (GUILayout.Button("Copy NormalMap 3rd"))
                        {
                            CopyCategory(bump3rdMapCategory, material);
                        }
                        lilEditorGUI.DrawLine();
                        if (GUILayout.Button("Paste NormalMap 3rd"))
                        {
                            PasteCategory(bump3rdMapCategory, material);
                        }
                        lilEditorGUI.DrawLine();
                        if (GUILayout.Button("Reset NormalMap 3rd"))
                        {
                            if (
                                EditorUtility.DisplayDialog(
                                    "Reset Confirmation",
                                    "NormalMap 3rd will be reset to their default values. \nAre you sure?",
                                    "Reset",
                                    "Cancel"
                                )
                            )
                            {
                                ResetCategory(bump3rdMapCategory, material);
                            }
                        }
                        EditorGUILayout.EndVertical();
                    }
                    EditorGUILayout.EndVertical();
                }
            }

            ltmedSet.isShowMatCap = lilEditorGUI.Foldout(
                GetLoc("sMatCapSetting"),
                ltmedSet.isShowMatCap
            );
            if (ltmedSet.isShowMatCap)
            {
                EditorGUILayout.BeginVertical(boxOuter);
                lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMatCap3rd, false);
                if (useMatCap3rd.floatValue == 1)
                {
                    EditorGUILayout.BeginVertical(boxInnerHalf);
                    lilEditorGUI.MatCapTextureGUI(
                        m_MaterialEditor,
                        false,
                        ref ltmedSet.isShowMatCap3rdUV,
                        matcapContent,
                        matCap3rdTex,
                        matCap3rdColor,
                        matCap3rdBlendUV1,
                        matCap3rdZRotCancel,
                        matCap3rdPerspective,
                        matCap3rdVRParallaxStrength
                    );
                    lilEditorGUI.LocalizedPropertyAlpha(matCap3rdColor);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap3rdMainStrength);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap3rdNormalStrength);
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.TextureGUI(
                        m_MaterialEditor,
                        false,
                        ref ltmedSet.isShowMatCap3rdBlendMask,
                        maskBlendRGBContent,
                        matCap3rdBlendMask,
                        matCap3rdBlend
                    );
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap3rdEnableLighting);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap3rdShadowMask);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap3rdBackfaceMask);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap3rdLod);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap3rdBlendMode);
                    if (
                        matCap3rdEnableLighting.floatValue != 0.0f
                        && matCap3rdBlendMode.floatValue == 3.0f
                        && lilEditorGUI.AutoFixHelpBox(GetLoc("sHelpMatCap3rdBlending"))
                    )
                    {
                        matCap3rdEnableLighting.floatValue = 0.0f;
                    }
                    if (isTransparent)
                        lilEditorGUI.LocalizedProperty(
                            m_MaterialEditor,
                            matCap3rdApplyTransparency
                        );
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, anisotropy2MatCap3rd);
                    lilEditorGUI.DrawLine();
                    if (GUILayout.Button("Copy MatCap 3rd"))
                    {
                        CopyCategory(matCap3rdCategory, material);
                    }
                    lilEditorGUI.DrawLine();
                    if (GUILayout.Button("Paste MatCap 3rd"))
                    {
                        PasteCategory(matCap3rdCategory, material);
                    }
                    lilEditorGUI.DrawLine();
                    if (GUILayout.Button("Reset MatCap 3rd"))
                    {
                        if (
                            EditorUtility.DisplayDialog(
                                "Reset Confirmation",
                                "MatCap 3rd will be reset to their default values. \nAre you sure?",
                                "Reset",
                                "Cancel"
                            )
                        )
                        {
                            ResetCategory(matCap3rdCategory, material);
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndVertical();

                if (renderingModeBuf != RenderingMode.Opaque)
                {
                    GUILayout.Label(
                        GetLoc("When using MatCap 4th, the rendering mode must be opaque"),
                        wrapLabel
                    );
                }
                else
                {
                    EditorGUILayout.BeginVertical(boxOuter);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMatCap4th, false);
                    if (useMatCap4th.floatValue == 1)
                    {
                        EditorGUILayout.BeginVertical(boxInnerHalf);
                        lilEditorGUI.MatCapTextureGUI(
                            m_MaterialEditor,
                            false,
                            ref ltmedSet.isShowMatCap4thUV,
                            matcapContent,
                            matCap4thTex,
                            matCap4thColor,
                            matCap4thBlendUV1,
                            matCap4thZRotCancel,
                            matCap4thPerspective,
                            matCap4thVRParallaxStrength
                        );
                        lilEditorGUI.LocalizedPropertyAlpha(matCap4thColor);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap4thMainStrength);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap4thNormalStrength);
                        lilEditorGUI.DrawLine();
                        lilEditorGUI.TextureGUI(
                            m_MaterialEditor,
                            false,
                            ref ltmedSet.isShowMatCap4thBlendMask,
                            maskBlendRGBContent,
                            matCap4thBlendMask,
                            matCap4thBlend
                        );
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap4thEnableLighting);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap4thShadowMask);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap4thBackfaceMask);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap4thLod);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap4thBlendMode);
                        if (
                            matCap4thEnableLighting.floatValue != 0.0f
                            && matCap4thBlendMode.floatValue == 3.0f
                            && lilEditorGUI.AutoFixHelpBox(GetLoc("sHelpMatCap4thBlending"))
                        )
                        {
                            matCap4thEnableLighting.floatValue = 0.0f;
                        }
                        if (isTransparent)
                            lilEditorGUI.LocalizedProperty(
                                m_MaterialEditor,
                                matCap4thApplyTransparency
                            );
                        lilEditorGUI.DrawLine();
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, anisotropy2MatCap4th);
                        lilEditorGUI.DrawLine();
                        if (GUILayout.Button("Copy MatCap 4th"))
                        {
                            CopyCategory(matCap4thCategory, material);
                        }
                        lilEditorGUI.DrawLine();
                        if (GUILayout.Button("Paste MatCap 4th"))
                        {
                            PasteCategory(matCap4thCategory, material);
                        }
                        lilEditorGUI.DrawLine();
                        if (GUILayout.Button("Reset MatCap 4th"))
                        {
                            if (
                                EditorUtility.DisplayDialog(
                                    "Reset Confirmation",
                                    "MatCap 4th will be reset to their default values. \nAre you sure?",
                                    "Reset",
                                    "Cancel"
                                )
                            )
                            {
                                ResetCategory(matCap4thCategory, material);
                            }
                        }
                        EditorGUILayout.EndVertical();
                    }
                    EditorGUILayout.EndVertical();
                }
            }

            ltmedSet.isShowGlitter = lilEditorGUI.Foldout(
                GetLoc("sGlitterSetting"),
                ltmedSet.isShowGlitter
            );
            if (ltmedSet.isShowGlitter)
            {
                if (useGlitter.floatValue == 0)
                {
                    GUILayout.Label(
                        GetLoc("If you use Glitter 3rd, you must use Glitter 2nd"),
                        wrapLabel
                    );
                    useGlitter2nd.floatValue = 0;
                }
                else
                {
                    EditorGUILayout.BeginVertical(boxOuter);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useGlitter2nd);
                    if (useGlitter2nd.floatValue == 1)
                    {
                        EditorGUILayout.BeginVertical(boxInnerHalf);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndUVMode);
                        lilEditorGUI.TextureGUI(
                            m_MaterialEditor,
                            false,
                            ref ltmedSet.isShowGlitter2ndColorTex,
                            colorMaskRGBAContent,
                            glitter2ndColorTex,
                            glitter2ndColor,
                            glitter2ndColorTex_UVMode,
                            "UV Mode|UV0|UV1|UV2|UV3"
                        );
                        EditorGUI.indentLevel++;
                        lilEditorGUI.LocalizedPropertyAlpha(glitter2ndColor);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndMainStrength);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndEnableLighting);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndShadowMask);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndBackfaceMask);
                        if (isTransparent)
                            lilEditorGUI.LocalizedProperty(
                                m_MaterialEditor,
                                glitter2ndApplyTransparency
                            );
                        EditorGUI.indentLevel--;
                        lilEditorGUI.DrawLine();
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndApplyShape);
                        if (glitter2ndApplyShape.floatValue > 0.5f)
                        {
                            EditorGUI.indentLevel++;
                            lilEditorGUI.TextureGUI(
                                m_MaterialEditor,
                                false,
                                ref ltmedSet.isShowGlitter2ndShapeTex,
                                customMaskContent,
                                glitter2ndShapeTex
                            );
                            lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndAtras);
                            lilEditorGUI.LocalizedProperty(
                                m_MaterialEditor,
                                glitter2ndAngleRandomize
                            );
                            EditorGUI.indentLevel--;
                        }
                        lilEditorGUI.DrawLine();

                        // Param1
                        var scale = new Vector2(
                            256.0f / glitter2ndParams1.vectorValue.x,
                            256.0f / glitter2ndParams1.vectorValue.y
                        );
                        float size =
                            glitter2ndParams1.vectorValue.z == 0.0f
                                ? 0.0f
                                : Mathf.Sqrt(glitter2ndParams1.vectorValue.z);
                        float density = Mathf.Sqrt(1.0f / glitter2ndParams1.vectorValue.w) / 1.5f;
                        float sensitivity = lilEditorGUI.RoundFloat1000000(
                            glitter2ndSensitivity.floatValue / density
                        );
                        density = lilEditorGUI.RoundFloat1000000(density);
                        EditorGUIUtility.wideMode = true;

                        EditorGUI.BeginChangeCheck();
                        EditorGUI.showMixedValue =
                            glitter2ndParams1.hasMixedValue || glitter2ndSensitivity.hasMixedValue;
                        scale = lilEditorGUI.Vector2Field(
                            Event.current.alt ? glitter2ndParams1.name + ".xy" : GetLoc("sScale"),
                            scale
                        );
                        size = lilEditorGUI.Slider(
                            Event.current.alt
                                ? glitter2ndParams1.name + ".z"
                                : GetLoc("sParticleSize"),
                            size,
                            0.0f,
                            2.0f
                        );
                        EditorGUI.showMixedValue = false;

                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndScaleRandomize);

                        EditorGUI.showMixedValue =
                            glitter2ndParams1.hasMixedValue || glitter2ndSensitivity.hasMixedValue;
                        density = lilEditorGUI.Slider(
                            Event.current.alt ? glitter2ndParams1.name + ".w" : GetLoc("sDensity"),
                            density,
                            0.001f,
                            1.0f
                        );
                        sensitivity = lilEditorGUI.FloatField(
                            Event.current.alt ? glitter2ndSensitivity.name : GetLoc("sSensitivity"),
                            sensitivity
                        );
                        EditorGUI.showMixedValue = false;

                        if (EditorGUI.EndChangeCheck())
                        {
                            scale.x = Mathf.Max(scale.x, 0.0000001f);
                            scale.y = Mathf.Max(scale.y, 0.0000001f);
                            glitter2ndParams1.vectorValue = new Vector4(
                                256.0f / scale.x,
                                256.0f / scale.y,
                                size * size,
                                1.0f / (density * density * 1.5f * 1.5f)
                            );
                            glitter2ndSensitivity.floatValue = Mathf.Max(
                                sensitivity * density,
                                0.25f
                            );
                        }

                        // Other
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndParams2);
                        lilEditorGUI.LocalizedProperty(
                            m_MaterialEditor,
                            glitter2ndVRParallaxStrength
                        );
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndNormalStrength);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndPostContrast);
                        lilEditorGUI.DrawLine();
                        if (GUILayout.Button("Copy Glitter 2nd"))
                        {
                            CopyCategory(glitter2ndCategory, material);
                        }
                        lilEditorGUI.DrawLine();
                        if (GUILayout.Button("Paste Glitter 2nd"))
                        {
                            PasteCategory(glitter2ndCategory, material);
                        }
                        lilEditorGUI.DrawLine();
                        if (GUILayout.Button("Reset Glitter 2nd"))
                        {
                            if (
                                EditorUtility.DisplayDialog(
                                    "Reset Confirmation",
                                    "Glitter 2nd will be reset to their default values. \nAre you sure?",
                                    "Reset",
                                    "Cancel"
                                )
                            )
                            {
                                ResetCategory(glitter2ndCategory, material);
                            }
                        }
                        EditorGUILayout.EndVertical();
                    }
                    EditorGUILayout.EndVertical();
                }
            }

            ltmedSet.isShowWarp = Foldout(
                "UVワープ / UV Warp",
                "UVワープ / UV Warp",
                ltmedSet.isShowWarp
            );
            if (ltmedSet.isShowWarp)
            {
                EditorGUILayout.BeginVertical(boxOuter);
                lilEditorGUI.LocalizedProperty(m_MaterialEditor, useWarp, false);
                if (useWarp.floatValue == 1)
                {
                    EditorGUILayout.BeginVertical(boxInnerHalf);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, warpAnimSpeed);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, warpIntensity);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, warpBigAmp);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, warpBigFreqX);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, warpBigFreqY);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, warpBigSpeedX);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, warpBigSpeedY);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, warpSmallAmp);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, warpSmallFreqX);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, warpSmallFreqY);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, warpSmallSpeedX);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, warpSmallSpeedY);
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useWarpUVMain);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useWarpUV0);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useWarpUV1);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useWarpUV2);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useWarpUV3);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useWarpUVMat);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useWarpUVRim);
                    GUILayout.Label(
                        "Notice\nMainUV and UV 0 warp almost everything.\nIf you want to warp only each main color, use the main color warping below."
                    );
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useWarpMain1st);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useWarpMain2nd);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useWarpMain3rd);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useWarpMain4th);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useWarpMain5th);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useWarpMain6th);
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, warpReplaceRefract);
                    GUILayout.Label(
                        "Warning\nThis replaces refraction\nPerformance improvements over built-in refraction\nOnly works in refraction mode"
                    );
                    lilEditorGUI.DrawLine();
                    if (GUILayout.Button("Copy Warp"))
                    {
                        CopyCategory(warpCategory, material);
                    }
                    lilEditorGUI.DrawLine();
                    if (GUILayout.Button("Paste Warp"))
                    {
                        PasteCategory(warpCategory, material);
                    }
                    lilEditorGUI.DrawLine();
                    if (GUILayout.Button("Reset Warp"))
                    {
                        if (
                            EditorUtility.DisplayDialog(
                                "Reset Confirmation",
                                "Warp will be reset to their default values. \nAre you sure?",
                                "Reset",
                                "Cancel"
                            )
                        )
                        {
                            ResetCategory(warpCategory, material);
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndVertical();
            }

            ltmedSet.isShowMole = Foldout("ほくろ / Mole", "ほくろ / Mole", ltmedSet.isShowMole);
            if (ltmedSet.isShowMole)
            {
                EditorGUILayout.BeginVertical(boxOuter);
                lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMole, false);
                if (useMole.floatValue == 1)
                {
                    EditorGUILayout.BeginVertical(boxInnerHalf);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, moleColor);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, moleBlendMode);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, moleAspectFix);
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMole1st);
                    if (useMole1st.floatValue == 1)
                    {
                        EditorGUI.indentLevel++;
                        Vector2 _1stPos = new Vector2(
                            mole1stPos.vectorValue.x,
                            mole1stPos.vectorValue.y
                        );
                        EditorGUI.BeginChangeCheck();
                        _1stPos.x = lilEditorGUI.Slider(
                            mole1stPos.displayName + " X",
                            _1stPos.x,
                            0,
                            1
                        );
                        _1stPos.y = lilEditorGUI.Slider(
                            mole1stPos.displayName + " Y",
                            _1stPos.y,
                            0,
                            1
                        );
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole1stRadius);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole1stRadiusMultiplier);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole1stBlur);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole1stShape);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole1stRotation);
                        if (EditorGUI.EndChangeCheck())
                        {
                            mole1stPos.vectorValue = _1stPos;
                        }
                        EditorGUI.indentLevel--;
                    }
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMole2nd);
                    if (useMole2nd.floatValue == 1)
                    {
                        EditorGUI.indentLevel++;
                        Vector2 _2ndPos = new Vector2(
                            mole2ndPos.vectorValue.x,
                            mole2ndPos.vectorValue.y
                        );
                        EditorGUI.BeginChangeCheck();
                        _2ndPos.x = lilEditorGUI.Slider(
                            mole2ndPos.displayName + " X",
                            _2ndPos.x,
                            0,
                            1
                        );
                        _2ndPos.y = lilEditorGUI.Slider(
                            mole2ndPos.displayName + " Y",
                            _2ndPos.y,
                            0,
                            1
                        );
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole2ndRadius);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole2ndRadiusMultiplier);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole2ndBlur);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole2ndShape);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole2ndRotation);
                        if (EditorGUI.EndChangeCheck())
                        {
                            mole2ndPos.vectorValue = _2ndPos;
                        }
                        EditorGUI.indentLevel--;
                    }
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMole3rd);
                    if (useMole3rd.floatValue == 1)
                    {
                        EditorGUI.indentLevel++;
                        Vector2 _3rdPos = new Vector2(
                            mole3rdPos.vectorValue.x,
                            mole3rdPos.vectorValue.y
                        );
                        EditorGUI.BeginChangeCheck();
                        _3rdPos.x = lilEditorGUI.Slider(
                            mole3rdPos.displayName + " X",
                            _3rdPos.x,
                            0,
                            1
                        );
                        _3rdPos.y = lilEditorGUI.Slider(
                            mole3rdPos.displayName + " Y",
                            _3rdPos.y,
                            0,
                            1
                        );
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole3rdRadius);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole3rdRadiusMultiplier);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole3rdBlur);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole3rdShape);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole3rdRotation);
                        if (EditorGUI.EndChangeCheck())
                        {
                            mole3rdPos.vectorValue = _3rdPos;
                        }
                        EditorGUI.indentLevel--;
                    }
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMole4th);
                    if (useMole4th.floatValue == 1)
                    {
                        EditorGUI.indentLevel++;
                        Vector2 _4thPos = new Vector2(
                            mole4thPos.vectorValue.x,
                            mole4thPos.vectorValue.y
                        );
                        EditorGUI.BeginChangeCheck();
                        _4thPos.x = lilEditorGUI.Slider(
                            mole4thPos.displayName + " X",
                            _4thPos.x,
                            0,
                            1
                        );
                        _4thPos.y = lilEditorGUI.Slider(
                            mole4thPos.displayName + " Y",
                            _4thPos.y,
                            0,
                            1
                        );
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole4thRadius);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole4thRadiusMultiplier);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole4thBlur);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole4thShape);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole4thRotation);
                        if (EditorGUI.EndChangeCheck())
                        {
                            mole4thPos.vectorValue = _4thPos;
                        }
                        EditorGUI.indentLevel--;
                    }
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMole5th);
                    if (useMole5th.floatValue == 1)
                    {
                        EditorGUI.indentLevel++;
                        Vector2 _5thPos = new Vector2(
                            mole5thPos.vectorValue.x,
                            mole5thPos.vectorValue.y
                        );
                        EditorGUI.BeginChangeCheck();
                        _5thPos.x = lilEditorGUI.Slider(
                            mole5thPos.displayName + " X",
                            _5thPos.x,
                            0,
                            1
                        );
                        _5thPos.y = lilEditorGUI.Slider(
                            mole5thPos.displayName + " Y",
                            _5thPos.y,
                            0,
                            1
                        );
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole5thRadius);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole5thRadiusMultiplier);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole5thBlur);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole5thShape);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole5thRotation);
                        if (EditorGUI.EndChangeCheck())
                        {
                            mole5thPos.vectorValue = _5thPos;
                        }
                        EditorGUI.indentLevel--;
                    }
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMole6th);
                    if (useMole6th.floatValue == 1)
                    {
                        EditorGUI.indentLevel++;
                        Vector2 _6thPos = new Vector2(
                            mole6thPos.vectorValue.x,
                            mole6thPos.vectorValue.y
                        );
                        EditorGUI.BeginChangeCheck();
                        _6thPos.x = lilEditorGUI.Slider(
                            mole6thPos.displayName + " X",
                            _6thPos.x,
                            0,
                            1
                        );
                        _6thPos.y = lilEditorGUI.Slider(
                            mole6thPos.displayName + " Y",
                            _6thPos.y,
                            0,
                            1
                        );
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole6thRadius);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole6thRadiusMultiplier);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole6thBlur);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole6thShape);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole6thRotation);
                        if (EditorGUI.EndChangeCheck())
                        {
                            mole6thPos.vectorValue = _6thPos;
                        }
                        EditorGUI.indentLevel--;
                    }
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMole7th);
                    if (useMole7th.floatValue == 1)
                    {
                        EditorGUI.indentLevel++;
                        Vector2 _7thPos = new Vector2(
                            mole7thPos.vectorValue.x,
                            mole7thPos.vectorValue.y
                        );
                        EditorGUI.BeginChangeCheck();
                        _7thPos.x = lilEditorGUI.Slider(
                            mole7thPos.displayName + " X",
                            _7thPos.x,
                            0,
                            1
                        );
                        _7thPos.y = lilEditorGUI.Slider(
                            mole7thPos.displayName + " Y",
                            _7thPos.y,
                            0,
                            1
                        );
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole7thRadius);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole7thRadiusMultiplier);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole7thBlur);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole7thShape);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole7thRotation);
                        if (EditorGUI.EndChangeCheck())
                        {
                            mole7thPos.vectorValue = _7thPos;
                        }
                        EditorGUI.indentLevel--;
                    }
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMole8th);
                    if (useMole8th.floatValue == 1)
                    {
                        EditorGUI.indentLevel++;
                        Vector2 _8thPos = new Vector2(
                            mole8thPos.vectorValue.x,
                            mole8thPos.vectorValue.y
                        );
                        EditorGUI.BeginChangeCheck();
                        _8thPos.x = lilEditorGUI.Slider(
                            mole8thPos.displayName + " X",
                            _8thPos.x,
                            0,
                            1
                        );
                        _8thPos.y = lilEditorGUI.Slider(
                            mole8thPos.displayName + " Y",
                            _8thPos.y,
                            0,
                            1
                        );
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole8thRadius);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole8thRadiusMultiplier);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole8thBlur);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole8thShape);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole8thRotation);
                        if (EditorGUI.EndChangeCheck())
                        {
                            mole8thPos.vectorValue = _8thPos;
                        }
                        EditorGUI.indentLevel--;
                    }
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMole9th);
                    if (useMole9th.floatValue == 1)
                    {
                        EditorGUI.indentLevel++;
                        Vector2 _9thPos = new Vector2(
                            mole9thPos.vectorValue.x,
                            mole9thPos.vectorValue.y
                        );
                        EditorGUI.BeginChangeCheck();
                        _9thPos.x = lilEditorGUI.Slider(
                            mole9thPos.displayName + " X",
                            _9thPos.x,
                            0,
                            1
                        );
                        _9thPos.y = lilEditorGUI.Slider(
                            mole9thPos.displayName + " Y",
                            _9thPos.y,
                            0,
                            1
                        );
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole9thRadius);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole9thRadiusMultiplier);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole9thBlur);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole9thShape);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole9thRotation);
                        if (EditorGUI.EndChangeCheck())
                        {
                            mole9thPos.vectorValue = _9thPos;
                        }
                        EditorGUI.indentLevel--;
                    }
                    lilEditorGUI.DrawLine();
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMole10th);
                    if (useMole10th.floatValue == 1)
                    {
                        EditorGUI.indentLevel++;
                        Vector2 _10thPos = new Vector2(
                            mole10thPos.vectorValue.x,
                            mole10thPos.vectorValue.y
                        );
                        EditorGUI.BeginChangeCheck();
                        _10thPos.x = lilEditorGUI.Slider(
                            mole10thPos.displayName + " X",
                            _10thPos.x,
                            0,
                            1
                        );
                        _10thPos.y = lilEditorGUI.Slider(
                            mole10thPos.displayName + " Y",
                            _10thPos.y,
                            0,
                            1
                        );
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole10thRadius);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole10thRadiusMultiplier);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole10thBlur);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole10thShape);
                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, mole10thRotation);
                        if (EditorGUI.EndChangeCheck())
                        {
                            mole10thPos.vectorValue = _10thPos;
                        }
                        EditorGUI.indentLevel--;
                    }
                    lilEditorGUI.DrawLine();
                    if (GUILayout.Button("Copy Mole"))
                    {
                        CopyCategory(moleCategory, material);
                    }
                    lilEditorGUI.DrawLine();
                    if (GUILayout.Button("Paste Mole"))
                    {
                        PasteCategory(moleCategory, material);
                    }
                    lilEditorGUI.DrawLine();
                    if (GUILayout.Button("Reset Mole"))
                    {
                        if (
                            EditorUtility.DisplayDialog(
                                "Reset Confirmation",
                                "Mole will be reset to their default values. \nAre you sure?",
                                "Reset",
                                "Cancel"
                            )
                        )
                        {
                            ResetCategory(moleCategory, material);
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndVertical();
            }
            ltmedSet.isShowRefraction = Foldout(
                "屈折設定 / Refraction Setting",
                "屈折設定 / Refraction Setting",
                ltmedSet.isShowRefraction
            );
            if (ltmedSet.isShowRefraction)
            {
                EditorGUILayout.BeginVertical(boxOuter);
                lilEditorGUI.LocalizedProperty(m_MaterialEditor, refractionType);
                lilEditorGUI.LocalizedProperty(m_MaterialEditor, refractionSGMBQuality);
                lilEditorGUI.DrawLine();
                if (GUILayout.Button("Copy Refraction Setting"))
                {
                    CopyCategory(refractionCategory, material);
                }
                lilEditorGUI.DrawLine();
                if (GUILayout.Button("Paste Refraction Setting"))
                {
                    PasteCategory(refractionCategory, material);
                }
                lilEditorGUI.DrawLine();
                if (GUILayout.Button("Reset Refraction Setting"))
                {
                    if (
                        EditorUtility.DisplayDialog(
                            "Reset Confirmation",
                            "Refraction Setting will be reset to their default values. \nAre you sure?",
                            "Reset",
                            "Cancel"
                        )
                    )
                    {
                        ResetCategory(refractionCategory, material);
                    }
                }
                EditorGUILayout.EndVertical();
            }
        }

        /// <summary>
        /// Replace to Custom Shaders
        /// </summary>
        protected override void ReplaceToCustomShaders()
        {
            lts = Shader.Find(shaderName + "/lilToon");
            ltsc = Shader.Find("Hidden/" + shaderName + "/Cutout");
            ltst = Shader.Find("Hidden/" + shaderName + "/Transparent");
            ltsot = Shader.Find("Hidden/" + shaderName + "/OnePassTransparent");
            ltstt = Shader.Find("Hidden/" + shaderName + "/TwoPassTransparent");

            ltso = Shader.Find("Hidden/" + shaderName + "/OpaqueOutline");
            ltsco = Shader.Find("Hidden/" + shaderName + "/CutoutOutline");
            ltsto = Shader.Find("Hidden/" + shaderName + "/TransparentOutline");
            ltsoto = Shader.Find("Hidden/" + shaderName + "/OnePassTransparentOutline");
            ltstto = Shader.Find("Hidden/" + shaderName + "/TwoPassTransparentOutline");

            ltsoo = Shader.Find(shaderName + "/[Optional] OutlineOnly/Opaque");
            ltscoo = Shader.Find(shaderName + "/[Optional] OutlineOnly/Cutout");
            ltstoo = Shader.Find(shaderName + "/[Optional] OutlineOnly/Transparent");

            ltstess = Shader.Find("Hidden/" + shaderName + "/Tessellation/Opaque");
            ltstessc = Shader.Find("Hidden/" + shaderName + "/Tessellation/Cutout");
            ltstesst = Shader.Find("Hidden/" + shaderName + "/Tessellation/Transparent");
            ltstessot = Shader.Find("Hidden/" + shaderName + "/Tessellation/OnePassTransparent");
            ltstesstt = Shader.Find("Hidden/" + shaderName + "/Tessellation/TwoPassTransparent");

            ltstesso = Shader.Find("Hidden/" + shaderName + "/Tessellation/OpaqueOutline");
            ltstessco = Shader.Find("Hidden/" + shaderName + "/Tessellation/CutoutOutline");
            ltstessto = Shader.Find("Hidden/" + shaderName + "/Tessellation/TransparentOutline");
            ltstessoto = Shader.Find(
                "Hidden/" + shaderName + "/Tessellation/OnePassTransparentOutline"
            );
            ltstesstto = Shader.Find(
                "Hidden/" + shaderName + "/Tessellation/TwoPassTransparentOutline"
            );

            ltsl = Shader.Find(shaderName + "/lilToonLite");
            ltslc = Shader.Find("Hidden/" + shaderName + "/Lite/Cutout");
            ltslt = Shader.Find("Hidden/" + shaderName + "/Lite/Transparent");
            ltslot = Shader.Find("Hidden/" + shaderName + "/Lite/OnePassTransparent");
            ltsltt = Shader.Find("Hidden/" + shaderName + "/Lite/TwoPassTransparent");

            ltslo = Shader.Find("Hidden/" + shaderName + "/Lite/OpaqueOutline");
            ltslco = Shader.Find("Hidden/" + shaderName + "/Lite/CutoutOutline");
            ltslto = Shader.Find("Hidden/" + shaderName + "/Lite/TransparentOutline");
            ltsloto = Shader.Find("Hidden/" + shaderName + "/Lite/OnePassTransparentOutline");
            ltsltto = Shader.Find("Hidden/" + shaderName + "/Lite/TwoPassTransparentOutline");

            ltsref = Shader.Find("Hidden/" + shaderName + "/Refraction");
            ltsrefb = Shader.Find("Hidden/" + shaderName + "/RefractionBlur");
            ltsfur = Shader.Find("Hidden/" + shaderName + "/Fur");
            ltsfurc = Shader.Find("Hidden/" + shaderName + "/FurCutout");
            ltsfurtwo = Shader.Find("Hidden/" + shaderName + "/FurTwoPass");
            ltsfuro = Shader.Find(shaderName + "/[Optional] FurOnly/Transparent");
            ltsfuroc = Shader.Find(shaderName + "/[Optional] FurOnly/Cutout");
            ltsfurotwo = Shader.Find(shaderName + "/[Optional] FurOnly/TwoPass");
            ltsgem = Shader.Find("Hidden/" + shaderName + "/Gem");
            ltsfs = Shader.Find(shaderName + "/[Optional] FakeShadow");

            ltsover = Shader.Find(shaderName + "/[Optional] Overlay");
            ltsoover = Shader.Find(shaderName + "/[Optional] OverlayOnePass");
            ltslover = Shader.Find(shaderName + "/[Optional] LiteOverlay");
            ltsloover = Shader.Find(shaderName + "/[Optional] LiteOverlayOnePass");

            ltsm = Shader.Find(shaderName + "/lilToonMulti");
            ltsmo = Shader.Find("Hidden/" + shaderName + "/MultiOutline");
            ltsmref = Shader.Find("Hidden/" + shaderName + "/MultiRefraction");
            ltsmfur = Shader.Find("Hidden/" + shaderName + "/MultiFur");
            ltsmgem = Shader.Find("Hidden/" + shaderName + "/MultiGem");
        }

        /// <summary>
        /// Editor Settings Singleton
        /// </summary>
        internal class lilToonMoreEditorSetting : ScriptableSingleton<lilToonMoreEditorSetting>
        {
            internal bool isShowBump3rdMap = false;
            internal bool isShowBump3rdScaleMask = false;
            internal bool isShowMatCap3rdUV = false;
            internal bool isShowMatCap3rdBlendMask = false;
            internal bool isShowMatCap3rdBumpMap = false;
            internal bool isShowMatCap4thUV = false;
            internal bool isShowMatCap4thBlendMask = false;
            internal bool isShowMatCap4thBumpMap = false;
            internal bool isShowGlitter2ndColorTex = false;
            internal bool isShowGlitter2ndShapeTex = false;
            internal bool isShowEmission3rdMap = false;
            internal bool isShowEmission3rdBlendMask = false;
            internal bool isShowMain = false;
            internal bool isShowEmission = false;
            internal bool isShowNormal = false;
            internal bool isShowMatCap = false;
            internal bool isShowGlitter = false;
            internal bool isShowWarp = false;
            internal bool isShowMole = false;
            internal bool isLightBasedAlphaMaskAdvanced = false;
            internal bool isAlphaMaskModeAdvanced = false;
            internal bool isShowRefraction = false;
        }
    }
}

#endif
