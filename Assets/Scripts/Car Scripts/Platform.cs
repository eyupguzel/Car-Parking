using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * (Time.deltaTime * 15f));
    }
}
