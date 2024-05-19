using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretComponent : MonoBehaviour
{
    MonsterComponent target;          //에너미 타겟 변수
    [SerializeField] Transform head;        //터렛 머리
    [SerializeField] Transform shootPos;    //미사일 발사 위치
    [SerializeField] GameObject missile;    //미사일 오브젝트

    [SerializeField] float dTime = 10f;     //터렛 삭제 시간
    [SerializeField] float atkRate = 0.3f;  //발사 간격
    [SerializeField] float rotSpeed = 10;   //회전 속도

    private void Start() // 터렛 삭제
    {
        Destroy(gameObject, dTime);
    }

    private void Update() // 타겟이 죽을 경우 타겟 해제
    {
        if (target)
        {
            if (target.isDead)
            {
                target = null;
                StopCoroutine(ShootMissile());
            }
        }
    }

    private void FixedUpdate() // 방향 설정 실행
    {
        if (target) 
            RotateHead();
    }

    private void OnTriggerStay(Collider other) //OnTriggerStay: Collider 범위 안에 있는 동안 실행
    {
        if (target) return; // 타겟이 있다면 리턴

        // 타겟 설정, 미사일 발사
        if (other.CompareTag("Enemy"))
        {
            target = other.GetComponent<MonsterComponent>();
            StartCoroutine(ShootMissile());
        }
    }

    private void OnTriggerExit(Collider other) //OnTriggerExit: Collider 범위 밖으로 나가면 실행
    {
        // 타겟 설정 초기화, 미사일 발사 중지
        if (other.CompareTag("Enemy"))
        {
            if (target.gameObject == other.gameObject)
            {
                target = null;
                StopCoroutine(ShootMissile());
            }
        }
    }

    void RotateHead() // 방향 설정
    {
        Vector3 dir = target.transform.position - transform.position; // 방향 설정
        Quaternion rot = Quaternion.LookRotation(dir); // 회전 각도
        head.rotation = Quaternion.Slerp(head.rotation, rot, Time.deltaTime * rotSpeed);
        // head에서 rot로 rotSpeed의 속도로 회전

    }


    IEnumerator ShootMissile() //미사일 생성, 코루틴으로 발사 간격 조절
    {
        while (target)
        {
            yield return new WaitForSeconds(atkRate); // 발사 간격
            GameObject temp = Instantiate(missile, shootPos.position, shootPos.rotation);
            // missile 오프젝트 생성
            temp.GetComponent<MissileComponent>().MissileMove(shootPos.forward);
        }
    }
}
