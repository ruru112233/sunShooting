using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class InspectorLocker
{
    private static EditorWindow mouseOverWindow;

    [MenuItem("Edit/Toggle Lock &q")]
    private static void Toggle()
    {
        if (mouseOverWindow == null)
        {
            if (!EditorPrefs.HasKey("LockableInspectorIndex"))
            {
                EditorPrefs.SetInt("LockableInspectorIndex", 0);
            }
            int i = EditorPrefs.GetInt("LockableInspectorIndex");

            var type = Assembly
                .GetAssembly(typeof(Editor))
                .GetType("UnityEditor.InspectorWindow")
            ;

            var list = Resources.FindObjectsOfTypeAll(type);
            mouseOverWindow = list.ElementAtOrDefault(i) as EditorWindow;
        }

        if (mouseOverWindow != null && mouseOverWindow.GetType().Name == "InspectorWindow")
        {
            var type = Assembly
                .GetAssembly(typeof(Editor))
                .GetType("UnityEditor.InspectorWindow")
            ;

            var propertyInfo = type.GetProperty("isLocked");
            var value = (bool)propertyInfo.GetValue(mouseOverWindow, null);
            propertyInfo.SetValue(mouseOverWindow, !value, null);
            mouseOverWindow.Repaint();
        }
    }
}
