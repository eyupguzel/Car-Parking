using System;
using System.Collections.Generic;
using UnityEngine;
using Car;
using Unity.VisualScripting;

public class CurrentLevelManager : MonoBehaviour
{
    public List<GameObject> cars;
    public Transform spawnPoint;
    [SerializeField] private int livesLeft;

    private void Awake()
    {
        cars = GameManager.Instance.GetCarPool(1);
        GetCar();
    }

    private void Start()
    {
        UpdateLivesLeft();
        CarController.isFinish = false;
        UIManager.finished = false;
        UIManager.Instance.CloseFinishPanel();
    }

    public void UpdateLivesLeft()
    {
        GameManager.livesLeft = livesLeft;
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
}