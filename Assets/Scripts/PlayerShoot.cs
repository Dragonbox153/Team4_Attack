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

    public SpriteRenderer meter;

    public AudioSource playershoot;

    public int _ammoCount = 15;
    public int ammoCount 
    {
        get 
        { 
            return _ammoCount; 
        }
        set 
        {
            ScoreBoard.Inst.updateAmmoCountUI(15, value);
            _ammoCount = value; 
        }
    }
    public float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        //ScoreBoard.Inst.SetAmmoCount(ammoCount);

        if(ammoCount < 15 && time >= 1)
        {
            time = 0;
            ammoCount++;
        }

        if (Input.GetKeyDown(KeyCode.Space) && ammoCount > 0)
        {
            ShootProjectile();
            ammoCount--;
        }
    }

    private void ShootProjectile()
    {
        playershoot.Play();
        ProjectileShot = Instantiate(Projectile, transform.position, Quaternion.identity);
        _proj = ProjectileShot.GetComponent<Projectile>();
        _proj.Launch(ShootPoint.transform.position - transform.position, PlayerMovement.Instance.TurretRotationPivot.transform.rotation);
    }
}
