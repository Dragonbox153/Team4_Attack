using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MermanMovement : MonoBehaviour
{
    [SerializeField] float enemySpeed = 2f;
    Vector2 enemySpawnPoint;
    Vector2 enemyTargetPoint;

    GameObject player;
    [SerializeField] GameObject enemyFallen;
    [SerializeField] WaterEnemySpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawnPoint = transform.position;
        player = GameObject.Find("Player");
        if (GameManager.Instance != null) 
        {
            float randomX = Random.Range(-spawner.spawnX, spawner.spawnX);
            float randomY = Random.Range(spawner.spawnY, GameManager.Instance.CurrentTideLevel);
            enemyTargetPoint = new Vector2(randomX, randomY);
        }
        else
        {
            enemyTargetPoint = new Vector2 (0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {

            //move towards Target
            if ((Vector2)transform.position != enemyTargetPoint)
            {
                Vector2.MoveTowards((Vector2)transform.position, enemyTargetPoint, enemySpeed);
            }
            // if the water rises, rise an amount based on how low plane is
            if (GameManager.Instance.movingUp == true)
            {
                enemyTargetPoint = new Vector2(enemyTargetPoint.x, enemyTargetPoint.y + ((spawner.spawnY - transform.position.y - 1) / (spawner.spawnY - GameManager.Instance.CurrentTideLevel)) * Time.deltaTime);
            }
            else if (GameManager.Instance.movingDown == true)
            {
                enemyTargetPoint = new Vector3(enemyTargetPoint.x, enemyTargetPoint.y - ((spawner.spawnY - transform.position.y) / (spawner.spawnY - GameManager.Instance.CurrentTideLevel)) * Time.deltaTime);
            }
        }
    }
}
