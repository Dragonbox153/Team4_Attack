using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerShoot : MonoBehaviour
{
    public GameObject ShootPoint;
    public GameObject Projectile;

    public GameObject ProjectileShot;
    public Projectile _proj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootProjectile();
        }


    }

    private void ShootProjectile()
    {
        ProjectileShot = Instantiate(Projectile, transform.position, Quaternion.identity);
        _proj = ProjectileShot.GetComponent<Projectile>();

        _proj.Launch(ShootPoint.transform.position - transform.position);
    }
}
