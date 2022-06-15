using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{
    public override void OnStartServer()
    {
        Debug.Log("OnStartServer");
    }

    public override void OnStopServer()
    {
        Debug.Log("OnStopServer");
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("OnClientConnect");
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        Debug.Log("OnClientDisconnect");
    }
}
