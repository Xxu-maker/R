/****************************************************
	文件：EnemySpwan.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwan : MonoBehaviour
{
    //敌人
    public GameObject yellowCube;
    public GameObject yellowCube1;
    public GameObject redCube;
    public GameObject blueCube;

    //奖品
    public GameObject addScore;
    public GameObject bomb;
    public GameObject slowDown;
    public GameObject invincible;
 
    public GameObject line;

    public float grade;
    float gameTime;
    float time;
    bool isGameover;
    private void Start()
    {
        grade = 0.2f;
        EventManage.Instance.On("Gameover",(object[] o)=> 
        {
            isGameover = true;
            return null;
        });

        EventManage.Instance.On("ReStart", (object[] o) =>
        {
            grade = 0.2f;
            isGameover = false;
            gameTime = 0;
            return null;
        });

        EventManage.Instance.On("CreateLine",(object[] o)=> 
        {
            GameObject go = PoolManage.Instance.GetItemByCreateFun("Line",()=> 
            {
                GameObject temp= Instantiate(line);
                temp.AddComponent<LineCC>();
                return temp;
            })as GameObject ;
            go.SetActive(true);
            go.GetComponent<LineCC>().Init(o[0]);
            return null;
        });
    }
    private void Update()
    {
        gameTime += Time.deltaTime;      
        time += Time.deltaTime;
        if (grade>=0.1)
        {
            grade -= 0.01f * grade * Time.deltaTime;
        }
        if (time>grade &&isGameover==false)
        {
            time = 0;
            if (SoraUtility.RandomResult(97))
            {
                SpawnEnemy(gameTime);
            }
            else
            {
                SpawnAward();
            }
           
        }
    }
    
    public GameObject GetEnemyOrAwardItem(string type,GameObject prafab)
    {
        return PoolManage.Instance.GetItemByCreateFun(type, () =>
        {
            GameObject temp = Instantiate(prafab);
            return temp;
        }) as GameObject;
    }
    public void SpawnAward()
    {
        GameObject go;
        switch (Random.Range(0,4))
        {
            case 0:
                go = GetEnemyOrAwardItem("AddScore", addScore);
                break;
            case 1:
                go = GetEnemyOrAwardItem("SlowDown", slowDown);
                break;
            case 2:
                go = GetEnemyOrAwardItem("Bomb", bomb);
                break;
            case 3:
                go = GetEnemyOrAwardItem("Invincible", invincible);
                break;
            default:
                go = GetEnemyOrAwardItem("Invincible", invincible);
                break;
        }
        TimeManage.Instance.timer.AddTimeTask((a) => { go.SetActive(true); }, 1000, PETimeUnit.Millisecond);
        go.GetComponent<AwardCC>().dir = Random.Range(0, 2) > 0 ? 1 : -1;
        go.GetComponent<AwardCC>().Init();
    }
    public void SpawnEnemy(float time)
    {
        GameObject go;

        if (time<10)
        {
            go = GetEnemyOrAwardItem("YellowCube", yellowCube);
          // go = GetEnemyOrAwardItem("BlueCube", blueCube);
        }
        else if (time<20)
        {
            if (SoraUtility.RandomResult(80))
            { go = GetEnemyOrAwardItem("YellowCube", yellowCube);
               // go = GetEnemyOrAwardItem("BlueCube", blueCube);
            }
            else
            {
                go = GetEnemyOrAwardItem("RedCube", redCube);
            }
        }
        else if(time<30)
        {
            int a = Random.Range(0,100);
            if (a<70)
            {
                go = GetEnemyOrAwardItem("YellowCube", yellowCube);
            }
            else if(a<80)
            {
                go = GetEnemyOrAwardItem("RedCube", redCube);
            }
            else
            {
                go = GetEnemyOrAwardItem("YellowCube1", yellowCube1);
            }
        }
        else
        {
            int a = Random.Range(0, 100);
            if (a<70)
            {             
                go = GetEnemyOrAwardItem("YellowCube", yellowCube);
            }
            else if (a<80)
            {              
                go = GetEnemyOrAwardItem("RedCube", redCube);
            }
            else if(a<90)
            {           
                go = GetEnemyOrAwardItem("YellowCube1", yellowCube1);
            }
            else 
            {
                go = GetEnemyOrAwardItem("BlueCube", blueCube);             
            }
        }
        TimeManage.Instance.timer.AddTimeTask((a) => { go.SetActive(true); }, 1000, PETimeUnit.Millisecond);
        go.GetComponent<EnemyCC>().dir = Random.Range(0, 2) > 0 ? 1 : -1;
        go.GetComponent<EnemyCC>().Init();

    }
    
}
