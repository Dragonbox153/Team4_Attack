using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneMovement : MonoBehaviour
{
    [SerializeField] float enemySpeed = 0.02f;
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            var fallenEnemy = Instantiate(enemyFallen, transform.position, Quaternion.Euler(0, 0, 0));
            fallenEnemy.transform.parent = player.transform.parent;
        }
    }

}
