using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InformacoesDerrota : MonoBehaviour
{
    [Header("UI de Tempo")]
    public TMP_Text textoTempoFinal;

    [Header("Som de derrota (opcional)")]
    public AudioSource audioComemoracao;

    

    private void Start()
    {
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
        SceneManager.LoadScene("0.backupMinhaCena_RECUPERADA");
    }

    public void Repetir()
    {
        SceneManager.LoadScene("TutorialReparoPlaca");
    }
}
