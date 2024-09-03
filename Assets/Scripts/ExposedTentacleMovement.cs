using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExposedTentacleMovement : MonoBehaviour
{
    [SerializeField] float speed = 0.2f;
    [SerializeField] EnemySpawner spawner;

    [SerializeField] GameObject enemyFallen;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (transform.position.y < GameManager.Instance.CurrentTideLevel - 6)
            {
                transform.position = -(Vector2)transform.position + (Vector2)transform.up * speed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerMovement.Instance.PlayerHit();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
            var fallenEnemy = Instantiate(enemyFallen, transform.position, Quaternion.Euler(0, 0, 0));
            fallenEnemy.transform.parent = player.transform.parent;
        }
    }
    
}
