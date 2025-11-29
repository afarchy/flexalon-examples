using UnityEditor;
using UnityEngine;

namespace Flexalon.Examples
{
    public class MaterialConversion : UnityEditor.Editor
    {
        [MenuItem("Assets/Convert URP to Standard")]
        public static void URPToStandard(MenuCommand command)
        {
            foreach (var obj in Selection.objects)
            {
                if (obj is Material mat)
                {
                    mat.shader = Shader.Find("Standard");

                    if (mat.HasProperty("_BaseColor"))
                    {
                        Color color = mat.GetColor("_BaseColor");
                        mat.SetColor("_Color", color);
                    }

                    if (mat.HasProperty("_BaseMap"))
                    {
                        Texture mainTex = mat.GetTexture("_BaseMap");
                        mat.SetTexture("_MainTex", mainTex);
                    }

                    if (mat.HasProperty("_Smoothness"))
                    {
                        float smoothness = mat.GetFloat("_Smoothness");
                        mat.SetFloat("_Glossiness", smoothness);
                    }

                    EditorUtility.SetDirty(mat);
                }
            }

            AssetDatabase.SaveAssets();
        }
    }
}