/****************************************************
	文件：AddScoreTextCC.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddScoreTextCC : MonoBehaviour
{
    public GameObject papa;
  
  
    private void Update()
    {
        if (papa!=null)
        {
          transform.localPosition =Camera.main.WorldToScreenPoint(papa.transform.position) - new Vector3(Screen.width / 2, Screen.height / 2, 20);
        }
    }
}
