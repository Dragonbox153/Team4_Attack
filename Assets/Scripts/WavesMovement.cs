using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesMovement : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer;

    public float _speed = 1;

    public float _TileOffSet = 2;

    // Update is called once per frame
    void Update()
    {
        _TileOffSet += Time.deltaTime * _speed;
        if (_TileOffSet < 2) _TileOffSet = 2;

        _spriteRenderer.size = new Vector2(_TileOffSet, 0.85714f);

        if (_TileOffSet > 100) _TileOffSet = 2;
    }
}
