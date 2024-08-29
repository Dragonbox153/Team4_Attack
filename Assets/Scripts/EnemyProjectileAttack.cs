using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AirplaneAttack : MonoBehaviour
{
    [SerializeField] GameObject enemyProjectile;
    [SerializeField] float attackTime = 2;
    [SerializeField] float extraAngle;

    public GameObject player;
    GameObject attackLevel;
    float time = 1;
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
            float attackAngle = (180 / Mathf.PI) * Mathf.Atan((transform.position.y - player.transform.position.y) / (transform.position.x - player.transform.position.x));
            if (player.transform.position.x > transform.position.x) 
            { 
                attackAngle += extraAngle;
            }
            var projectile = Instantiate(enemyProjectile, transform.position, Quaternion.Euler(0, 0, attackAngle));
            projectile.transform.SetParent(attackLevel.transform);

            time = 0;
        }
    }
}
