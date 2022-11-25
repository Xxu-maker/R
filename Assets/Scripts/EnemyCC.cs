/****************************************************
	文件：EnemyCC.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum EnemyOrAwardType
{
    AddScore,
    SlowDown,
    Bomb,
    Invincible,
    YellowCube,
    YellowCube1,
    RedCube,
    BlueCube
}
public class EnemyCC : MonoBehaviour
{
    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    public GameObject P4;

    public AudioSource source;

    public EnemyOrAwardType Type;
    public int dir;
    public float speed=10;
    public TrailRenderer[] trails;
    public Color color;
    public bool isGameover;
    public int type;

    public bool fangDa;
    public bool suoXiao;

    public float angle;
    public TrailRenderer trail;

    public void Trail()
    {
        foreach (var item in trails)
        {
            item.enabled = false;
        }
    }
    public void Init()
    {
        if (dir==1)
        {
            P1.SetActive(true);
            P2.SetActive(true);
            P3.SetActive(false);
            P4.SetActive(false);
        }
        else
        {
            P3.SetActive(true);
            P4.SetActive(true);
            P1.SetActive(false);
            P2.SetActive(false);
        }
        foreach (var item in trails)
        {
            item.enabled = false;
          
        }
        TimeManage.Instance.timer.AddTimeTask((d)=> 
        {
            foreach (var item in trails)
            {
                item.enabled = true;
            }
        },2000,PETimeUnit.Millisecond);
        transform.position = new Vector3(dir*80,Random.Range(GameManage.Instance.Y-15, GameManage.Instance.Y + 100),0);
        if (Type!=EnemyOrAwardType.BlueCube)
        {
            transform.localScale = new Vector3(dir, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(dir, 1, 1)*0.4f;
            if (dir==-1)
            {
                transform.eulerAngles = new Vector3(0, 0, 30);
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
            }
            
        }
        
    }
    private void Start()
    {
        EventManage.Instance.On("BOMB",(object[] o)=> 
        {
            if (Mathf.Abs(transform.position.y - Camera.main.transform.position.y)<13 && Mathf.Abs(transform.position.x)<22)
            {
                AudioManage.Instance.PlaySound4("Posui");
                GameObject go = PoolManage.Instance.GetItemByCreateFun(type.ToString(), () =>
                {
                    return Instantiate(GameManage.Instance.des[type]);
                }) as GameObject;
                ParticleSystem particle = go.GetComponent<ParticleSystem>();
                go.transform.position = transform.position;
                go.GetComponent<ParticleSystem>().Play();

                TimeManage.Instance.timer.AddTimeTask((a) => { PoolManage.Instance.Recover(type.ToString(), go); }, 2500, PETimeUnit.Millisecond);

                GameObject go1 = PoolManage.Instance.GetItemByCreateFun("ADD", () =>
                {
                    return Instantiate(GameManage.Instance.addScorePartical);
                }) as GameObject;
                go1.SetActive(true);
                go1.transform.position = gameObject.transform.position;
                TimeManage.Instance.timer.AddTimeTask((a) => { PoolManage.Instance.Recover("ADD", go1); go1.SetActive(false); }, 3000, PETimeUnit.Millisecond);
                EventManage.Instance.Event("AddScoreText", new object[] { go });
                UIManage.Instance.Score++;


                // 销毁自身  回收  特效
                gameObject.SetActive(false);
                TimeManage.Instance.timer.AddTimeTask((b) => { PoolManage.Instance.Recover(Type.ToString(), gameObject); }, 5000, PETimeUnit.Millisecond);


            }
            return null;
        });

        EventManage.Instance.On("Gameover",(object[] o)=> 
        {
            isGameover = true;
            return null;
        });
        EventManage.Instance.On("ReStart", (object[] o) =>
        {
            isGameover = false;
            return null;
        });
        EventManage.Instance.On("Des",(object[] o)=> 
        {
            
            GameObject go= PoolManage.Instance.GetItemByCreateFun(type.ToString(),()=> 
            {
                return Instantiate(GameManage.Instance.des[type]);
            })as GameObject;
            ParticleSystem particle = go.GetComponent<ParticleSystem>();        
            go.transform.position = transform.position;           
            go.GetComponent<ParticleSystem>().Play();

            TimeManage.Instance.timer.AddTimeTask((a)=> { PoolManage.Instance.Recover(type.ToString(),go); },2500,PETimeUnit.Millisecond);
      
            gameObject.SetActive(false);
            TimeManage.Instance.timer.AddTimeTask((b)=> { PoolManage.Instance.Recover(Type.ToString(), gameObject); },5000,PETimeUnit.Millisecond);
            
            return null;
        });
    }
    private void Update()
    {
        if (!isGameover)
        {
            if (Type!=EnemyOrAwardType.BlueCube)
            {
                transform.Translate(new Vector3(-Time.deltaTime * speed * dir, 0, 0));
            }
            else
            {
               
                if (dir==1)
                {
                    transform.Translate(new Vector3(-Time.deltaTime * speed * dir, -Time.deltaTime * speed * dir * angle, 0));  
                }
                else
                {
                   // transform.Translate(new Vector3(-Time.deltaTime * speed * dir, 0, 0));
                   // transform.Translate(new Vector3(Time.deltaTime * speed, -Time.deltaTime * speed *0.5777f, 0));
                    //Debug.Log(55);
                    transform.Translate(new Vector3(12*Time.deltaTime,-2*Time.deltaTime, 0));
                   // transform.Translate(new Vector3(-Time.deltaTime * speed * dir, -Time.deltaTime * speed * dir * angle, 0));
                }
            }
           
        }
        if (GameManage.Instance.isSlowDown)
        {
            speed = 6;
        }
        else
        {
            speed = 14;
        }

        if (Type== EnemyOrAwardType.BlueCube)
        {
            if (gameObject.transform.localScale.y>0.8)
            {
                fangDa = false;
                suoXiao = true;
            }
            if (gameObject.transform.localScale.y <0.41)
            {
                fangDa = true ;
                suoXiao = false;
            }
            if (fangDa)
            {
                if (dir==1)
                {
                    gameObject.transform.localScale += Time.deltaTime * 0.15f * Vector3.one;
                }
                else
                {
                    gameObject.transform.localScale += Time.deltaTime * 0.15f * new Vector3(-1,1,1);
                }
       
                trail.startWidth = gameObject.transform.localScale.y * 4;



            }
            if (suoXiao)
            {
                if (dir == 1)
                {
                    gameObject.transform.localScale -= Time.deltaTime * 0.15f * Vector3.one;
                }
                else
                {
                    gameObject.transform.localScale -= Time.deltaTime * 0.15f * new Vector3(-1, 1, 1);
                }
                trail.startWidth = gameObject.transform.localScale.y * 4;
            }
           

        }

    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag=="DeadLine")
    //    {
    //        if (Mathf.Abs(transform.position.y - Camera.main.transform.position.y) < 15)
    //        {
    //            AudioManage.Instance.PlaySound("Posui");
    //        }
           
    //        TimeManage.Instance.timer.AddTimeTask((a)=> 
    //        {
    //            PoolManage.Instance.Recover(Type.ToString(), gameObject);
    //        },15000,PETimeUnit.Millisecond);
    //        gameObject.SetActive(false);
    //        Debug.Log(888);

    //        foreach (var item in trails)
    //        {
    //            item.enabled = false;
    //        }
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (dir==-1 && collision.gameObject.tag == "Right")
        {
            if (Mathf.Abs(transform.position.y-Camera.main.transform.position.y) <15 )
            {
                AudioManage.Instance.PlaySound1("Posui");

            }
           
            GameObject go = PoolManage.Instance.GetItemByCreateFun(type.ToString(), () =>
            {
                return Instantiate(GameManage.Instance.des[type]);
            }) as GameObject;
            ParticleSystem particle = go.GetComponent<ParticleSystem>();
            go.transform.position = transform.position;
            go.GetComponent<ParticleSystem>().Play();

            TimeManage.Instance.timer.AddTimeTask((a) => { PoolManage.Instance.Recover(type.ToString(), go); }, 2500, PETimeUnit.Millisecond);

            gameObject.SetActive(false);
            TimeManage.Instance.timer.AddTimeTask((b) => { PoolManage.Instance.Recover(Type.ToString(), gameObject); }, 5000, PETimeUnit.Millisecond);




            //TimeManage.Instance.timer.AddTimeTask((a) =>
            //{
            //    PoolManage.Instance.Recover(Type.ToString(), gameObject);
            //}, 15000, PETimeUnit.Millisecond);
            //gameObject.SetActive(false);
            //foreach (var item in trails)
            //{
            //    item.enabled = false;
            //}


        }
        else if (dir == 1 && collision.gameObject.tag == "Left")
        {
            if (Mathf.Abs(transform.position.y - Camera.main.transform.position.y) < 15)
            {
                AudioManage.Instance.PlaySound1("Posui");
            }
            //TimeManage.Instance.timer.AddTimeTask((a) =>
            //{
            //    PoolManage.Instance.Recover(Type.ToString(), gameObject);
            //}, 15000, PETimeUnit.Millisecond);
            //gameObject.SetActive(false);
            //foreach (var item in trails)
            //{
            //    item.enabled = false;
            //}
            GameObject go = PoolManage.Instance.GetItemByCreateFun(type.ToString(), () =>
            {
                return Instantiate(GameManage.Instance.des[type]);
            }) as GameObject;
            ParticleSystem particle = go.GetComponent<ParticleSystem>();
            go.transform.position = transform.position;
            go.GetComponent<ParticleSystem>().Play();

            TimeManage.Instance.timer.AddTimeTask((a) => { PoolManage.Instance.Recover(type.ToString(), go); }, 2500, PETimeUnit.Millisecond);

            gameObject.SetActive(false);
            TimeManage.Instance.timer.AddTimeTask((b) => { PoolManage.Instance.Recover(Type.ToString(), gameObject); }, 5000, PETimeUnit.Millisecond);


        }      
    }

}
