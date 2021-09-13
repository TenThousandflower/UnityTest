using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGameInit : MonoBehaviour
{
    private void Awake()
    {
        List<object> initData;
        initData = SceneMgr.instance.ReadSceneData();
        
    }

}
