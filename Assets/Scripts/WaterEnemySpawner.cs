using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEnemySpawner : MonoBehaviour
{
    [SerializeField] public float spawnY = -5;
    [SerializeField] public float spawnX = 11;
    [SerializeField] int spawnPeriod = 6;
    [SerializeField] float time = 6;

    [SerializeField] GameObject[] waterEnemies;
    GameObject attackLevel;

    // Start is called before the first frame update
    void Start()
    {
        attackLevel = GameObject.FindWithTag("Level");
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= spawnPeriod)
        {
            float randomX = spawnX;
            float randomY = spawnY;

            int enemyType = Random.Range(0, waterEnemies.Length);
            GameObject enemy = Instantiate(waterEnemies[Random.Range(0, enemyType)]);
            EnemySpawner.instance.liveEnemies.Add(enemy);

            if(enemyType == 0)
            {
                randomX = Random.Range(-spawnX + 1, spawnX - 1);
                randomY = -13f;
            }
            else
            {
                randomX = FlipX();
                randomY = Random.Range(spawnY, GameManager.Instance.CurrentTideLevel);
            }

            enemy.transform.position = new Vector2(randomX, randomY);
            enemy.transform.SetParent(attackLevel.transform);

            time = 0;
        }

        time += Time.deltaTime;
    }

    float FlipX()
    {
        if (Random.Range(0, 2) == 0)
        {
            return spawnX;
        }
        else
        {
            return -spawnX;
        }
    }
}
