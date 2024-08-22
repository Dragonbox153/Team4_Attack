using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UFOMovement : MonoBehaviour
{
    bool goingDown = true;
    float speed = 2;
    [SerializeField] EnemySpawner spawner;
    [SerializeField] GameObject enemyFallen;

    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

        // if object reaches top or bottom of move area, switch directions
        if(transform.position.y <= GameManager.Instance.CurrentTideLevel)
        {
            goingDown = false;
        }
        else if(transform.position.y >= spawner.spawnY)
        {
            goingDown = true;
        }

        if(goingDown)
        {
            transform.position = (Vector2)transform.position - (Vector2)transform.up * speed * Time.deltaTime;
        }
        else
        {
            transform.position = (Vector2)transform.position + (Vector2)transform.up * speed * Time.deltaTime;
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
