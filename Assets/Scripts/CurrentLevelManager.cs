using System;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLevelManager : MonoBehaviour
{
    [SerializeField] GameObject platform;
    [SerializeField] float rotationSpeed;
    public int carCount;
    private List<GameObject> cars;
    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        GameManager.Instance.ClickFalse();
        cars = GameManager.Instance.GetCarPool(carCount);
        //GameManager.Instance.ReturnCar(cars);
    }

    private void Start()
    {
        UIManager.finished = false;

        UIManager.Instance.CarCountText(carCount);
        UIManager.Instance.CloseFinishPanel();
        GetCar();
    }

    void Update()
    {
        PlatformRotation();
        if(carCount <= 0)
            UIManager.Instance.FinishPanel();
    }

    private void PlatformRotation()
    {
        platform.transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime), Space.Self);
    }

    public void GetCar()
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

}