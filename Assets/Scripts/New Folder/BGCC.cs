/****************************************************
	文件：BGCC.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BGCC : MonoBehaviour
{
    // 旋转   + 缩放
    public float time;
    private void Start()
    {
        Rotate();
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time>=5)
        {
            time = 0;
            Rotate();
        }
    }

    public void Rotate()
    {
        transform.DORotate(new Vector3(0,0,Random.Range(-30,30)),5);
    }
}
