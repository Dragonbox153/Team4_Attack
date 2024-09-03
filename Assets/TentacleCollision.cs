using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(gameObject.transform.parent);
            //var fallenEnemy = Instantiate(enemyFallen, transform.position, Quaternion.Euler(0, 0, 0));
            //fallenEnemy.transform.parent = player.transform.parent;
        }
    }
}
