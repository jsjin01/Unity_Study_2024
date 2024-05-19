using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileComponent : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 5; //미사일 속도

    public void MissileMove(Vector3 enemy)
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = enemy * speed; // enemy 방향으로 이동
    }

    private void OnTriggerEnter(Collider other) // 충동 감지
    {
        if(other.CompareTag("Enemy")) // Tag가 "Enemy"일 경우
        {
            //몬스터 TakeDamage()
            Destroy(gameObject);  //오브젝트 삭제
        }
    }
}
