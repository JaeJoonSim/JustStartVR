using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Spawn_Item))]
public class item_list : Editor
{
    SerializedProperty exampleProperty;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        exampleProperty = serializedObject.FindProperty("Item");
        EditorGUILayout.PropertyField(exampleProperty);
        exampleProperty = serializedObject.FindProperty("RadomProbability");
        EditorGUILayout.PropertyField(exampleProperty);
        exampleProperty = serializedObject.FindProperty("Spawn_Point");
        EditorGUILayout.PropertyField(exampleProperty);
        exampleProperty = serializedObject.FindProperty("SpawnCount");
        EditorGUILayout.PropertyField(exampleProperty);

        serializedObject.ApplyModifiedProperties();

        Spawn_Item button = (Spawn_Item)target;
        button.Setitem();
    }
}