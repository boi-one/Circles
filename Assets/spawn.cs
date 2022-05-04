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
        //dit kan waarschijnlijk veel simpeler
        if(countup > 5)
        {
            cooldown = 5;
        }
        if (countup > 10)
        {
            cooldown = 4;
        }
        if (countup > 15)
        {
            cooldown = 3.5f;
        }
        if (countup > 20)
        {
            cooldown = 3;
        }
        if (countup > 25)
        {
            cooldown = 2;
        }
        if (countup > 30)
        {
            cooldown = 1.5f;
        }
        if(countup > 40)
        {
            cooldown = 1;
        }
    }
}
