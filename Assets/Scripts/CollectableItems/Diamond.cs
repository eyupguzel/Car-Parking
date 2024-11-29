using UnityEngine;

public class Diamond : MonoBehaviour
{
    public SaveSystem saveSystem;

    public void AddDiamond(int amount)
    {
        saveSystem.data.totalDiamonds += amount;
        saveSystem.SaveData();
    }
}
