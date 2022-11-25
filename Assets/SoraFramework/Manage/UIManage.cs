/****************************************************
	文件：UIManage.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManage : MonoBehaviour
{
    public GameObject gameoverPanel;
    public Text time;
    public Text currentScore;
    public Text history;


    public Animation scoreAni;
    public static UIManage Instance;
    public Text gameoverTip;
    public GameObject addScoreText;
    public GameObject addScoreText1;
    public Canvas canvas;

    public Text JiShi;
    private void Awake()
    {
        EventManage.Instance.On("Gameover",(object[] o)=> 
        {

            // time.text =((int)(GameManage.Instance.gameTime/60)).ToString()+":"+((int)(GameManage.Instance.gameTime % 60)).ToString();
            time.text = (int)GameManage.Instance.gameTime+"."+((int)(GameManage.Instance.gameTime * 100 % 100)).ToString();
            currentScore.text = score.ToString();
            if (score> PlayerPrefs.GetInt("Score"))
            {
                history.text = score.ToString();
                PlayerPrefs.SetInt("Score",score);
            }
            else
            {
                history.text = PlayerPrefs.GetInt("Score").ToString();
            }
            score = 0;
            scoreText.text = "";
            return null;
        });
        Instance = this;
        EventManage.Instance.On("AddScoreText",(object[] o)=> 
        {
            GameObject go= PoolManage.Instance.GetItemByCreateFun("AddscoreText",()=> 
            {
                return Instantiate(addScoreText);
            })as GameObject;
            go.SetActive(true);
            go.transform.SetParent(canvas.transform);
          //  go.transform.position =
            go.GetComponent<AddScoreTextCC>().papa = (GameObject)o[0];
            TimeManage.Instance.timer.AddTimeTask((a) => { PoolManage.Instance.Recover("AddscoreText", go); go.GetComponent<AddScoreTextCC>().papa=null; go.SetActive(false); }, 10000, PETimeUnit.Millisecond);
           // go.transform.position = (Vector3)o[0];
            
            return null;

        });
        EventManage.Instance.On("AddScoreText1", (object[] o) =>
        {
            GameObject go = PoolManage.Instance.GetItemByCreateFun("AddscoreText1", () =>
            {
                return Instantiate(addScoreText1);
            }) as GameObject;
            go.SetActive(true);
            go.transform.SetParent(canvas.transform);
            //  go.transform.position =
            go.GetComponent<AddScoreTextCC>().papa = (GameObject)o[0];
            TimeManage.Instance.timer.AddTimeTask((a) => { PoolManage.Instance.Recover("AddscoreText1", go); go.GetComponent<AddScoreTextCC>().papa = null; go.SetActive(false); }, 10000, PETimeUnit.Millisecond);
            // go.transform.position = (Vector3)o[0];

            return null;

        });
        EventManage.Instance.On("JiShi",(object[]o)=> 
        {
            JiShi.text = "";
            float time = 50;
            DOTween.To(()=>time,x=> 
            {
                if (GameManage.Instance.isGameover)
                {
                    JiShi.text = "";
                }
                else
                {
                    time = x;
                    JiShi.text = (Mathf.Floor(time) / 10.0f).ToString();
                    if (time < 1)
                    {
                        JiShi.text = "";
                    }
                }
               
            },0,5);
            return null;
        });

    }
    public Text scoreText;
    int score;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            scoreText.text = score.ToString();
            scoreAni.Stop();
            scoreAni.Play();
        }
    }

    public void ShowGameoverTip(bool state)
    {
        if (state)
        {
            gameoverTip.DOFade(1, 0.5f);
            gameoverPanel.SetActive(true);
        }
        else
        {
            gameoverTip.DOFade(0, 0.5f);
            gameoverPanel.SetActive(false);


        }
   
    }
    
}
