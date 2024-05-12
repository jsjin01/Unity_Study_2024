using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Hp : ItemComponent
{
    [SerializeField] int recovery_Hp = 5; // 회복되는 체력
    public override void GetItem(GameObject obj)
    {
        Debug.Log("ddd");
        obj.GetComponent<PlayerManager>().Recovery(recovery_Hp);
        // obj에 PlayerManager의 Recovery를 실행
    }
}
