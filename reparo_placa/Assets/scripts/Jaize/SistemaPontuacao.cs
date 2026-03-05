using UnityEngine;
using TMPro;
using System.Collections;

public class SistemaPontuacao : MonoBehaviour
{
    public int pontuacao = 0;

    public TextMeshProUGUI textoPontuacao;

    void Start()
    {
        AtualizarPontuacao();

        if (textoPontuacao != null)
            textoPontuacao.gameObject.SetActive(false);
    }

    public void AdicionarPontos(int pontos)
    {
        pontuacao += pontos;

        // IMPEDIR PONTUAÇÃO NEGATIVA
        if (pontuacao < 0)
        {
            pontuacao = 0;
        }

        AtualizarPontuacao();

        if (textoPontuacao != null)
        {
            textoPontuacao.gameObject.SetActive(true);
            StartCoroutine(EsconderPontuacao());
        }
    }

    void AtualizarPontuacao()
    {
        if (textoPontuacao != null)
            textoPontuacao.text = pontuacao.ToString();
    }

    IEnumerator EsconderPontuacao()
    {
        yield return new WaitForSeconds(2f);

        if (textoPontuacao != null)
            textoPontuacao.gameObject.SetActive(false);
    }
}