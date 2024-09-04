using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDestroyer : MonoBehaviour
{
    [SerializeField] float destroyTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
