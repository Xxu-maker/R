/****************************************************
	文件：Test.cs
	作者：空银子
	邮箱: 1184840945@qq.com
	日期：#CreateTime# 	
	功能：Nothing
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

namespace AA
{
    public  class Test : MonoBehaviour
    {

        [MenuItem("Tool/导出资源包 %e", false, 1500)]
        private static void AA()
        {       
            AssetDatabase.ExportPackage("Assets/SoraFramework", DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss")+".unitypackage",ExportPackageOptions.Recurse);
            Application.OpenURL("file:///"+ Path.Combine(Application.dataPath, "../"));        
        }
      

    }
  

}
