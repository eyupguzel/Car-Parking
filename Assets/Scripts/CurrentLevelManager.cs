using System;
using System.Collections.Generic;
using UnityEngine;
using Car;
using UnityEngine.Splines;

public class CurrentLevelManager : MonoBehaviour
{
    public GameObject car;
    public Vector3 spawnPoint = new Vector3(-0.31f,-0.6f,-3.77f);
    [SerializeField] private int livesLeft;

    public SplineContainer  spline;

    private void Awake()
    {
        GameManager.isStart = true;
        car = GameManager.Instance.GetCar();
        GetCar();
        spline = GameObject.FindWithTag("Spline").GetComponent<SplineContainer>();
        CarController.UpdateSpline(spline);
    }
    private void Start()
    {
        UpdateLivesLeft();
        GameManager.finished = false;
    }

    public Vector3 SetCarPosition()
    {
       Vector3 startPosition = spline.Spline.EvaluatePosition(0f);
       return startPosition;
    }
    public void UpdateLivesLeft()
    {
        GameManager.livesLeft = livesLeft;
    }
    public virtual void GetCar()
    {
        foreach (var item in GameManager.Instance.carsSO) 
        {
            if (item.selected) 
                car.transform.GetChild(0).GetComponent<MeshFilter>().mesh = item.car;
        }
    }

    void OnDisable()
    {
        car.transform.GetChild(1).gameObject.SetActive(false);
    }
}