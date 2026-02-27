using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private GameObject _testObj;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.IsRacing = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.IsRacing = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_testObj, transform.position, transform.rotation);
        }
    }
}
