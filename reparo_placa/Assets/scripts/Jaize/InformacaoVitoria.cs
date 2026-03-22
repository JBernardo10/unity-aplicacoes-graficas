using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class InformacaoVitoria : MonoBehaviour
{
    [Header("UI de Tempo")]
    public TMP_Text textoTempoFinal;

    [Header("Cones de Vitória (arraste aqui os 3 ícones)")]
    public GameObject cone1;
    public GameObject cone2;
    public GameObject cone3;
    [SerializeField] GameObject botaoProximaFase;

    [Header("Som de Comemoração")]
    public AudioSource audioComemoracao; // arraste um AudioSource com o som aqui

    private void Start()
    {
        int numeroFase = PlayerPrefs.GetInt("UltimoFaseConcluida");
        if (numeroFase == 5)
        {
            botaoProximaFase.SetActive(false);
        }


        // Recupera o tempo da fase
        float tempo = PlayerPrefs.GetFloat("UltimoTempoFase", 0f);
        textoTempoFinal.text = $"Tempo: {tempo:F0} segundos";

        // Esconde os cones inicialmente
        cone1.SetActive(false);
        cone2.SetActive(false);
        cone3.SetActive(false);

        // Calcula quantos cones devem aparecer
        int cones = 1;
        if (tempo <= 30f)
            cones = 3;
        else if (tempo <= 50f)
            cones = 2;

        // Toca o som de comemoração
        if (audioComemoracao != null)
            audioComemoracao.Play();

        // Começa a animação de entrada dos cones
        StartCoroutine(MostrarConesGradualmente(cones));
    }

    private IEnumerator MostrarConesGradualmente(int quantidade)
    {
        float delay = 0.6f; // tempo entre um cone e outro

        if (quantidade >= 1)
        {
            yield return StartCoroutine(AparecerComEfeito(cone1));
            yield return new WaitForSeconds(delay);
        }

        if (quantidade >= 2)
        {
            yield return StartCoroutine(AparecerComEfeito(cone2));
            yield return new WaitForSeconds(delay);
        }

        if (quantidade >= 3)
        {
            yield return StartCoroutine(AparecerComEfeito(cone3));
        }
    }

    private IEnumerator AparecerComEfeito(GameObject cone)
    {
        cone.SetActive(true);
        Image img = cone.GetComponent<Image>();
        Color corInicial = img.color;
        corInicial.a = 0;
        img.color = corInicial;

        cone.transform.localScale = Vector3.zero;

        float duracao = 0.5f;
        float t = 0f;

        while (t < duracao)
        {
            t += Time.deltaTime;
            float progresso = Mathf.Clamp01(t / duracao);

            // Suaviza escala e opacidade
            cone.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.SmoothStep(0, 1, progresso));
            img.color = new Color(corInicial.r, corInicial.g, corInicial.b, progresso);

            yield return null;
        }
    }

    public void MenuInicial()
    {
        SceneManager.LoadScene("MenuFases");
    }

    public void ProximaFase()
    {
        string proximaFase = "";
        int numeroFase = PlayerPrefs.GetInt("UltimoFaseConcluida");
         switch (numeroFase)
        {
            case 1: proximaFase = "0.backupMinhaCena_RECUPERADA"; break;
            case 2: proximaFase = "ColocarProcessador"; break;
            case 3: proximaFase = "TutorialCircuitoCarregador"; break;
            case 4: proximaFase = "DescarteLixo"; break;
        }
        SceneManager.LoadScene(proximaFase);
    }

    public void Repetir()
    {
        string repetirFase = "";
        int numeroFase = PlayerPrefs.GetInt("UltimoFaseConcluida");
        Debug.Log(numeroFase);
        switch (numeroFase)
        {
            case 1: repetirFase = "TutorialReparoPlaca"; break;
            case 2: repetirFase = "0.backupMinhaCena_RECUPERADA"; break;
            case 3: repetirFase = "ColocarProcessador"; break;
            case 4: repetirFase = "TutorialCircuitoCarregador"; break;
            case 5: repetirFase = "TutorialDescarteLixo"; break;
        }

        SceneManager.LoadScene(repetirFase);
    }
}
