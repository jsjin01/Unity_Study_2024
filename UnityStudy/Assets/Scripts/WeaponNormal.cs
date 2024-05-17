using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class WeaponNormal : WeaponComponent
{
    [SerializeReference] float range = 50f;

    Ray ray; 
    RaycastHit hit; //ray의 충돌
    LineRenderer line; //선그리기
    int hitenemy; //광선이 적과 충돌했을 때 저장할 함수

    private void Start()
    {
        line = GetComponent<LineRenderer>(); //최기화
        hitenemy = LayerMask.GetMask("Enemy"); //ray와 hit한 Enemy태그의 오브젝트 저장
    }

    public override void Shot()
    {
        if (!canShoot) return;
            ray.origin = transform.position; //ray의 시작점
            ray.direction = transform.forward; //캐릭터의 z축

            line.startWidth = 0.1f;
            line.SetPosition(0, ray.origin);

        
        if (Physics.Raycast(ray, out hit, range, hitenemy)) {
            line.SetPosition(1, hit.point);
            DamageEnemy(hit.transform.gameObject); //hit된 enemy까지 선 그리기
            }
        else {
            line.SetPosition(1, ray.origin + ray.direction * range); // 아닐 경우 사거리만큼 선 그리기
            Debug.Log("ray");
            }
        StartCoroutine(Linereset());
        StartCoroutine(ShotRate());
       
    }

    IEnumerator Linereset() //선이 사라지는 함수
    {
        float w = line.startWidth;
        float t = 0;

        while (true)
        {
            line.startWidth = Mathf.Lerp(w, 0, t += 3 * Time.deltaTime);

            if (line.startWidth == 0)
                break;
        }
        yield return new WaitForEndOfFrame();
    }
}
