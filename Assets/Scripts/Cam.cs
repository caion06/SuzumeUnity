using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform player; // Transform do jogador que será seguido
    public Vector3 offset = new Vector3(0, 5, -10); // Distância padrão entre a câmera e o jogador
    public float smoothSpeed = 0.125f; // Fator de suavização (quanto menor, mais suave o atraso)
    
    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset; // Calcula a posição alvo ideal
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Interpola suavemente da posição atual até o alvo
        transform.position = smoothedPosition; // Atualiza a posição da câmera

        transform.LookAt(player); // Mantém a câmera sempre rotacionada na direção do jogador
    }
}