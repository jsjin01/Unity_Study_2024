using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager i;

    public AudioSource audioSource;

    public AudioClip[] weaponAudio; //무기 오디오 배열
    public AudioClip[] mosterAudio; //몬스터 오디오 배열

    private void Awake()
    {
        i = this;
    }

    public void weaponAudioPlay(int i)
    {
        audioSource.clip = weaponAudio[i];  //오디오 클립 바꾸기
        audioSource.Play();     //오디오 플레이
    }

    public void monsterAudioPlay(int i)
    {
        audioSource.clip = weaponAudio[i];  //오디오 클립 바꾸기
        audioSource.Play();     //오디오 플레이
    }
}
