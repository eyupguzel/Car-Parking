using UnityEngine;

public class Level2Script : CurrentLevelManager
{
    CurrentLevelManager currentLevelManager;
    public override void GetCar()
    {
        // car.transform.position = spawnPoint.position;
        car.transform.GetChild(1).gameObject.SetActive(true);
        car.SetActive(true);
    }
}
