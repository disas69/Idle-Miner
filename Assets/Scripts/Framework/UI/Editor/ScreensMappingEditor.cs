using Framework.UI.Configuration;
using UnityEditor;
using UnityEngine;

namespace Framework.UI.Editor
{
    [CustomEditor(typeof(ScreensMapping))]
    public class ScreensMappingEditor : UnityEditor.Editor
    {
        private ScreensMapping _screensMapping;
        private GUIStyle _headerStyle;

        private void OnEnable()
        {
            _screensMapping = target as ScreensMapping;
            _headerStyle = new GUIStyle
            {
                normal = {textColor = Color.black},
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleLeft
            };
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Pages Mapping", _headerStyle);
            var screenSettings = serializedObject.FindProperty("ScreenSettings");

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                var count = screenSettings.arraySize;
                for (int i = 0; i < count; i++)
                {
                    EditorGUILayout.BeginHorizontal(GUI.skin.box);
                    {
                        var element = screenSettings.GetArrayElementAtIndex(i);
                        var screen = element.FindPropertyRelative("Screen");
                        var isCached = element.FindPropertyRelative("IsCached");

                        var screenReference = _screensMapping.ScreenSettings[i].Screen;
                        var elementName = screenReference != null ? screenReference.GetType().Name : "Screen";

                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.PropertyField(screen, new GUIContent(elementName));
                            EditorGUILayout.PropertyField(isCached);
                        }
                        EditorGUILayout.EndVertical();

                        if (GUILayout.Button("Remove", GUILayout.Width(100f)))
                        {
                            RecordObject();
                            _screensMapping.ScreenSettings.RemoveAt(i);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();
                }
            }
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("Add"))
            {
                RecordObject();
                _screensMapping.ScreenSettings.Add(null);
            }

            EditorGUILayout.LabelField("Popups Mapping", _headerStyle);
            var popupSettings = serializedObject.FindProperty("PopupSettings");

            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                var count = popupSettings.arraySize;
                for (int i = 0; i < count; i++)
                {
                    EditorGUILayout.BeginHorizontal(GUI.skin.box);
                    {
                        var element = popupSettings.GetArrayElementAtIndex(i);
                        var poppup = element.FindPropertyRelative("Popup");

                        var popupReference = _screensMapping.PopupSettings[i].Popup;
                        var elementName = popupReference != null ? popupReference.GetType().Name : "Popup";

                        EditorGUILayout.BeginVertical();
                        {
                            EditorGUILayout.PropertyField(poppup, new GUIContent(elementName));
                        }
                        EditorGUILayout.EndVertical();

                        if (GUILayout.Button("Remove", GUILayout.Width(100f)))
                        {
                            RecordObject();
                            _screensMapping.PopupSettings.RemoveAt(i);
                        }
                    }
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space();
                }
            }
            EditorGUILayout.EndVertical();

            if (GUILayout.Button("Add"))
            {
                RecordObject();
                _screensMapping.PopupSettings.Add(null);
            }

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(_screensMapping);
            }
        }

        private void RecordObject(string changeDescription = "Screens Mapping Change")
        {
            Undo.RecordObject(serializedObject.targetObject, changeDescription);
        }
    }
}