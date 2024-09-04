using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour
{
    public bool BaseBG = true;


    // Update is called once per frame
    void Update()
    {
        transform.position = (BaseBG) ? new Vector3(transform.position.x, GameManager.Instance.CurrentTideLevel - 12.7f, transform.position.z) : new Vector3(transform.position.x, GameManager.Instance.CurrentTideLevel - 10.5f, transform.position.z);
    }
}
