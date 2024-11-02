using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Car
{
    public class CarController : MonoBehaviour
    {
         //private GameObject parent;
        [SerializeField] float carSpeed;
        private Rigidbody rb;
        //private CurrentLevelManager currentLevelManager;
        private bool platformTrigger;

        private float timer;

        private GameObject parentPool;
        private Transform firstChildObject;
        private bool state;
        private bool stopped;

        private Quaternion targetRotation1 = Quaternion.Euler(-10, 180, 0); 
        private Quaternion targetRotation2 = Quaternion.Euler(0, 180, 0);
        private Quaternion targetRotation3 = Quaternion.Euler(10, 180, 0); 

        [SerializeField] private float rotationSpeed = 5f;

       public
        enum CarState
        {
            idle,
            stopping,
            moving
        }

        public CarState carState;

        public void SetCarState(CarState state)
        {
            carState = state;
        }
        private void Awake()
        {
            
            rb = GetComponent<Rigidbody>();
            parentPool = GameObject.FindWithTag("CarPool");

            firstChildObject = gameObject.transform.GetChild(0);
            Debug.Log(firstChildObject.name);
        }

        private void OnEnable()
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            platformTrigger = false;
            //parent = GameObject.FindWithTag("Platform");
            //currentLevelManager = FindObjectOfType<CurrentLevelManager>();
            rb.isKinematic = false;
        }

        void FixedUpdate()
        {
           switch (carState)
           {
               case CarState.moving: OnMoving();
                   MovedRotation();
                   break;
               case CarState.stopping: OnStopping();
                   StoppedRotation();
                   break;
           }
           
           if (UIManager.finished)
           {
               transform.SetParent(parentPool.transform);
               gameObject.SetActive(false);

           }
        }

        
        
        private void OnMoving()
        {
            if (!platformTrigger)
            {
                transform.position += transform.forward * (carSpeed * Time.deltaTime);
               // StartCoroutine(_Click());

            }
        }

        private void OnStopping()
        {
            rb.linearVelocity = Vector3.zero;
        }

        private void StoppedRotation()
        {
            if (stopped)
            {
                firstChildObject.rotation = Quaternion.Lerp(firstChildObject.transform.rotation, targetRotation3, Time.deltaTime * rotationSpeed);
                StartCoroutine(Rotating());
                stopped = false;
            }
        }

        private void MovedRotation()
        {
            if (!state)
            {
                firstChildObject.rotation = Quaternion.Lerp(firstChildObject.transform.rotation, targetRotation1, Time.deltaTime * rotationSpeed);
                StartCoroutine(Rotating());
                state = true;
            }
           
        }

        private IEnumerator Rotating()
        {
            yield return new WaitForSeconds(.25f);
            firstChildObject.rotation = Quaternion.Lerp(firstChildObject.transform.rotation, targetRotation2, Time.deltaTime * (rotationSpeed * 3));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PlatformTrigger"))
            {
                platformTrigger = true;
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                
                GameManager.carCount--;
                UIManager.Instance.CarCountText(GameManager.carCount);
                
                SetCarState(CarState.stopping);
                stopped = true;
                rb.isKinematic = true;
                
                //platformTrigger = false;
                GameManager.Instance.CheckCarCount();

            }

            if (other.gameObject.CompareTag("Diamond"))
            {
                other.gameObject.SetActive(false);
                AudioManager.Instance.CollactableDiamondSound();
            }

            if (other.gameObject.CompareTag("Obstacle"))
            {
                AudioManager.Instance.CrashSound();
                gameObject.SetActive(false);
                UIManager.Instance.FinishPanel();
            }
        }
    }
}
