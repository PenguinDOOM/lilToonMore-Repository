#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

using Object = UnityEngine.Object;

namespace lilToon
{
    public class lilToonMoreInspector : lilToonInspector
    {
        // Custom properties
        //private static bool isShowCustomProperties;
        private const string shaderName = "lilToonMore";
        private static bool isShowCustomProperties;
        public static lilToonMoreEditorSetting ltmedSet { get { return lilToonMoreEditorSetting.instance; } }
        

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
            "_AudioLink2Main5th"
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
            "_AudioLink2Main6th"
        };
        
        readonly string[] bump3rdMapCategory = new string[]
        {
            "_Bump3rdMap",
            "_Bump3rdMap_UVMode",
            "_Bump3rdScale",
            "_Bump3rdScaleMask"
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
            "_MatCap3rdCustomNormal",
            "_MatCap3rdBumpMap",
            "_MatCap3rdBumpScale",
            "_Anisotropy2MatCap3rd"
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
            "_MatCap4thCustomNormal",
            "_MatCap4thBumpMap",
            "_MatCap4thBumpScale",
            "_Anisotropy2MatCap4th"
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
            "_Glitter2ndVRParallaxStrength"
        };


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
        private MaterialProperty matCap3rdCustomNormal;
        private MaterialProperty matCap3rdBumpMap;
        private MaterialProperty matCap3rdBumpScale;
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
        private MaterialProperty matCap4thCustomNormal;
        private MaterialProperty matCap4thBumpMap;
        private MaterialProperty matCap4thBumpScale;
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
        
        // ▼ コピー／ペースト用バッファ
        Dictionary<string, object> copyBuffer = new Dictionary<string, object>();
        
        void CopyCategory(string[] props, Material material)
        {
            copyBuffer.Clear();

            foreach (var prop in props)
            {
                if (!material.HasProperty(prop)) continue;
                int idx = material.shader.FindPropertyIndex(prop);
                if (idx < 0) continue;

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
                "Properties copied.",
                "OK"
            );
        }

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
        
        void PasteCategory(string[] props, Material material)
        {
            if (copyBuffer.Count == 0)
            {
                EditorUtility.DisplayDialog("Paste Custom Property", "Please copy it first.", "OK");
                return;
            }

            if (!EditorUtility.DisplayDialog(
                "Paste Confirmation",
                "Overwrite with the copied content.\n\nよろしいですか？",
                "Paste",
                "Cancel"))
            {
                return;
            }

            Undo.RecordObject(material, "Paste Custom Property");

            foreach (var prop in props)
            {
                if (!material.HasProperty(prop)) continue;
                if (!copyBuffer.ContainsKey(prop)) continue;

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
            }

            EditorUtility.SetDirty(material);
        }


        
        void ResetCategory(string[] props, Material material)
        {
            // Undo 対応
            Undo.RecordObject(material, "Reset Custom Property");

            // Shader デフォルト値を取得
            Material defaultMat = material.shader != null
                ? new Material(material.shader)
                : null;

            if (defaultMat == null) return;

            foreach (var prop in props)
            {
                if (!material.HasProperty(prop)) continue;
                int idx = material.shader.FindPropertyIndex(prop);
                if (idx < 0) continue;

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

            // Editor の Inspector を更新させる
            EditorUtility.SetDirty(material);
            UnityEngine.Object.DestroyImmediate(defaultMat);
        }


        
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
            matCap3rdCustomNormal = FindProperty("_MatCap3rdCustomNormal", props);
            matCap3rdBumpMap = FindProperty("_MatCap3rdBumpMap", props);
            matCap3rdBumpScale = FindProperty("_MatCap3rdBumpScale", props);
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
            matCap4thCustomNormal = FindProperty("_MatCap4thCustomNormal", props);
            matCap4thBumpMap = FindProperty("_MatCap4thBumpMap", props);
            matCap4thBumpScale = FindProperty("_MatCap4thBumpScale", props);
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
        }

