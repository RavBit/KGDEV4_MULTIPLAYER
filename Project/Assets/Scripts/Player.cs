using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
public class Player : NetworkBehaviour {

    [SyncVar]
    public User User;
    [SyncVar]
    private bool _isDead = false;

    [SerializeField]
    [SyncVar]
    private int score;
    [SyncVar]
    public bool _hunter = false;
    [SerializeField]
    [SyncVar]
    public Class CurrentClass = Class.None;
    public bool isDead
    {
        get
        {
            return _isDead;
        }
        protected set
        {
            _isDead = value;
        }
    }

    [SerializeField]
    private float maxHealth = 100;

    [SerializeField]
    [SyncVar]
    private float currentHealth;

    public Renderer rend;
    [SyncVar]
    public Color objectColor;

    private Canvas deathScreen;
    private Text deathTextmessage;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;
    public void Setup()
    {
        deathTextmessage = GetComponentInChildren<Text>();
        deathScreen = GetComponentInChildren<Canvas>();
        rend = GetComponent<Renderer>();
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }
        objectColor = Color.red;
        SetDefaults();
    }
    public void AddScore(int _score)
    {
        score = score + _score;
    }
    void Update()
    {
        if (!isLocalPlayer)
            return;
    }
    public void SetDefaults()
    {
        deathScreen.transform.gameObject.SetActive(false);
        isDead = false;
        currentHealth = maxHealth;
        //GetComponentInChildren<PlayerInterface>().AdjustHealth(currentHealth / 2000f);
        rend.material.shader = Shader.Find("_Color");
        rend.material.SetColor("_Color", objectColor);
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = true;
    }
    public void InitStartGame()
    {
        EventManager.Weapon_Check();
    }

    [ClientRpc]
    public void RpcTakeDamage(int _amount, string name)
    {
        if (_isDead)
            return;
        currentHealth -= _amount;
        Debug.Log("TOOK DAMAGE : " + _amount);
        Debug.Log("Was hit and took  " + _amount + " damage hit by " + name) ;
        if (currentHealth <= 0)
        {
          Die(name);
        }
    }

    private void Die(string name)
    {
        isDead = true;
        Player p = GameManager.GetPlayer(name);
        p.AddScore(1);

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }
        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = false;
        StartCoroutine("Respawn");
    }

    IEnumerator Respawn()
    {
        deathTextmessage.text = "You have been killed";
        //MessageCenter.Message(transform.name + "has been killed by" + _hitby);
        deathScreen.transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(GameManager.instance.MatchSettings.respawnTime);
        SetDefaults();
        Transform _startPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _startPoint.position;
        transform.rotation = _startPoint.rotation;
    }
}
