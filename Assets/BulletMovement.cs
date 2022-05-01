using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public Vector3 targetDir;
    public float speed;
    public int distance;
    public float radius;
    // Update is called once per frame
    void Update()
    {
        transform.position += targetDir * 1 / 60 * speed;
    }
}
