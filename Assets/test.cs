/****************************************************
	文件：test.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Text JiShi;
    private void Start()
    {
        JiShi = GetComponent<Text>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float   time = 50;
            DOTween.To(() =>time, x =>
            {
                time =x;
                JiShi.text = (Mathf.Floor(time) / 10.0f).ToString();
            }, 0, 5);
        }
    }
}
