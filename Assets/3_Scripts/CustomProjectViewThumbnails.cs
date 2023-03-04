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
                // Calculate the preview rectangle
                Rect previewRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width, selectionRect.height - 12f);

                // Draw the sprite in the preview rectangle
                EditorGUI.DrawTextureTransparent(previewRect, spriteRenderer.sprite.texture);
            }
            else
            {
                // If the GameObject does not have a SpriteRenderer or its SpriteRenderer does not have a sprite, draw the default preview thumbnail
                //EditorGUI.ObjectPreview(previewRect, go);
            }
        }
    }
}