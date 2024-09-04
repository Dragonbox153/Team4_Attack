using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TentacleMovement : MonoBehaviour
{
    [SerializeField] float speed = 0.2f;
    [SerializeField] EnemySpawner spawner;
    [SerializeField] GameObject enemyFallen;
    [SerializeField] GameObject exposedEnemy;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            if (transform.position.y < GameManager.Instance.CurrentTideLevel - 11)
            {
                transform.position = (Vector2)transform.position + (Vector2)transform.up * speed * Time.deltaTime;
            }
        }
    }

    public void SwapTentacle()
    {
        Destroy(gameObject);
        GameObject newTentacle = Instantiate(exposedEnemy, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), transform.rotation);
        newTentacle.transform.SetParent(EnemySpawner.instance.attackLevel.transform);
    }
}
