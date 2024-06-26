using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControl : MonoBehaviour
{
    public static PlayerMoveControl i;
    public Vector3 inputVec;
    public float speed; //플레이어 속도
    //health를 선언 후 받아서 사용하는 건 ok => hp에도 변경 사항을 적용시켜줘야하는데 못했음

    Rigidbody rigid;
    Animator anim;

    void Awake()
    {
        i = this;
        rigid = GetComponent<Rigidbody>(); //초기화
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        speed = PlayerManager.i.moveSpeed;      //PlayerManager스크립트에서 받아오기
        UiManager.i.hpBar(PlayerManager.i.hp);  //hp바 적용 
    }


    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal"); //A,D로 좌우 이동
        inputVec.z = Input.GetAxisRaw("Vertical"); //W, S로 앞뒤 이동

        Vector3 nextVec = inputVec.normalized * speed * Time.deltaTime; // normalized: 벡터값을 1로 수정 nextVec: 다음으로 이동하는 방향
        rigid.MovePosition(rigid.position + nextVec); // 내 위치 + 다음으로 이동하는 방향

        anim.SetBool("isWalk", nextVec != Vector3.zero);    //움직이면 걷는 애니메이션
        LookMouse();
    }
    void LookMouse()    //마우스를 바라보는 함수
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (GroupPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);
            transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
        }
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
        PlayerManager.i.hp -= dmg; // 데미지가 들어오면 체력 감소
        UiManager.i.hpBar(PlayerManager.i.hp);
        Debug.Log("Player: " + PlayerManager.i.hp); // 현재 플레이어 체력

        if (PlayerManager.i.hp <= 0) // 체력이 0 이하면 Dead
        {
            Dead();
        }
        return PlayerManager.i.hp;
    }

    public void Dead() // 오브젝트 비활성화
    {
       UiManager.i.Gameover();
        Time.timeScale = 0;
       //gameObject.SetActive(false);  //굳이 비활성화 할 필요가 없다
    }

}
