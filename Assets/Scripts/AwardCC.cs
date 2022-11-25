/****************************************************
	文件：AwardCC.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardCC : MonoBehaviour
{
    public EnemyOrAwardType Type;
    public GameObject outSide;
    public GameObject inSide;
    public float smooth;
    [HideInInspector]
    public int dir;
    public float speed = 10;

    public Color color;
    [HideInInspector]
    public bool isGameover;

    public void Init()
    {    
        transform.position = new Vector3(dir * 25, Random.Range(GameManage.Instance.Y - 25, GameManage.Instance.Y + 25), 0);    
    }
   
    private void Start()
    {
        EventManage.Instance.On("Gameover", (object[] o) =>
        {
            isGameover = true;
            return null;
        });
        EventManage.Instance.On("Des", (object[] o) =>
        {
            gameObject.SetActive(false);
            PoolManage.Instance.Recover(Type.ToString(), gameObject);
            return null;
        });
        EventManage.Instance.On("ReStart", (object[] o) =>
        {
            isGameover = false;
            return null;
        });
       
    }
    private void Update()
    {

        if (!isGameover)
        {
            transform.Translate(new Vector3(-Time.deltaTime * speed * dir, 0, 0));
            outSide.transform.Rotate(new Vector3(0,0,Time.deltaTime*smooth*0.618f));
            inSide.transform.Rotate(new Vector3(0, 0, -Time.deltaTime * smooth));
        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DeadLine")
        {
            PoolManage.Instance.Recover(Type.ToString(), gameObject);
            gameObject.SetActive(false);
           
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {    
        if (dir == -1 && collision.gameObject.tag == "Right")
        {
            PoolManage.Instance.Recover(Type.ToString(), gameObject);
            gameObject.SetActive(false);         
        }
        else if (dir == 1 && collision.gameObject.tag == "Left")
        {
            PoolManage.Instance.Recover(Type.ToString(), gameObject);
            gameObject.SetActive(false);         
        }
    }
}
