using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {

    [SerializeField]
    public Transform weaponHolder;
    [SerializeField]
    private PlayerWeapon primaryWeapon;

    [SerializeField]
    private PlayerWeapon currentWeapon;

	void Start () {
        EquipWeapon(primaryWeapon);
        //weaponGFX.layer = LayerMask.NameToLayer("Weapon");
    }
	
    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }
    void EquipWeapon (PlayerWeapon _weapon)
    {
        currentWeapon = _weapon;

        GameObject _weaponIns = (GameObject)Instantiate(_weapon.Graphics, weaponHolder.position, weaponHolder.rotation);
        _weaponIns.transform.SetParent(weaponHolder);
        if (isLocalPlayer)
            _weaponIns.layer = LayerMask.NameToLayer("Weapon");
    }
}
