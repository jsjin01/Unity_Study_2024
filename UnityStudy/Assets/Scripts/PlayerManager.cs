using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] GameObject shield_PreFab;              //쉴드 프리펩
    [SerializeField] GameObject turret_Prefab;              // 미사일 프리펩
    int atkLv = 1;
    int atkspdLv = 1;
    int criLv = 1; 
    int spdLv = 1;
    int MhpLv = 1;

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
        UiManager.i.hpBar(PlayerManager.i.hp);
    }
    
    public void plusExp()
    {
        exp += 10;
    }


    public void ShieldOn()  // 쉴드 생성
    {
        GameObject shield = Instantiate(shield_PreFab, transform.position + new Vector3(0, 0.93f, 0), Quaternion.identity);
        shield.transform.SetParent(this.transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ItemShield")) //쉴드 획득
        {
            ShieldOn(); //쉴드 켜짐
            Destroy(other.gameObject); 
        }
        else if (other.CompareTag("ItemTurret"))    //터렛 획득시
        {
            Instantiate(turret_Prefab, transform.position, Quaternion.identity);   //터렛 생성
            Destroy(other.gameObject); 
        }
        else if (other.CompareTag("ItemFlame"))
        {
            //화염방사기
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("ItemTazer"))
        {
            //번개 건
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("ItemShotgun"))
        {
            //샷건 
            Destroy(other.gameObject);
        }
    }

    public void cardAtkUP() //카드에서 공격력 높힐 때
    {
        atk += 5;
        atkLv++;
        UiManager.i.StatUp(0, atkLv);
    }
    public void cardAtkSPDUP() //카드에서공격 속도 높힐 때
    {
        atkspd *= 0.9f;
        atkspdLv++;
        UiManager.i.StatUp(1, atkspdLv);
    }

    public void cardCriUp()  //카드에서 치명타 확률 높힐 때
    {
        cri += 5;
        criLv++;
        UiManager.i.StatUp(2, criLv);
    }

    public void cardSpdUP()  //카드에서 플레이어 스피드 높힐 때
    {
        moveSpeed *= 1.1f;
        spdLv++;
        UiManager.i.StatUp(3, spdLv);
    }
    public void cardMaxHpUP() //카드에서 최대 체력 높힐 때
    {
        maxHp += 10;
        hp += 10;
        GameObject.Find("Hp").GetComponent<Slider>().maxValue = maxHp;
        UiManager.i.hpBar(PlayerManager.i.hp);
        MhpLv++;
        UiManager.i.StatUp(4, MhpLv);
    }
}
