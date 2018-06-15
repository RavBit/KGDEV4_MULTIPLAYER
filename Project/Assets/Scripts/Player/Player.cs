using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
public class Player : NetworkBehaviour {

    [Header("User data:")]
    [SyncVar]
    public User User;

    [Header("Main values:")]
    [SerializeField]
    private float maxHealth = 100;

    [SerializeField]
    [SyncVar]
    private float currentHealth;

    [SyncVar]
    private bool _isDead = false;
    [SerializeField]
    [SyncVar]
    private int score;
    [SyncVar]
    public bool _hunter = false;

    [SerializeField]
    private Behaviour[] disableOnDeath;

    private bool[] wasEnabled;

    [Header("Graphic Components")]
    public Renderer rend;
    [SyncVar]
    public Color objectColor;

    private Canvas deathScreen;

    private Text deathTextmessage;


    public bool isDead {
        get {
            return _isDead;
        }
        protected set {
            _isDead = value;
        }
    }
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    public void Setup()
    {
        deathTextmessage = GetComponentInChildren<Text>();
        deathScreen = GetComponentInChildren<Canvas>();

        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }
        SetDefaults();
    }
    public void AddScore(int _score)
    {
        score = score + _score;
        AppManager.instance.Score = score;
    }

    public float GetScore()
    {
        return score;
    }
    public void SetDefaults()
    {
        deathScreen.transform.gameObject.SetActive(false);
        isDead = false;
        currentHealth = maxHealth;
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
        deathScreen.transform.gameObject.SetActive(true);

        yield return new WaitForSeconds(GameManager.instance.MatchSettings.respawnTime);

        SetDefaults();

        Transform _startPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _startPoint.position;
        transform.rotation = _startPoint.rotation;
    }
}
