/****************************************************
	文件：LineCC.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCC : MonoBehaviour
{
    public void Init(object y)
    {
        transform.position = new Vector3(0,(float)y+10,0);
    }
    private void Start()
    {
        EventManage.Instance.On("Gameover",(object[] o)=> 
        {
            gameObject.SetActive(false);
            PoolManage.Instance.Recover("Line",gameObject);
            return null;
        });
    }
}
