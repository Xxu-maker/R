/****************************************************
	文件：SoraUitility.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoraUtility : MonoBehaviour
{


    public static bool RandomResult(int percent)
    {
        return Random.Range(0, 100) < percent;
    }
 
    public static T GetRandomValueFrom<T>(params T[] values)
    {
        return values[Random.Range(0, values.Length)];
    }


}
