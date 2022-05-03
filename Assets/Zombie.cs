using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public int hp = 6;
    private float nextdamage = 0;
    public float damagecooldown;
    bool dead = false;

    List<GameObject> Players;
    void Start()
    {
        Players = GameObject.FindObjectsOfType<GameObject>().Where(c => c.GetComponent<Player>() != null).ToList();
    }


    void Update()
    {
        // find closest player
        GameObject closest = null;
        float closestDist = 999999;
        foreach(GameObject c in Players)
        {
            if (closest == null)
                closest = c;
            if (Vector3.Distance(transform.position, c.transform.position) <  closestDist)
            {
                closest = c;
                closestDist = Vector3.Distance(transform.position, c.transform.position);
            }
        }

        // walk  towards closest player, if not close already
        if (closestDist > 1f)
        {
            float speed = 2;
            Vector3 dir = (closest.transform.position - transform.position).normalized;
            transform.position += dir * speed * Time.deltaTime;
        }
        // otherwise deal damage to the player we're infront of
        else
        {
            if (Time.time > nextdamage)
            {
                  GetComponent<Player>().playerhp -= 1;
                  nextdamage = Time.time + damagecooldown;   
            }
        }
        if(hp <= 0)
        {
            Destroy(gameObject);
            dead = true;
            if (dead == true)
            {
                GetComponent<Player>().money += 100;

                Debug.Log("money");
            }
        }
    }
}
