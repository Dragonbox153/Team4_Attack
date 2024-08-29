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

    public GameObject[] enemies;
    GameObject attackLevel;
    WaterEnemySpawner waterSpawner;

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
            if (GameManager.Instance._baseTimeElapsed > GameManager.Instance._startTimeElapsed * GameManager.Instance.DayNightCycleSpeedDelta * 1.25f)
            {
                enemyType = Random.Range(0, 4);
            }
            else if (GameManager.Instance._baseTimeElapsed > GameManager.Instance._startTimeElapsed * GameManager.Instance.DayNightCycleSpeedDelta)
            {
                enemyType = Random.Range(0, 3);
            }
            if (GameManager.Instance._baseTimeElapsed > GameManager.Instance._startTimeElapsed * GameManager.Instance.DayNightCycleSpeedDelta / 2)
            {
                 enemyType = Random.Range(0, 2);
            }

            if (GameManager.Instance._startTimeElapsed * GameManager.Instance.DayNightCycleSpeedDelta * 1.25f - GameManager.Instance._baseTimeElapsed < spawnPeriod && GameManager.Instance._startTimeElapsed * GameManager.Instance.DayNightCycleSpeedDelta * 1.25f - GameManager.Instance._baseTimeElapsed > 0)
            {
                enemyType = 2;
            }
            else if (GameManager.Instance._startTimeElapsed * GameManager.Instance.DayNightCycleSpeedDelta - GameManager.Instance._baseTimeElapsed < spawnPeriod && GameManager.Instance._startTimeElapsed * GameManager.Instance.DayNightCycleSpeedDelta - GameManager.Instance._baseTimeElapsed > 0)
            {
                enemyType = 1;
            }
            if (GameManager.Instance._startTimeElapsed * GameManager.Instance.DayNightCycleSpeedDelta / 2 - GameManager.Instance._baseTimeElapsed < spawnPeriod && GameManager.Instance._startTimeElapsed * GameManager.Instance.DayNightCycleSpeedDelta / 2 - GameManager.Instance._baseTimeElapsed > 0)
            {
                enemyType = 0;
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
            enemy.transform.SetParent(this.transform);
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
