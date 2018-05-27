using UnityEditor;
using UnityEngine;

namespace Framework.Tools.FSM.Editor
{
    [CustomEditor(typeof(FSMState))]
    public class FSMStateEditor : UnityEditor.Editor
    {
        private FSMState _fsmState;
        private GUIStyle _headerStyle;

        private void OnEnable()
        {
            _fsmState = target as FSMState;
            _headerStyle = new GUIStyle
            {
                normal = {textColor = Color.black},
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            DrawInspector();

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(_fsmState);
            }
        }

        private void DrawInspector()
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                EditorGUILayout.LabelField(string.Format("FSMState: {0}", _fsmState.name), _headerStyle);
                EditorGUILayout.BeginVertical(GUI.skin.box);
                {
                    var stateName = serializedObject.FindProperty("_name");
                    EditorGUILayout.PropertyField(stateName);

                    EditorGUILayout.LabelField("Action", _headerStyle);
                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    {
                        var action = serializedObject.FindProperty("_action");
                        EditorGUILayout.PropertyField(action);
                    }
                    EditorGUILayout.EndVertical();
                    EditorGUILayout.Space();

                    EditorGUILayout.LabelField("Transitions", _headerStyle);
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
                                RecordObject();
                                _fsmState.Transitions.RemoveAt(i);
                            }
                        }
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.Space();
                    }

                    if (GUILayout.Button("Add"))
                    {
                        RecordObject();
                        _fsmState.Transitions.Add(new FSMTransition());
                    }
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();
        }

        private void RecordObject(string changeDescription = "FSMState Change")
        {
            Undo.RecordObject(serializedObject.targetObject, changeDescription);
        }
    }
}