﻿using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class BetterHierarchy
{
    private const string toggleStyleName = "OL Toggle";

    // ===============================================================================================

    private static readonly Dictionary<Type, string> iconOverrides = new Dictionary<Type, string>
    {
    };

    private static readonly HashSet<Type> importantList = new HashSet<Type>
    {
        typeof(Camera),
        typeof(Rigidbody2D),
        typeof(Rigidbody),
        typeof(TMPro.TextMeshProUGUI),
        typeof(TMPro.TextMeshPro),
        typeof(Collider),
        typeof(Collider2D),
        typeof(Renderer),
        typeof(CanvasRenderer)
    };

    private static readonly HashSet<Type> blacklist = new HashSet<Type>
    {
        typeof(Transform),
        typeof(RectTransform)
    };


    // ===============================================================================================

    static BetterHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI = DrawItem;
    }

    static void DrawItem(int instanceID, Rect rect)
    {
        // Get's object for given item
        GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (go != null)
        {
            bool isHeader = go.name.StartsWith("---");

            if (isHeader)
            {
                DrawHeader(rect, go);
            }
            if (!isHeader || go.transform.childCount > 0)
            {
                DrawActivityToggle(rect, go);
            }
            DrawComponentIcons(rect, go);
        }
    }

    private static void DrawHeader(Rect rect, GameObject go)
    {
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

    private static void DrawComponentIcons(Rect rect, GameObject go)
    {
        Dictionary<Texture, int> usedIcons = new Dictionary<Texture, int>();
        List<(Texture texture, bool important)> iconsToDraw = new List<(Texture icon, bool important)>();

        foreach (var component in go.GetComponents<Component>())
        {
            Type type = component.GetType();

            if (blacklist.Contains(type))
                continue;

            Texture texture = GetIconFor(component, type);
            bool important = CheckTypeResursive(type, importantList);

            if (usedIcons.TryGetValue(texture, out int index))
            {
                var icon = iconsToDraw[index];
                icon.important |= important;
                iconsToDraw[index] = icon;
            }
            else
            {
                iconsToDraw.Add((texture, important));
                usedIcons.Add(texture, iconsToDraw.Count - 1);
            }
        }

        for (int i = 0; i < iconsToDraw.Count; i++)
        {
            (Texture texture, bool important) = iconsToDraw[i];
            Color tint = important
                ? new Color(1, 1, 1, 1)
                : new Color(0.8f, 0.8f, 0.8f, 0.25f);
            GUI.DrawTexture(GetRightRectWithOffset(rect, i), texture, ScaleMode.ScaleToFit, true, 0, tint, 0, 0);
        }
    }

    private static bool CheckTypeResursive(Type t, HashSet<Type> set)
    {
        if (set.Contains(t))
            return true;

        if (t.BaseType == null)
            return false;

        return CheckTypeResursive(t.BaseType, set);
    }

    private static Texture GetIconFor(Component c, Type type)
    {
        return iconOverrides.TryGetValue(type, out string icon)
            ? EditorGUIUtility.IconContent(icon).image
            : EditorGUIUtility.ObjectContent(c, type).image;
    }

    private static void DrawActivityToggle(Rect rect, GameObject go)
    {
        // Get's style of toggle
        GUIStyle toggleStyle = toggleStyleName;

        // Sets rect for toggle
        var toggleRect = new Rect(rect);
        toggleRect.width = toggleRect.height;
        toggleRect.x -= 28;

        // Creates toggle
        bool state = GUI.Toggle(toggleRect, go.activeSelf, GUIContent.none, toggleStyle);

        // Sets game's active state to result of toggle
        if (state != go.activeSelf)
        {
            Undo.RecordObject(go, $"{(state ? "Enabled" : "Disabled")}");
            go.SetActive(state);
            Undo.FlushUndoRecordObjects();
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
