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
            // enquanto o botão esquerdo do mouse estiver pressionado, tocar o som
            if (Input.GetMouseButton(0))
            {
                if (!somSolda.isPlaying)
                    somSolda.Play();
            }
            else
            {
                if (somSolda.isPlaying)
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
