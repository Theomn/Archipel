using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class CustomProjectViewThumbnails
{
    static CustomProjectViewThumbnails()
    {
        EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemOnGUI;
    }

    private static void OnProjectWindowItemOnGUI(string guid, Rect selectionRect)
    {
        string assetPath = AssetDatabase.GUIDToAssetPath(guid);

        if (AssetDatabase.LoadAssetAtPath<Object>(assetPath) is GameObject go && PrefabUtility.IsPartOfAnyPrefab(go))
        {
            SpriteRenderer spriteRenderer = go.GetComponentInChildren<SpriteRenderer>();

            if (spriteRenderer != null && spriteRenderer.sprite != null)
            {
                Rect previewRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width, selectionRect.height - 12f);
                EditorGUI.DrawTextureTransparent(previewRect, spriteRenderer.sprite.texture);
            }
        }
    }
}