using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
public class Player : NetworkBehaviour {

    [SyncVar]
    private bool _isDead = false;
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
    private float maxHealth = 2000;

    [SyncVar]
    private float currentHealth;

    [SyncVar]
    public Class currentClass = Class.Hunter;

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
        if (isServer)
        {
            objectColor = Color.green;
            currentClass = Class.Chaser;
        }
        SetDefaults();
    }

    void Update()
    {
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("_Color");
        rend.material.SetColor("_Color", objectColor);
        if (!isLocalPlayer)
            return;
    }
    public void SetDefaults()
    {
        deathScreen.transform.gameObject.SetActive(false);
        isDead = false;
        currentHealth = maxHealth;
        GetComponentInChildren<PlayerInterface>().AdjustHealth(currentHealth / 2000f);
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
    
    public void TakeDamage(int _amount)
    {
        if (_isDead)
            return;
        currentHealth -= _amount;
        Debug.Log("TOOK DAMAGE : " + _amount);
        Debug.Log("Was hit and took  " + _amount + " damage");
        GetComponentInChildren<PlayerInterface>().AdjustHealth(currentHealth / 2000f);
        //MessageCenter.Message(transform.name + " now lost health. Now has: " + currentHealth);
        //if (currentHealth <= 0)
        //    Die(_hitby);
    }
    private void Die(string _hitby)
    {
        isDead = true;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }
        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = false;
        StartCoroutine("Respawn", _hitby);
    }

    IEnumerator Respawn(string _hitby)
    {
        deathTextmessage.text = "You have been killed by: " + _hitby;
        //MessageCenter.Message(transform.name + "has been killed by" + _hitby);
        deathScreen.transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(GameManager.instance.MatchSettings.respawnTime);
        SetDefaults();
        Transform _startPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _startPoint.position;
        transform.rotation = _startPoint.rotation;
    }
}
