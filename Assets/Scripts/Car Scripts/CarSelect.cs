using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CarSelect : MonoBehaviour
{
    private MeshFilter meshFilter;
    int totalCoin = 2350;
    int i = 0;
    GameManager gameManager;
    List<CarSO> cars;
    
    [Header("GarageUiElements")]
    public Button rightButton;
    public Button leftButton;
    public Button selectButton;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI carNameText;

    
    public enum CarType
    {
        car1 = 1,
        car2 = 2,
        car3 = 3
    }
    public static CarType carType;

    void Start()
    {
       meshFilter = GetComponent<MeshFilter>();
       cars = GameManager.Instance.carsSO;
    }

    public void RightButton()
    {
        AudioManager.Instance.MenuClickSound2();
        if (cars.Count - 1 > i)
        {
            i++;
            meshFilter.mesh = cars[i].car;
            carNameText.text = cars[i].name;
            leftButton.interactable = true;
            
            CarStats();
            if (cars.Count - 1 == i)
            {
                rightButton.interactable = false;
            }
        }
    }
    public void LeftButton()
    {
        AudioManager.Instance.MenuClickSound2();
        if (i != 0)
        {
            i--;
            meshFilter.mesh = cars[i].car;
            carNameText.text = cars[i].name;
            rightButton.interactable = true;

            CarStats();
            if (i == 0)
                leftButton.interactable = false;
        }
        
    }

    public void CarStats()
    {
        if (cars[i].isPurchased && cars[i].selected)
        {
            priceText.text = "Selected";
        }
        else if (cars[i].isPurchased && !cars[i].selected)
        {
            priceText.text = "Select";
        }
        else if (!cars[i].isPurchased)
        {
            priceText.text = $"{cars[i].price}";
        }

        if (totalCoin > cars[i].price)
        {
            selectButton.interactable = true;
        }
        else
        {
            selectButton.interactable = false;
        }
    }

    public void CarSelected()
    {
        foreach (var item in cars)
        {
            item.selected = false;
        }
        cars[i].selected = true;
        cars[i].isPurchased = true;
        priceText.text = "Selected";

        carType = (CarType)i;
    }

    public void BackToMainMenu()
    {
        AudioManager.Instance.MenuClickSound3();
        SceneManager.LoadScene(1);
    }
    
   
}
