using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool IsRacing;
    public bool IsPrized;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        Init();
    }

    void Init()
    {
        IsRacing = false;
        IsPrized = false;
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