using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExposedTentacleMovement : MonoBehaviour
{
    [SerializeField] float speed = 0.2f;
    [SerializeField] WaterEnemySpawner spawner;

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
                transform.position = (Vector2)transform.position + (Vector2)transform.up * speed * Time.deltaTime;
            }

            // if the water lowers, lower an amount based on how low Tentacle is
            if (GameManager.Instance.movingDown == true)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + ((spawner.spawnY - transform.position.y + 1) / (spawner.spawnY - GameManager.Instance.CurrentTideLevel)) * Time.deltaTime, transform.position.z);
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
    }
}
