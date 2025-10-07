using UnityEngine;

public class SomFerroSolda : MonoBehaviour
{
    public AudioSource somSolda;        // arraste aqui o áudio de solda
    public string ferramentaAtiva;      // nome da ferramenta selecionada (ex: "ferroSolda")
    public string nomeFerramenta = "ImageFerroSolda"; // nome desta ferramenta

    void Update()
    {
        // verifica se esta ferramenta está ativa
        if (ferramentaAtiva == nomeFerramenta)
        {
            // se o botão esquerdo do mouse for pressionado, tocar o som
            if (Input.GetMouseButtonDown(0))
            {
                somSolda.Play();
            }

            // se o botão esquerdo do mouse for solto, parar o som
            if (Input.GetMouseButtonUp(0))
            {
                somSolda.Stop();
            }
        }
        else
        {
            // caso o jogador troque de ferramenta, para o som
            if (somSolda.isPlaying)
                somSolda.Stop();
        }
    }
}
