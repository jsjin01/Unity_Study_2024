using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager i;      //static 선언

    [SerializeField] public float moveSpeed = 2.5f;    //속도
    [SerializeField] public float hp = 100f;           //체력
    [SerializeField] public float maxHp = 100f;        //최대 체력
    [SerializeField] public float atk = 10f;                //데미지
    [SerializeField] public float cri = 5f;                 //크리데미지
    [SerializeField] public float atkspd = 1f;              //공격 속도
    [SerializeField] public int exp = 0;                    //Exp
    [SerializeField] public int Maxexp = 100;               //MaxExp

    [SerializeField] GameObject shield_PreFab;
    [SerializeField] GameObject turret_Prefab;

    private void Awake()
    {
        i = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Recovery(int value) //hp 회복
    {
        hp += value;
        if (hp > maxHp) hp = maxHp;
    }
    public void ShieldOn()  //쉴드 생성
    {
        GameObject shield = Instantiate(shield_PreFab, transform.position+new Vector3(0, 0.93f, 0), Quaternion.identity);
        shield.transform.SetParent(this.transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ItemShield")) //쉴드 아이템을 먹었을 때
        {
            ShieldOn(); //쉴드 생성하기
            Destroy(other.gameObject);  //쉴드 아이템 없애기
        }
        else if (other.CompareTag("ItemTurret"))    //터렛 아이템을 먹었을 때
        {
            Instantiate(turret_Prefab, transform.position, Quaternion.identity);    //터렛 생성
            Destroy(other.gameObject);  //터렛 아이템 없애기
        }
        else if (other.CompareTag("ItemFlame"))
        {
            //화염방사기 장착
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("ItemTazer"))
        {
            //화염방사기 장착
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("ItemShotgun"))
        {
            //샷건 장착
            ShootComponent.i.SetWeapon(WEAPON.SHOTGUN);
            Destroy(other.gameObject);
        }
    }

    public void cardAtkUP() //카드에서 공격력 증가
    {
        atk += 5;
    }
    public void cardAtkSPDUP() //카드에서 공속 증가
    {
        atkspd *= 0.9f;
    }

    public void cardCriUp() //카드에서 치명타 확률 증가
    {
        cri += 5;
    }

    public void cardSpdUP() //카드에서 이동속도 증가
    {
        moveSpeed *= 1.1f;
    }
    public void cardMaxHpUP() //카드에서 최대 체력 증가
    {
        maxHp += 10;
        GameObject.Find("Hp").GetComponent<Slider>().maxValue = maxHp;
    }
    public void plusExp()
    {
        exp += 10;
    }
}
