/****************************************************
	文件：LoadManage.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：2020/2/20
	功能：资源加载管理器
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoadManage : MonoBehaviour
{
    public static LoadManage Instance = null;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {     
    }
   
    // 用于加载游戏场景
    public void AsyncLoadScene(string sceneName, Action action)
    {
       
    }
    // 用于加载音频资源
    private Dictionary<string, AudioClip> audioClipDic = new Dictionary<string, AudioClip>();
    public AudioClip LoadAudioClip(string path, bool cache = false)
    {
        AudioClip clip = null;
        if (!audioClipDic.TryGetValue(path, out clip))
        {
            clip = Resources.Load<AudioClip>(path);
            if (cache)
            {
                audioClipDic.Add(path, clip);
            }
        }
        return clip;
    }
    // 用于加载游戏物体
    public Dictionary<string, GameObject> gameObjectDic = new Dictionary<string, GameObject>();
    public GameObject LoadGameobject(string path, bool cache = false)
    {
        if (gameObjectDic.TryGetValue(path, out GameObject go))
        {
            return go;
        }
        if (cache)
        {
            gameObjectDic.Add(path, Resources.Load(path) as GameObject);
        }
        return Resources.Load(path) as GameObject;
    }
    // 加载 sprite
    public Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();
    public Sprite LoadSprite(string path, bool cache = false)
    {
        Sprite sp = null;
        if (!spriteDic.TryGetValue(path, out sp))
        {
            sp = Resources.Load<Sprite>(path);
            if (cache)
            {
                spriteDic.Add(path, sp);
            }
        }
        return sp;
    }
}
