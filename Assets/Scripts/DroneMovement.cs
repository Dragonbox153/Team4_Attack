using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DroneMovement : MonoBehaviour
{
    [SerializeField] float enemySpeed = 0.05f;
    [SerializeField] public float divePoint = 1;
    Vector2 enemySpawnPoint;
    Vector2 playerPosition;

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
            playerPosition = player.transform.position;

            //move towards player
            if (transform.position.x > playerPosition.x)
            {
                transform.position = (Vector2)(transform.position) + new Vector2(-enemySpeed * Time.deltaTime, 0);
            }
            else if (transform.position.x < playerPosition.x)
            {
                transform.position = (Vector2)(transform.position) + new Vector2(enemySpeed * Time.deltaTime, 0);
            }

            // dive at player when close enough
            if (transform.position.x >= playerPosition.x - divePoint && transform.position.x <= playerPosition.x + divePoint && transform.position.y > playerPosition.y)
            {
                transform.position = (Vector2)(transform.position) + new Vector2(0, (-enemySpeed / 2) * Time.deltaTime);
            }

            if (transform.position.y < -5 || transform.position.x > spawner.spawnX || transform.position.x < -spawner.spawnX)
            {
                Destroy(gameObject);
            }

            // if the water rises, rise an amount based on how low Drone is
            if (GameManager.Instance.movingUp == true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + ((spawner.spawnY - transform.position.y + 1) / (spawner.spawnY - GameManager.Instance.CurrentTideLevel)) * Time.deltaTime, transform.position.z);
            }
            else if (GameManager.Instance.movingDown == true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - ((spawner.spawnY - transform.position.y + 1) / (spawner.spawnY - GameManager.Instance.CurrentTideLevel)) * Time.deltaTime, transform.position.z);
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
        else if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
