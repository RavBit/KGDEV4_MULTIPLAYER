using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class RoleManager : NetworkBehaviour {
    private void Start()
    {
        State_Manager.EventRandomizeRoles_RPC += RpcSetTurns;
        State_Manager.EventSwitchRoles_RPC += RpcSwitchTurns;
    }

    [ClientRpc]
    public void RpcSetTurns()
    {
        Debug.Log("SET TURNS");
        Dictionary<string, Player> tempplayers = GameManager.instance.GetPlayers();
        bool hunteractive = false;
        foreach (KeyValuePair<string, Player> t in tempplayers)
        {
            t.Value._hunter = (Random.value > 0.5f);
            if (!hunteractive && t.Value._hunter == false)
            {
                Debug.Log("Set hunter active");
                t.Value._hunter = true;
                hunteractive = true;
            }
            else
            {
                t.Value._hunter = false;
            }
            t.Value.InitStartGame();
        }
    }

    [ClientRpc]
    public void RpcSwitchTurns()
    {
        Dictionary<string, Player> tempplayers = GameManager.instance.GetPlayers();
        foreach (KeyValuePair<string, Player> t in tempplayers)
        {
            t.Value._hunter = !t.Value._hunter;
            t.Value.InitStartGame();
        }
    }
}
