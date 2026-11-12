using UnityEngine;
using UnityEngine.UI;

public class ShineController : MonoBehaviour
{
    public Image backgroundShine;

    public void Shine100()
    {
        MudarAlpha(0f);
    }

    public void Shine50()
    {
        MudarAlpha(0.5f);
    }

    public void Shine25()
    {
        MudarAlpha(0.75f);
    }

    private void MudarAlpha(float valorAlpha)
    {
        Color corAtual = backgroundShine.color;

        corAtual.a = valorAlpha;

        backgroundShine.color = corAtual;
    }
}
