using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float cooldown;
    private float nextfire = 0;
    public GameObject bulletPrefab;
    public Transform firepoint;
    public float bulletSpeed = 50;
    public Rigidbody2D rb;

    Vector2 dir;
    float angle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextfire)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
                nextfire = Time.time + cooldown;
            }
        }
        
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 dir = mousePos - rb.position;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * bulletSpeed, ForceMode2D.Impulse);
    }
}
