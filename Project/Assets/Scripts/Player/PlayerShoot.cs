using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour
{
    public Text Debugtext;

    [SerializeField]
    [SyncVar]
    private PlayerWeapon currentWeapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private LayerMask layerMask;

    private WeaponManager weaponManager;

    void Start()
    {
        if (cam == null)
        {
            this.enabled = false;
        }
        weaponManager = GetComponent<WeaponManager>();
        currentWeapon = weaponManager.GetCurrentWeapon();
        EventManager.WeaponCheck += setCurrentWeapon;
    }
    void Update()
    {
        currentWeapon = weaponManager.GetCurrentWeapon();
        if (PauseMenu.isOn)
            return;
        if (Input.GetButton("Fire1"))
        {
            if (!GetComponent<Player>()._hunter)
                return;
            Shoot();
        }
        if(GetComponent<Player>()._hunter == false)
        {
            GetComponent<WeaponManager>().weaponHolder.gameObject.SetActive(false);
        }
        if (GetComponent<Player>()._hunter == true)
        {
            GetComponent<WeaponManager>().weaponHolder.gameObject.SetActive(true);
        }
    }

    //Player shoots and function triggered on client
    [Client]
    void Shoot()
    {
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, 10, mask))
        {
            if (_hit.collider.tag == "Player")
            {
                Debug.Log("SHOOT");
                CmdPlayerShot(_hit.collider.name, currentWeapon.damage);
            }
        }
    }

    //Set the currentweapon the player has
    private void setCurrentWeapon()
    {
        currentWeapon = weaponManager.GetCurrentWeapon();
    }

    //Command that's called on client and will continue to the player that has been hit
    [Command]
    void CmdPlayerShot(string player, int damage)
    {
        Debug.Log(player + " has been shot");
        Player _player = GameManager.GetPlayer(player);
        _player.RpcTakeDamage(damage, GetComponent<Player>().name);
    }

}

