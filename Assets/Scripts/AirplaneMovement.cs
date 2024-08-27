using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AirplaneMovement : MonoBehaviour
{
    [SerializeField] float enemySpeed = 2f;
    Vector2 enemySpawnPoint;

    GameObject player;
    [SerializeField] GameObject enemyFallen;
    [SerializeField] EnemySpawner spawner;

    // on Start
    private void Start()
    {
        enemySpawnPoint = transform.position;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {

            //move towards player
            if (enemySpawnPoint.x < 0)
            {
            transform.position = (Vector2)(transform.position) + new Vector2(enemySpeed * Time.deltaTime, 0);
            }
            else
            {
                transform.position = (Vector2)(transform.position) - new Vector2(enemySpeed * Time.deltaTime, 0);
            }

            if (transform.position.x > spawner.spawnX || transform.position.x < -spawner.spawnX)
            {
                EnemySpawner.instance.liveEnemies.Remove(this.gameObject);
                Destroy(gameObject);
            }


            // if the water rises, rise an amount based on how low plane is
            if (GameManager.Instance.movingUp == true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + ((spawner.spawnY - transform.position.y + 1) / (spawner.spawnY - GameManager.Instance.CurrentTideLevel)) * Time.deltaTime, transform.position.z);
            }
            else if(GameManager.Instance.movingDown == true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - ((spawner.spawnY - transform.position.y) / (spawner.spawnY - GameManager.Instance.CurrentTideLevel)) * Time.deltaTime, transform.position.z);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
            var fallenEnemy = Instantiate(enemyFallen, transform.position, Quaternion.Euler(0, 0, 0));
            fallenEnemy.transform.parent = player.transform.parent;
        }
    }
}
