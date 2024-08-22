using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteBehaviour : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public List<Sprite> EnemySprites;
    public List<Sprite> DeadEnemySprites;

    public int spriteNumber;


    void Awake()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void SelectRandomSprite()
    {
        spriteNumber = UnityEngine.Random.Range(0, 2);
        spriteRenderer.sprite = EnemySprites[spriteNumber];
    }

    public void EnemyDead(int SpriteNumber)
    {
        spriteRenderer.sprite = DeadEnemySprites[SpriteNumber];
    }
}
