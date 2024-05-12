using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager i;      //static 선언

    [SerializeField] public float moveSpeed = 2.5f;    //속도
    [SerializeField] public float hp = 100f;           //체력
    [SerializeField] public float maxHp = 100f;        //최대 체력
    [SerializeField] public float cri;                 //크리데미지
    [SerializeField] public float atkspd;              //공격 속도

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
}
