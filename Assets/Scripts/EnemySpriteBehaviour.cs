using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteBehaviour : MonoBehaviour
{
    [SerializeField] public SpriteRenderer spriteRenderer;

    public Sprite DeadEnemySprite;

    void Awake()
    {
        
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
