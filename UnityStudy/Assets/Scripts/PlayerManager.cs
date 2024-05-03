using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager i;      //static 선언

    [SerializeField] public float moveSpeed = 2.5f;    //플레이어 속도
    [SerializeField] public float hp = 100f;           //플레이어 체력

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
}
