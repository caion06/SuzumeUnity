using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VFX;

public class Collectibles : MonoBehaviour
{
    // Contadores, tags de identificação e referências de UI/efeitos configuráveis no Inspector
    [Header("Coletaveis")]
    [SerializeField] private int keys;
    [SerializeField] private int memories;
    [SerializeField] private int maxKeys;
    [SerializeField] private int maxMemories;
    [SerializeField] private string tagKeys;
    [SerializeField] private string tagMemories;
    [SerializeField] private string tagPorta;
    [SerializeField] private string tagReiniciar;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private GameObject videoPlayer;

    [SerializeField] private VisualEffect smoke;

    // Disparado automaticamente ao entrar em um colisor marcado como Trigger
    private void OnTriggerEnter(Collider other)
    {
        // Coleta de chave: incrementa a contagem, atualiza a UI e ativa a fumaça ao atingir o máximo
        if (other.CompareTag(tagKeys))
        {
            keys++;
            textMesh.text = $"Chaves: {keys}/10";

            if (maxKeys == keys)
            {
                smoke.SendEvent("OnPlay");
                textMesh.text = "Vá para a porta!";
            }
        }
        // Coleta de memória: incrementa a contagem, atualiza a UI e avisa ao atingir o máximo
        else if (other.CompareTag(tagMemories))
        {
            memories++;
            textMesh.text = $"Memorias: {memories}/5";

            if (maxMemories == memories)
            {
                textMesh.text = "Vá para a porta!";
            }
        }
        // Interação com a porta: avança de cena (chaves completas) ou ativa o vídeo (memórias completas)
        else if (other.gameObject.CompareTag(tagPorta))
        {
            if (maxKeys == keys)
            {
                SceneManager.LoadScene(2);
            }

            if (maxMemories == memories)
            {
                videoPlayer.SetActive(true);
            }
        }
        // Reinicia a fase atual ao colidir com um objeto marcado como "Finish"
        if (other.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        // Remove o objeto colidido da cena
        Destroy(other.gameObject);
    }
}