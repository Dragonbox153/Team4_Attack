using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteBehaviour : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public Sprite DeadEnemySprite;

    void Awake()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }


    public void EnemyDead(int SpriteNumber)
    {
        //spriteRenderer.sprite = DeadEnemySprites[SpriteNumber];
    }

    public void EnemyDead()
    {
        spriteRenderer.sprite = DeadEnemySprite;
    }
}
