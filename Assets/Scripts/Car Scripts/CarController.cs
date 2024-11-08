using System;
using System.Collections;
using System.Collections.Generic;
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

        public static bool isFinish;
        private GameObject parentPool;
        private Transform firstChildObject;
        private bool state;
        private bool stopped;

        private Quaternion targetRotation1 = Quaternion.Euler(-10, 180, 0);
        private Quaternion targetRotation2 = Quaternion.Euler(0, 180, 0);
        private Quaternion targetRotation3 = Quaternion.Euler(10, 180, 0);

        public static bool _checked;
        public Vector3 startPosition;
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
            GameManager.Instance.checkPoint = transform.position;
        }

        private void OnEnable()
        {
            startPosition = transform.position;
            _checked = false;
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
                case CarState.moving:
                    OnMoving();
                    MovedRotation();
                    break;
                case CarState.stopping:
                    OnStopping();
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
                rb.linearVelocity = transform.forward * carSpeed;
                rb.isKinematic = false;
                _checked = false;
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
                firstChildObject.rotation = Quaternion.Lerp(firstChildObject.transform.rotation, targetRotation3,
                    Time.deltaTime * rotationSpeed);
                StartCoroutine(Rotating());
                stopped = false;
            }
        }

        private void MovedRotation()
        {
            if (!state)
            {
                firstChildObject.rotation = Quaternion.Lerp(firstChildObject.transform.rotation, targetRotation1,
                    Time.deltaTime * rotationSpeed);
                StartCoroutine(Rotating());
                state = true;
            }
        }

        private IEnumerator Rotating()
        {
            yield return new WaitForSeconds(.25f);
            firstChildObject.rotation = Quaternion.Lerp(firstChildObject.transform.rotation, targetRotation2,
                Time.deltaTime * (rotationSpeed * 3));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("PlatformTrigger"))
            {
                isFinish = true;
                platformTrigger = true;
                UIManager.Instance.FinishPanel();
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                SetCarState(CarState.stopping);
                stopped = true;
                rb.isKinematic = true;
            }

            if (other.gameObject.CompareTag("Diamond"))
            {
                other.gameObject.SetActive(false);
                AudioManager.Instance.SoundPlay(AudioManager.AudioType.DiamondSound);
            }

            if (other.gameObject.CompareTag("Obstacle"))
            {
                GameManager.livesLeft--;
                SetCarState(CarState.stopping);
                AudioManager.Instance.SoundPlay(AudioManager.AudioType.CrashSound);

                if (GameManager.livesLeft > 0 && _checked)
                {
                    transform.position = GameManager.Instance.checkPoint;
                }
                else if (GameManager.livesLeft > 0)
                {
                    transform.position = startPosition;
                } 
                else if (GameManager.livesLeft <= 0)
                {
                    gameObject.SetActive(false);
                    UIManager.Instance.FinishPanel();
                }

                rb.isKinematic = true;
            }

            if (other.gameObject.CompareTag("CheckPoint"))
            {
                GameManager.Instance.checkPoint = other.gameObject.transform.position;
                _checked = true;
                other.gameObject.SetActive(false);
                //rb.isKinematic = true;
                SetCarState(CarState.stopping);
            }
        }
    }
}