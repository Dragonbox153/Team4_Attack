using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public float spawnY = 5;
    [SerializeField] float spawnX = 11;
    [SerializeField] int spawnPeriod = 6;
    [SerializeField] float time = 6;

    public GameObject[] enemies;
    GameObject attackLevel;
    // Start is called before the first frame update
    void Start()
    {
        attackLevel = GameObject.FindWithTag("Level");
    }

    // Update is called once per frame
    void Update()
    {
        // if time is over 6 seconds
        if(time >= spawnPeriod)
        {
            float randomY = Random.Range(0,  spawnY);
            float randomX = spawnX;
            int enemyType = Random.Range(0, enemies.Length + 1);

            var enemy = Instantiate(enemies[Random.Range(0, enemies.Length)]);

            switch(enemyType) { 
                case 0:
                    randomX = FlipX();
                    if (randomX < 0)
                    {
                        enemy.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    randomY = Random.Range(GameManager.Instance.CurrentTideLevel, spawnY);
                    break;
                case 1:
                    randomY = Random.Range(GameManager.Instance.CurrentTideLevel + 3, spawnY + 1); // the plus 1 is so that it includes spawnY in the range
                    if (randomY == spawnY)
                    {
                        randomX = Random.Range(-spawnX, spawnX);
                    }
                    else
                    {
                        randomX = FlipX();
                    }
                    break;
                default:
                    randomY = spawnY;
                    randomX = Random.Range(-spawnX + 1, spawnX - 1);
                    break;
             }
            enemy.transform.position = new Vector2 (randomX, randomY);
            enemy.transform.SetParent(attackLevel.transform);

            time = 0;
        }

        time += Time.deltaTime;
    }

    float FlipX()
    {
        if (Random.Range(0, 2) == 0)
        {
            return -spawnX;
        }
        else
        {
            return -spawnX;
        }
    }
}
