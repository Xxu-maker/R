/****************************************************
	文件：CircleParticalCC.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleParticalCC : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3 (0,0,Time.deltaTime*90));
    }
}
