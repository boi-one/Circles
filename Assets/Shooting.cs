using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Shooting : NetworkBehaviour
{
    public float cooldown;
    private float nextfire = 0;
    public GameObject bulletPrefab;
    public Transform firepoint;
    public float bulletSpeed = 50;
    public Rigidbody2D rb;
    public AudioSource gun;

    Vector2 dir;
    float angle;

    void Update()
    {
        if (isLocalPlayer == false) return;

        if(Time.time > nextfire)
        {
            if (Input.GetMouseButton(0))
            {
                CmdShoot();
                nextfire = Time.time + cooldown;
            }
        }
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 dir = mousePos - rb.position;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
    }



    [Command] void CmdShoot() => RpcShoot();
    [ClientRpc] void RpcShoot() => Shoot();
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * bulletSpeed, ForceMode2D.Impulse);
        gun.Play();
        
    }
}
