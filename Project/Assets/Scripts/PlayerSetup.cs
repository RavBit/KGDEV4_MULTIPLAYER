using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {
    [SerializeField]
    Behaviour[] ToDisable;

    [SerializeField]
    Camera maincamera;

    [SerializeField]
    GameObject PlayerUIprefab;
    private GameObject playerUIinstance;
    void Start() {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            maincamera = Camera.main;
            if(maincamera != null)
            {
                maincamera.gameObject.SetActive(false);
            }
            playerUIinstance = Instantiate(PlayerUIprefab);
            playerUIinstance.name = PlayerUIprefab.name;
            playerUIinstance.transform.parent = this.transform;
        }
        GetComponent<Player>().Setup();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartClient();
        RegisterPlayer();
    }

    [Client]
    void RegisterPlayer()
    {
        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();
        GameManager.RegisterPlayer(AppManager.instance.User.username, _player);
        
        //SCORE 
        //WebManager.instance.StartCoroutine("SetScore", 100);
    }
    void DisableComponents()
    {
        for (int i = 0; i < ToDisable.Length; i++)
        {
            ToDisable[i].enabled = false;
        }
    }

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("RemotePlayer");
    }
    void OnDisable()
    {
        Destroy(playerUIinstance);
        if (maincamera != null)
            maincamera.gameObject.SetActive(true);

        GameManager.UnRegisterPlayer(transform.name);
    }
}
