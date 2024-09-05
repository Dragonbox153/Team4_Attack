using System;
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

    public float LazerShootTime = 3f;
    public float LazerLength = 15;

    GameObject player;

    ObjectShaker shaker;

    public SpriteRenderer Lazer;

    public int UFOHealth = 2;

    private void Start()
    {
        player = GameObject.Find("Player");

        shaker = GetComponent<ObjectShaker>();

        StartCoroutine(ShootLazer());
    }

    private IEnumerator ShootLazer()
    {
        float ElapsedTime = 0;
        while (true) {

            float t = ElapsedTime / LazerShootTime;
            float length = Mathf.Lerp(0, LazerLength, t);

            Lazer.size = new Vector2(0.92f, length);

            ElapsedTime += Time.deltaTime;

            yield return null;
        }

    }

    // Update is called once per frame
    void Update()
    {
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
            if (UFOHealth > 0)
            {
                UFOHealth--;
                shaker.ShakeObject();
            }
            else
            {
                Destroy(gameObject);
                var fallenEnemy = Instantiate(enemyFallen, transform.position, Quaternion.Euler(0, 0, 0));
                fallenEnemy.transform.parent = player.transform.parent;
            }

        }
        else if (collision.gameObject.tag == "Player")
        {
            PlayerMovement.Instance.PlayerHit();
            Destroy(gameObject);
        }
    }
}
