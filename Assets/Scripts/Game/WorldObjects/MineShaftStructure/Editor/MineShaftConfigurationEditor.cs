using Editor;
using Game.WorldObjects.MineShaftStructure.Configuration;
using UnityEditor;
using UnityEngine;

namespace Game.WorldObjects.MineShaftStructure.Editor
{
    [CustomEditor(typeof(MineShaftConfiguration))]
    public class MineShaftConfigurationEditor : CustomEditorBase<MineShaftConfiguration>
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            GenerateId();
        }

        private void GenerateId()
        {
            var id = serializedObject.FindProperty("_id");
            if (string.IsNullOrEmpty(id.stringValue))
            {
                id.stringValue = GUID.Generate().ToString();
            }
        }

        protected override void DrawInspector()
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.LabelField(string.Format("Mine Shaft Configuration: {0}", Target.name), HeaderStyle);
                EditorGUILayout.BeginVertical(GUI.skin.box);
                {
                    var id = serializedObject.FindProperty("_id");

                    GUI.enabled = false;
                    EditorGUILayout.PropertyField(id);
                    GUI.enabled = true;

                    var managerCost = serializedObject.FindProperty("_managerCost");
                    EditorGUILayout.PropertyField(managerCost);

                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Mine Shaft Settings", HeaderStyle);
                    var settingsList = serializedObject.FindProperty("_settingsList");
                    for (int i = 0; i < settingsList.arraySize; i++)
                    {
                        EditorGUILayout.BeginHorizontal(GUI.skin.box);
                        {
                            EditorGUILayout.BeginVertical();
                            {
                                var element = settingsList.GetArrayElementAtIndex(i);
                                var level = element.FindPropertyRelative("_level");
                                var upgradeCost = element.FindPropertyRelative("_upgradeCost");
                                var units = element.FindPropertyRelative("_units");
                                var load = element.FindPropertyRelative("_load");
                                var moveTime = element.FindPropertyRelative("_moveTime");
                                var workTime = element.FindPropertyRelative("_workTime");

                                EditorGUILayout.LabelField(string.Format("Mine Shaft Settings Level {0}", level.intValue), LabelStyle);
                                EditorGUILayout.PropertyField(level);
                                EditorGUILayout.PropertyField(upgradeCost);
                                EditorGUILayout.PropertyField(units);
                                EditorGUILayout.PropertyField(load);
                                EditorGUILayout.PropertyField(moveTime);
                                EditorGUILayout.PropertyField(workTime);
                            }
                            EditorGUILayout.EndVertical();

                            if (GUILayout.Button("Remove", GUILayout.Width(100f)))
                            {
                                RecordObject("MineShaftConfiguration change");
                                Target.SettingsList.RemoveAt(i);
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.Space();
                    }

                    if (GUILayout.Button("Add"))
                    {
                        RecordObject("MineShaftConfiguration change");
                        Target.SettingsList.Add(new MineShaftSettings());
                    }
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();
        }
    }
}