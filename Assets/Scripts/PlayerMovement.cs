using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float _Speed = 10f;
    float LeftBoundary, RightBoundary;

    public float TurretAngleChangeDelta = 5f;
    float TurretAngle = 0f;
    public float rightEdgeWorldPositionX;
    public float leftEdgeWorldPositionX;

    public GameObject TurretRotationPivot;
    public GameObject level;

    public int LivesLeft = 4;

    BoxCollider2D _collider;
    SpriteRenderer _spriteRenderer;
    [SerializeField]SpriteRenderer _barrelSpriteRenderer;

    public static PlayerMovement Instance;
    private void Awake()
    {
        Instance = this;
        rightEdgeWorldPositionX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, Camera.main.nearClipPlane)).x;
        leftEdgeWorldPositionX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, Camera.main.nearClipPlane)).x;

        LeftBoundary = leftEdgeWorldPositionX + 0.5f;
        RightBoundary = rightEdgeWorldPositionX - 0.5f;

        _collider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x < LeftBoundary) ScreenWrap(RightBoundary);
            transform.Translate(new Vector3(-_Speed * Time.deltaTime, 0, 0));
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x > RightBoundary) ScreenWrap(LeftBoundary);
            transform.Translate(new Vector3(_Speed * Time.deltaTime, 0, 0));
        }

        /////TURNING/////

        //if(PlayerGoingLeft)
        //{
        //    transform.localScale = Vector3.one;
        //}
        //else
        //{
        //    transform.localScale = new Vector3(-1,1,1);
        //}

        /////TURRET MOVEMENT/////
        if (Input.GetKey(KeyCode.RightArrow))
        {
            TurretAngle -= TurretAngleChangeDelta * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            TurretAngle += TurretAngleChangeDelta * Time.deltaTime;
        }

        TurretRotationPivot.transform.localEulerAngles = new Vector3(0, 0, TurretAngle);
    }

    private void ScreenWrap(float BoundaryPoint)
    {
        transform.position = new Vector3(BoundaryPoint, transform.position.y, 0);
    }

    public void PlayerHit()
    {
        LivesLeft--;

        if (LivesLeft < 0) return;

        _collider.enabled = false;
        StartCoroutine(TurnOnCollider());
        BlinkRoutine = StartCoroutine(BlinkPlayer());

        if (LivesLeft == 0)
        {
            GameOver();
        }
        else
        { ScoreBoard.Inst.LowerHealth(LivesLeft - 1); }
    }

    private void GameOver()
    {
        //See if player made highscore
        //if they did player pref it

        ScoreBoard.Inst.CheckIfNewHiScore();

        level.SetActive(false);
        GameOverMenu.instance.gameObject.SetActive(true);
        GameOverMenu.instance.SetupUITextForGameEnd();
        ScoreBoard.Inst.gameObject.SetActive(false);
    }

    IEnumerator TurnOnCollider()
    {
        yield return new WaitForSeconds(2.5f);
        _collider.enabled = true;
        StopCoroutine(BlinkRoutine);
    }

    Coroutine BlinkRoutine;
    IEnumerator BlinkPlayer()
    {
        while (true)
        {
            _spriteRenderer.material.color = new Color(1, 1, 1, 0.5f);
            _barrelSpriteRenderer.material.color = new Color(1, 1, 1, 0.35f);
            
            yield return new WaitForSecondsRealtime(0.33f);
            
            _spriteRenderer.material.color = new Color(1, 1, 1, 1f);
            _barrelSpriteRenderer.material.color = new Color(1, 1, 1, 1f);
            
            yield return new WaitForSecondsRealtime(0.33f);
        }
    }
}
