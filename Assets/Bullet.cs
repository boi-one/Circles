using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject blood;

    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Zombie(Clone)")
        {
            other.GetComponent<Zombie>().shot.Play();
            Instantiate(blood, transform.position, transform.rotation);
            other.GetComponent<Zombie>().hp -= 2;
            Destroy(gameObject);
        }
        if (other.name == "tilemap_walls")
        {
            Destroy(gameObject);
        }
    }
    
}
