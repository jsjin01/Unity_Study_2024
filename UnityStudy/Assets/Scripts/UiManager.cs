using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager i;

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
        hpBar();
    }

    public void hpBar()
    {
        GameObject.Find("Hp").GetComponent<Slider>().value = PlayerManager.i.hp;
    }
}
