/****************************************************
	文件：WindBase.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelBase : MonoBehaviour
{
    public virtual void HideOrShow(bool isActive = true)
    {
        if (gameObject.activeSelf != isActive)
        {
            SetActive(gameObject, isActive);
        }
        if (isActive)
        {
            Init();
        }
        else
        {
            CleanWind();
        }
    }
    protected virtual void Init()
    {

    }
    protected void CleanWind()
    {
    }
    
    //  SetActive 
    public void SetActive(GameObject go, bool isActive = true)
    {
        go.SetActive(isActive);
    }
    public void SetActive(Transform trans, bool isActive = true)
    {
        trans.gameObject.SetActive(isActive);
    }
    public void SetActive(RectTransform trans, bool isActive = true)
    {
        trans.gameObject.SetActive(isActive);
    }
    public void SetActive(Image image, bool isActive = true)
    {
        image.gameObject.SetActive(isActive);
    }
    public void SetActive(Text text, bool isActive = true)
    {
        text.gameObject.SetActive(isActive);
    }



    // Set
    public void SetText(Text text, string value = "")
    {
        text.text = value;
    }
    public void SetText(Text text, int value = 0)
    {
        text.text = value.ToString();
    }
    public void SetText(Text text, float value = 0)
    {
        text.text = value.ToString();
    }
    public void SetText(Text text, double value = 0)
    {
        text.text = value.ToString();
    }


    public void SetText(Transform trans, string value = "")
    {
        trans.GetComponent<Text>().text = value;
    }
    public void SetText(Transform trans, int value = 0)
    {
        trans.GetComponent<Text>().text = value.ToString(); ;
    }
    public void SetText(Transform trans, float value = 0)
    {
        trans.GetComponent<Text>().text = value.ToString(); ;
    }
    public void SetText(Transform trans, double value = 0)
    {
        trans.GetComponent<Text>().text = value.ToString(); ;
    }


    public void SetText(GameObject go, string value = "")
    {
        go.GetComponent<Text>().text = value; ;
    }
    public void SetText(GameObject go, int value = 0)
    {
        go.GetComponent<Text>().text = value.ToString(); ;
    }
    public void SetText(GameObject go, float value = 0)
    {
        go.GetComponent<Text>().text = value.ToString(); ;
    }
    public void SetText(GameObject go, double value = 0)
    {
        go.GetComponent<Text>().text = value.ToString(); ;
    }

    public void SetSprite(Button button, string path)
    {
        button.image.sprite = LoadManage.Instance.LoadSprite(path);
    }

   

}
