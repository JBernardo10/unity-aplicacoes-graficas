using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DropSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject prefabImagemCorreta; // arraste o prefab do capacitor aqui
    public Transform painelFerramentas;    // onde está o original (opcional, se quiser manter referência visual)
    public GameObject audioCapacitorCorretoEncaixado;
    public GameObject audioFerroSolda;
    public GameObject audioEstanho;

    [Header("Feedback")]
    public Image feedbackImage;  // arraste aqui uma UI Image
    public Sprite certoSprite;   // sprite do ✔
    public Sprite erradoSprite;  // sprite do ✖

    public float tempoEstanho = 2f;
    public float tempoFerro = 2f;

    public TMP_Text Textomensagem;

    [SerializeField] GameObject ImageCampoTexto;


    private enum Estado { SlotVazio, CapacitorInserido, EstanhoAplicado, Soldado }
    private Estado estado = Estado.SlotVazio;

    private bool dentro = false;
    private string ferramentaAtual = "";
    private Coroutine processo = null;
    private bool preenchido = false; // controla se já foi usado corretamente
    private bool objetivoRegistrado = false; // evita contar o mesmo capacitor duas vezes

     public SistemaPontuacao sistemaPontuacao;

     // ⭐ CONTROLE DE PONTUAÇÃO
    private bool pontoFerro = false;
    private bool pontoEstanho = false;
    

    static int totFerramenta = 0;
    private string ultimaFerramentaErro = ""; // NOVO: controla erro repetido
    public int capacitoresDescartados = 0;

    public void OnDrop(PointerEventData eventData)
    {
        if (preenchido) return;
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        // Verifica se o objeto arrastado é um capacitor
        if (dropped.CompareTag("Capacitor"))
        {
            // Instancia uma nova cópia do capacitor no slot
            GameObject novoCapacitor = Instantiate(prefabImagemCorreta, transform);
            novoCapacitor.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            // Som do capacitor bom quando é adicionado no slot
            GameObject preFab = Instantiate(audioCapacitorCorretoEncaixado, transform.position, Quaternion.identity);
            Destroy(preFab.gameObject, 1f);

            Debug.Log("✅ Novo capacitor instanciado no slot!");
            if (!preenchido && sistemaPontuacao != null){
                    sistemaPontuacao.AdicionarPontos(20);
                    //totFerramenta+=1;
                    TelaVitoriaJaize controlador = FindObjectOfType<TelaVitoriaJaize>();

                if (controlador != null)
                {
                    controlador.RegistrarFerramentaConcluido(4);
                    //Debug.Log($"🏆 transformador {name} concluído e registrado!");
                }
                   
                } 
                

            MostrarFeedback(true);

            preenchido = true;
            estado = Estado.CapacitorInserido;
        }
        else
        {
            Debug.Log("❌ Objeto errado, não é um capacitor.");
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        ferramentaAtual = eventData.pointerDrag.tag;
        dentro = true;

        if (estado == Estado.SlotVazio) return;

        // Passo 1: aplicar estanho
        if (estado == Estado.CapacitorInserido && ferramentaAtual == "Estanho")
        {
           ultimaFerramentaErro = ""; // NOVO: controla erro repetido
            GameObject preFab = Instantiate(audioEstanho, transform.position, Quaternion.identity);
            Destroy(preFab.gameObject, 2f);
            if (!pontoEstanho && sistemaPontuacao != null){
                    sistemaPontuacao.AdicionarPontos(20);
                    pontoEstanho = true;
                    //totFerramenta+=1;
                    TelaVitoriaJaize controlador = FindObjectOfType<TelaVitoriaJaize>();

                if (controlador != null)
                {
                    controlador.RegistrarFerramentaConcluido(5);
                    //Debug.Log($"🏆 transformador {name} concluído e registrado!");
                }
                   
                } 

            processo = StartCoroutine(ProcessarFerramenta("Aplicando Estanho...", tempoEstanho, Estado.EstanhoAplicado));
        }
        // Passo 2: aplicar ferro de solda
        else if (estado == Estado.EstanhoAplicado && ferramentaAtual == "FerroSolda")
        {
            ultimaFerramentaErro = ""; // NOVO: controla erro repetido
            GameObject preFab = Instantiate(audioFerroSolda, transform.position, Quaternion.identity);
            Destroy(preFab.gameObject, 2f);
            if (!pontoFerro && sistemaPontuacao != null){
                    sistemaPontuacao.AdicionarPontos(20);
                    pontoFerro = true;
                    //totFerramenta+=1;
                    TelaVitoriaJaize controlador = FindObjectOfType<TelaVitoriaJaize>();

                if (controlador != null)
                {
                    controlador.RegistrarFerramentaConcluido(6);
                    //Debug.Log($"🏆 transformador {name} concluído e registrado!");
                }
                   
                } 

            processo = StartCoroutine(ProcessarFerramenta("Soldando capacitor...", tempoFerro, Estado.Soldado));
        }
        else
        {
            // só perde ponto se trocar a ferramenta
                if (ferramentaAtual != ultimaFerramentaErro)
                {      
                if (sistemaPontuacao != null)
                    sistemaPontuacao.AdicionarPontos(-10);

                pontoFerro = false;
                pontoEstanho = false;
                
                // Se ainda não descartou nenhum capacitor → reset total
                if (capacitoresDescartados == 0)
                {
                    totFerramenta = 0;
                }
                else
                {
                    // Se já descartou, apenas reinicia a sequência atual
                    totFerramenta = capacitoresDescartados * 3; 
                    // cada capacitor exige 3 ferramentas corretas
                }

                TelaVitoriaJaize controlador = FindObjectOfType<TelaVitoriaJaize>();
                if (controlador != null)
                {
                    controlador.RegistrarFerramentaConcluido(4);
                }

                ultimaFerramentaErro = ferramentaAtual;
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
        if (ImageCampoTexto != null)
            ImageCampoTexto.SetActive(false);
    }

    private IEnumerator ProcessarFerramenta(string msgDurante, float tempo, Estado proximo)
    {
        float elapsed = 0f;
        while (elapsed < tempo && dentro)
        {
            Textomensagem.text = msgDurante + $"({elapsed:F1}/{tempo:F1}s)";
            if (ImageCampoTexto != null)
                ImageCampoTexto.SetActive(true);

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (dentro)
        {
            estado = proximo;

            // Quando o capacitor for soldado pela primeira vez → registrar objetivo concluído
            if (estado == Estado.Soldado && !objetivoRegistrado)
            {
                TelaVitoriaJaize controlador = FindObjectOfType<TelaVitoriaJaize>();
                if (controlador != null)
                {
                    controlador.RegistrarObjetivoConcluido();
                    objetivoRegistrado = true;
                    Debug.Log($"🏆 Capacitor {name} concluído e registrado!");
                }
                else
                {
                    Debug.LogWarning("Nenhum objeto com o script TelaVitoriaJaize foi encontrado na cena!");
                }
            }
        }
    }
}