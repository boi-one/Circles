using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //if (Physics2D.OverlapPointAll(transform.position + new Vector3(-speed, 0, 0) * Time.deltaTime).Any(c => c.name.StartsWith("Wall")) == false)
                transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //if (Physics2D.OverlapPointAll(transform.position + new Vector3(speed, 0, 0) * Time.deltaTime).Any(c => c.name.StartsWith("Wall")) == false)
                transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            //if (Physics2D.OverlapPointAll(transform.position + new Vector3(0, speed, 0) * Time.deltaTime).Any(c => c.name.StartsWith("Wall")) == false)
                transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //if (Physics2D.OverlapPointAll(transform.position + new Vector3(0, -speed, 0) * Time.deltaTime).Any(c => c.name.StartsWith("Wall")) == false)
                transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
        }
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos); //kijkt waar muis is
        Vector2 Direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y); //draait de object waar dit script op zit
        transform.up = Direction; //weet niet waarom dit up is maar alles word fucked als je position gebruik
        //Debug.Log("X " + mousePos.x + "Y " + mousePos.y);
    }
    /*
    public void Shoot()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        GameObject bullet = Instantiate(bulletprefab, firepoint.position, firepoint.rotation);
        bullet.GetComponent<BulletMovement>().targetDir =(bullet.transform.position += (Camera.main.ScreenToWorldPoint(Input.mousePosition) - bullet.transform.position));   
    }
    */
}
