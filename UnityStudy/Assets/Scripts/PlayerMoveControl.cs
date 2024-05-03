using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{
    public Vector3 inputVec;
    public float speed; //플레이어 속도
    public float health; //플레이어 체력

    Rigidbody rigid;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>(); //초기화
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        speed = PlayerManager.i.moveSpeed;      //PlayerManager스크립트에서 받아오기
        health=PlayerManager.i.hp;              //PlayerManager스크립트에서 받아오기
    }


    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal"); //A,D로 좌우 이동
        inputVec.z = Input.GetAxisRaw("Vertical"); //W, S로 앞뒤 이동

        Vector3 nextVec = inputVec.normalized * speed * Time.deltaTime; // normalized: 벡터값을 1로 수정 nextVec: 다음으로 이동하는 방향
        rigid.MovePosition(rigid.position + nextVec); // 내 위치 + 다음으로 이동하는 방향

        anim.SetBool("isWalk", nextVec != Vector3.zero);    //움직이면 걷는 애니메이션
        transform.LookAt(transform.position + nextVec);     //바라보는 방향으로 회전
    }

    private void OnTriggerEnter(Collider collision) // 충돌 감지
    {
        if(!collision.CompareTag("Enemy")) // Enemy가 아니면 return
            return;

        //float dmg = collision.GetComponent<Enemy>().dmg;
        //TakeDamage(dmg);
    }

    void FreezeRotation() 
    {
        rigid.velocity= Vector3.zero;  //몬스터 충돌 할 때 가속도 때문에 추가
    }

    void FixedUpdate()
    {
        FreezeRotation();
    }
    //player enemy와 충돌했을때 이동 물리가 이동방해하는 것 방지

    public float TakeDamage(float dmg) // 데미지 계산
    {
        health -= dmg; // 데미지가 들어오면 체력 감소
        Debug.Log("Player: " + health); // 현재 플레이어 체력

        if (health <= 0) // 체력이 0 이하면 Dead
        {
            Dead();
        }
        return health;
    }

    public void Dead() // 오브젝트 비활성화
    {
        gameObject.SetActive(false);
    }
}
