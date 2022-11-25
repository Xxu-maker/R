/****************************************************
	文件：MainManage.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnviromentMode
{
    Developing,
    Test,
    Production,
}

//  让具体模块 继承这个脚本  比如测试   开发 
public abstract class MainManage : MonoBehaviour
{
    public EnviromentMode Mode;

    private static EnviromentMode mSharedMode;
    private static bool mModeSetted = false;

    void Start()
    {
        if (!mModeSetted)
        {
            mSharedMode = Mode;
            mModeSetted = true;
        }

        switch (mSharedMode)
        {
            case EnviromentMode.Developing:
                LaunchInDevelopingMode();
                break;
            case EnviromentMode.Test:
                LaunchInTestMode();
                break;
            case EnviromentMode.Production:
                LaunchInProductionMode();
                break;
        }





    }

    protected abstract void LaunchInDevelopingMode();
    protected abstract void LaunchInTestMode();
    protected abstract void LaunchInProductionMode();
}
