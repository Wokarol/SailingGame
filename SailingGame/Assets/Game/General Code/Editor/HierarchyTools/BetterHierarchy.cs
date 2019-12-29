using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class BetterHierarchy
{
    private const string toggleStyleName = "OL Toggle";

    // Bindings for all icons on right
    private static readonly (Type type, string icon)[] bindings = new (Type, string)[] {
        (typeof(MonoBehaviour), "cs Script Icon"),
        (typeof(Camera), "Camera Gizmo")
    };

    // Cache for all icon textures
    private static readonly Dictionary<Type, Texture> textureCache = new Dictionary<Type, Texture>();

    static BetterHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI = DrawItem;
        foreach (var (type, icon) in bindings) {
            textureCache.Add(type, 
                EditorGUIUtility.IconContent(icon).image);
        }
    }

    static void DrawItem(int instanceID, Rect rect)
    {
        // Get's object for given item
        GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if(go != null) {

            if (go.name.StartsWith("---")) {
                // Creating highlight rect and style
                Rect highlightRect = new Rect(rect);
                highlightRect.width -= highlightRect.height;

                GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
                labelStyle.fontStyle = FontStyle.Bold;
                labelStyle.alignment = TextAnchor.MiddleCenter;
                labelStyle.fontSize -= 1;
                highlightRect.height -= 1;
                highlightRect.y += 1;

                // Drawing background
                EditorGUI.DrawRect(highlightRect, Color.grey);

                // Offseting text
                highlightRect.height -= 2;
                highlightRect.y += 1;

                // Drawing label
                EditorGUI.LabelField(highlightRect, go.name.Replace("---", "").ToUpperInvariant(), labelStyle);
            }

            // Get's style of toggle
            GUIStyle toggleStyle = toggleStyleName;

            // Sets rect for toggle
            var toggleRect = new Rect(rect);
            toggleRect.width = toggleRect.height;
            toggleRect.x -= 28;

            // Creates toggle
            bool state = GUI.Toggle(toggleRect, go.activeSelf, GUIContent.none, toggleStyle);

            // Draws icon for each binded component type
            int i = 0;
            foreach (var (type, _) in bindings) {
                if (go.GetComponent(type) != null) {
                    GUI.DrawTexture(GetRightRectWithOffset(rect, i), textureCache[type]);
                    i++;
                }
            }

            // Sets game's active state to result of toggle
            if (state != go.activeSelf) {
                Undo.RecordObject(go, $"{(state ? "Enabled" : "Disabled")}");
                go.SetActive(state);
                Undo.FlushUndoRecordObjects();
            }
        }
    }

    static Rect GetRightRectWithOffset(Rect rect, int offset)
    {
        var newRect = new Rect(rect);
        newRect.width = newRect.height;
        newRect.x = rect.x + rect.width - (rect.height * offset) - 8;
        return newRect;
    }
}
