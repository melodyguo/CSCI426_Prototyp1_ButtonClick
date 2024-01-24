using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace JoeyTA101
{
    [ExecuteInEditMode]
    public class GlobalShaderParams : MonoBehaviour
    {
        [Serializable]
        public class ShaderParam
        {
            public enum DataType
            {
                Boolean,
                Float,
                Vector,
                Color,
                Texture,
                Gradient
            }

            public DataType dataType;
            public string propertyName;

            public bool boolValue;
            public float floatValue;
            public Vector4 vectorValue;
            [ColorUsage(true, true)] public Color colorValue;
            public Texture textureValue;
            [GradientUsage(true)] public Gradient gradientValue;
            [NonSerialized] public Texture2D gradientTextureValue;
        }

        public List<ShaderParam> shaderParams = new List<ShaderParam>();

        private void OnEnable()
        {
            ApplyValues();
        }

        private void OnDisable()
        {
            ApplyValues(true);
        }

        public void ApplyValues(bool clear = false)
        {
            for (int i = 0; i < shaderParams.Count; i++)
            {
                ApplyValue(shaderParams[i], clear);
            }
        }

        public void ApplyValue(ShaderParam param, bool clear = false)
        {
            //Cannot recreate on GUI change
            if (param.dataType == ShaderParam.DataType.Gradient) GradientToTexture(param);

            switch (param.dataType)
            {
                case ShaderParam.DataType.Boolean:
                    Shader.SetGlobalFloat(param.propertyName, clear ? 0f : (param.boolValue ? 1 : 0));
                    break;
                case ShaderParam.DataType.Float:
                    Shader.SetGlobalFloat(param.propertyName, clear ? 0f : param.floatValue);
                    break;
                case ShaderParam.DataType.Vector:
                    Shader.SetGlobalVector(param.propertyName, clear ? Vector4.zero : param.vectorValue);
                    break;
                case ShaderParam.DataType.Color:
                    Shader.SetGlobalColor(param.propertyName, clear ? Color.clear : param.colorValue);
                    break;
                case ShaderParam.DataType.Texture:
                    Shader.SetGlobalTexture(param.propertyName, clear ? null : param.textureValue);
                    break;
                case ShaderParam.DataType.Gradient:
                    Shader.SetGlobalTexture(param.propertyName, clear ? null : param.gradientTextureValue);
                    break;
            }
        }

        private const int GRADIENT_RESOLUTION = 64;

        private static void GradientToTexture(ShaderParam param, bool hdr = true)
        {
            if (!param.gradientTextureValue)
            {
                param.gradientTextureValue = new Texture2D(GRADIENT_RESOLUTION, 1,
                    hdr ? TextureFormat.RGBAFloat : TextureFormat.RGBA32, true, true);
                param.gradientTextureValue.wrapMode = TextureWrapMode.Clamp;
                param.gradientTextureValue.filterMode = FilterMode.Bilinear;
            }

            for (int x = 0; x < GRADIENT_RESOLUTION; x++)
            {
                Color gradientTexel = param.gradientValue.Evaluate(x / (float) GRADIENT_RESOLUTION);
                param.gradientTextureValue.SetPixel(x, 0, gradientTexel);
            }

            param.gradientTextureValue.name = param.propertyName;
            param.gradientTextureValue.Apply();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(GlobalShaderParams))]
    public class GlobalShaderParamsInspector : Editor
    {
        private GlobalShaderParams component;
        private SerializedProperty shaderParams;

        private string proSkinPrefix => EditorGUIUtility.isProSkin ? "d_" : "";

        private void OnEnable()
        {
            component = (GlobalShaderParams) target;
            shaderParams = serializedObject.FindProperty("shaderParams");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            for (int i = 0; i < shaderParams.arraySize; i++)
            {
                using (new EditorGUILayout.HorizontalScope(EditorStyles.helpBox))
                {
                    using (new EditorGUILayout.VerticalScope())
                    {
                        GUILayout.Space(5f);

                        SerializedProperty param = shaderParams.GetArrayElementAtIndex(i);

                        SerializedProperty dataType = param.FindPropertyRelative("dataType");
                        EditorGUILayout.PropertyField(dataType, GUILayout.MaxWidth(EditorGUIUtility.labelWidth + 75f));

                        SerializedProperty propertyName = param.FindPropertyRelative("propertyName");
                        EditorGUILayout.PropertyField(propertyName);
                        if (propertyName.stringValue == string.Empty)
                            EditorGUILayout.HelpBox("Property name must not be empty!", MessageType.Warning);

                        switch ((GlobalShaderParams.ShaderParam.DataType) dataType.intValue)
                        {
                            case GlobalShaderParams.ShaderParam.DataType.Boolean:
                                EditorGUILayout.PropertyField(param.FindPropertyRelative("boolValue"));
                                break;
                            case GlobalShaderParams.ShaderParam.DataType.Float:
                                EditorGUILayout.PropertyField(param.FindPropertyRelative("floatValue"));
                                break;
                            case GlobalShaderParams.ShaderParam.DataType.Vector:
                                EditorGUI.indentLevel++;
                                EditorGUILayout.PropertyField(param.FindPropertyRelative("vectorValue"), true);
                                EditorGUI.indentLevel--;
                                break;
                            case GlobalShaderParams.ShaderParam.DataType.Color:
                                EditorGUILayout.PropertyField(param.FindPropertyRelative("colorValue"));
                                break;
                            case GlobalShaderParams.ShaderParam.DataType.Texture:
                                EditorGUILayout.PropertyField(param.FindPropertyRelative("textureValue"));
                                break;
                            case GlobalShaderParams.ShaderParam.DataType.Gradient:
                                EditorGUILayout.PropertyField(param.FindPropertyRelative("gradientValue"));
                                break;
                        }

                        GUILayout.Space(5f);
                    }

                    if (GUILayout.Button(
                        new GUIContent("", EditorGUIUtility.IconContent(proSkinPrefix + "TreeEditor.Trash").image,
                            "Remove parameter"), EditorStyles.miniButton, GUILayout.Width(30f)))
                        shaderParams.DeleteArrayElementAtIndex(i);
                }

                GUILayout.Space(3f);
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();

                if (GUILayout.Button(
                    new GUIContent(" Add", EditorGUIUtility.IconContent(proSkinPrefix + "Toolbar Plus").image,
                        "Insert new parameter"), EditorStyles.miniButton, GUILayout.Width(60f)))
                {
                    shaderParams.InsertArrayElementAtIndex(shaderParams.arraySize);
                }
            }

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                component.ApplyValues();
            }
        }
    }
#endif
}