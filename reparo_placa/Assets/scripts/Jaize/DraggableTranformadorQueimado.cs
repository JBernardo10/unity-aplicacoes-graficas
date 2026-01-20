using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DraggableTranformadorQueimado: MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text mensagemUI; // arraste o Text do Canvas
    public float tempoFerro = 2f;
    public GameObject audioFerroSolda, audioSugadorSolda, audioPinca;
    public float tempoSugador = 2f;
    [SerializeField] private GameObject PainelCampoTexto;
    
        

    private enum Estado { PresoNaPlaca, FerroAquecido, Sugado, ProntoParaPinca, PresoNaPinca }
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

            if (PainelCampoTexto != null)
                PainelCampoTexto.SetActive(true); // mostra o painel

            if (estado == Estado.PresoNaPlaca && ferramentaAtual == "FerroSolda")
            {
                GameObject preFab = Instantiate(audioFerroSolda, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
                Destroy(preFab.gameObject, 2f);

                processo = StartCoroutine(ProcessarFerramenta("Aquecendo solda...", tempoFerro, Estado.FerroAquecido, "Solda aquecida! Use o sugador."));

            }
            else if (estado == Estado.FerroAquecido && ferramentaAtual == "Sugador")
            {
                GameObject preFab = Instantiate(audioSugadorSolda, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
                Destroy(preFab.gameObject, 2f);

                processo = StartCoroutine(ProcessarFerramenta("Removendo solda...", tempoSugador, Estado.Sugado, "Solda removida! Use a pinça."));
            }
            else if (estado == Estado.Sugado && ferramentaAtual == "Pinca")
            {
                GameObject preFab = Instantiate(audioPinca, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), Quaternion.identity);
                Destroy(preFab.gameObject, 2f);

                mensagemUI.text = "Transformador preso na pinça! Leve até a lixeira.";
                if (PainelCampoTexto != null)
                    PainelCampoTexto.SetActive(true);
                estado = Estado.PresoNaPinca;
                pinca = eventData.pointerDrag.transform;
                transform.SetParent(pinca);
                transform.localPosition = Vector3.zero;

            }
            else
            {
                processo = StartCoroutine(ProcessarFerramenta("Ferramenta errada!", 1f, Estado.PresoNaPlaca, "Utilize o ferro de solda"));

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

        if (PainelCampoTexto != null && estado != Estado.PresoNaPinca)
            PainelCampoTexto.SetActive(false); // esconde o painel ao sair
    }

    public void OnDrop(PointerEventData eventData)
    {
        // usado se quiser que só conte quando "soltar" a ferramenta no capacitor
    }

    private IEnumerator ProcessarFerramenta(string msgDurante, float tempo, Estado proximo, string msgDepois)
    {
        float elapsed = 0f;
        while (elapsed < tempo && dentro)
        {
            if (tempo == 1f)
            {
                mensagemUI.text = msgDurante;
            }
            else
            {
                mensagemUI.text = msgDurante + $" ({elapsed:F1}/{tempo:F1}s)";
            }

            if (PainelCampoTexto != null)
                PainelCampoTexto.SetActive(true);
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (dentro)
        {
            estado = proximo;
            mensagemUI.text = msgDepois;
            if (PainelCampoTexto != null)
                PainelCampoTexto.SetActive(true);
        }
    }

    public void Descartar()
    {
        if (estado == Estado.PresoNaPinca)
        {
            mensagemUI.text = "Transformador removido e descartado!";
            if (PainelCampoTexto != null)
                PainelCampoTexto.SetActive(true);
            Destroy(gameObject);
        }
    }
    
}