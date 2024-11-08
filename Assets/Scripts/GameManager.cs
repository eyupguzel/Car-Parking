using System.Collections.Generic;
using Car;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    public Vector3 checkPoint;
    public List<GameObject> cars = new List<GameObject>();

    public static int livesLeft;
    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (var car in cars)
            {
                if (car.activeInHierarchy)
                {
                    car.gameObject.GetComponent<CarController>().SetCarState(CarController.CarState.moving);
                    //break;
                }
            }
        }
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

    public void ReturnCar()
    {
        foreach (GameObject car in cars)
        {
            car.SetActive(false);
        }
    }
}