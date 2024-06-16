using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveComponent : MonoBehaviour
{
    [SerializeField] int dmg = 50;
    [SerializeField] float dTime = 0.5f;
    SphereCollider sc;

    void Start()
    {
        sc = GetComponent<SphereCollider>();
        Invoke("SetCollider", 0.1f);
        Destroy(gameObject, dTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<MonsterComponent>().TakeDamage(dmg);
        }
    }

    void SetCollider()
    {
        sc.enabled = false; // 콜라이더 비활성화
    }
}
