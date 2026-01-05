using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponitem : ItemComponent
{
    [SerializeField] WEAPON w;
    public override void GetItem(GameObject obj)
    {
        obj.GetComponentInChildren<ShootComponent>().SetWeapon(w);
    }
}
