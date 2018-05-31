using UnityEngine.Networking;
using UnityEngine;

public class Truck_Settings : NetworkBehaviour {
    private int damage;
    private string shotby;
    public ParticleSystem[] explosions;
	void Start () {
        Invoke("DestroyObject", 10);
    }
    public void SetForce(int force, string playername)
    {
        GetComponent<Rigidbody>().AddForce(-transform.up * force);
        damage = force;
        shotby = playername;
    }

    void DestroyObject()
    {
        explosions[0].Play();
        explosions[1].Play();
        explosions[2].Play();
        Destroy(gameObject, 1);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (col.gameObject.name != shotby)
            {
                DestroyObject();
                Takedamage(col.gameObject.name);
            }
           
        }
    }

    void Takedamage(string id)
    {
        Player _player = GameManager.GetPlayer(id);
        _player.RpcTakeDamage(damage, transform.name);
    }

}
