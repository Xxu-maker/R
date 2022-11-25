/****************************************************
	文件：TimeSvs.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManage
{
    public PETimer timer = null;
    static TimeManage instance;
    public static TimeManage Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new TimeManage();
            }
            return instance;
        }
    }

    public void Init()
    {
        timer = new PETimer();    
    }

    private void Update()
    {
        timer.Update();
    }

}
