using UnityEngine;
[CreateAssetMenu(fileName = "CarSO",menuName = "ScriptableObjects/CarSO")]
public class CarSO : ScriptableObject
{
    public Mesh car;
    public string carName;
    public bool isPurchased;
    public int price;
    public int speed;
    public bool selected;
}
