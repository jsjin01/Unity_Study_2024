using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemComponent : MonoBehaviour
{
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider item) 
    {
        if (item.CompareTag("Player")) // 아이템이 플에이어와 접촉하면 아이템 사용되고 아이템 삭제
        {
            GetItem(item.gameObject);
            Destroy(gameObject);
        }
    }

    public abstract void GetItem(GameObject obj);  // 아이템의 효과
    // abstract: 추상 함수, 상속 받은 곳에서 구현
}
