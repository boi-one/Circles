using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public float cooldown = 6;
    private float nextspawn = 0;
    int countup = 0;
    public GameObject zombieprefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextspawn)
        {
            Instantiate(zombieprefab, transform.position, transform.rotation);
            nextspawn = Time.time + cooldown;
            countup++;
        }
        if(countup > 10)
        {
            cooldown = 5;
        }
        if (countup > 20)
        {
            cooldown = 4;
        }
    }
}
