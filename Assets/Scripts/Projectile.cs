using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed = 0;
    public bool Moving = false;
    public Vector3 MoveDir = Vector3.zero;

    private void Start()
    {
        
    }

    public void Launch(Vector3 _moveDir)
    {
        _speed = 10f;
        MoveDir = _moveDir;
        Moving = true;
        Destroy(this.gameObject, 2f);
    }
    
    void Update()
    {
        if (Moving)
        { 
           transform.Translate(MoveDir.normalized * _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemySpawner.instance.liveEnemies.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
