using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class Collectibles : MonoBehaviour
{
    [Header("Coletaveis")]
    [SerializeField] private int keys;
    [SerializeField] private int memories;
    [SerializeField] private int maxKeys;
    [SerializeField] private int maxMemories;
    [SerializeField] private string tagKeys;
    [SerializeField] private string tagMemories;
    [SerializeField] private string tagPorta;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private GameObject videoPlayer;

    [SerializeField] private VisualEffect smoke;

    // Metodos de colição
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o jogador colidio com a chave e se sim adiciona mas
        // um a quantidade de chaves e verifica se já chegou ao maximo
        if (other.CompareTag(tagKeys))
        {
            keys++;
            textMesh.text = $"Chaves: {keys}/5";

            if (maxKeys == keys)
            {
                smoke.SendEvent("OnPlay");
                textMesh.text = "Vá para a porta!";
            }
        }
        // A mesma coisa da chave só que com as memorias
        else if (other.CompareTag(tagMemories))
        {
            memories++;
            textMesh.text = $"Memorias: {memories}/5";

            if (maxMemories == memories)
            {
                textMesh.text = "Vá para a porta!";
            }
        }

        // Destroi o obijeto que ele coletar
        Destroy(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(tagPorta))
        {
            if (maxKeys == keys)
            {
                SceneManager.LoadScene(2);
            }

            if (maxMemories == memories)
            {
                Instantiate(videoPlayer);
            }
        }
    }
}
