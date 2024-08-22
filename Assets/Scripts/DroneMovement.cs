using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            if (transform.position.y < -5)
            {
                Destroy(gameObject);
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
