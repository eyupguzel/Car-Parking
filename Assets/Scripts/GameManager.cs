using System;
using System.Collections;
using System.Collections.Generic;
using Car;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    [SerializeField] private List<GameObject> cars = new List<GameObject>();

    public bool click;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            click = true;
#elif UNITY_ANDROID
        //...
#endif
    }

    public GameObject GetCar()
    {
        Debug.Log("ZORT");
        foreach (var car in cars)
        {
            if (!car.activeInHierarchy)
            {
                car.SetActive(true);
                return car;
            }
        }

        return null;
    }
}