using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    // Switching sprites when object is lifted fixes a bug where sprites do not display correctly over other sprites when tilted.
    
    public static void SetHighSprite(SpriteRenderer spriteRenderer, float heightFromGround)
    {
        Vector3 skewedHeight = new Vector3(0, heightFromGround, heightFromGround);
        float yPivot = -Vector3.Distance(Vector3.zero, skewedHeight);
        yPivot /= spriteRenderer.transform.lossyScale.x;
        yPivot /= spriteRenderer.sprite.texture.height / 100f;
        var newPivot = new Vector2(0.5f, yPivot);
        spriteRenderer.sprite = Sprite.Create(spriteRenderer.sprite.texture, spriteRenderer.sprite.rect, newPivot);
        spriteRenderer.transform.localPosition = Vector3.zero;
        spriteRenderer.transform.position -= skewedHeight;
    }

    public static void SetHighSprite(Item item, float heightFromGround)
    {
        SetHighSprite(item.GetComponentInChildren<SpriteRenderer>(), heightFromGround);
    }

    public static void SetHighSprite(Transform t, float heightFromGround)
    {
        SetHighSprite(t.GetComponentInChildren<SpriteRenderer>(), heightFromGround);
    }

    public static void ResetHighSprite(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.sprite = Sprite.Create(spriteRenderer.sprite.texture, spriteRenderer.sprite.rect, new Vector2(0.5f, 0));
        spriteRenderer.transform.localPosition = Vector3.zero;
    }

    public static void ResetHighSprite(Item item)
    {
        ResetHighSprite(item.GetComponentInChildren<SpriteRenderer>());
    }
}
