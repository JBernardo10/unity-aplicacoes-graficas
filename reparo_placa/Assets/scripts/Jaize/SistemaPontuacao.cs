using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SistemaPontuacao : MonoBehaviour
{
    public int pontuacao = 0;
    

    public TextMeshProUGUI textoPontuacao;

    public TMP_Text textoTempo, textoFerra, textoObj;

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

    public void AtualizaTempo(string tempo)  
    {
       if (textoTempo != null)
        textoTempo.text = tempo;

    }

    public void AtualizaObj(string obj)
    {
       if (textoObj != null)
        textoObj.text = obj;

    }

    void PerdeFase()
    {
        if(pontuacao == 0)
        {
            SceneManager.LoadScene("TelaDerrota");
            Screen.orientation = ScreenOrientation.Portrait;
        }
    }

    public void AtualizarFerramentas(string ferr)
    {   
        if(textoFerra != null)
        {
            textoFerra.text = ferr;          
        }
    }
}