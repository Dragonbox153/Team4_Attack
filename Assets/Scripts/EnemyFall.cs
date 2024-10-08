using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyFall : MonoBehaviour
{
    [SerializeField] float fallSpeed = 2f;

    // Update is called once per frame
    void Update()
    {
        transform.position = (Vector2)(transform.position) + Vector2.down * fallSpeed * Time.deltaTime;

        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
