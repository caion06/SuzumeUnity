using UnityEngine;

public class Collectibles : MonoBehaviour
{
    [Header("Coletaveis")]
    [SerializeField] private int keys;
    [SerializeField] private int memories;
    [SerializeField] private int maxKeys;
    [SerializeField] private int maxMemories;
    [SerializeField] private string tagKeys;
    [SerializeField] private string tagMemories;

    // Metodos de colição
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o jogador colidio com a chave e se sim adiciona mas
        // um a quantidade de chaves e verifica se já chegou ao maximo
        if (other.CompareTag(tagKeys))
        {
            keys++;

            if (maxKeys == keys)
            {
                //funcinalidade a se aplicada
            }
        }
        // A mesma coisa da chave só que com as memorias
        else if (other.CompareTag(tagMemories))
        {
            memories++;
            if (maxMemories == memories)
            {
                //funcinalidade a se aplicada
            }
        }

        // Destroi o obijeto que ele coletar
        Destroy(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        //funcinalidade a se aplicada
    }
}
