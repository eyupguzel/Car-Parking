using UnityEngine;

public class CurrentLevelManager : MonoBehaviour
{
    [SerializeField] GameObject platform;
    [SerializeField] float rotationSpeed;
    
    private void Start()
    {
        GameManager.Instance.GetCar();
    }

    void Update()
    {
        PlatformRotation();
    }

    private void PlatformRotation()
    {
        platform.transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime), Space.Self);
    }
}