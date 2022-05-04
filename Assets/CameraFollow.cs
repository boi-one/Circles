using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform Player;
    void Start()
    {
        Player = GameObject.Find("Player").transform;
    }
    void Update()
    {
        float speed = 0.5f * Mathf.Pow(Vector3.Distance(Player.position, transform.position),2);
        Vector3 dir = (Player.position - transform.position).normalized;
        dir.z = 0;

        transform.position += dir * speed * Time.deltaTime;

        if (Input.mouseScrollDelta.y < 0)
        {
            GetComponent<Camera>().orthographicSize *= 1.2f;
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            GetComponent<Camera>().orthographicSize /= 1.2f;
        }
        if (GetComponent<Camera>().orthographicSize < 5)
            GetComponent<Camera>().orthographicSize = 5;
    }
}
