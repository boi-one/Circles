using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class spawn : NetworkBehaviour
{
    public float cooldown = 6;
    private float nextspawn = 6;
    int countup = 0;
    public GameObject zombieprefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer == false) return;

        if(Time.time > nextspawn)
        {
            GameObject made = Instantiate(zombieprefab, transform.position, transform.rotation);
            NetworkServer.Spawn(made);
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
        if (countup > 20)
        {
            cooldown = 3.5f;
        }
        if (countup > 30)
        {
            cooldown = 3;
        }
        if (countup > 40)
        {
            StartCoroutine(Wait());
            cooldown = 2;
        }
        if (countup > 50)
        {
            cooldown = 1.5f;
        }
        if(countup > 60)
        {
            StartCoroutine(Wait());
            cooldown = 1;
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(12);
    }
}
