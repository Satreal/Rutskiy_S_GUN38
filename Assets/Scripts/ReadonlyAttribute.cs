
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace DefaultNamespace
{
    public class ReadonlyAttribute : PropertyAttribute { }
#if UNITY_EDITOR

[UnityEditor.CustomPropertyDrawer(typeof(ReadonlyAttribute))]
public class ReadonlyPropertyDrawer : UnityEditor.PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        UnityEditor.EditorGUI.PropertyField(position, property, label);
        GUI.enabled = true;


    }
     
}
#endif
}




