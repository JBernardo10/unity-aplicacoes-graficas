using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorVida : MonoBehaviour
{
    [Header("Corações do Jogador")]
    public Image[] coracoes; // arraste os 5 corações no Inspector

    public Color corCheia = Color.red;
    public Color corVazia = Color.black;

    private float tempoInicio;
    public float tempoTotalFase;  

    private void Start()
    {
        tempoInicio = Time.time;
    }

    void Update()
    {
        AtualizarCoroes();

        // Verifica se o jogador perdeu todas as vidas
        if (TesteLixeira.vidas == 0)
        {
            PerderFase();
        }

        // Verifica se o jogador venceu
        if (TesteLixeira.todosLixosCorretos)
        {
            VencerFase();
        }
    }

    void AtualizarCoroes()
    {
        for (int i = 0; i < coracoes.Length; i++)
        {
            if (i < TesteLixeira.vidas)
                coracoes[i].color = corCheia;
            else
                coracoes[i].color = corVazia;
        }
    }

    void PerderFase()
    {   
        tempoTotalFase = Time.time - tempoInicio;
        PlayerPrefs.SetFloat("UltimoTempoFase", tempoTotalFase);
        Debug.Log("Game Over! O jogador perdeu a fase.");

        SceneManager.LoadScene("TelaDerrota");
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void VencerFase()
    {
        tempoTotalFase = Time.time - tempoInicio;
        PlayerPrefs.SetFloat("UltimoTempoFase", tempoTotalFase);
        Debug.Log("Vitória! O jogador completou a fase.");

        SceneManager.LoadScene("TelaVitoria");
        Screen.orientation = ScreenOrientation.Portrait;
    }
}