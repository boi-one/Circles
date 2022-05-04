using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class destroy : MonoBehaviour
{
    public GameObject blood;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 20)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < -20)
        {
            Destroy(gameObject);
        }
        if (transform.position.y > 20)
        {
            Destroy(gameObject);
        }
        if (transform.position.y < -20)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Zombie(Clone)")
        {
            other.GetComponent<Zombie>().shot.Play();
            Instantiate(blood);
            blood.transform.transform.position = transform.position;
            blood.transform.rotation = transform.rotation;
            other.GetComponent<Zombie>().hp -= 2;
            Destroy(gameObject);
        }
        if (other.name == "walls")
        {
            Destroy(gameObject);
        }
    }

}