using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;

    public float playerhp
    {
        get { return _playerhp; }
        set
        {
            _playerhp = value;
            GameObject.Find("HUD").transform.Find("Health").GetComponent<TMPro.TMP_Text>().text = value.ToString();
        }
    }
    float _playerhp = 10;

    public int money
    {
        get { return _money; }
        set
        {
            _money = value;
            GameObject.Find("HUD").transform.Find("Cash").GetComponent<TMPro.TMP_Text>().text = "$ " + value;
        }
    }
    int _money = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {

        if (_playerhp <= 0)
        {
            SceneManager.LoadScene("dead");
        }
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Healthpickup")
        {
            if(money >= 600)
            {
                playerhp += 3;
                money -= 600;
                Debug.Log("hp up");
            }
        }
        if (other.name == "upgrade")
        {
            if (money >= 4000)
            {
                gameObject.GetComponent<Shooting>().cooldown = 0.2f;
                money -= 4000;
            }
        }
    }
}
