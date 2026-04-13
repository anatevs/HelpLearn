using Gameplay;
using UnityEditor;
using UnityEngine;

namespace EditorExtensions
{
    [CustomEditor(typeof(SpawnRoom))]
    public class SpawnRoomEditor : Editor
    {
        private readonly string _addField = "_addLocation";
        private readonly string _removeField = "_removeLocation";
        private readonly string _locationsField = "_spawnPoints";

        public override void OnInspectorGUI()
        {
            SpawnRoom spawnRoom = (SpawnRoom)target;

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(serializedObject.FindProperty(_addField));
            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Add location"))
            {
                spawnRoom.AddLocation();
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.Space();


            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(serializedObject.FindProperty(_removeField));
            serializedObject.ApplyModifiedProperties();


            if (GUILayout.Button("Remove location"))
            {
                spawnRoom.RemoveLocation();
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.Space();

            SerializedProperty listProp = serializedObject.FindProperty(_locationsField);

            serializedObject.Update();

            GUI.enabled = false;

            EditorGUILayout.LabelField("Room locations:", EditorStyles.boldLabel);

            for (int i = 0; i < listProp.arraySize; i++)
            {
                SerializedProperty element = listProp.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(element, new GUIContent($"{i}"), true);
            }

            GUI.enabled = true;

            serializedObject.ApplyModifiedProperties();


            EditorGUILayout.Space();
            if (GUILayout.Button("Clear empty"))
            {
                spawnRoom.ClearEmpty();
            }
        }
    }
}