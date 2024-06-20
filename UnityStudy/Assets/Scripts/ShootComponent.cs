using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootComponent : MonoBehaviour
{
    public static ShootComponent i;
    [SerializeField] WEAPON wType = WEAPON.NORMAL;  //현재 무기 타입
    [SerializeField] GameObject[] weapon;           //생성할 무기 오브젝트 배열
    GameObject currentWeapon;                       //현재 무기를 담아둘 변수
    WeaponComponent wc;                             //현재 무기의 WeaponComponent를 담아둠


    private void Awake()
    {
        i = this;
    }
    private void Start()
    {
        SetWeapon(wType);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            wc.Shot(); //발사
        }
    }

    public void SetWeapon(WEAPON _t) //무기 바꿀때 호출
    {
        wType = _t;
        UiManager.i.WeaponTitle((int)_t);
        if (currentWeapon) //currentWeapon이 null인지 아닌지 판단
        {
            Destroy(currentWeapon); //null이 아니라면 삭제
        }

        currentWeapon = Instantiate(weapon[(int)_t], transform.position, Quaternion.identity, transform);
        currentWeapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
        wc = currentWeapon.GetComponent<WeaponComponent>();
        //_t에 해당하는 무기를 자식으로 생성
    }
}
