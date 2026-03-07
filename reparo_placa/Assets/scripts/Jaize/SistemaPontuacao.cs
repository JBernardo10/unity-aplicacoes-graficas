using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SistemaPontuacao : MonoBehaviour
{
    public int pontuacao = 0;

    public TextMeshProUGUI textoPontuacao;

    void Start()
    {
        AtualizarPontuacao();
    }

    public void AdicionarPontos(int pontos)
    {
        pontuacao += pontos;

        // Impede que a pontuação fique negativa
        if (pontuacao < 0)
        {
            pontuacao = 0;
        }

        AtualizarPontuacao();
         PerdeFase();

    }

    void AtualizarPontuacao()
    {
        if (textoPontuacao != null)
        {
            textoPontuacao.text = pontuacao.ToString();
        }
    }

    void PerdeFase()
    {
        if(pontuacao == 0)
        {
            SceneManager.LoadScene("TelaDerrota");
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }
}