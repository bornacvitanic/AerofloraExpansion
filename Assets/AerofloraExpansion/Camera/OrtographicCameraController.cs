using UnityEngine;

namespace AerofloraExpansion.Camera
{
    public class OrthographicCameraController : MonoBehaviour
    {
        [SerializeField] private float panSpeed = 20f;
        [SerializeField] private float zoomSpeed = 5f;
        [SerializeField] private float rotationSpeed = 100f;
        [SerializeField] private float tiltSpeed = 100f;
        [SerializeField] private UnityEngine.Camera controlledCamera;
        
        private Vector3 actionPoint;
        private bool actionStarted = false;

        private void Awake()
        {
            if (controlledCamera == null)
            {
                controlledCamera = GetComponent<UnityEngine.Camera>();
            }
        }

        private void Update()
        {
            ZoomCamera();
            RotateCamera();
            TiltCamera();
            PanCamera();
        }

        private bool StartAction()
        {
            Ray ray = controlledCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                actionPoint = hit.point;
                return true;
            }
            return false;
        }

        private void PanCamera()
        {
            if (!Input.GetMouseButton(0)) return;
            if (actionStarted) return; // Prevent panning while rotating or performing other actions

            var moveX = Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
            var moveY = Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;

            // Calculate the direction to pan the camera based on its orientation
            Vector3 moveHorizontal = -transform.right * moveX;
            Vector3 moveVertical = -transform.up * moveY;

            // Combine the horizontal and vertical movement directions
            Vector3 moveDirection = moveHorizontal + moveVertical;

            // Apply the movement to the camera's position
            transform.position += moveDirection;

            // Alternatively, to keep the camera movement strictly horizontal/vertical relative to the ground plane,
            // you can ignore the camera's up/down tilt (pitch) and only use its left/right rotation (yaw) by adjusting the moveVertical calculation:
            // Vector3 moveVertical = Quaternion.Euler(0, transform.eulerAngles.y, 0) * Vector3.forward * moveY;
        }

        private void ZoomCamera()
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            controlledCamera.orthographicSize = Mathf.Max(controlledCamera.orthographicSize - scroll, 1f);
        }

        private void RotateCamera()
        {
            if (Input.GetMouseButtonDown(1) && StartAction())
            {
                actionStarted = true;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                actionStarted = false;
            }

            if (actionStarted)
            {
                var rotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
                transform.RotateAround(actionPoint, Vector3.up, rotation);
            }
        }

        private void TiltCamera()
        {
            if (Input.GetMouseButtonDown(2) && StartAction())
            {
                actionStarted = true;
            }
            else if (Input.GetMouseButtonUp(2))
            {
                actionStarted = false;
            }

            if (actionStarted)
            {
                var tilt = Input.GetAxis("Mouse Y") * tiltSpeed * Time.deltaTime;
                var currentRotation = transform.eulerAngles;
                var desiredXRotation = Mathf.Clamp(currentRotation.x + tilt, 0, 90);
                transform.RotateAround(actionPoint, transform.right, desiredXRotation - currentRotation.x);
            }
        }
    }
}