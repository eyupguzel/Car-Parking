using System;
using Unity.Mathematics;
using UnityEngine;

namespace Car
{
    public class CarController : MonoBehaviour
    {
         private GameObject parent;
        [SerializeField] float carSpeed;
        private Rigidbody rb;
        private CurrentLevelManager currentLevelManager;
        private bool platformTrigger;

        private GameObject parentPool;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            parent = GameObject.FindWithTag("Platform");
            currentLevelManager = FindObjectOfType<CurrentLevelManager>();
            parentPool = GameObject.FindWithTag("CarPool");
            
        }

        private void OnEnable()
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            platformTrigger = false;
            parent = GameObject.FindWithTag("Platform");
            currentLevelManager = FindObjectOfType<CurrentLevelManager>();
        }

        void Update()
        {
            if (GameManager.Instance.click && !platformTrigger)
            {
                OnClick();
            }
          
            if (UIManager.finished)
            {
                transform.SetParent(parentPool.transform);
                gameObject.SetActive(false);
                
            }
        }

        private void OnClick()
        {
            transform.position += transform.forward * (carSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PlatformTrigger"))
            {
                platformTrigger = true;
                
                gameObject.transform.SetParent(parent.transform, true);
                rb.velocity = Vector3.zero;
                currentLevelManager.GetCar();
                currentLevelManager.carCount--;
                UIManager.Instance.CarCountText(currentLevelManager.carCount);
                GameManager.Instance.click = false;
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }
}