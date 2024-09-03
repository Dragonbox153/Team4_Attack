using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public float spawnY = 5;
    public float spawnX;
    [SerializeField] int spawnPeriod = 6;
    [SerializeField] float time = 0;

    [SerializeField] GameObject[] waterEnemies;
    public GameObject[] enemies;
    GameObject attackLevel;

    int stage = 1;

    public static EnemySpawner instance;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnX = PlayerMovement.Instance.rightEdgeWorldPositionX - 0.5f;
        attackLevel = GameObject.FindWithTag("Level");
    }

    // Update is called once per frame
    void Update()
    {
        int enemyType = 0;
        float randomX = 0, randomY = 0;

        if (time % spawnPeriod < Time.deltaTime)
        {
            if (GameManager.Instance._baseTimeElapsed > GameManager.Instance._startTimeElapsed * GameManager.Instance.DayNightCycleSpeedDelta)
            {
                enemyType = Random.Range(0, 3);
                if (stage == 4)
                {
                    stage++;
                    enemyType = 2;
                }
            }
            else if (GameManager.Instance._baseTimeElapsed > GameManager.Instance._startTimeElapsed * GameManager.Instance.DayNightCycleSpeedDelta / 2)
            {
                enemyType = Random.Range(0, 2);
                if (stage == 2)
                {
                    stage++;
                    enemyType = 1;
                }
            }

            GameObject enemy = Instantiate(enemies[enemyType]);

            switch (enemyType)
            {
                case 0:
                    randomX = FlipX();
                    if (randomX < 0)
                    {
                        enemy.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    randomY = Random.Range(GameManager.Instance.CurrentTideLevel + 1, spawnY + 1);
                    break;
                case 1:
                    randomY = Random.Range(GameManager.Instance.CurrentTideLevel + 1, spawnY + 1); // the plus 1 is so that it includes spawnY in the range
                    if (randomY == spawnY)
                    {
                        randomX = Random.Range(-spawnX + 3, spawnX - 3);
                    }
                    else
                    {
                        randomX = FlipX();
                    }
                    break;
                default:
                    randomY = spawnY;
                    randomX = Random.Range(-spawnX + 3, spawnX - 3);
                    break;
            }

            enemy.transform.position = new Vector3(randomX, randomY, 0);
            enemy.GetComponent<EnemySpriteBehaviour>().spriteRenderer.flipX = (randomX > 0) ? false : true; 
            enemy.transform.SetParent(this.transform);

            // water enemys

            if (GameManager.Instance._baseTimeElapsed > GameManager.Instance._startTimeElapsed * GameManager.Instance.DayNightCycleSpeedDelta * 3 / 4)
            {
                enemyType = Random.Range(0, 2);
                if (stage == 3)
                {
                    stage++;
                    enemyType = 1;
                }
            }
            else if (GameManager.Instance._baseTimeElapsed > GameManager.Instance._startTimeElapsed * GameManager.Instance.DayNightCycleSpeedDelta /4)
            {
                if (stage == 1)
                {
                    stage++;
                    enemyType = 0;
                }
            }

            if (GameManager.Instance._baseTimeElapsed > GameManager.Instance._startTimeElapsed * GameManager.Instance.DayNightCycleSpeedDelta / 4)
            {
                GameObject waterEnemy = Instantiate(waterEnemies[enemyType]);

                if (enemyType == 1)
                {
                    randomX = Random.Range(-spawnX + 3, spawnX - 3);
                    randomY = -12f;
                }
                else
                {
                    randomX = FlipX();
                    randomY = Random.Range(-spawnY, GameManager.Instance.CurrentTideLevel - 1);
                }

                if (randomX < 0)
                {
                    waterEnemy.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
                }

                waterEnemy.transform.position = new Vector2(randomX, randomY);
                waterEnemy.GetComponent<EnemySpriteBehaviour>().spriteRenderer.flipX = (randomX > 0) ? true : false;
                waterEnemy.transform.SetParent(attackLevel.transform);
            }
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
            return spawnX;
        }
    }
}
