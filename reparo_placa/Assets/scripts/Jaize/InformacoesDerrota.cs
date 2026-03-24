using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InformacoesDerrota : MonoBehaviour
{
    [Header("UI de Tempo")]
    public TMP_Text textoTempoFinal;
    public TMP_Text textoAcertos;
    public TMP_Text textoErros;
    [SerializeField] GameObject botaoProximaFase;

    [Header("Som de derrota (opcional)")]
    public AudioSource audioComemoracao;

    

    private void Start()
    {
        int numeroFase = PlayerPrefs.GetInt("UltimoFaseConcluida");
        if (numeroFase == 5)
        {
            botaoProximaFase.SetActive(false);
            textoAcertos.text = textoAcertos.text + " " + TesteLixeira.acertos;
            textoErros.text   = textoErros.text + " " + TesteLixeira.erros;
        }
        else
        {
            textoAcertos.gameObject.SetActive(false); 
            textoErros.gameObject.SetActive(false); 
        }
        // Recupera o tempo salvo da fase
        float tempo = PlayerPrefs.GetFloat("UltimoTempoFase", 0f);
        textoTempoFinal.text = $"Tempo: {tempo:F0} segundos";

        // Se quiser tocar som de vitória
        if (audioComemoracao != null)
            audioComemoracao.Play();
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
            case 4: proximaFase = "TutorialDescarteLixo"; break;
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
