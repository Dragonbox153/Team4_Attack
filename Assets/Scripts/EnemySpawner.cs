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
<<<<<<< Updated upstream
                randomX = -spawnX;
=======
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
>>>>>>> Stashed changes
            }
            var enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], new Vector3(randomX, randomY, 0), Quaternion.Euler(0, 0, 0));
            enemy.transform.SetParent(attackLevel.transform);

            time = 0;
        }

        time += Time.deltaTime;
    }
}
