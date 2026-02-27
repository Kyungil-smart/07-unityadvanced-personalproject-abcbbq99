using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static  GameManager _instance;
    public static bool IsRacing;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        
        Init();
    }

    void Init()
    {
        IsRacing = false;
        // 중간 관리자 매니저들
        GenerateManager<AudioManager>();
        GenerateManager<SceneLoader>();
    }

    void GenerateManager<T>()  where T : Component
    {
        if(FindAnyObjectByType<T>() != null) return;
        
        var go = new GameObject(typeof(T).Name);
        go.AddComponent<T>();
        DontDestroyOnLoad(go);
    }
}