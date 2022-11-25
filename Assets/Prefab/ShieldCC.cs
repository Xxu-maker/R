/****************************************************
	文件：ShieldCC.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCC : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(new Vector3(0,0,Random.Range(1,10)));
    }
}
