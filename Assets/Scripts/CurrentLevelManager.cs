using System;
using System.Collections.Generic;
using UnityEngine;
using Car;
using Unity.VisualScripting;

public class CurrentLevelManager : MonoBehaviour
{
    public int carCount;
    public List<GameObject> cars;
    public Transform spawnPoint;

    private void Awake()
    {
        cars = GameManager.Instance.GetCarPool(carCount);
        GetCar();
    }

    private void Start()
    {
        UIManager.finished = false;
        UpdateCarCaount();
        UIManager.Instance.CarCountText(carCount);
        UIManager.Instance.CloseFinishPanel();
    }

    public virtual void GetCar()
    {
        foreach (GameObject car in cars)
        {
            if (!car.activeInHierarchy)
            {
                car.transform.position = spawnPoint.position;
                car.SetActive(true);
                Debug.Log(car.name);
                return;
            }
        }
    }

    public void UpdateCarCaount()
    {
        GameManager.carCount = carCount;
    }
}