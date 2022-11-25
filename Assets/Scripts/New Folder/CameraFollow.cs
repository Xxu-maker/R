/****************************************************
	文件：CameraFoloow.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow :SoraMono
{
    //public Material material;

    [HideInInspector]
    public float start_Y;
    Vector3 originalPos;
    bool isGameover;

    private void Start()
    {
        originalPos = transform.position;
        start_Y = transform.position.y;
        EventManage.Instance.On("Gameover",(object[] o)=> 
        {
            isGameover = true;
            return null;
        });
        EventManage.Instance.On("ReStart",(object[] o)=> 
        {
            transform.position = originalPos;
            isGameover = false;
            return null;
        });
    }
    private void Update()
    {
        
        if ((transform.position.y-start_Y)>SoraConst.LineOffset)
        {
            start_Y = transform.position.y;
            EventManage.Instance.Event("CreateLine",new object[] { start_Y});
        }
        if (!isGameover)
        {
            SetLocalPosY(GameManage.Instance.Y-3.5f);
          //  material.SetFloat("_X", GameManage.Instance.Y * 0.180f);
        }
       

    }
}
