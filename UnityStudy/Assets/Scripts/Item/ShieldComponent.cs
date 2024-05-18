using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldComponent : MonoBehaviour
{
    [SerializeField] float shieldHp = 30f;   //쉴드 체력

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //적과 만나면 적의 데미지만큼 쉴드 체력깎기
            //쉴드 체력이 0이 되면 없애기
        }
    }
    void DestroyShield()
    {
        Destroy(gameObject);
    }
}
