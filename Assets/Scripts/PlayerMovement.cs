using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float _Speed = 10f;
    float LeftBoundary, RightBoundary;

    bool PlayerGoingLeft;

    public float TurretAngleChangeDelta = 5f;
    float TurretAngle = 0f;
    public float rightEdgeWorldPositionX;
    public float leftEdgeWorldPositionX;

    public GameObject TurretRotationPivot;



    public static PlayerMovement Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        rightEdgeWorldPositionX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, Camera.main.nearClipPlane)).x;
        leftEdgeWorldPositionX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, Camera.main.nearClipPlane)).x;

        LeftBoundary = leftEdgeWorldPositionX + 0.5f;
        RightBoundary = rightEdgeWorldPositionX - 0.5f;
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            PlayerGoingLeft = true;

            if (transform.position.x < LeftBoundary) return;
            transform.Translate(new Vector3(-_Speed * Time.deltaTime, 0, 0));
        }

        if (Input.GetKey(KeyCode.D))
        {
            PlayerGoingLeft=false;

            if (transform.position.x > RightBoundary) return;
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
        if (Input.GetKey(KeyCode.W))
        {
            TurretAngle -= TurretAngleChangeDelta * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            TurretAngle += TurretAngleChangeDelta * Time.deltaTime;
        }

        TurretRotationPivot.transform.localEulerAngles = new Vector3(0, 0, TurretAngle);
    }
}
