using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float _Speed = 10f;
    public GameObject ShootingPoint;
    public float LeftBoundary, RightBoundary;

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
