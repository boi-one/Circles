using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public GameObject upgrade;
    public GameObject upgradetext;
    public GameObject speedupgrade;
    public GameObject speedupgradetext;

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


    public void Update()
    {
        if (_playerhp <= 0)
        {
            SceneManager.LoadScene("dead");
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (CheckIfAllowedPosition(transform.position + new Vector3(-speed, 0, 0) * Time.deltaTime))
                transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (CheckIfAllowedPosition(transform.position + new Vector3(speed, 0, 0) * Time.deltaTime))
                transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (CheckIfAllowedPosition(transform.position + new Vector3(0, speed, 0) * Time.deltaTime))
            transform.position += new Vector3(0, speed, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (CheckIfAllowedPosition(transform.position + new Vector3(0,-speed, 0) * Time.deltaTime))
            transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }


        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos); //kijkt waar muis is
        Vector2 Direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y); //draait de object waar dit script op zit
        transform.up = Direction; //weet niet waarom dit up is maar alles word fucked als je position gebruik
        //Debug.Log("X " + mousePos.x + "Y " + mousePos.y);
    }



    public static int UnlockedStages = 1;
    static bool CheckIfAllowedPosition(Vector3 pos)
    {
        List<GameObject> all = new List<GameObject>();
        for (int i = 1; i <= UnlockedStages; i++)
        {
            foreach(Transform t in GameObject.Find("Map").transform.Find("AreaDefiners").Find("Stage"+i))
            {
                all.Add(t.gameObject);
            }
        }
        if (all.Any(c => c.GetComponent<SpriteMask>().bounds.Contains(pos)))
            return true;
        return false;
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
                Destroy(upgrade);
                Destroy(upgradetext);
            }
        }
        if (other.name == "runfaster")
        {
            if(money >= 1000)
            {
                speed = 7;
                money -= 1000;
                Destroy(speedupgrade);
                Destroy(speedupgradetext);
            }
        }
    }
}
