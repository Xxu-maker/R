/****************************************************
	文件：PlayerCC.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：角色管理器
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerCC : MonoBehaviour
{
    //角色放大的速度 以及 移动速度  或许？
    public float largenSpeed;
    public float speed;

    //用于播放向上 向下音效的声音组件，不知道当时为何这么写，大家根据自己习惯即可
    public AudioSource source1;
    public AudioSource source2;

    //无敌效果的特效预制体
    public GameObject wudiE;
    public bool wudi;
    public float wudiTime;

    public bool isGameover;
    bool dir;//角色的移动方向，true就上移或者下移之类的
    float Y;//记录Y轴位置

    //记录初始位置，估计是重置游戏时用的
    Vector3 originalPos;

  
    public ParticleSystem particle;

    private void Start()
    {
        //存储角色初始位置
        originalPos = transform.position;

        //注册重置游戏时，角色要做的事情，  具体细节不必深究，可能是瞎写的
        EventManage.Instance.On("ReStart", (object[] o) =>
        {
            AudioManage.Instance.audioMusic.Play();
            transform.localScale = Vector3.one;
            dir = false;
            Time.timeScale = 1;
            transform.position = originalPos;
            gameObject.SetActive(true);         
            isGameover = false;
            Y = 0;
            return null;
        });
    }
    private void Update()
    {
        if (wudiTime>0)
        {
           
            wudiTime -= Time.deltaTime;
            wudi = true;

        }
        else
        {
            wudiE.SetActive(false);
            wudi = false;
        }

        //控制角色上移下移的逻辑 
        //PS:这个游戏的设定是撞到敌人角色就死亡  但是如果角色撞到敌人的尾巴 或者敌人撞到角色的尾巴，就额外加分， 
        //   撞到敌人的逻辑 写在了碰撞检测的事件函数里，碰到尾巴的逻辑写在了这里的射线检测里。
        //   因为据我测试，子物体的碰撞器被撞了，父物体也会有反应之类的，具体我忘记了，大家可以自行测试一下
        //   ★★★★★★★  下面的代码虽然很多，但都是些很白痴的代码， 耐心仔细看一遍的话 基本都能看懂。大家也可以试着重构一下代码，去掉一些冗余的，
        //                当时写的时候经验一是懒得动脑，二是经验也不是很足，代码比较幼稚。
        if (!isGameover)
        {
            RaycastHit2D[] hit= Physics2D.RaycastAll(transform.position,-transform.up,4);
            foreach (var item in hit)
            {
                if (item.collider.tag=="Enemy")
                {
                    AudioManage.Instance.PlaySound3("addScore");
                    CameraShake.instance.shake();
                    UIManage.Instance.Score++;
                    GameObject go1 = PoolManage.Instance.GetItemByCreateFun("addPartical", () =>
                    {
                        return Instantiate(GameManage.Instance.addPartical);
                    }) as GameObject;
                    go1.SetActive(true);
                    go1.transform.position = item.collider.gameObject.transform.position;
                    TimeManage.Instance.timer.AddTimeTask((a) => { PoolManage.Instance.Recover("addPartical", go1); go1.SetActive(false); }, 3000, PETimeUnit.Millisecond);
                    particle.Stop();
                    particle.Play();
                  
                    GameObject go = PoolManage.Instance.GetItemByCreateFun("ADD", () =>
                    {
                        return Instantiate(GameManage.Instance.addScorePartical);
                    }) as GameObject;
                    go.SetActive(true);
                    go.transform.position = item.collider.transform.position;
                    TimeManage.Instance.timer.AddTimeTask((a) => { PoolManage.Instance.Recover("ADD", go); go.SetActive(false); }, 3000, PETimeUnit.Millisecond);                 
                    item.collider.gameObject.transform.parent.gameObject.SetActive(false);
                    item.collider.gameObject.transform.parent.GetComponent<EnemyCC>().Trail();
                    TimeManage.Instance.timer.AddTimeTask((b)=> 
                    {
                        PoolManage.Instance.Recover(item.collider.gameObject.transform.parent.gameObject.GetComponent<EnemyCC>().Type.ToString(), item.collider.gameObject.transform.parent.gameObject);
                    },15000,PETimeUnit.Millisecond);
                    
                    EventManage.Instance.Event("AddScoreText", new object[] { go });
                }
               
            }

            transform.localScale += largenSpeed* Vector3.one;
            if (Input.GetMouseButtonDown(0))
            {
                dir = !dir;
                if (dir)
                {
                    source1.Play();
                }
                else
                {
                    source2.Play();
                }
               
                transform.DOScale(Vector3.one,0.2f);
               // transform.localScale = Vector3.one;
            }
            if (dir)
            {
                transform.Translate(0, -Time.deltaTime * speed, 0);
            }
            else
            {
                transform.Translate(0, Time.deltaTime * speed, 0);
            }
            if (transform.position.y>Y)
            {
              
                Y = transform.position.y;
                GameManage.Instance.Y = Y;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Line")
        {
            AudioManage.Instance.PlaySound2("Line_2020-02-28-19-06-37");
            CameraShake.instance.shake();
            UIManage.Instance.Score++;
            //回收line
            PoolManage.Instance.Recover("Line", collision.transform.parent.gameObject);
            collision.transform.parent.gameObject.SetActive(false);
            //生成粒子特效
            GameObject go = PoolManage.Instance.GetItemByCreateFun("LinePartical", () =>
            {
                return Instantiate(GameManage.Instance.linePartical);
            }) as GameObject;
            go.SetActive(true);
            go.transform.position = collision.gameObject.transform.position;
            TimeManage.Instance.timer.AddTimeTask((a) => { PoolManage.Instance.Recover("LinePartical", go); go.SetActive(false); }, 3000, PETimeUnit.Millisecond);
        }
        if (collision.collider.tag == "Enemy" || collision.collider.tag == "DeadLine")
        {
            if (collision.collider.tag == "Enemy" &&wudi)
            {
                UIManage.Instance.Score++;

                AudioManage.Instance.PlaySound3("addScore");
                Debug.Log("ADD");
                particle.Stop();
                particle.Play();
                GameObject go1 = PoolManage.Instance.GetItemByCreateFun("addPartical", () =>
                {
                    return Instantiate(GameManage.Instance.addPartical);
                }) as GameObject;
                go1.SetActive(true);
                go1.transform.position = collision.gameObject.transform.position;
                TimeManage.Instance.timer.AddTimeTask((a) => { PoolManage.Instance.Recover("addPartical", go1); go1.SetActive(false); }, 3000, PETimeUnit.Millisecond);

                collision.collider.gameObject.transform.parent.gameObject.SetActive(false);
                PoolManage.Instance.Recover("Enemy", collision.collider.gameObject.transform.parent.gameObject);

                GameObject go2 = PoolManage.Instance.GetItemByCreateFun("ADD", () =>
                {
                    return Instantiate(GameManage.Instance.addScorePartical);
                }) as GameObject;
                go2.SetActive(true);
                go2.transform.position = collision.gameObject.transform.position;
                TimeManage.Instance.timer.AddTimeTask((a) => { PoolManage.Instance.Recover("ADD", go2); go2.SetActive(false); }, 3000, PETimeUnit.Millisecond);
                EventManage.Instance.Event("AddScoreText", new object[] { go2 });
                return;
            }
            AudioManage.Instance.audioSound.Stop();
            AudioManage.Instance.PlaySound3("Dead");
            AudioManage.Instance.StopMusic();
            EventManage.Instance.Event("Des", null);
            EventManage.Instance.Event("Gameover", null);
            isGameover = true;
            Time.timeScale = 0.2f;
            UIManage.Instance.ShowGameoverTip(true);

            GameObject go = PoolManage.Instance.GetItemByCreateFun("addPartical", () =>
            {
                return Instantiate(GameManage.Instance.addPartical);
            }) as GameObject;
            go.SetActive(true);
            go.transform.position = transform.position;
            TimeManage.Instance.timer.AddTimeTask((a) => { PoolManage.Instance.Recover("addPartical", go); go.SetActive(false); }, 3000, PETimeUnit.Millisecond);

            gameObject.SetActive(false);
        }
        if (collision.collider.tag == "ADD")
        {

            CameraShake.instance.shake();
            UIManage.Instance.Score++;
            Debug.Log("ADD");
            particle.Stop();
            particle.Play();
            GameObject go1 = PoolManage.Instance.GetItemByCreateFun("addPartical", () =>
            {
                return Instantiate(GameManage.Instance.addPartical);
            }) as GameObject;
            go1.SetActive(true);
            go1.transform.position = collision.gameObject.transform.position;
            TimeManage.Instance.timer.AddTimeTask((a) => { PoolManage.Instance.Recover("addPartical", go1); go1.SetActive(false); }, 3000, PETimeUnit.Millisecond);

            collision.collider.gameObject.transform.parent.GetComponent<EnemyCC>().Trail();
            collision.collider.gameObject.transform.parent.gameObject.SetActive(false);
            TimeManage.Instance.timer.AddTimeTask((b) =>
            {
                PoolManage.Instance.Recover(collision.collider.gameObject.transform.parent.gameObject.GetComponent<EnemyCC>().Type.ToString(), collision.collider.gameObject.transform.parent.gameObject);
            }, 15000, PETimeUnit.Millisecond);

           

            GameObject go = PoolManage.Instance.GetItemByCreateFun("ADD", () =>
            {
                return Instantiate(GameManage.Instance.addScorePartical);
            }) as GameObject;
            go.SetActive(true);
            go.transform.position = collision.gameObject.transform.position;
            TimeManage.Instance.timer.AddTimeTask((a) => { PoolManage.Instance.Recover("ADD", go); go.SetActive(false); }, 3000, PETimeUnit.Millisecond);
     
            EventManage.Instance.Event("AddScoreText", new object[] { go });



        }

        if (collision.collider.tag == "Bomb")
        {
            AudioManage.Instance.PlaySound3("add");
            CameraShake.instance.shake();
            Time.timeScale = 0.4f;
            TimeManage.Instance.timer.AddTimeTask((a)=> { Time.timeScale = 1; },450,PETimeUnit.Millisecond);
            Debug.Log("Bomb");
            EventManage.Instance.Event("BOMB", null);
            collision.collider.transform.parent.gameObject.SetActive(false);
            PoolManage.Instance.Recover(collision.collider.transform.parent.gameObject.GetComponent<AwardCC>().Type.ToString(), collision.collider.transform.parent.gameObject);
        }
        if (collision.collider.tag == "SlowDown")
        {
            AudioManage.Instance.PlaySound3("add");
            AudioManage.Instance.PlaySound("Jishi");
            EventManage.Instance.Event("JiShi",null);
            GameManage.Instance.slowDownTime = 5f;
            
            Debug.Log("SlowDown");
            collision.collider.transform.parent.gameObject.SetActive(false);
            PoolManage.Instance.Recover(collision.collider.transform.parent.gameObject.GetComponent<AwardCC>().Type.ToString(), collision.collider.transform.parent.gameObject);
        }
        if (collision.collider.tag == "AddScore")
        {
            AudioManage.Instance.PlaySound3("add");
            Debug.Log(8888888);
            GameObject go = PoolManage.Instance.GetItemByCreateFun("ADD", () =>
            {
                return Instantiate(GameManage.Instance.addScorePartical);
            }) as GameObject;
            go.SetActive(true);
            go.transform.position = collision.gameObject.transform.position;
            TimeManage.Instance.timer.AddTimeTask((a) => { PoolManage.Instance.Recover("ADD", go); go.SetActive(false); }, 3000, PETimeUnit.Millisecond);
            EventManage.Instance.Event("AddScoreText1", new object[] {go });
            UIManage.Instance.Score+=5;
            Debug.Log("AddScore");
            collision.collider.transform.parent.gameObject.SetActive(false);
            PoolManage.Instance.Recover(collision.collider.transform.parent.gameObject.GetComponent<AwardCC>().Type.ToString(), collision.collider.transform.parent.gameObject);
        }
        if (collision.collider.tag == "Invincible")
        {
            AudioManage.Instance.PlaySound3("add");
            AudioManage.Instance.PlaySound("Jishi");
           
            wudiE.SetActive(true);
            EventManage.Instance.Event("JiShi", null);
            wudiTime = 5f;

            Debug.Log("Invincible");
            collision.collider.transform.parent.gameObject.SetActive(false);
            PoolManage.Instance.Recover(collision.collider.transform.parent.gameObject.GetComponent<AwardCC>().Type.ToString(), collision.collider.transform.parent.gameObject);
        }



    }
    


}
