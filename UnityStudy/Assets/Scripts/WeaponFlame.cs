using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFlame : WeaponComponent
{
    BoxCollider bc;
    ParticleSystem fx;

    public override void Shot()
    {
        if (!isAuto)
        {
            isAuto = true;
            StartCoroutine(SetFlame());
        }
        else
        {
            isAuto = false;
        }

        SoundManager.i.weaponAudioPlay(1);
        
    }

    IEnumerator SetFlame()
    {
        if(bc == null) //초기화
        {
            bc = GetComponent<BoxCollider>();
            fx = GetComponent<ParticleSystem>();
        }


        while (isAuto)
        {
            if (!fx.isPlaying)
            {
                fx.Play();//파티클 실행
            }
            bc.enabled = true;
            ReduceAmmo();
            yield return new WaitForEndOfFrame();
            bc.enabled = false;
        }
        
        fx.Stop();
    }
}
//?
