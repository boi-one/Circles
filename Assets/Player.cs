using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bullet;
    public Transform firepoint;
    public float speed;
    float lastShot;
    public float cooldown;

    bool bruh = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time - lastShot < cooldown)
            {
                return;
            }
            else 
            {
                GameObject Bullet = Instantiate(bullet, firepoint.position, firepoint.rotation);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
        }
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos); //kijkt waar muis is
        Vector2 Direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y); //draait de object waar dit script op zit
        transform.up = Direction; //weet niet waarom dit up is maar alles word fucked als je position gebruikt
    }
}
