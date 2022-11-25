/****************************************************
	文件：SoraMono.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoraMono : MonoBehaviour
{
    // transform 简化   
    //TODO:扩充pos 以及XYZ的排列组合
    public void SetLocalPosX(float x)
    {
        Vector3 pos = transform.position;
        pos.x = x;
        transform.position = pos;
    }
    public void SetLocalPosY(float y)
    {
        Vector3 pos = transform.position;
        pos.y = y;
        transform.position = pos;
    }

    //重置transform
    public void Identity()
    {
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
    }
    RectTransform _rect;
    public RectTransform rect
    {
        get
        {
            if (_rect==null)
            {
                _rect = GetComponent<RectTransform>();
                if (_rect==null)
                {
                    _rect = gameObject.AddComponent<RectTransform>();
                }
            }
            return _rect;
        }
    } 
    
    // 显示隐藏的简化
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }


}

