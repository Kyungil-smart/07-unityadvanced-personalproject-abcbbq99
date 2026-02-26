using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public SceneLoader Instance{get; private set;}
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void ConvertScene(SceneType sceneType)
    {
        SceneManager.LoadScene(sceneType.ToString());
    }
}

public enum SceneType
{
    Title, Race, Prize
}