using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    // Configurações de pivô, sensibilidade, limites de inclinação e estado do input
    public Transform cameraPivot;
    public float sensitivity = 0.1f;
    public float minAngle = -35f;
    public float maxAngle = 60f;

    private InputAction lookAction;
    private float verticalRotation;

    // Obtém a referência da ação "Look" no Input System antes do jogo iniciar
    private void Awake()
    {
        lookAction = InputSystem.actions.FindAction("Look");
    }
    // Trava o cursor no centro da tela e o esconde
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Lê o input de visão e aplica a rotação vertical no pivô e horizontal no personagem
    private void Update()
    {
        // Interrompe a execução se o pivô ou o input não estiverem configurados
        if (cameraPivot == null || lookAction == null)
        {
            return;
        }

        Vector2 look = lookAction.ReadValue<Vector2>();

        // Olhar para cima e para baixo (limitado entre minAngle e maxAngle)
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