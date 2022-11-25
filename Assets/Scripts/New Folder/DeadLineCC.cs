/****************************************************
	文件：DeadLineCC.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLineCC : MonoBehaviour
{
    public bool isGameover;
    public float speed;
    public Vector3 originalPos;
    private void Start()
    {
        originalPos = transform.position;
        EventManage.Instance.On("Gameover", (object[] o) =>
        {
            isGameover = true;
            return null;
        });
        EventManage.Instance.On("ReStart",(object[] o)=> 
        {
            transform.position = originalPos;
            isGameover = false;
            return null;
        });

    }
    private void Update()
    {

        if (!isGameover)
        {        
            transform.Translate(new Vector3(0,Time.deltaTime*speed,0));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Enemy" || collision.collider.gameObject.tag == "ADD")
        {
           
            if (Mathf.Abs(transform.position.y - Camera.main.transform.position.y) < 15)
            {
                AudioManage.Instance.PlaySound1("Posui");
            }
            GameObject go = PoolManage.Instance.GetItemByCreateFun(collision.collider.transform.parent.GetComponent<EnemyCC>().type.ToString(), () =>
            {
                return Instantiate(GameManage.Instance.des[collision.collider.transform.parent.GetComponent<EnemyCC>().type]);
            }) as GameObject;
            ParticleSystem particle = go.GetComponent<ParticleSystem>();
            go.transform.position = transform.position;
            go.GetComponent<ParticleSystem>().Play();

            TimeManage.Instance.timer.AddTimeTask((a) => { PoolManage.Instance.Recover(collision.collider.transform.parent.GetComponent<EnemyCC>().type.ToString(), go); }, 2500, PETimeUnit.Millisecond);


            collision.collider.gameObject.transform.parent.gameObject.SetActive(false);
            TimeManage.Instance.timer.AddTimeTask((a) =>
            {
                PoolManage.Instance.Recover(collision.collider.gameObject.transform.parent.gameObject.GetComponent<EnemyCC>().Type.ToString(), collision.collider.gameObject.transform.parent.gameObject);
            }, 1000, PETimeUnit.Millisecond);
        }
        else if (collision.collider.gameObject.tag != "Player")
        {
            collision.collider. transform.parent.gameObject.SetActive(false);
            PoolManage.Instance.Recover(collision.collider.transform.parent.gameObject.GetComponent<AwardCC>().Type.ToString(), collision.collider.transform.parent.gameObject);
        }
    }
 
}
