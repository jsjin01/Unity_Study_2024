using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /// <summary>
    /// x : -25 ~ 25
    /// z : -25 ~ 25
    /// Invoke와 코루틴의 차이점 => invoke가 훨씬 무거움, 비활성화시도 사용됨
    ///                          => 코루틴은 비활성화시 멈춤, 매개 변수를 받을 수 잇음
    /// </summary>

    [SerializeField] bool isSpawn = true;
    [SerializeField] float bunnyRate = 1f;
    [SerializeField] float bearRate = 3f;
    [SerializeField] float helephantRate = 5f;

    Vector3 playerPos; //player 위치 

    Vector3 pos;      
    Ray ray;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        //몬스터 종류별로 소환
        StartCoroutine(SpawnE(ENEMY.BUNNY, bunnyRate));
        StartCoroutine(SpawnE(ENEMY.BEAR, bearRate));
        StartCoroutine(SpawnE(ENEMY.HELEPHANT, helephantRate));
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

            if((playerPos - hit.point).magnitude < 5)
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

    IEnumerator SpawnE(ENEMY _t, float time) // 사간마다 소환되도록 코루틴 함수로 작성
    {
        while (isSpawn)
        {
            yield return new WaitForSeconds(time);
            EnemyPoolManager.i.EnemySpawn(_t, setPoint());
        }
    }
}
