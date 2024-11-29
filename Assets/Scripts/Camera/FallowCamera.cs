using System.Collections;
using UnityEngine;

using Car;

public class FallowCamera : MonoBehaviour
{
    private GameObject car;
    private Vector3 offset;
    public static bool cameraStop;
    
    void Start()
    {
        car = GameObject.FindWithTag("Car");
        offset =transform.position - car.transform.position;
    }

    
    void Update()
    {
        if (cameraStop)
        {
            transform.position = Vector3.Lerp(transform.position,car.transform.position + offset,Time.deltaTime * 10f);
            StartCoroutine(checkTimer());
        }
    }

    private IEnumerator checkTimer()
    {
        yield return new WaitForSeconds(0.5f);
        cameraStop = false;
    }
}
