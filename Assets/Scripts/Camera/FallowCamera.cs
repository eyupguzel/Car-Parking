using System.Collections;
using UnityEngine;

using Car;

public class FallowCamera : MonoBehaviour
{
    private GameObject car;
    private Vector3 offset;
    
    void Start()
    {
        car = GameObject.FindWithTag("Car");

        Debug.Log(car.name);
            offset =transform.position - car.transform.position;
    }

    
    void Update()
    {
        if (CarController._checked)
        {
            transform.position = Vector3.Lerp(transform.position,car.transform.position + offset,Time.deltaTime * 10f);
            StartCoroutine(checkTimer());
        }
    }

    private IEnumerator checkTimer()
    {
        yield return new WaitForSeconds(1.5f);
        CarController._checked = false;
    }
}
