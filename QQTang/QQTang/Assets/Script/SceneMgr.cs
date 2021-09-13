using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public static SceneMgr instance;
    private Stack<List<object>> sceneOneShotData = new Stack<List<object>>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    public void WriteSceneData(List<object> data)
    {
        if (sceneOneShotData.Count != 0)
            Debug.LogError("数据条数不合理 " + sceneOneShotData.Count);
        
        this.sceneOneShotData.Push(data);
    }

    public List<object> ReadSceneData()
    {
        if (sceneOneShotData.Count > 1)
            Debug.LogError("数据条数不合理 " + sceneOneShotData.Count);

        return this.sceneOneShotData.Pop();
    }

    public AsyncOperation ToSceneGame(string a)
    {
        List<object> newSceneData = new List<object>();
        newSceneData.Add(a);
        SceneMgr.instance.WriteSceneData(newSceneData);

        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        return operation;
    }
}