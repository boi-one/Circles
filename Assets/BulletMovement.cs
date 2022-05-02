using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public Vector3 targetDir;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += targetDir * speed;
    }
}
