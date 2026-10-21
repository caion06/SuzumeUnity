using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalk : MonoBehaviour
{
    // Unity Engine
    private Animator anim;
    private CharacterController controller;

    // Movimento
    private Vector3 move;
    private float verticalVelocity;
    private bool jumpRequested;

    // Configuracoes
    public float speed = 5f;
    public float jumpForce = 5f;
    public float gravity = -20f;
    public float rotationSpeed = 5f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Recebe o movimento do Input System
    private void OnMove(InputValue inputValue)
    {
        Vector2 input = inputValue.Get<Vector2>();

        move = new Vector3(input.x, 0f, input.y);
        move = Vector3.ClampMagnitude(move, 1f);
    }

    // Recebe o comando de pulo
    private void OnJump()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = jumpForce;
            anim.CrossFade("Jump", 0.1f);
        }
    }

private void Update()
    {
        bool grounded = controller.isGrounded;

        // Mantem o personagem junto ao chao
        if (grounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        // Combina movimento horizontal e vertical
        Vector3 velocity = move * speed;
        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);

        // Rotaciona na direcao do movimento
        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);

            transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,rotationSpeed * Time.deltaTime);
        }

        // Animacoes
        anim.SetBool("IsRun", move.sqrMagnitude > 0.01f);
    }
}
