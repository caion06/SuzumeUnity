using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Referências de componentes e estados internos de movimento
    private Animator anim;
    private CharacterController controller;

    private Vector3 move;
    private float verticalVelocity;
    private bool jumpRequested;

    // Proteções contra oscilação do CharacterController
    private bool isJumping;
    private float jumpLockTimer;
    private float groundedGraceTimer;

    [Header("Configurações")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float rotationSpeed = 10f;

    // Obtém as referências dos componentes no início da execução
    private void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Atualiza física de movimento, animações e verifica queda fora do cenário
    private void Update()
    {
        Movement();
        HandleAnimations();

        // Reposiciona o jogador caso caia abaixo do limite do mapa (respawn)
        if (transform.position.y <= -14f)
        {
            transform.position = new Vector3(-3.5f, -1.5f, 0f);
            verticalVelocity = 0f;
            isJumping = false;
        }
    }

    // Captura o input de direção e limita a magnitude para evitar velocidade extra na diagonal
    private void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();
        move = Vector3.ClampMagnitude(new Vector3(input.x, 0f, input.y), 1f);
    }

    // Captura o input de pulo e valida se o personagem pode saltar
    private void OnJump()
    {
        // Permite pular se estiver no chão ou dentro da margem de tolerância (0.1s)
        if ((controller.isGrounded || groundedGraceTimer > 0f) && !isJumping)
        {
            jumpRequested = true;
        }
    }

    // Gerencia timers, aplicação de pulo, gravidade, deslocamento e rotação
    private void Movement()
    {
        // Atualiza tolerância de chão (evita falhas em desníveis pequenos)
        if (controller.isGrounded)
        {
            groundedGraceTimer = 0.1f;
        }
        else
        {
            groundedGraceTimer -= Time.deltaTime;
        }

        // Reduz o tempo de trava inicial do pulo
        if (jumpLockTimer > 0f)
        {
            jumpLockTimer -= Time.deltaTime;
        }

        // Mantém o personagem firme no chão quando não está pulando
        if (controller.isGrounded && verticalVelocity < 0f && jumpLockTimer <= 0f)
        {
            verticalVelocity = -3f;
        }

        // Inicia o pulo
        if (jumpRequested)
        {
            verticalVelocity = jumpForce;
            isJumping = true;
            jumpLockTimer = 0.2f; // Ignora checagem de chão nos primeiros 0.2s do salto
            groundedGraceTimer = 0f;

            anim.SetBool("IsRun", false);
            anim.CrossFadeInFixedTime("Jump", 0.1f);
            jumpRequested = false;
        }

        // Aplica a aceleração da gravidade continuamente
        verticalVelocity += gravity * Time.deltaTime;

        // Combina velocidade horizontal e vertical para mover o CharacterController
        Vector3 velocity = move * speed;
        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);

        // Rotaciona suavemente o personagem na direção em que está se movendo
        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // Controla a transição de aterrissagem e o estado de corrida no Animator
    private void HandleAnimations()
    {
        // Só verifica aterrissagem depois que o tempo mínimo do pulo passou
        if (controller.isGrounded && jumpLockTimer <= 0f)
        {
            bool isMoving = move.sqrMagnitude > 0.01f;

            // Ao tocar o chão após um pulo, faz transição suave para corrida ou parado
            if (isJumping)
            {
                isJumping = false;
                anim.CrossFadeInFixedTime(isMoving ? "Fast Run" : "Idle", 0.1f);
            }

            anim.SetBool("IsRun", isMoving);
        }
    }
}