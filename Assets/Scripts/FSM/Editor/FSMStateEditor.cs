using Editor;
using UnityEditor;
using UnityEngine;

namespace FSM.Editor
{
    [CustomEditor(typeof(FSMState))]
    public class FSMStateEditor : CustomEditorBase<FSMState>
    {
        protected override void DrawInspector()
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.LabelField(string.Format("FSMState: {0}", Target.name), HeaderStyle);
                EditorGUILayout.BeginVertical(GUI.skin.box);
                {
                    var stateName = serializedObject.FindProperty("_name");
                    EditorGUILayout.PropertyField(stateName);

                    EditorGUILayout.LabelField("Action", HeaderStyle);
                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    {
                        var action = serializedObject.FindProperty("_action");
                        EditorGUILayout.PropertyField(action);
                    }
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("Transitions", HeaderStyle);
                    var transitions = serializedObject.FindProperty("_transitions");
                    for (int i = 0; i < transitions.arraySize; i++)
                    {
                        EditorGUILayout.BeginHorizontal(GUI.skin.box);
                        {
                            EditorGUILayout.BeginVertical();
                            {
                                var element = transitions.GetArrayElementAtIndex(i);
                                var condition = element.FindPropertyRelative("_condition");
                                var state = element.FindPropertyRelative("_state");

                                var transitionName = "None";
                                if (condition.objectReferenceValue != null && state.objectReferenceValue != null)
                                {
                                    transitionName = string.Format("{0}. Transition: {1} -> {2}", i + 1,
                                        condition.objectReferenceValue.name, state.objectReferenceValue.name);
                                }

                                EditorGUILayout.LabelField(transitionName);
                                EditorGUILayout.PropertyField(condition);
                                EditorGUILayout.PropertyField(state);
                            }
                            EditorGUILayout.EndVertical();

                            if (GUILayout.Button("Remove", GUILayout.Width(100f)))
                            {
                                RecordObject("FSMState Change");
                                Target.Transitions.RemoveAt(i);
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.Space();
                    }

                    if (GUILayout.Button("Add"))
                    {
                        RecordObject("FSMState Change");
                        Target.Transitions.Add(new FSMTransition());
                    }
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();
        }
    }
}