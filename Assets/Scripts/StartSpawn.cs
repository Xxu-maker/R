/****************************************************
	文件：StartSpawn.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpawn : MonoBehaviour
{
    public GameObject circle;
    public float time;
    PETimer timer;
    private void Start()
    {
        timer = new PETimer();
    }
    private void Update()
    {
        timer.Update();
        time += Time.deltaTime;
        if (time>0.5)
        {
            time = 0;
            Spawn();
        }
    }
    public void Spawn()
    {
        GameObject go = PoolManage.Instance.GetItemByCreateFun("CirclePartical",()=> 
        {
            return Instantiate(circle);
        })as GameObject;
        go.SetActive(true);
        go.transform.position = new Vector3(Random.Range(-30,30),Random.Range(-18,18),0);
        timer.AddTimeTask((a)=> 
        {
            PoolManage.Instance.Recover("CirclePartical",go);
            go.SetActive(false);
        },3000,PETimeUnit.Millisecond);
    }
}
