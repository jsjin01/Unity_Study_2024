using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum STATUS
{
    NORMAL,
    KNOCK
}

public class MonsterComponent : MonoBehaviour
{
    public int maxHealth; //최대 체력
    public Transform target; //ai를 통한 플레이어 따라다니기 타겟

    public bool isDead = false; // 죽음 판단 변수

    Rigidbody rigid;
    BoxCollider boxCollider;
    NavMeshAgent nav;
    SkinnedMeshRenderer smr;
    
    [SerializeField] int hp = 100; //적 체력설정
    [SerializeField] int hpMax = 100; //최대 체력설정
    [SerializeField] int atk = 10; //공격력 설정
    [SerializeField] float atkRate = 0.2f; //공격속도 설정
    [SerializeField] Material[] mat = new Material[2];      //원본과 피격 시 변경해 줄 매터리얼을 담아둘 배열

    bool isKnock = false;
    bool hit = true;


    void Awake() 
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
        smr = GetComponentInChildren<SkinnedMeshRenderer>();

        isDead = false;
    }

    private void OnEnable()
    {
       hp = hpMax; //hp 초기화
       isDead = false; //죽음 상태 전환
        
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
    }

    void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        FreezeVelocity();
    }
    //플레이어와 충돌시 ai nav와 물리작용으로 이동 방해 방지

    void Update()
    {
        target = GameObject.Find("Player").transform;
        nav.SetDestination(target.position);
    }
    //플레이어 따라다니는 ai



    
    public void TakeDamage(int dmg, STATUS _st = STATUS.NORMAL, bool cri = false)
    {
        if (isDead) return;

        hp -= dmg; //hp 감소
        StartCoroutine(SetHitColor());
        
        if (_st == STATUS.KNOCK)
        {
            if (!isKnock)
            {
                StartCoroutine(KnockBack());
            }
        }
        Vector3 p = transform.position;

        SoundManager.i.monsterAudioPlay(0);
    }
    IEnumerator SetHitColor()
    {
        smr.material = mat[1];      //히트 매터리얼로 변경
        yield return new WaitForSeconds(0.1f);
        smr.material = mat[0];      //원본 매터리얼로 변경
    }

    IEnumerator KnockBack()
    {
        isKnock = true;
        rigid.AddForce(-transform.forward * 1000);
        yield return new WaitForSeconds(0.5f);
        isKnock = false;
    }

    IEnumerator hitrate()//공격 속도 설정
    {
        hit = false;
        yield return new WaitForSeconds(1f);
        hit = true;
    }

    private void OnCollisionStay(Collision collision)//몬스터와 충돌했을 때 적용
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isDead)
            {
                return;
            }
            if (hit)
            {
                PlayerMoveControl.i.TakeDamage(atk);
                StartCoroutine(hitrate());
            }
        }
    }
}
