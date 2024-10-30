using System;
using System.Collections;
using System.Collections.Generic;
using Car;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    public List<GameObject> cars =  new List<GameObject>();


    public bool click;

    private void Awake()
    {
       // Application.targetFrameRate = 60;

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            click = true;
    }

    public List<GameObject> GetCarPool(int carCount)
    {
        List<GameObject> carsToUse = new List<GameObject>();

        int index = 0;
        foreach (GameObject car in cars)
        {
            if (!car.activeInHierarchy)
            {
                carsToUse.Add(car);
                index++;

                if (index >= carCount) 
                    break;
            }
            
        }
        return carsToUse;
    }

    public void ReturnCar(List<GameObject> cars)
    {
        //Debug.Log("ben çalıştım");
        foreach (GameObject car in cars)
        {
            car.SetActive(false);
        }
    }

    public void ClickFalse()
    {
        click = false;
    }
}