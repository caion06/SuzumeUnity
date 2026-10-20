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
        controller.Move(move * Time.deltaTime * speed);
    }

    // Recebe o comando de pulo
    private void OnJump()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = jumpForce;
            anim.SetBool("IsJump", true);
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

        // Aplica a gravidade
        verticalVelocity += gravity * Time.deltaTime;

        // Movimento horizontal
        controller.Move(new Vector3(0, gravity, 0) * Time.deltaTime);

        // Rotaciona o personagem na direcao do movimento
        if (move != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), Time.deltaTime * rotationSpeed);

        // Animacoes
        anim.SetBool("IsRun", move != Vector3.zero);

        if (controller.isGrounded && verticalVelocity <= 0f)
        {
            anim.SetBool("IsJump", false);
        }
    }
}
