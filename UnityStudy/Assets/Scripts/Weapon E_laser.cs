using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Lightnig : WeaponComponent
{
    BoxCollider bc;
    ParticleSystem fx;
    public override void Shot()
    {
        if(!isAuto)
        {
            isAuto = true;
            StartCoroutine(SetLightning());
        }
        else isAuto = false;
    }

    IEnumerator SetLightning()
    {
        if(bc == null) 
        { 
            bc = GetComponent<BoxCollider>();
            fx = GetComponent<ParticleSystem>();
        }
        
        while (isAuto)
        {
            if(!fx.isPlaying)
            {
                fx.Play();
            }           //¿Ã∆Â∆Æ Ω««‡
        

        bc.enabled = true;
        ReduceAmmo();

        yield return new WaitForSeconds(rate);

        bc.enabled = false;
        }

        fx.Stop(); //¿Ã∆Â∆Æ ¡ﬂ¡ˆ
    }
}
