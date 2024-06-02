using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] HpItems;      //체력 회복 아이템
    [SerializeField] GameObject[] Items;        //쉴드, 터렛, 지뢰
    [SerializeField] GameObject[] WeaponItems;  //무기 아이템

    [SerializeField] bool isSpawn = true;
    [SerializeField] float itemRate = 3f;

    Vector3 playerPos; //player 위치 

    Vector3 pos;
    Ray ray;
    RaycastHit hit;

    void Start()
    {
        StartCoroutine(spawnItem(HpItems, itemRate));
        StartCoroutine(spawnItem(Items, itemRate));
        StartCoroutine(spawnItem(WeaponItems, itemRate));
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = GameObject.Find("Player").transform.position; //player의 위치를 받아옴
    }
    Vector3 setPoint() //바닥인지 아닌지 구분
    {
        while (true)
        {
            while (true)
            {
                pos = new Vector3(Random.Range(-24, 24), 10, Random.Range(-24, 24));      //랜덤한 위치 설정

                ray.origin = pos;                                                          //광선 시작지점
                ray.direction = Vector3.down;                                              //광선 방향

                if (Physics.Raycast(ray, out hit, 20))                                      //  시작점 방향이 정의된 ray / hit에 맞은 지점의 좌표
                                                                                            // /ray가 나가는 최대 거리
                {
                    if (hit.collider.CompareTag("Floor")) break;                           //바닥에 닿으면 좌표값 반환
                }
            }

            if ((playerPos - hit.point).magnitude < 5)
            {
                continue;
            }
            else
            {
                break;
            }
        }
        return hit.point;       //광선이 맞은 지점 반환
    }
    IEnumerator spawnItem(GameObject[] item, float time)
    {
        while (isSpawn)
        {
            int index = Random.Range(0, item.Length);       //랜덤 생성
            yield return new WaitForSeconds(time);          //생성 시간만큼 기다리기

            Instantiate(item[index], setPoint(), Quaternion.identity);  //아이템 랜덤 생성
        }
    }
}
