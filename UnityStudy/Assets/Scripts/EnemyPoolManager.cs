using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ENEMY //몬스터 타입별로 분류 열거문 enum 사용
{
    BUNNY,
    BEAR,
    HELEPHANT
}
public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager i;          //public static으로
                                               //선언하여 다른 script에서도 사용할 수 있도록함

    [SerializeField]GameObject[] EnemyPrefabs; //몬스터 종류 3가지
    int initEnemy = 2;                        //초반 Enemy 갯수 

    private void Awake()
    {
        i = this; //i에 자기 자신을 저장
    }

    private void Start()
    {
        CreateEnemy(ENEMY.BUNNY, initEnemy); //종류별로 20마리씩 생성 => 여기서 _t는 의미 없는 값
    }

    void CreateEnemy(ENEMY _t, int cnt = 1) //Enemy 생성 
    {
        if(cnt > 1)
        {
            for (int i = 0; i < EnemyPrefabs.Length; i++)              //Enemy 별로 관리할 pool 따로 생성
            {
                GameObject temp = new GameObject(EnemyPrefabs[i].name); // 새로운 GameObject 생성:해당 이름으로 생성
                temp.transform.SetParent(transform);
            }
            for (int i = 0;i < EnemyPrefabs.Length;i++) //종류별로 생성 
            {
                for(int j = 0;j < cnt; j++)  //prefab로 만들어진 GameObject 생성
                {
                    Instantiate(EnemyPrefabs[i], transform.GetChild(i)); //종류에 맞는 pool 안에 저장
                }
            }
        }
        else
        {
            Instantiate(EnemyPrefabs[(int)_t], transform.GetChild((int)_t)); //한마리 생성
        }
    }

    public void EnemySpawn(ENEMY _t, Vector3 pos)//생성한 Enemy를 가져와서 사용
    {
        GameObject temp;//게임 오브젝트를 담을 변수

        if(transform.GetChild((int)_t).childCount == 0) //해당 EnemyPool에 아무것도 없으면 새로 생성
        {
           CreateEnemy(_t); 
        }

        temp = transform.GetChild((int)_t).GetChild(0).gameObject; //해당 EnemyPool에서 하나 새로 가져옴


        temp.transform.position = pos; //위치 설정
        temp.transform.SetParent(null);// 부모 설정 해제
        temp.SetActive(true);          //활성화

    }

    public void EnemyDestory(ENEMY _t, GameObject _e) //사용한 Enemy을 다시 EnemyPool 넣음
    {
        _e.SetActive(false);                                 //비활성화
        _e.transform.SetParent(transform.GetChild((int)_t)); //다시pool안으로 다지고 들어옴
                                                             //=> transform은 script가 있는 곳을 기준으로 함 
    }
}
