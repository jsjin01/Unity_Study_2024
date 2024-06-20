using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager i;
    [SerializeField] GameObject GameOverUi; //게임 오버 UI
    public Slider expSlider;
    [SerializeField] Text[] stat; //스탯 UI 
    [SerializeField] Text[] weapon;//웨폰 UI

    private void Awake()
    {
        i =this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        expBar();
    }

    public void hpBar(float hp)
    {
        GameObject.Find("Hp").GetComponent<Slider>().value = hp;
    }

    public void expBar()
    {
        expSlider.value = PlayerManager.i.exp;
        expSlider.maxValue = PlayerManager.i.Maxexp;


        if (expSlider.value >= expSlider.maxValue)  //경험치바 100이상이면
        {
            expSlider.value = 0;    //슬라이더 초기화
            PlayerManager.i.exp = 0;    //경험치 초기화
            CardManager.i.SelectedCard();   //카드뽑기
            Time.timeScale = 0; //시간 멈춤

        }
    }

    public void Gameover()
    { 
        //GameObject.Find("Gameover").SetActive(true); //Find => 활성화된 객체만 찾을 수 있음
        GameOverUi.SetActive(true);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1;
    }

    public void StatUp(int id, int lv)
    {
        stat[id].text = "LV : " + lv;
    }

    public void WeaponTitle(int id)
    {
        switch (id)
        {
            case 0:
                weapon[0].text = "NORMAL";
                weapon[1].text = "∞";
                break;
            case 1:
                weapon[0].text = "FLAME";
                SetAmmo(5000);
                break;
            case 2:
                weapon[0].text = "SHOTGUN";
                SetAmmo(300);
                break;
            case 3:
                weapon[0].text = "E_LASER";
                SetAmmo(5000);
                break;
            default:
                break;
        }
    }

    public void SetAmmo(int ammo)
    {
        weapon[1].text = ammo.ToString();
    }
}
