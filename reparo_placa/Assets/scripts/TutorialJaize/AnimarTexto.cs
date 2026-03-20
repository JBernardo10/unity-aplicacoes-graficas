using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class AnimarTexto : MonoBehaviour
{
    public float delayInicial = 0.5f;
    public float duracaoFade = 1f;

    [Header("Elementos")]
    public TextMeshProUGUI texto;
    public Image[] imagens; // quadradinhos

    private Color corTextoInicial;
    private Color[] coresImagens;

    void Start()
    {
        // Salva cor inicial do texto
        corTextoInicial = texto.color;

        // Começa invisível
        texto.color = new Color(corTextoInicial.r, corTextoInicial.g, corTextoInicial.b, 0);

        // Salva e zera as imagens
        coresImagens = new Color[imagens.Length];

        for (int i = 0; i < imagens.Length; i++)
        {
            coresImagens[i] = imagens[i].color;
            imagens[i].color = new Color(coresImagens[i].r, coresImagens[i].g, coresImagens[i].b, 0);
        }

        StartCoroutine(Animacao());
    }

    IEnumerator Animacao()
    {
        yield return new WaitForSeconds(delayInicial);

        float tempo = 0;

        // FADE IN (texto + imagens juntos)
        while (tempo < duracaoFade)
        {
            tempo += Time.deltaTime;
            float t = tempo / duracaoFade;

            // Texto
            texto.color = new Color(corTextoInicial.r, corTextoInicial.g, corTextoInicial.b, t);

            // Imagens
            for (int i = 0; i < imagens.Length; i++)
            {
                imagens[i].color = new Color(coresImagens[i].r, coresImagens[i].g, coresImagens[i].b, t);
            }

            yield return null;
        }

        // Garante visível
        texto.color = new Color(corTextoInicial.r, corTextoInicial.g, corTextoInicial.b, 1);

        for (int i = 0; i < imagens.Length; i++)
        {
            imagens[i].color = new Color(coresImagens[i].r, coresImagens[i].g, coresImagens[i].b, 1);
        }

        // 🚀 LIBERA O JOGO (lixos começam a se mover)
        Teste.jogoIniciado = true;

        // ❗ NÃO DESATIVA O OBJETO (texto permanece na tela)
    }
}