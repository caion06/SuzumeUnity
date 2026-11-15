using UnityEngine;

public class Cam : MonoBehaviour
{
    //Camera
    public Transform player; // Arraste seu jogador aqui no Inspector
    public Vector3 offset = new Vector3(0, 5, -10); // Dist?ncia da c?mera para o jogador
    public float smoothSpeed = 0.125f; // Velocidade de suaviza??o
    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(player); // Faz a c?mera olhar sempre para o jogador
    }
}
