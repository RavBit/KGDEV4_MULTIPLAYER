using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class InitGame : NetworkManager
{
    public NetworkManager nmanager;

    void Awake()
    {
        // set new network manager
        nmanager = GetComponent<NetworkManager>();
    }
    //Assign a Text component in the GameObject's Inspector
    public Text m_Text;

    //Detect when a client connects to the Server
    public override void OnServerConnect(NetworkConnection connection)
    {
        Debug.Log("test");
        //Change the text to show the connection and the client's ID
        m_Text.text = "Client " + connection.connectionId + " Connected!";
    }
}