using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleMovement : MonoBehaviour
{
    [SerializeField] float enemySpeed = 0.5f;
    Vector2 playerPosition;

    GameObject player;
    [SerializeField] GameObject enemyFallen;
    [SerializeField] WaterEnemySpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && transform.position.y < GameManager.Instance.CurrentTideLevel / 2) 
        {
            transform.position = (Vector2)transform.position + Vector2.up * enemySpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
            var fallenEnemy = Instantiate(enemyFallen, transform.position, Quaternion.Euler(0, 0, 0));
            fallenEnemy.transform.parent = player.transform.parent;
        }
        
    }
}
