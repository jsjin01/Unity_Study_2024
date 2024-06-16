using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShotGun : WeaponComponent
{
    BoxCollider bc;
    ParticleSystem fx;

    public override void Shot()
    {
        StartCoroutine(SetShotGun());
        StartCoroutine(ShotRate());

        SoundManager.i.weaponAudioPlay(2);
    }

    IEnumerator SetShotGun()
    {
        if (bc == null)
        {
            bc = GetComponent<BoxCollider>();
            fx = GetComponentInChildren<ParticleSystem>();//자식에 있는 파티클 시스템을 가져옴
        }

        fx.Play();              //FX 재생
        bc.enabled = true;      //충돌 허용
        ReduceAmmo();           //총알 사용
        yield return new WaitForSeconds(0.2f);
        bc.enabled = false;     //충돌 불허용
    }
}