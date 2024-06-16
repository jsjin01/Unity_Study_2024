using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager i;
    int[] cardnum = {0, 0 ,0 };

    private void Awake()
    {
        i = this;
    }
    void Start()
    {

    }
    void Update()
    {
        
    }

    public void SelectedCard() //카드 뽑기 알고리즘 
    {
        GameObject CardPack = gameObject.transform.GetChild(1).gameObject;//card의 상위 객체 추출
        while (true)
        {
            cardnum[0] = Random.Range(0, 5);
            cardnum[1] = Random.Range(0, 5);
            cardnum[2] = Random.Range(0, 5);

            if (cardnum[0] != cardnum[1] && cardnum[1] != cardnum[2] && cardnum[0] != cardnum[2])
            {
                break;//같은 카드가 하나도 안나와야지 탈출
            }
        }

        for(int j = 0; j <3; j++) //활성화
        {
            CardPack.transform.GetChild(cardnum[j]).gameObject.SetActive(true);
        }
    }
}
