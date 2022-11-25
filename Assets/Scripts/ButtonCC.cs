/****************************************************
	文件：ButtonCC.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class ButtonCC : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    public Animator ani;
    public GameObject bg;
    public Text text;
    public Animation clickAni;
    public Image image;
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManage.Instance.PlaySound("anniu_2020-02-28-19-00-01");
        clickAni.Play();
        Invoke("AA",0.75f);
        image.DOFade(1, 0.75f);
        
    }
    public void AA()
    {

        SceneManager.LoadScene("01");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {     
        bg.SetActive(true);
        ani.enabled = true;
      
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        bg.SetActive(false);
        ani.enabled = false;
        text.color = new Color(1, 1, 1, 1);
        
    }
}
