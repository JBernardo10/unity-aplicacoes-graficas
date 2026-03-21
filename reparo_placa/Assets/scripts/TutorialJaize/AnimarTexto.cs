using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class AnimarTexto : MonoBehaviour
{
    public float delayInicial = 0.5f;
    public float duracaoFade = 1f;

    [Header("Elementos")]
    public TextMeshProUGUI texto;
    public Image[] imagens; // quadradinhos
    public Image imageBarraMensagem;
    public TextMeshProUGUI textoBarra; // TEXTO DENTRO DA BARRA

    private Color corTextoInicial;
    private Color[] coresImagens;
    private Color corImagemBarra;
    private Color corTextoBarra;

    void Start()
    {
        // TEXTO PRINCIPAL
        corTextoInicial = texto.color;
        texto.color = new Color(corTextoInicial.r, corTextoInicial.g, corTextoInicial.b, 0);

        // QUADRADINHOS
        coresImagens = new Color[imagens.Length];
        for (int i = 0; i < imagens.Length; i++)
        {
            coresImagens[i] = imagens[i].color;
            imagens[i].color = new Color(coresImagens[i].r, coresImagens[i].g, coresImagens[i].b, 0);
        }

        // BARRA
        if (imageBarraMensagem != null)
        {
            corImagemBarra = imageBarraMensagem.color;
            imageBarraMensagem.color = new Color(corImagemBarra.r, corImagemBarra.g, corImagemBarra.b, 0);
        }

        // TEXTO DA BARRA
        if (textoBarra != null)
        {
            corTextoBarra = textoBarra.color;
            textoBarra.color = new Color(corTextoBarra.r, corTextoBarra.g, corTextoBarra.b, 0);
        }

        StartCoroutine(Animacao());
    }

    IEnumerator Animacao()
    {
        yield return new WaitForSeconds(delayInicial);

        float tempo = 0;

        // FADE IN (TUDO JUNTO)
        while (tempo < duracaoFade)
        {
            tempo += Time.deltaTime;
            float t = tempo / duracaoFade;

            // TEXTO PRINCIPAL
            texto.color = new Color(corTextoInicial.r, corTextoInicial.g, corTextoInicial.b, t);

            // QUADRADINHOS
            for (int i = 0; i < imagens.Length; i++)
            {
                imagens[i].color = new Color(coresImagens[i].r, coresImagens[i].g, coresImagens[i].b, t);
            }

            // BARRA
            if (imageBarraMensagem != null)
            {
                imageBarraMensagem.color = new Color(corImagemBarra.r, corImagemBarra.g, corImagemBarra.b, t);
            }

            // TEXTO DA BARRA
            if (textoBarra != null)
            {
                textoBarra.color = new Color(corTextoBarra.r, corTextoBarra.g, corTextoBarra.b, t);
            }

            yield return null;
        }

        // GARANTE VISÍVEL
        texto.color = new Color(corTextoInicial.r, corTextoInicial.g, corTextoInicial.b, 1);

        for (int i = 0; i < imagens.Length; i++)
        {
            imagens[i].color = new Color(coresImagens[i].r, coresImagens[i].g, coresImagens[i].b, 1);
        }

        if (imageBarraMensagem != null)
        {
            imageBarraMensagem.color = new Color(corImagemBarra.r, corImagemBarra.g, corImagemBarra.b, 1);
        }

        if (textoBarra != null)
        {
            textoBarra.color = new Color(corTextoBarra.r, corTextoBarra.g, corTextoBarra.b, 1);
        }
        //Teste teste = FindObjectOfType<Teste>();
        // 🚀 LIBERA O JOGO
        Teste.jogoIniciado = true;
    }
    public void AbrirFase5()
    {
        SceneManager.LoadScene("DescarteLixo");
    }
    public void VoltarFases()
    {
        SceneManager.LoadScene("MenuFases");
    }
}