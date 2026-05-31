using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public static class EditorUtil
{
    public static void DrawLabel(string label, Color bgColor, Color textColor, int fontSize = 12)
    {
        GUIStyle style = new();
        style.normal.textColor = textColor;
        style.fontSize = fontSize;

        Vector2 size = style.CalcSize(new GUIContent(label));
        Rect rect = GUILayoutUtility.GetRect(size.x, size.y);
        EditorGUI.DrawRect(rect, bgColor);

        EditorGUI.LabelField(rect, label, style);
    }

    public static void DrawIcons(List<Sprite> sprites, int width = 10, int dimension = 42, List<string> tooltips = null)
    {
        EditorGUILayout.Space(20);

        int counter = 0;
        GUILayout.BeginHorizontal();
            foreach(int i in 0.To(sprites.Count - 1))
            {
                if(sprites[i] == null)
                    continue;
                if(counter % width == 0)
                {
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                } 
                var texture = AssetPreview.GetAssetPreview(sprites[i]);
                string tooltip = "";
                try
                {
                    tooltip = tooltips[i];
                } catch {}
                var content = new GUIContent(texture, tooltip);
                GUILayout.Label(content, GUILayout.Width(dimension), GUILayout.Height(dimension));
                counter++;
            }
        GUILayout.EndHorizontal();
    }

    public static List<T> AssetsList<T>(string path) where T : Object
    {
        path = $"Assets/{path}";

        string[] assetGuids = AssetDatabase.FindAssets("", new[] {path});
        var assets = assetGuids
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<T>)
            .Where(asset => asset != null)
            .ToList();

        return assets;
    }
}
