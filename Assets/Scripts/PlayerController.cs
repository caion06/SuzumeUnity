using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // UnityEngine
    private Animator anim;
    private CharacterController controller;

    // Movimento
    private Vector3 move;
    private float verticalVelocity;
    private bool jumpRequested;

    [Header("Configurações")]
    [Tooltip("Variaveis de movimentação e gravidade.")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float rotationSpeed = 5f;
    

    // Metodos MonoBehaviour
    private void Start()
    {
        // Pega os componetes 
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Contem toda a movimentação do player
        Movement();

        // Animacoes
        anim.SetBool("IsRun", move.sqrMagnitude > 0.01f);
    }

    // Metodos Player Input
    private void OnMove(InputValue inputValue)
    {
        // Pega os comandos do jogador e passa para a variavel move
        Vector2 input = inputValue.Get<Vector2>();
        move = Vector3.ClampMagnitude(new Vector3(input.x, 0f, input.y), 1f);
    }

    // Recebe o comando de pulo
    private void OnJump()
    {
        // Verifica se está no chão e aplica a força de pulo casos seja verdadeiro
        if (controller.isGrounded)
        {
            verticalVelocity = jumpForce;
            anim.CrossFade("Jump", 0.1f);
        }
    }

    // Metodos encapsulados
    private void Movement()
    {
        // Pega a verificação de está no chão do CharacterController
        bool grounded = controller.isGrounded;

        // Aplica gravidade
        verticalVelocity += gravity * Time.deltaTime;

        // Combina movimento horizontal e vertical
        Vector3 velocity = move * speed;
        velocity.y = verticalVelocity;

        // Aplica a movimentação ao CharacterController
        controller.Move(velocity * Time.deltaTime);

        // Rotaciona na direcao do movimento
        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
