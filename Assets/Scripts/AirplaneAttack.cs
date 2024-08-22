using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AirplaneAttack : MonoBehaviour
{
    [SerializeField] GameObject enemyProjectile;
    [SerializeField] float attackTime = 2;

    public GameObject player;
    GameObject attackLevel;
    float time = 2;
    // Start is called before the first frame update
    void Start()
    {
        attackLevel = GameObject.FindWithTag("Level");
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= attackTime && player != null)
        {
            var projectile = Instantiate(enemyProjectile, transform.position, Quaternion.Euler(0, 0, 0));
            projectile.transform.SetParent(attackLevel.transform);

            time = 0;
        }
    }
}
