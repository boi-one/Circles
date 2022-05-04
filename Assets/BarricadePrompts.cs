using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadePrompts : MonoBehaviour
{
    List<GameObject> spawned = new List<GameObject>();
    List<Vector3> locationsSpawned = new List<Vector3>();
    void Update()
    {
        foreach(Transform t in transform)
        {
            if (locationsSpawned.Contains(t.position) == false && Vector3.Distance(GameObject.Find("Player").transform.position, t.position) < 3)
            {
                locationsSpawned.Add(t.position);
                if (t.name == "Barricade")
                    spawned.Add(Instantiate(Resources.Load<GameObject>("RepairPrompt"), t.position, Quaternion.identity));
                else if (t.name == "UnlockBarricade")
                    spawned.Add(Instantiate(Resources.Load<GameObject>("UnlockPrompt"), t.position, Quaternion.identity));
            }
            else if (locationsSpawned.Contains(t.position) == true && Vector3.Distance(GameObject.Find("Player").transform.position, t.position) > 3)
            {
                locationsSpawned.Remove(t.position);
                GameObject target = spawned.Find(c => c.transform.position == t.position);
                spawned.Remove(target);
                Destroy(target);
            }

            if (Vector3.Distance(GameObject.Find("Player").transform.position, t.position) < 3 && Input.GetKey(KeyCode.E))
            {
                if (t.name == "Barricade" && GameObject.Find("Player").GetComponent<Player>().money > 0 && t.GetComponent<SpriteRenderer>().color.a < 1f)
                {
                    t.GetComponent<SpriteRenderer>().color = new Color(
                        t.GetComponent<SpriteRenderer>().color.r,
                        t.GetComponent<SpriteRenderer>().color.g,
                        t.GetComponent<SpriteRenderer>().color.b,
                        t.GetComponent<SpriteRenderer>().color.a + 2f * Time.deltaTime
                        );
                    GameObject.Find("Player").GetComponent<Player>().money -= 1;
                }
                if (t.name == "UnlockBarricade" && GameObject.Find("Player").GetComponent<Player>().money >= 1000)
                {
                    GameObject.Find("Player").GetComponent<Player>().money -= 1000;
                    locationsSpawned.Remove(t.position);
                    GameObject target = spawned.Find(c => c.transform.position == t.position);
                    spawned.Remove(target);
                    Destroy(target);
                    Destroy(t.gameObject);
                    Player.UnlockedStages++;
                }
            }
        }
    }
}
