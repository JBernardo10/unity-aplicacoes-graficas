using UnityEngine;
using UnityEngine.UI;

public class ControladorVida : MonoBehaviour
{
    [Header("Corações do Jogador")]
    public Image[] coracoes; // arraste os 5 corações no Inspector

    public Color corCheia = Color.red;
    public Color corVazia = Color.black;

    void Update()
    {
        AtualizarCoroes();
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

    /*void PerderFase()
    {
        // Aqui você escolhe o que acontece quando perde
        Debug.Log("Game Over! O jogador perdeu a fase.");

        // Exemplo 1: carregar cena de Game Over
        SceneManager.LoadScene("GameOverScene");

        // Exemplo 2: fechar o jogo (se for build standalone)
        // Application.Quit();
    }*/

}
