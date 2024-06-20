using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFlame : WeaponComponent  //잔버그 
{
    BoxCollider bc;
    ParticleSystem fx;


    private void OnEnable()
    {
        atk *= 0.1f;
    }
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
    }

    IEnumerator SetFlame()
    {
        if(bc == null) //초기화
        {
            bc = GetComponent<BoxCollider>();
            fx = GetComponentInChildren<ParticleSystem>();
        }


        while (isAuto)
        {
            if (!fx.isPlaying)
            {
                SoundManager.i.weaponAudioPlay(1);
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

