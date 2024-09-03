using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MermanMovement : MonoBehaviour
{
    [SerializeField] float enemySpeed = 2f;
    Vector2 enemySpawnPoint;
    [SerializeField]Vector3 enemyTargetPoint = Vector3.zero;

    GameObject player;
    [SerializeField] GameObject enemyFallen;
    [SerializeField] EnemySpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnPoint = transform.position;
        player = GameObject.Find("Player");
        if (GameManager.Instance != null) 
        {
            float randomX = Random.Range(-spawner.spawnX, spawner.spawnX);
            float randomY = Random.Range(-spawner.spawnY + 1.5f, GameManager.Instance.CurrentTideLevel - 0.5f);
            enemyTargetPoint = new Vector3(randomX, randomY, transform.position.z);
        }
        else
        {
            enemyTargetPoint = new Vector3 (0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {

            //move towards Target
            if (Vector2.Distance((Vector2)transform.position, (Vector2)enemyTargetPoint) > 1)
            {
                transform.position = transform.position + new Vector3((enemyTargetPoint.x - transform.position.x) * enemySpeed * Time.deltaTime, (enemyTargetPoint.y - transform.position.y) * Time.deltaTime, 0);
            }
            else 
            { 
                enemySpeed = 4;
            }

            // if the water rises, rise an amount based on how low merman is
            if (GameManager.Instance.movingUp == true)
            {
                enemyTargetPoint = new Vector3(enemyTargetPoint.x, enemyTargetPoint.y + ((-spawner.spawnY - enemyTargetPoint.y + 1) / (-spawner.spawnY - GameManager.Instance.CurrentTideLevel)) * Time.deltaTime, enemyTargetPoint.z);
            }
            else if (GameManager.Instance.movingDown == true)
            {
                enemySpeed = 4;
                enemyTargetPoint = new Vector3(enemyTargetPoint.x, enemyTargetPoint.y - ((-spawner.spawnY - enemyTargetPoint.y - 2) / (-spawner.spawnY - GameManager.Instance.CurrentTideLevel)) * Time.deltaTime, enemyTargetPoint.z);
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
