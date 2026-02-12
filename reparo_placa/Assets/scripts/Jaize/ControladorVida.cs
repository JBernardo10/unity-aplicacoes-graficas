using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorVida : MonoBehaviour
{
    [Header("Corações do Jogador")]
    public Image[] coracoes; // arraste os 5 corações no Inspector

    public Color corCheia = Color.red;
    public Color corVazia = Color.black;
    //public int vidas = 5;        // começa com 5 vidas
    //public int vidasMaximas = 5; // limite máximo
    private float tempoInicio;
    public float tempoTotalFase; 

    private void Start()
    {
        // Marca o tempo inicial da fase
        tempoInicio = Time.time;
    }
    void Update()
    {
        AtualizarCoroes();

        //Verifica se o jogador perdeu todas as vidas
        if(TesteLixeira.vidas == 0)
        {
            PerderFase();
        }
    
    }

    void AtualizarCoroes()
    {
        for (int i = 0; i < coracoes.Length; i++)
        {
            if (i < TesteLixeira.vidas)
                coracoes[i].color = corCheia; // coração cheio
            else
                coracoes[i].color = corVazia; // coração vazio
        }
    }

    void PerderFase()
    {   
        tempoTotalFase = Time.time - tempoInicio;
        PlayerPrefs.SetFloat("UltimoTempoFase", tempoTotalFase);
        // Aqui você escolhe o que acontece quando perde
        Debug.Log("Game Over! O jogador perdeu a fase.");

        // Exemplo 1: carregar cena de Game Over
        SceneManager.LoadScene("TelaDerrota");
        Screen.orientation = ScreenOrientation.Portrait;

        // Exemplo 2: fechar o jogo (se for build standalone)
        // Application.Quit();
    }

}
