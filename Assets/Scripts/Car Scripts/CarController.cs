using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        public ParticleSystem[] sandParticles = new ParticleSystem[2];
        private string ground;
        private string currentSurface;
        public CheckPointManager checkPointManager;
        public CommandManager commandManager;

        public static SplineAnimate spline;
        private float progress;
        [SerializeField] float carSpeed;

        private Rigidbody rb;


        private float timer;

        private GameObject parentPool;
        private Transform firstChildObject;
        
        private bool state;
        private bool stopped;
        private bool platformTrigger;

        private Quaternion targetRotation1 = Quaternion.Euler(-10, 180, 0);
        private Quaternion targetRotation2 = Quaternion.Euler(0, 180, 0);
        private Quaternion targetRotation3 = Quaternion.Euler(10, 180, 0);

        public  bool _checked;
        public Vector3 startPosition = new Vector3(-.33f,0.6f,-3.71f);
        [SerializeField] private float rotationSpeed = 5f;

        public static Action collectDiamond;
        

        public enum CarState
        {
            empty,
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
            GameManager.Instance.gameStarted += ResetValues;
            GameManager.Instance.CrashHandler += Crashed;
            rb = GetComponent<Rigidbody>();
            parentPool = GameObject.FindWithTag("CarPool");
            firstChildObject = gameObject.transform.GetChild(0);

            spline = GetComponent<SplineAnimate>();
        }
        void OnDisable()
        {
            SetCarState(CarState.stopping);
            transform.position = startPosition;
        }

        public void ResetValues()
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            platformTrigger = false;
            rb.isKinematic = false;
            SetCarState(CarState.stopping);
        }

        private void FixedUpdate()
        {
            switch (carState)
            {
                case CarState.moving:
                    OnMoving();
                    CheckGround();
                   // MovedRotation();
                    break;
                case CarState.stopping:
                    OnStopping();
                    AllParticleSystemStop();
                    //StoppedRotation();
                    break;
            }

            if (GameManager.finished)
            {
                transform.SetParent(parentPool.transform);
                gameObject.SetActive(false);
            }
        }

        void Update()
        {
            //CheckGround();
        }


        private void OnMoving()
        {
            if (!platformTrigger)
            {
                spline.Play();
            }
        }

        private void OnStopping()
        {
            spline.Pause();
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
                platformTrigger = true;
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                SetCarState(CarState.stopping);
                stopped = true;
                rb.isKinematic = true;
                transform.position = startPosition;

                GameManager.finished = true;
            }

            if (other.gameObject.CompareTag("Diamond"))
            {
                collectDiamond?.Invoke();
                other.gameObject.SetActive(false);
                SaveSystem.Instance.data.totalDiamonds += 1;
                SaveSystem.Instance.SaveData();
            }

            if (other.gameObject.CompareTag("Obstacle"))
            {
                GameManager.isCrahs = true;
            }

            if (other.gameObject.CompareTag("CheckPoint"))
            {
                progress = spline.NormalizedTime;

                checkPointManager.SetCheckPoint(transform);
                _checked = true;
                FallowCamera.cameraStop = true;
                other.gameObject.SetActive(false);
                SetCarState(CarState.stopping);
            }

        }
        
        public void Crashed()
        {
            UpdateSplineProgress();

            SetCarState(CarState.stopping);
            if (GameManager.livesLeft > 0 && _checked)
            {
                var resetCommand = new ResetToCheckpointCommand(transform,
                    checkPointManager.GetCheckPointPosition(),
                    checkPointManager.GetCheckPointRotation());
                commandManager.ExecuteCommand(resetCommand);
            }
            else if (GameManager.livesLeft > 0 && !_checked)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (GameManager.livesLeft < 0)
            {
                GameManager.finished = true;
                gameObject.SetActive(false);
            }

            rb.isKinematic = true;
        }

        public static void UpdateSpline(SplineContainer _spline)
        {
            spline.NormalizedTime = 0f;
            spline.Container = _spline;
        }

        private void UpdateSplineProgress()
        {
            spline.NormalizedTime = progress;
        }

        
        private void CheckGround()
        {
            Debug.Log("a");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                ground = hit.collider.tag;
                    currentSurface = ground;
                    UpdateSurface(currentSurface);
                
            }
        }

        private void UpdateSurface(string surface)
        {
            Debug.Log("b");

            switch (surface)
            {
                case "Sand": sandParticles[0].Play(); sandParticles[1].Play(); break;
                case "Asphalt" : break;
            }
        }

        private void AllParticleSystemStop()
        {
            Debug.Log("c");

            foreach (ParticleSystem sand in sandParticles)
            {
                sand.Stop();
            }
        }
        
    }
}