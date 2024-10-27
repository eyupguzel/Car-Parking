using UnityEngine;

namespace Car
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] float carSpeed;
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (GameManager.Instance.click)
            {
                OnClick();
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
                Debug.Log((other.gameObject.name));
                gameObject.transform.SetParent(parent, true);
                rb.velocity = Vector3.zero;
                Destroy(gameObject.GetComponent<CarController>());
                GameManager.Instance.GetCar();
                GameManager.Instance.click = false;
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }
}