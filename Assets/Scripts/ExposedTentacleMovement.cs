using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExposedTentacleMovement : MonoBehaviour
{
    [SerializeField] float speed = 0.2f;
    [SerializeField] EnemySpawner spawner;

    [SerializeField] GameObject enemyFallen;

    [SerializeField] Transform DeathSpawnPoints;

    ObjectShaker shaker;
    
    GameObject player;


    [SerializeField] int TentacleHealth = 5;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        shaker = GetComponent<ObjectShaker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (transform.position.y < GameManager.Instance.CurrentTideLevel - 11)
            {
                transform.position = (Vector2)transform.position + (Vector2)transform.up * speed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovement.Instance.PlayerHit();
            TakeDamage();
        }

        if (collision.gameObject.tag == "Projectile")
        {
            TakeDamage();
        }
        if(collision.gameObject.tag == "FallenEnemy")
        {
            TakeDamage();
            Destroy(collision);
        }
    }

    private void TakeDamage()
    {
        TentacleHealth--;
        shaker.ShakeObject();

        if(TentacleHealth == 0)
        {
            Destroy(gameObject);

            for (int i = 0; i < DeathSpawnPoints.transform.childCount; i++)
            {
                Instantiate(enemyFallen, DeathSpawnPoints.transform.GetChild(i).position, Quaternion.Euler(0, 0, 0));
            }

            GameManager.Instance.numTentaclesSpawned--;
        }
    }
}