        protected override void DrawCustomProperties(Material material)
        {
            // GUIStyles Name   Description
            // ---------------- ------------------------------------
            // boxOuter         outer box
            // boxInnerHalf     inner box
            // boxInner         inner box without label
            // customBox        box (similar to unity default box)
            // customToggleFont label for box

            isShowCustomProperties = Foldout("lilToonMore Settings", "lilToonMore Settings", isShowCustomProperties);
            if(isShowCustomProperties)
            {
                
                EditorGUILayout.BeginVertical(boxOuter);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMain4thTex, false);
                    if(useMain4thTex.floatValue == 1)
                    {
                        EditorGUILayout.BeginVertical(boxInnerHalf);
                            lilEditorGUI.LocalizedPropertyTexture(m_MaterialEditor, colorRGBAContent, main4thTex, color4th);
                            EditorGUI.indentLevel += 2;
                                lilEditorGUI.LocalizedPropertyAlpha(color4th);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main4thTexIsMSDF);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main4thTex_Cull);
                            EditorGUI.indentLevel -= 2;
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main4thEnableLighting);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main4thTexBlendMode);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main4thTexAlphaMode);
                                lilEditorGUI.DrawLine();
                                lilEditorGUI.UV4Decal(m_MaterialEditor, main4thTexIsDecal, main4thTexIsLeftOnly, main4thTexIsRightOnly, main4thTexShouldCopy, main4thTexShouldFlipMirror, main4thTexShouldFlipCopy, main4thTex, main4thTex_ScrollRotate, main4thTexAngle, main4thTexDecalAnimation, main4thTexDecalSubParam, main4thTex_UVMode);
                                lilEditorGUI.DrawLine();
                                lilEditorGUI.LocalizedPropertyTexture(m_MaterialEditor, maskBlendContent, main4thBlendMask);
                                EditorGUILayout.LabelField(GetLoc("sDistanceFade"));
                            EditorGUI.indentLevel++;
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main4thDistanceFade);
                            EditorGUI.indentLevel--;
                                /*lilEditorGUI.DrawLine();
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main4thDissolveParams);
                                if(main4thDissolveParams.vectorValue.x == 1.0f)                                                lilEditorGUI.TextureGUI(m_MaterialEditor, false, ref ltmedSet.isShowMain4thDissolveMask, maskBlendContent, main4thDissolveMask);
                                if(main4thDissolveParams.vectorValue.x == 2.0f && main4thDissolveParams.vectorValue.y == 0.0f) lilEditorGUI.LocalizedProperty(m_MaterialEditor, main4thDissolvePos, "sPosition|2");
                                if(main4thDissolveParams.vectorValue.x == 2.0f && main4thDissolveParams.vectorValue.y == 1.0f) lilEditorGUI.LocalizedProperty(m_MaterialEditor, main4thDissolvePos, "sVector|2");
                                if(main4thDissolveParams.vectorValue.x == 3.0f && main4thDissolveParams.vectorValue.y == 0.0f) lilEditorGUI.LocalizedProperty(m_MaterialEditor, main4thDissolvePos, "sPosition|3");
                                if(main4thDissolveParams.vectorValue.x == 3.0f && main4thDissolveParams.vectorValue.y == 1.0f) lilEditorGUI.LocalizedProperty(m_MaterialEditor, main4thDissolvePos, "sVector|3");
                                if(main4thDissolveParams.vectorValue.x != 0.0f)
                                {
                                    lilEditorGUI.TextureGUI(m_MaterialEditor, false, ref ltmedSet.isShowMain4thDissolveNoiseMask, noiseMaskContent, main4thDissolveNoiseMask, main4thDissolveNoiseStrength, main4thDissolveNoiseMask_ScrollRotate);
                                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, main4thDissolveColor);
                                }*/
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
                                        if (EditorUtility.DisplayDialog(
                                            "Reset Confirmation",
                                            "MainColor 4th will be reset to their default values. \nCannot be undone. Are you sure?",
                                            "Reset",
                                            "Cancel"))
                                        {
                                            ResetCategory(mainColor4thCategory, material);
                                        }
                                    }
                                lilEditorGUI.DrawLine();
                        EditorGUILayout.EndVertical();
                    }
                EditorGUILayout.EndVertical();
                
                EditorGUILayout.BeginVertical(boxOuter);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMain5thTex, false);
                    if(useMain5thTex.floatValue == 1)
                    {
                        EditorGUILayout.BeginVertical(boxInnerHalf);
                                lilEditorGUI.LocalizedPropertyTexture(m_MaterialEditor, colorRGBAContent, main5thTex, color5th);
                            EditorGUI.indentLevel += 2;
                                lilEditorGUI.LocalizedPropertyAlpha(color5th);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main5thTexIsMSDF);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main5thTex_Cull);
                            EditorGUI.indentLevel -= 2;
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main5thEnableLighting);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main5thTexBlendMode);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main5thTexAlphaMode);
                            lilEditorGUI.DrawLine();
                                lilEditorGUI.UV4Decal(m_MaterialEditor, main5thTexIsDecal, main5thTexIsLeftOnly, main5thTexIsRightOnly, main5thTexShouldCopy, main5thTexShouldFlipMirror, main5thTexShouldFlipCopy, main5thTex, main5thTex_ScrollRotate, main5thTexAngle, main5thTexDecalAnimation, main5thTexDecalSubParam, main5thTex_UVMode);
                            lilEditorGUI.DrawLine();
                                lilEditorGUI.LocalizedPropertyTexture(m_MaterialEditor, maskBlendContent, main5thBlendMask);
                            EditorGUILayout.LabelField(GetLoc("sDistanceFade"));
                            EditorGUI.indentLevel++;
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main5thDistanceFade);
                            EditorGUI.indentLevel--;
                            /*lilEditorGUI.DrawLine();
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main5thDissolveParams);
                                if(main5thDissolveParams.vectorValue.x == 1.0f)                                                lilEditorGUI.TextureGUI(m_MaterialEditor, false, ref ltmedSet.isShowMain5thDissolveMask, maskBlendContent, main5thDissolveMask);
                                if(main5thDissolveParams.vectorValue.x == 2.0f && main5thDissolveParams.vectorValue.y == 0.0f) lilEditorGUI.LocalizedProperty(m_MaterialEditor, main5thDissolvePos, "sPosition|2");
                                if(main5thDissolveParams.vectorValue.x == 2.0f && main5thDissolveParams.vectorValue.y == 1.0f) lilEditorGUI.LocalizedProperty(m_MaterialEditor, main5thDissolvePos, "sVector|2");
                                if(main5thDissolveParams.vectorValue.x == 3.0f && main5thDissolveParams.vectorValue.y == 0.0f) lilEditorGUI.LocalizedProperty(m_MaterialEditor, main5thDissolvePos, "sPosition|3");
                                if(main5thDissolveParams.vectorValue.x == 3.0f && main5thDissolveParams.vectorValue.y == 1.0f) lilEditorGUI.LocalizedProperty(m_MaterialEditor, main5thDissolvePos, "sVector|3");
                                if(main5thDissolveParams.vectorValue.x != 0.0f)
                                {
                                    lilEditorGUI.TextureGUI(m_MaterialEditor, false, ref ltmedSet.isShowMain5thDissolveNoiseMask, noiseMaskContent, main5thDissolveNoiseMask, main5thDissolveNoiseStrength, main5thDissolveNoiseMask_ScrollRotate);
                                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, main5thDissolveColor);
                                }*/
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
                                    if (EditorUtility.DisplayDialog(
                                        "Reset Confirmation",
                                        "MainColor 5th will be reset to their default values. \nCannot be undone. Are you sure?",
                                        "Reset",
                                        "Cancel"))
                                    {
                                        ResetCategory(mainColor5thCategory, material);
                                    }
                                }
                            lilEditorGUI.DrawLine();
                        EditorGUILayout.EndVertical();
                    }
                EditorGUILayout.EndVertical();
                
                EditorGUILayout.BeginVertical(boxOuter);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMain6thTex, false);
                    if(useMain6thTex.floatValue == 1)
                    {
                        EditorGUILayout.BeginVertical(boxInnerHalf);
                                lilEditorGUI.LocalizedPropertyTexture(m_MaterialEditor, colorRGBAContent, main6thTex, color6th);
                            EditorGUI.indentLevel += 2;
                                lilEditorGUI.LocalizedPropertyAlpha(color6th);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main6thTexIsMSDF);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main6thTex_Cull);
                            EditorGUI.indentLevel -= 2;
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main6thEnableLighting);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main6thTexBlendMode);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main6thTexAlphaMode);
                            lilEditorGUI.DrawLine();
                                lilEditorGUI.UV4Decal(m_MaterialEditor, main6thTexIsDecal, main6thTexIsLeftOnly, main6thTexIsRightOnly, main6thTexShouldCopy, main6thTexShouldFlipMirror, main6thTexShouldFlipCopy, main6thTex, main6thTex_ScrollRotate, main6thTexAngle, main6thTexDecalAnimation, main6thTexDecalSubParam, main6thTex_UVMode);
                            lilEditorGUI.DrawLine();
                                lilEditorGUI.LocalizedPropertyTexture(m_MaterialEditor, maskBlendContent, main6thBlendMask);
                            EditorGUILayout.LabelField(GetLoc("sDistanceFade"));
                            EditorGUI.indentLevel++;
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main6thDistanceFade);
                            EditorGUI.indentLevel--;
                            /*lilEditorGUI.DrawLine();
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, main6thDissolveParams);
                                if(main6thDissolveParams.vectorValue.x == 1.0f)                                                lilEditorGUI.TextureGUI(m_MaterialEditor, false, ref ltmedSet.isShowMain6thDissolveMask, maskBlendContent, main6thDissolveMask);
                                if(main6thDissolveParams.vectorValue.x == 2.0f && main6thDissolveParams.vectorValue.y == 0.0f) lilEditorGUI.LocalizedProperty(m_MaterialEditor, main6thDissolvePos, "sPosition|2");
                                if(main6thDissolveParams.vectorValue.x == 2.0f && main6thDissolveParams.vectorValue.y == 1.0f) lilEditorGUI.LocalizedProperty(m_MaterialEditor, main6thDissolvePos, "sVector|2");
                                if(main6thDissolveParams.vectorValue.x == 3.0f && main6thDissolveParams.vectorValue.y == 0.0f) lilEditorGUI.LocalizedProperty(m_MaterialEditor, main6thDissolvePos, "sPosition|3");
                                if(main6thDissolveParams.vectorValue.x == 3.0f && main6thDissolveParams.vectorValue.y == 1.0f) lilEditorGUI.LocalizedProperty(m_MaterialEditor, main6thDissolvePos, "sVector|3");
                                if(main6thDissolveParams.vectorValue.x != 0.0f)
                                {
                                    lilEditorGUI.TextureGUI(m_MaterialEditor, false, ref ltmedSet.isShowMain6thDissolveNoiseMask, noiseMaskContent, main6thDissolveNoiseMask, main6thDissolveNoiseStrength, main6thDissolveNoiseMask_ScrollRotate);
                                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, main6thDissolveColor);
                                }*/
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
                                        if (EditorUtility.DisplayDialog(
                                            "Reset Confirmation",
                                            "MainColor 6th will be reset to their default values. \nCannot be undone. Are you sure?",
                                            "Reset",
                                            "Cancel"))
                                        {
                                            ResetCategory(mainColor6thCategory, material);
                                        }
                                    }
                                lilEditorGUI.DrawLine();
                        EditorGUILayout.EndVertical();
                    }
                EditorGUILayout.EndVertical();
                
                EditorGUILayout.BeginVertical(boxOuter);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useBump3rdMap, false);
                    if(useBump3rdMap.floatValue == 1)
                    {
                        EditorGUILayout.BeginVertical(boxInnerHalf);
                            lilEditorGUI.TextureGUI(m_MaterialEditor, false, ref ltmedSet.isShowBump3rdMap, normalMapContent, bump3rdMap, bump3rdScale, bump3rdMap_UVMode, "UV Mode|UV0|UV1|UV2|UV3");
                        lilEditorGUI.DrawLine();
                            lilEditorGUI.TextureGUI(m_MaterialEditor, false, ref ltmedSet.isShowBump3rdScaleMask, maskStrengthContent, bump3rdScaleMask);
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
                                if (EditorUtility.DisplayDialog(
                                    "Reset Confirmation",
                                    "NormalMap 3rd will be reset to their default values. \nCannot be undone. Are you sure?",
                                    "Reset",
                                    "Cancel"))
                                {
                                    ResetCategory(bump3rdMapCategory, material);
                                }
                            }
                        lilEditorGUI.DrawLine();
                        EditorGUILayout.EndVertical();
                    }
                EditorGUILayout.EndVertical();
                
                EditorGUILayout.BeginVertical(boxOuter);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMatCap3rd, false);
                    if(useMatCap3rd.floatValue == 1)
                    {
                        EditorGUILayout.BeginVertical(boxInnerHalf);
                                lilEditorGUI.MatCapTextureGUI(m_MaterialEditor, false, ref ltmedSet.isShowMatCap3rdUV, matcapContent, matCap3rdTex, matCap3rdColor, matCap3rdBlendUV1, matCap3rdZRotCancel, matCap3rdPerspective, matCap3rdVRParallaxStrength);
                                lilEditorGUI.LocalizedPropertyAlpha(matCap3rdColor);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap3rdMainStrength);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap3rdNormalStrength);
                                lilEditorGUI.DrawLine();
                                lilEditorGUI.TextureGUI(m_MaterialEditor, false, ref ltmedSet.isShowMatCap3rdBlendMask, maskBlendRGBContent, matCap3rdBlendMask, matCap3rdBlend);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap3rdEnableLighting);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap3rdShadowMask);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap3rdBackfaceMask);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap3rdLod);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap3rdBlendMode);
                                if(matCap3rdEnableLighting.floatValue != 0.0f && matCap3rdBlendMode.floatValue == 3.0f && lilEditorGUI.AutoFixHelpBox(GetLoc("sHelpMatCap3rdBlending")))
                                {
                                    matCap3rdEnableLighting.floatValue = 0.0f;
                                }
                                if(isTransparent) lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap3rdApplyTransparency);
                            lilEditorGUI.DrawLine();
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap3rdCustomNormal);
                                if(matCap3rdCustomNormal.floatValue == 1)
                                {
                                    lilEditorGUI.TextureGUI(m_MaterialEditor, false, ref ltmedSet.isShowMatCap3rdBumpMap, normalMapContent, matCap3rdBumpMap, matCap3rdBumpScale);
                                }
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
                                    if (EditorUtility.DisplayDialog(
                                        "Reset Confirmation",
                                        "MatCap 3rd will be reset to their default values. \nCannot be undone. Are you sure?",
                                        "Reset",
                                        "Cancel"))
                                    {
                                        ResetCategory(matCap3rdCategory, material);
                                    }
                                }
                            lilEditorGUI.DrawLine();
                        EditorGUILayout.EndVertical();
                    }
                EditorGUILayout.EndVertical();
                
                EditorGUILayout.BeginVertical(boxOuter);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useMatCap4th, false);
                    if(useMatCap4th.floatValue == 1)
                    {
                        EditorGUILayout.BeginVertical(boxInnerHalf);
                                lilEditorGUI.MatCapTextureGUI(m_MaterialEditor, false, ref ltmedSet.isShowMatCap4thUV, matcapContent, matCap4thTex, matCap4thColor, matCap4thBlendUV1, matCap4thZRotCancel, matCap4thPerspective, matCap4thVRParallaxStrength);
                                lilEditorGUI.LocalizedPropertyAlpha(matCap4thColor);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap4thMainStrength);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap4thNormalStrength);
                            lilEditorGUI.DrawLine();
                                lilEditorGUI.TextureGUI(m_MaterialEditor, false, ref ltmedSet.isShowMatCap4thBlendMask, maskBlendRGBContent, matCap4thBlendMask, matCap4thBlend);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap4thEnableLighting);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap4thShadowMask);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap4thBackfaceMask);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap4thLod);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap4thBlendMode);
                                if(matCap4thEnableLighting.floatValue != 0.0f && matCap4thBlendMode.floatValue == 3.0f && lilEditorGUI.AutoFixHelpBox(GetLoc("sHelpMatCap4thBlending")))
                                {
                                    matCap4thEnableLighting.floatValue = 0.0f;
                                }
                                if(isTransparent) lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap4thApplyTransparency);
                            lilEditorGUI.DrawLine();
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, matCap4thCustomNormal);
                                if(matCap4thCustomNormal.floatValue == 1)
                                {
                                    lilEditorGUI.TextureGUI(m_MaterialEditor, false, ref ltmedSet.isShowMatCap4thBumpMap, normalMapContent, matCap4thBumpMap, matCap4thBumpScale);
                                }
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
                                    if (EditorUtility.DisplayDialog(
                                        "Reset Confirmation",
                                        "MatCap 4th will be reset to their default values. \nCannot be undone. Are you sure?",
                                        "Reset",
                                        "Cancel"))
                                    {
                                        ResetCategory(matCap4thCategory, material);
                                    }
                                }
                            lilEditorGUI.DrawLine();
                        EditorGUILayout.EndVertical();
                    }
                EditorGUILayout.EndVertical();
                
                EditorGUILayout.BeginVertical(boxOuter);
                    lilEditorGUI.LocalizedProperty(m_MaterialEditor, useGlitter2nd);
                    if(useGlitter2nd.floatValue == 1)
                    {
                        EditorGUILayout.BeginVertical(boxInnerHalf);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndUVMode);
                                lilEditorGUI.TextureGUI(m_MaterialEditor, false, ref ltmedSet.isShowGlitter2ndColorTex, colorMaskRGBAContent, glitter2ndColorTex, glitter2ndColor, glitter2ndColorTex_UVMode, "UV Mode|UV0|UV1|UV2|UV3");
                            EditorGUI.indentLevel++;
                                lilEditorGUI.LocalizedPropertyAlpha(glitter2ndColor);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndMainStrength);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndEnableLighting);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndShadowMask);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndBackfaceMask);
                                if(isTransparent) lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndApplyTransparency);
                            EditorGUI.indentLevel--;
                            lilEditorGUI.DrawLine();
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndApplyShape);
                                if(glitter2ndApplyShape.floatValue > 0.5f)
                                {
                                    EditorGUI.indentLevel++;
                                        lilEditorGUI.TextureGUI(m_MaterialEditor, false, ref ltmedSet.isShowGlitter2ndShapeTex, customMaskContent, glitter2ndShapeTex);
                                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndAtras);
                                        lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndAngleRandomize);
                                    EditorGUI.indentLevel--;
                                }
                            lilEditorGUI.DrawLine();

                                // Param1
                                var scale = new Vector2(256.0f/glitter2ndParams1.vectorValue.x, 256.0f/glitter2ndParams1.vectorValue.y);
                                float size = glitter2ndParams1.vectorValue.z == 0.0f ? 0.0f : Mathf.Sqrt(glitter2ndParams1.vectorValue.z);
                                float density = Mathf.Sqrt(1.0f / glitter2ndParams1.vectorValue.w) / 1.5f;
                                float sensitivity = lilEditorGUI.RoundFloat1000000(glitter2ndSensitivity.floatValue / density);
                                density = lilEditorGUI.RoundFloat1000000(density);
                            EditorGUIUtility.wideMode = true;

                            EditorGUI.BeginChangeCheck();
                            EditorGUI.showMixedValue = glitter2ndParams1.hasMixedValue || glitter2ndSensitivity.hasMixedValue;
                                scale = lilEditorGUI.Vector2Field(Event.current.alt ? glitter2ndParams1.name + ".xy" : GetLoc("sScale"), scale);
                                size = lilEditorGUI.Slider(Event.current.alt ? glitter2ndParams1.name + ".z" : GetLoc("sParticleSize"), size, 0.0f, 2.0f);
                            EditorGUI.showMixedValue = false;

                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndScaleRandomize);

                            EditorGUI.showMixedValue = glitter2ndParams1.hasMixedValue || glitter2ndSensitivity.hasMixedValue;
                                density = lilEditorGUI.Slider(Event.current.alt ? glitter2ndParams1.name + ".w" : GetLoc("sDensity"), density, 0.001f, 1.0f);
                                sensitivity = lilEditorGUI.FloatField(Event.current.alt ? glitter2ndSensitivity.name : GetLoc("sSensitivity"), sensitivity);
                            EditorGUI.showMixedValue = false;

                            if(EditorGUI.EndChangeCheck())
                            {
                                scale.x = Mathf.Max(scale.x, 0.0000001f);
                                scale.y = Mathf.Max(scale.y, 0.0000001f);
                                glitter2ndParams1.vectorValue = new Vector4(256.0f/scale.x, 256.0f/scale.y, size * size, 1.0f / (density * density * 1.5f * 1.5f));
                                glitter2ndSensitivity.floatValue = Mathf.Max(sensitivity * density, 0.25f);
                            }

                                // Other
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndParams2);
                                lilEditorGUI.LocalizedProperty(m_MaterialEditor, glitter2ndVRParallaxStrength);
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
                                    if (EditorUtility.DisplayDialog(
                                        "Reset Confirmation",
                                        "Glitter 2nd will be reset to their default values. \nCannot be undone. Are you sure?",
                                        "Reset",
                                        "Cancel"))
                                    {
                                        ResetCategory(glitter2ndCategory, material);
                                    }
                                }
                            lilEditorGUI.DrawLine();
                        EditorGUILayout.EndVertical();
                    }
                EditorGUILayout.EndVertical();
            }
        }


        protected override void ReplaceToCustomShaders()
        {
            lts         = Shader.Find(shaderName + "/lilToon");
            ltsc        = Shader.Find("Hidden/" + shaderName + "/Cutout");
            ltst        = Shader.Find("Hidden/" + shaderName + "/Transparent");
            ltsot       = Shader.Find("Hidden/" + shaderName + "/OnePassTransparent");
            ltstt       = Shader.Find("Hidden/" + shaderName + "/TwoPassTransparent");

            ltso        = Shader.Find("Hidden/" + shaderName + "/OpaqueOutline");
            ltsco       = Shader.Find("Hidden/" + shaderName + "/CutoutOutline");
            ltsto       = Shader.Find("Hidden/" + shaderName + "/TransparentOutline");
            ltsoto      = Shader.Find("Hidden/" + shaderName + "/OnePassTransparentOutline");
            ltstto      = Shader.Find("Hidden/" + shaderName + "/TwoPassTransparentOutline");

            ltsoo       = Shader.Find(shaderName + "/[Optional] OutlineOnly/Opaque");
            ltscoo      = Shader.Find(shaderName + "/[Optional] OutlineOnly/Cutout");
            ltstoo      = Shader.Find(shaderName + "/[Optional] OutlineOnly/Transparent");

            ltstess     = Shader.Find("Hidden/" + shaderName + "/Tessellation/Opaque");
            ltstessc    = Shader.Find("Hidden/" + shaderName + "/Tessellation/Cutout");
            ltstesst    = Shader.Find("Hidden/" + shaderName + "/Tessellation/Transparent");
            ltstessot   = Shader.Find("Hidden/" + shaderName + "/Tessellation/OnePassTransparent");
            ltstesstt   = Shader.Find("Hidden/" + shaderName + "/Tessellation/TwoPassTransparent");

            ltstesso    = Shader.Find("Hidden/" + shaderName + "/Tessellation/OpaqueOutline");
            ltstessco   = Shader.Find("Hidden/" + shaderName + "/Tessellation/CutoutOutline");
            ltstessto   = Shader.Find("Hidden/" + shaderName + "/Tessellation/TransparentOutline");
            ltstessoto  = Shader.Find("Hidden/" + shaderName + "/Tessellation/OnePassTransparentOutline");
            ltstesstto  = Shader.Find("Hidden/" + shaderName + "/Tessellation/TwoPassTransparentOutline");

            ltsl        = Shader.Find(shaderName + "/lilToonLite");
            ltslc       = Shader.Find("Hidden/" + shaderName + "/Lite/Cutout");
            ltslt       = Shader.Find("Hidden/" + shaderName + "/Lite/Transparent");
            ltslot      = Shader.Find("Hidden/" + shaderName + "/Lite/OnePassTransparent");
            ltsltt      = Shader.Find("Hidden/" + shaderName + "/Lite/TwoPassTransparent");

            ltslo       = Shader.Find("Hidden/" + shaderName + "/Lite/OpaqueOutline");
            ltslco      = Shader.Find("Hidden/" + shaderName + "/Lite/CutoutOutline");
            ltslto      = Shader.Find("Hidden/" + shaderName + "/Lite/TransparentOutline");
            ltsloto     = Shader.Find("Hidden/" + shaderName + "/Lite/OnePassTransparentOutline");
            ltsltto     = Shader.Find("Hidden/" + shaderName + "/Lite/TwoPassTransparentOutline");

            ltsref      = Shader.Find("Hidden/" + shaderName + "/Refraction");
            ltsrefb     = Shader.Find("Hidden/" + shaderName + "/RefractionBlur");
            ltsfur      = Shader.Find("Hidden/" + shaderName + "/Fur");
            ltsfurc     = Shader.Find("Hidden/" + shaderName + "/FurCutout");
            ltsfurtwo   = Shader.Find("Hidden/" + shaderName + "/FurTwoPass");
            ltsfuro     = Shader.Find(shaderName + "/[Optional] FurOnly/Transparent");
            ltsfuroc    = Shader.Find(shaderName + "/[Optional] FurOnly/Cutout");
            ltsfurotwo  = Shader.Find(shaderName + "/[Optional] FurOnly/TwoPass");
            ltsgem      = Shader.Find("Hidden/" + shaderName + "/Gem");
            ltsfs       = Shader.Find(shaderName + "/[Optional] FakeShadow");

            ltsover     = Shader.Find(shaderName + "/[Optional] Overlay");
            ltsoover    = Shader.Find(shaderName + "/[Optional] OverlayOnePass");
            ltslover    = Shader.Find(shaderName + "/[Optional] LiteOverlay");
            ltsloover   = Shader.Find(shaderName + "/[Optional] LiteOverlayOnePass");

            ltsm        = Shader.Find(shaderName + "/lilToonMulti");
            ltsmo       = Shader.Find("Hidden/" + shaderName + "/MultiOutline");
            ltsmref     = Shader.Find("Hidden/" + shaderName + "/MultiRefraction");
            ltsmfur     = Shader.Find("Hidden/" + shaderName + "/MultiFur");
            ltsmgem     = Shader.Find("Hidden/" + shaderName + "/MultiGem");
        }
        
        public class lilToonMoreEditorSetting : ScriptableSingleton<lilToonMoreEditorSetting>
        {
            /*public bool isShowMain4thDissolveMask       = false;
            public bool isShowMain4thDissolveNoiseMask  = false;
            public bool isShowMain5thDissolveMask       = false;
            public bool isShowMain5thDissolveNoiseMask  = false;
            public bool isShowMain6thDissolveMask       = false;
            public bool isShowMain6thDissolveNoiseMask  = false;
            public bool isShowMain7thDissolveMask       = false;
            public bool isShowMain7thDissolveNoiseMask  = false;
            public bool isShowMain8thDissolveMask       = false;
            public bool isShowMain8thDissolveNoiseMask  = false;
            public bool isShowMain9thDissolveMask       = false;
            public bool isShowMain9thDissolveNoiseMask  = false;
            public bool isShowMain10thDissolveMask      = false;
            public bool isShowMain10thDissolveNoiseMask = false;*/
            public bool isShowBump3rdMap                = false;
            public bool isShowBump3rdScaleMask          = false;
            public bool isShowMatCap3rdUV               = false;
            public bool isShowMatCap3rdBlendMask        = false;
            public bool isShowMatCap3rdBumpMap          = false;
            public bool isShowMatCap4thUV               = false;
            public bool isShowMatCap4thBlendMask        = false;
            public bool isShowMatCap4thBumpMap          = false;
            public bool isShowGlitter2ndColorTex        = false;
            public bool isShowGlitter2ndShapeTex        = false;
        }
    }
}

#endif
