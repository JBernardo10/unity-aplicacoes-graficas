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
    private Coroutine processo = null;
    private Transform pinca;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            ferramentaAtual = eventData.pointerDrag.tag;
            dentro = true;

            // FERRO DE SOLDA
            if (estado == Estado.PresoNaPlaca && ferramentaAtual == "FerroSolda")
            {
                GameObject preFab = Instantiate(audioFerroSolda, transform.position, Quaternion.identity);
                Destroy(preFab.gameObject, 2f);

                if (sistemaPontuacao != null)
                    sistemaPontuacao.AdicionarPontos(20);

                processo = StartCoroutine(ProcessarFerramenta(tempoFerro, Estado.FerroAquecido));
            }

            // SUGADOR
            else if (estado == Estado.FerroAquecido && ferramentaAtual == "Sugador")
            {
                GameObject preFab = Instantiate(audioSugadorSolda, transform.position, Quaternion.identity);
                Destroy(preFab.gameObject, 2f);

                if (sistemaPontuacao != null)
                    sistemaPontuacao.AdicionarPontos(20);

                processo = StartCoroutine(ProcessarFerramenta(tempoSugador, Estado.Sugado));
            }

            // PINÇA
            else if (estado == Estado.Sugado && ferramentaAtual == "Pinca")
            {
                GameObject preFab = Instantiate(audioPinca, transform.position, Quaternion.identity);
                Destroy(preFab.gameObject, 2f);

                if (sistemaPontuacao != null)
                    sistemaPontuacao.AdicionarPontos(20);

                estado = Estado.PresoNaPinca;
                pinca = eventData.pointerDrag.transform;
                transform.SetParent(pinca);
                transform.localPosition = Vector3.zero;
            }

            // FERRAMENTA ERRADA
            else
            {
                if (sistemaPontuacao != null)
                    sistemaPontuacao.AdicionarPontos(-10);

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