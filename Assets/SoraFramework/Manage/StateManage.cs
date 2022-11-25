/****************************************************
	文件：StateManage.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateEnum
{
    
}
public interface IState
{
    void Enter(EntityBase entity, params object[] args);
    void Exit(EntityBase entity, params object[] args);
    void Process(EntityBase entity, params object[] args);
}
public class EntityBase
{

}
public class StateManage : MonoBehaviour
{
    public Dictionary<StateEnum, IState> fsm = new Dictionary<StateEnum, IState>();
    public void ChangeState(EntityBase entity, StateEnum target, params object[] args)
    {
       
    }

}


