using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DraggableTranformadorQueimado : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public float tempoFerro = 2f;
    public GameObject audioFerroSolda, audioSugadorSolda, audioPinca;
    public float tempoSugador = 2f;

    public SistemaPontuacao sistemaPontuacao;
    

    private enum Estado { PresoNaPlaca, FerroAquecido, Sugado, PresoNaPinca }
    private Estado estado = Estado.PresoNaPlaca;

    private bool dentro = false;
    private string ferramentaAtual = "";
    private string ultimaFerramentaErro = ""; // NOVO: controla erro repetido
    private Coroutine processo = null;
    private Transform pinca;

       // ⭐ CONTROLE DE PONTUAÇÃO
    private bool pontoFerro = false;
    private bool pontoSugador = false;
    private bool pontoPinca = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            ferramentaAtual = eventData.pointerDrag.tag;
            dentro = true;

            // ================= FERRO DE SOLDA =================
            if (estado == Estado.PresoNaPlaca && ferramentaAtual == "FerroSolda")
            {
                ultimaFerramentaErro = ""; // reseta erro

                GameObject preFab = Instantiate(audioFerroSolda, transform.position, Quaternion.identity);
                Destroy(preFab.gameObject, 2f);

                if (!pontoFerro && sistemaPontuacao != null){
                    sistemaPontuacao.AdicionarPontos(20);
                    pontoFerro = true;

                }   

                processo = StartCoroutine(ProcessarFerramenta(tempoFerro, Estado.FerroAquecido));
            }

            // ================= SUGADOR =================
            else if (estado == Estado.FerroAquecido && ferramentaAtual == "Sugador")
            {
                ultimaFerramentaErro = ""; // reseta erro

                GameObject preFab = Instantiate(audioSugadorSolda, transform.position, Quaternion.identity);
                Destroy(preFab.gameObject, 2f);

                if (!pontoSugador && sistemaPontuacao != null)
                    sistemaPontuacao.AdicionarPontos(20);
                    pontoSugador= true;

                processo = StartCoroutine(ProcessarFerramenta(tempoSugador, Estado.Sugado));
            }

            // ================= PINÇA =================
            else if (estado == Estado.Sugado && ferramentaAtual == "Pinca")
            {
                ultimaFerramentaErro = ""; // reseta erro

                GameObject preFab = Instantiate(audioPinca, transform.position, Quaternion.identity);
                Destroy(preFab.gameObject, 2f);

                if (!pontoPinca && sistemaPontuacao != null)
                    sistemaPontuacao.AdicionarPontos(20);
                    pontoPinca= true;

                estado = Estado.PresoNaPinca;
                pinca = eventData.pointerDrag.transform;
                transform.SetParent(pinca);
                transform.localPosition = Vector3.zero;
            }

            // ================= FERRAMENTA ERRADA =================
            else
            {
                // só perde ponto se trocar a ferramenta
                if (ferramentaAtual != ultimaFerramentaErro)
                {
                    if (sistemaPontuacao != null)
                        sistemaPontuacao.AdicionarPontos(-10);
                        pontoFerro = false;
                        pontoSugador = false;
                        pontoPinca = false;

                    ultimaFerramentaErro = ferramentaAtual;
                }

                processo = StartCoroutine(ProcessarFerramenta(1f, Estado.PresoNaPlaca));
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dentro = false;

        if (processo != null)
        {
            StopCoroutine(processo);
            processo = null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        // opcional
    }

    private IEnumerator ProcessarFerramenta(float tempo, Estado proximo)
    {
        float elapsed = 0f;

        while (elapsed < tempo && dentro)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (dentro)
        {
            estado = proximo;
        }
    }

    public void Descartar()
    {
        if (estado == Estado.PresoNaPinca)
        {
            Destroy(gameObject);
        }
    }
}