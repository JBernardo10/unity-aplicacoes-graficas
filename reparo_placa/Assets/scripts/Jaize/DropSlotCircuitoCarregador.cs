using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DropSlotCircuitoCarregador : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject ImageTranformador; // arraste o prefab do capacitor aqui
    public Transform painelFerramentas;    // onde está o original (opcional, se quiser manter referência visual)
    public GameObject audioTranformadorCorretoEncaixado;
    public GameObject audioFerroSolda;
    public GameObject audioEstanho;

    [Header("Feedback")]
    public Image feedbackImage;  // arraste aqui uma UI Image
    public Sprite certoSprite;   // sprite do ✔
    public Sprite erradoSprite;  // sprite do ✖
    public TMP_Text Textomensagem; // arraste o Text do Canvas
    [SerializeField] private GameObject ImagemCampoTexto;

    public TMP_Text Textomensagem2; // arraste o Text do Canvas
    [SerializeField] private GameObject ImagemCampoTexto2;

    public float tempoEstanho = 2f;
    public float tempoFerro = 2f;

    private enum Estado { SlotVazio, TranformadorInserido, EstanhoAplicado, Soldado }
    private Estado estado = Estado.SlotVazio;

    private bool dentro = false;
    private string ferramentaAtual = "";
    private Coroutine processo = null;
    private bool preenchido = false; // controla se já foi usado corretamente
    private bool objetivoRegistrado = false; // evita contar o mesmo capacitor duas vezes

    public void OnDrop(PointerEventData eventData)
    {
        if (preenchido) return;
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        // Verifica se o objeto arrastado é um tranformador
        if (dropped.CompareTag("Tranformador"))
        {
            // Instancia uma nova cópia do tranformador no slot
            GameObject novoTransformador = Instantiate(ImageTranformador, transform);
            novoTransformador.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            // Som do tranformador bom quando é adicionado no slot
            GameObject preFab = Instantiate(audioTranformadorCorretoEncaixado, transform.position, Quaternion.identity);
            Destroy(preFab.gameObject, 1f);

            Debug.Log("✅ Novo tranformador instanciado no slot!");
            Textomensagem2.text = "Tranformador adicionado corretamente! Agora utilize o estanho.";
            ImagemCampoTexto2.SetActive(true);

            CancelInvoke(nameof(EsconderCampo));
            Invoke(nameof(EsconderCampo), 2.5f);
            MostrarFeedback(true);

            preenchido = true;
            estado = Estado.TranformadorInserido;
        }
        else
        {
            Debug.Log("❌ Objeto errado, não é um tranformador.");
            MostrarFeedback(false);
        }
    }

    void MostrarFeedback(bool correto)
    {
        if (feedbackImage != null)
        {
            feedbackImage.sprite = correto ? certoSprite : erradoSprite;
            feedbackImage.gameObject.SetActive(true);

            CancelInvoke(nameof(EsconderFeedback));
            Invoke(nameof(EsconderFeedback), 1.5f);
        }
    }

    void EsconderFeedback()
    {
        if (feedbackImage != null)
            feedbackImage.gameObject.SetActive(false);
    }

    void EsconderCampo()
    {
        if (ImagemCampoTexto2 != null)
            ImagemCampoTexto2.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        ferramentaAtual = eventData.pointerDrag.tag;
        dentro = true;

        if (estado == Estado.SlotVazio) return;
        if (ImagemCampoTexto != null)
            ImagemCampoTexto.SetActive(true);

        // Passo 1: aplicar estanho
        if (estado == Estado.TranformadorInserido && ferramentaAtual == "Estanho")
        {
            GameObject preFab = Instantiate(audioEstanho, transform.position, Quaternion.identity);
            Destroy(preFab.gameObject, 2f);

            processo = StartCoroutine(ProcessarFerramenta(
                "Aplicando estanho...", tempoEstanho,
                Estado.EstanhoAplicado,
                "Agora utilize o ferro de solda."
            ));
        }
        // Passo 2: aplicar ferro de solda
        else if (estado == Estado.EstanhoAplicado && ferramentaAtual == "FerroSolda")
        {
            GameObject preFab = Instantiate(audioFerroSolda, transform.position, Quaternion.identity);
            Destroy(preFab.gameObject, 2f);

            processo = StartCoroutine(ProcessarFerramenta(
                "Soldando capacitor...", tempoFerro,
                Estado.Soldado,
                "Capacitor soldado com sucesso na placa mãe!"
            ));
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

        if (ImagemCampoTexto != null)
            ImagemCampoTexto.SetActive(false);
    }

    private IEnumerator ProcessarFerramenta(string msgDurante, float tempo, Estado proximo, string msgDepois)
    {
        float elapsed = 0f;
        while (elapsed < tempo && dentro)
        {
            Textomensagem.text = msgDurante + $" ({elapsed:F1}/{tempo:F1}s)";
            if (ImagemCampoTexto != null)
                ImagemCampoTexto.SetActive(true);

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (dentro)
        {
            estado = proximo;
            Textomensagem.text = msgDepois;
            if (ImagemCampoTexto != null)
                ImagemCampoTexto.SetActive(true);

            // Quando o capacitor for soldado pela primeira vez → registrar objetivo concluído
            if (estado == Estado.Soldado && !objetivoRegistrado)
            {
                TelaVitoriaJaize controlador = FindObjectOfType<TelaVitoriaJaize>();
                if (controlador != null)
                {
                    controlador.RegistrarObjetivoConcluido();
                    objetivoRegistrado = true;
                    Debug.Log($"🏆 tranformador {name} concluído e registrado!");
                }
                else
                {
                    Debug.LogWarning("Nenhum objeto com o script TelaVitoriaJaize foi encontrado na cena!");
                }
            }
        }
    }
}
