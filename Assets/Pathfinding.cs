using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour
{
    void Start()
    {
        WayPoints = new List<Vector3>();
        foreach (Transform t in transform)
        {
            WayPoints.Add(t.position);
        }

        Vector3 sp = new Vector3(-9.5f, 5.2f);

        List<Vector3> result = GetWayPoints(sp, new Vector3(-5.5f,14.12f));
        foreach (Vector3 c in result)
            Debug.Log(c);

        Debug.DrawLine(sp, sp+ new Vector3(1, 1), Color.green, 50f);
        Debug.DrawLine(new Vector3(-5.5f, 14.12f), new Vector3(-5.5f, 14.12f) + new Vector3(1, 1), Color.green, 50f);
    }


    public static List<Vector3> WayPoints;

    /// <summary>
    /// Uses pathfinding to check where to go to reach some place
    /// Returns the closest waypoint you can go to that leads to said place
    /// </summary>
    /// <param name="current">fe a zombies current position</param>
    /// <param name="end">the final destination, for example a zombies objective</param>
    /// <returns></returns>
    public static List<Vector3> GetWayPoints(Vector3 current, Vector3 end)
    {
        List<Vector3> available = WayPoints;
        List<Vector3> chosenPoints = new List<Vector3>();
        List<Vector3> tupik = new List<Vector3>();

        while (true)
        {
            List<Vector3> visible = available.Where(c => CheckSight(current, c) && chosenPoints.Any(c2 => c2 == c) == false && tupik.Any(c2 => c2 == c) == false).ToList();

            if (visible.Count == 0 == false)
            {
                current = visible[0];
                chosenPoints.Add(visible[0]);

                // end is visible
                if (CheckSight(current, end))
                {
                    current = end;
                    chosenPoints.Add(end);
                    break;
                }
            }
            else
            {
                if (chosenPoints.Count > 1)
                {
                    Debug.DrawLine(current, chosenPoints[chosenPoints.Count - 2], Color.red, 1f);
                    tupik.Add(current);
                    chosenPoints.RemoveAt(chosenPoints.Count - 1);
                    current = chosenPoints[chosenPoints.Count - 1];
                }
                else
                {
                    break;
                }
            }
        }
        return chosenPoints;
    }
    // check if point B is visible from point A
    public static bool CheckSight(Vector3 a, Vector3 b)
    {
        while(Vector3.Distance(a, b) > 0.5f)
        {
            if (GameObject.Find("Map").transform.Find("walls").GetComponent<Tilemap>().HasTile(GameObject.Find("Map").transform.Find("walls").GetComponent<Tilemap>().WorldToCell(a)))
                return false;

            a += (b - a).normalized / 15f;
        }
        return true;
    }
}
