using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform cameraPivot;
    public float sensitivity = 0.1f;
    public float minAngle = -35f;
    public float maxAngle = 60f;

    private InputAction lookAction;
    private float verticalRotation;

    private void Awake()
    {
        lookAction = InputSystem.actions.FindAction("Look");
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (cameraPivot == null || lookAction == null)
        {
            return;
        }

        Vector2 look = lookAction.ReadValue<Vector2>();

        // Olhar para cima e para baixo
        verticalRotation -= look.y * sensitivity;
        verticalRotation = Mathf.Clamp(
            verticalRotation, minAngle, maxAngle
        );

        cameraPivot.localRotation =
            Quaternion.Euler(verticalRotation, 0f, 0f);

        // Girar o personagem para os lados
        transform.Rotate(
            Vector3.up * look.x * sensitivity
        );
    }
}
