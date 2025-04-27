using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(ScriptableObject), true)]
public class GameEventEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ScriptableObject scriptableObject = (ScriptableObject)target;

        Type type = scriptableObject.GetType();
        if (type.BaseType != null && type.BaseType.IsGenericType &&
            type.BaseType.GetGenericTypeDefinition() == typeof(GameEventSO<>))
        {
            if (GUILayout.Button("Invoke"))
            {
                Type eventType = type.BaseType.GetGenericArguments()[0];
                FieldInfo valueField = type.BaseType.GetField("Data", BindingFlags.Public | BindingFlags.Instance);
                object value = valueField.GetValue(scriptableObject);

                MethodInfo method = type.GetMethod("Invoke");
                method?.Invoke(scriptableObject, new object[] { value });
            }
        }
    }
}
