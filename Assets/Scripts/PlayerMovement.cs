using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float _Speed = 10f;
    public GameObject ShootingPoint;
    public float LeftBoundary, RightBoundary;

    private void Start()
    {
        float rightEdgeWorldPositionX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, Camera.main.nearClipPlane)).x;
        float leftEdgeWorldPositionX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, Camera.main.nearClipPlane)).x;

        LeftBoundary = leftEdgeWorldPositionX + 0.5f;
        RightBoundary = rightEdgeWorldPositionX - 0.5f;
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x < LeftBoundary) return;
            transform.Translate(new Vector3(-_Speed * Time.deltaTime, 0, 0));
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x > RightBoundary) return;
            transform.Translate(new Vector3(_Speed * Time.deltaTime, 0, 0));
        }
    }
}
