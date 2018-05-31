using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MessageCenter : NetworkBehaviour {
    [SerializeField]
    private Text messageCenter;

    public static MessageCenter instance;
    void Awake()
    {
        Debug.Log("awaoke");
        RpcReset();
        if (instance != null)
            Debug.LogError("More than one MessageCenter's in scene");
        else
            instance = this;
    }

    [ClientRpc]
    private void RpcReset()
    {
        messageCenter.text = "";
    }

    static public void Message(string test)
    {
        instance.RpcUpdateMessage(test);
    }
    [ClientRpc]
    public void RpcUpdateMessage(string text)
    {
        instance.messageCenter.text = text;
        Invoke("RpcReset", 3);
    }


}
