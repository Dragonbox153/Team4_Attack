using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TentacleTop : MonoBehaviour
{
    [SerializeField] GameObject enemyFallen;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            var fallenEnemy = Instantiate(enemyFallen, transform.position, Quaternion.Euler(0, 0, 0));
            fallenEnemy.transform.parent = player.transform.parent;
        }
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
