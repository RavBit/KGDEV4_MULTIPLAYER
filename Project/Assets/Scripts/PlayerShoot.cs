using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour
{


    private PlayerWeapon currentWeapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private LayerMask LayerMask;

    private WeaponManager weaponManager;

    [SerializeField]
    private GameObject truck;

    [SerializeField]
    private float chargecounter;

    void Start()
    {
        if (cam == null)
        {
            this.enabled = false;
        }
        weaponManager = GetComponent<WeaponManager>();

    }
    void Update()
    {
        if (PauseMenu.isOn)
            return;
        if (Input.GetButton("Fire1"))
        {
            chargecounter = chargecounter + 0.01f;
            if (chargecounter > 1)
                return;
        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (chargecounter < 0.2)
            {
                chargecounter = 0;
                return;
            }     
            CmdShoot(chargecounter);
            chargecounter = 0;
        }
        //}
        else
        {
            /*if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot", 0f, 1f/currentWeapon.fireRate);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }*/
        }
        GetComponentInChildren<PlayerInterface>().AdjustNeedle(chargecounter);
    }
    [Command]
    void CmdShoot(float force)
    {
        RaycastHit _hit;
        RpcShoot(force);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, 10, mask))
        {
            //if (_hit.collider.tag == "Player")
                //CmdPlayerShot(_hit.collider.name, currentWeapon.damage);
        }
    }

    [ClientRpc]
    void RpcShoot(float force)
    {
        GameObject truckin = NetworkManager.Instantiate(truck, transform.position + 1f * transform.forward, Quaternion.Euler(new Vector3(270, transform.eulerAngles.y, 0)));
        truckin.GetComponent<Truck_Settings>().SetForce((int)(force * 200), transform.name);
        ResetCharge();
    }

    void ResetCharge()
    {
        chargecounter = 0;
    }
}

