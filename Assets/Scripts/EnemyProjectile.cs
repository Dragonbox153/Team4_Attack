using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float speed = 0.05f;

    Vector2 moveTowards = Vector2.zero;
    GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        moveTowards = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2)(transform.position) + moveTowards * speed * Time.deltaTime;

        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }    
    }
}
