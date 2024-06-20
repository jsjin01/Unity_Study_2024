using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public enum WEAPON //무기 종류 => 열거문으로 선언 
{
    NORMAL,
    FLAME,
    SHOTGUN,
    E_LASER
}
public abstract class WeaponComponent : MonoBehaviour  //추상 클래스로 선언
{
    [SerializeField] protected float atk;                //공격력
    [SerializeField] protected float rate;             //공격속도
    [SerializeField] protected float cri;              //크리티컬 확률
   

    [SerializeField] protected WEAPON wType;          //무기 종류
    protected bool canShoot = true;                   //발사 가능 판단 변수
    [SerializeField] int ammo = 0;                    //총알량
    [SerializeField] protected bool isAuto = false; //자동발사

    public int AMMO { get { return ammo; } }          //현재 남은 탄약 발수
    //get으로 바로 ammo를 반환해서 옴

    private void Update()
    {
        atk = PlayerManager.i.atk;
    }
    public abstract void Shot();                     //재정의할 함수
                                                     //총의 종류마다 쏘는 방법만 다르기 때문에
                                                     //추상클래스로 상속받아서 사용할 예정
  
    
    protected IEnumerator ShotRate() //공격속도에 따른 딜레이 추가
    {
        canShoot = false;
        yield return new WaitForSeconds(rate);
        canShoot = true;
    }
    private void OnTriggerEnter(Collider other) //무기를 이펙트로 설정하기 때문에 몬스터와 무기가 충돌 시 데미지를 주도록 설정
    {
        if (other.CompareTag("Enemy"))
        {
            DamageEnemy(other.gameObject); //데미지 주는 부분
        }
    }

    protected void DamageEnemy(GameObject obj) // 크리티컬에 따라 데미지 증가 => 데미지 주는 부분
    {
        float dmg = atk; //데미지 <나중에 공격력 증가 적용을 위해 따로 선언>
        if(cri >= Random.Range(0f, 100f)) //크리티컬 터질 때
        {
            dmg *= 2;   //데미지 두배
        }
        
        //obj로 받은 객체의 TakeDamage(dmg) 함수 호출
        obj.GetComponent<MonsterComponent>().TakeDamage(dmg);
    }

    protected void ReduceAmmo()
    {
        ammo--;
        if(ammo == 0)
        {
            Debug.Log("dsfs");
            isAuto = false;
            gameObject.GetComponentInParent<ShootComponent>().SetWeapon(WEAPON.NORMAL); //탄창 0이 되면 노말로 교체
            Destroy(gameObject);
        }
    }
}
