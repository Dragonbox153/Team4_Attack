using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public int spawnY = 5;
    [SerializeField] public int spawnX = 11;
    [SerializeField] int spawnPeriod = 6;
    float time = 100;

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
            int enemyType = Random.Range(0, enemies.Length);
            var enemy = Instantiate(enemies[enemyType]);
            int randomY = 0;
            int randomX = 0;

            switch (enemyType)
            {
                case 0:
                    randomX = FlipX();
                    if (randomX < 0)
                    {
                        enemy.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    randomY = Random.Range(0, spawnY);
                    break;
                case 1:
                    randomY = Random.Range(3, spawnY + 1); // the plus 1 is so that it includes spawnY in the range
                    if(randomY == spawnY)
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

            enemy.transform.position = new Vector2(randomX, randomY);
            enemy.transform.SetParent(attackLevel.transform);

            time = 0;
        }

        time += Time.deltaTime;
    }

    int FlipX()
    {
        if (Random.Range(0, 2) == 0)
        {
            return -spawnX;
        }
        else
        {
            return spawnX;
        }
    }
}
