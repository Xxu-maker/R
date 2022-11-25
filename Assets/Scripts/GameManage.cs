/****************************************************
	文件：GameManage.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：游戏管理器
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManage : MonoBehaviour
{
    //单例
    public static GameManage Instance;
    private void Awake()
    {
        Instance = this;
        //初始化一下时间管理器
        TimeManage.Instance.Init();    
    }


    public float Y;//记录Y轴位置信息
    //各类特效预制体
    public GameObject linePartical;
    public GameObject addScorePartical;   
    public GameObject[] des;
    public GameObject addPartical;

    //记录游戏状态的一些变量
    public bool isGameover;
    public bool isSlowDown;
    public float slowDownTime;  
    public float time;
    public float gameTime;

    //用来做渐隐渐现效果的image
    public Image image;


    private void Start()
    {
        image.DOFade(0,5f);//粗糙的渐隐渐现效果

        //注册一下游戏结束的逻辑
        EventManage.Instance.On("Gameover", (object[] o) =>
        {
            Y = 1;
            isGameover = true;
            time = 1f;                  
            return null;       
        });

    }


    private void Update()
    {
        //游戏时间累加
        gameTime += Time.deltaTime;

        //应该是无敌buff的时间，写太久记不清了
        if (time>0)
        {
            time -= Time.deltaTime;
        }
        //这句代码不必去理解。因为偷懒用了别人写的定时系统，所以照他的规定这么来写就行。
        TimeManage.Instance.timer.Update();

        //重置游戏的判定
        if (isGameover && Input.GetMouseButtonDown(0) &&time<0.1)
        {
            AudioManage.Instance.PlaySound1("anniu_2020-02-28-19-00-01");
            UIManage.Instance.ShowGameoverTip(false);
            EventManage.Instance.Event("ReStart", null);
            isGameover = false;
            gameTime = 0;
          
        }
        if (slowDownTime>0)
        {
            slowDownTime -= Time.deltaTime;
            isSlowDown = true;
        }
        else
        {
            isSlowDown = false;
        }
    }
}
