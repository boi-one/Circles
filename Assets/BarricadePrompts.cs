using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BarricadePrompts : NetworkBehaviour
{
    List<GameObject> spawned = new List<GameObject>();
    List<Vector3> locationsSpawned = new List<Vector3>();

    public void SetPlayer(GameObject p)
    {
        player = p;
    }

    GameObject player;


    List<GameObject> AllBarricades = new List<GameObject>();
    private void Start()
    {
        foreach (Transform t in transform)
            AllBarricades.Add(t.gameObject);
    }
    void Update()
    {
        if (player == null) return;
        if (player.GetComponent<NetworkBehaviour>().isLocalPlayer == false) return;

        foreach(Transform t in transform)
        {
            if (locationsSpawned.Contains(t.position) == false && Vector3.Distance(player.transform.position, t.position) < 3)
            {
                locationsSpawned.Add(t.position);
                if (t.name == "Barricade")
                    spawned.Add(Instantiate(Resources.Load<GameObject>("RepairPrompt"), t.position, Quaternion.identity));
                else if (t.name == "UnlockBarricade")
                    spawned.Add(Instantiate(Resources.Load<GameObject>("UnlockPrompt"), t.position, Quaternion.identity));
            }
            else if (locationsSpawned.Contains(t.position) == true && Vector3.Distance(player.transform.position, t.position) > 3)
            {
                locationsSpawned.Remove(t.position);
                GameObject target = spawned.Find(c => c.transform.position == t.position);
                spawned.Remove(target);
                Destroy(target);
            }

            if (Vector3.Distance(player.transform.position, t.position) < 3 && Input.GetKey(KeyCode.E))
            {
                if (t.name == "Barricade" && player.GetComponent<Player>().money > 0 && t.GetComponent<SpriteRenderer>().color.a < 1f)
                {
                    CmdHealBarricade(t.position);
                }
                if (t.name == "UnlockBarricade" && player.GetComponent<Player>().money >= 1000)
                {
                    locationsSpawned.Remove(t.position);
                    GameObject target = spawned.Find(c => c.transform.position == t.position);
                    spawned.Remove(target);
                    Destroy(target);
                    CmdUnlockBarricade(t.position);
                }
            }
        }
    }


    // healing barricade
    [Command (requiresAuthority = false)] void CmdHealBarricade(Vector3 barricade, NetworkConnectionToClient caller = null) => RpcHealBarricade(barricade, caller.identity.gameObject);
    [ClientRpc] void RpcHealBarricade(Vector3 barricade, GameObject player) => HealBarricade(player, barricade);
    void HealBarricade(GameObject player, Vector3 barricade)
    {
        GameObject b = AllBarricades.Find(c => c.transform.position == barricade);

        b.GetComponent<SpriteRenderer>().color = new Color(
                        b.GetComponent<SpriteRenderer>().color.r,
                        b.GetComponent<SpriteRenderer>().color.g,
                        b.GetComponent<SpriteRenderer>().color.b,
                        b.GetComponent<SpriteRenderer>().color.a + 2f * Time.deltaTime
                        );
        player.GetComponent<Player>().money -= 1;
    }

    // unlock barricade
    [Command (requiresAuthority = false)] void CmdUnlockBarricade(Vector3 barricade, NetworkConnectionToClient caller = null) => RpcUnlockBarricade(barricade, caller.identity.gameObject);
    [ClientRpc] void RpcUnlockBarricade(Vector3 barricade, GameObject player) => UnlockBarricade(player, barricade);
    void UnlockBarricade(GameObject player, Vector3 barricade)
    {
        GameObject b = AllBarricades.Find(c => c.transform.position == barricade);

        player.GetComponent<Player>().money -= 1000;
        Destroy(b);
        Player.UnlockedStages++;
    }
}
