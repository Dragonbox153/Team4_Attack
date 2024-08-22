using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int spawnY = 5;
    [SerializeField] int spawnX = 11;
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
            int randomY = Random.Range(0,  spawnY);
            int randomX = spawnX;
            if(Random.Range(0, 2) == 0)
            {
                randomX = -spawnX;
            }
            var enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(randomX, randomY, 0), Quaternion.Euler(0, 0, 0));
            enemy.transform.SetParent(attackLevel.transform);

            time = 0;
        }

        time += Time.deltaTime;
    }
}
