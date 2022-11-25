/****************************************************
	文件：AudioManage.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：2020/2/20
	功能：声音管理器
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManage : MonoBehaviour
{
    public AudioSource audioMusic;
    public AudioSource audioSound;
    public AudioSource audioSound1;
    public AudioSource audioSound2;
    public AudioSource audioSound3;
    public AudioSource audioSound4;

    public static AudioManage Instance = null;
    private void Awake()
    {
        Instance = this;
    }
   

    public void PlaySound(string path)
    {
        audioSound.clip = LoadManage.Instance.LoadAudioClip(path, true);
        audioSound.Play();
    }
    public void PlaySound1(string path)
    {
        audioSound1.clip = LoadManage.Instance.LoadAudioClip(path, true);
        audioSound1.Play();
    }
    public void PlaySound2(string path)
    {
        audioSound2.clip = LoadManage.Instance.LoadAudioClip(path, true);
        audioSound2.Play();
    }
    public void PlaySound3(string path)
    {
        audioSound3.clip = LoadManage.Instance.LoadAudioClip(path, true);
        audioSound3.Play();
    }
    public void PlaySound4(string path)
    {
        audioSound4.clip = LoadManage.Instance.LoadAudioClip(path, true);
        audioSound4.Play();
    }


    public void PlayMusic(string path, bool loop)
    {
        audioMusic.clip = LoadManage.Instance.LoadAudioClip(path, true);
        audioMusic.loop = loop;
        audioMusic.Play();
    }
    public void StopMusic()
    {
        audioMusic.Stop();
    }

    public void PauseMusic()
    {
        audioMusic.Pause();
    }

    public void ResumeMusic()
    {
        audioMusic.UnPause();
    }


    public void MusicOff()
    {
        audioMusic.Pause();
        audioMusic.mute = true;
    }

    public void SoundOff()
    {
        audioSound.Pause();
        audioSound.mute = true;

        audioSound1.Pause();
        audioSound1.mute = true;

        audioSound2.Pause();
        audioSound2.mute = true;

        audioSound3.Pause();
        audioSound3.mute = true;

        audioSound4.Pause();
        audioSound4.mute = true;
    }

    public void MusicOn()
    {
        audioMusic.UnPause();
        audioMusic.mute = false;

    }

    public void SoundOn()
    {
        audioSound.UnPause();
        audioSound.mute = false;

        audioSound1.UnPause();
        audioSound1.mute = false;

        audioSound2.Pause();
        audioSound2.mute = true;

        audioSound3.Pause();
        audioSound3.mute = true;

        audioSound4.Pause();
        audioSound4.mute = true;
    }


}




  


