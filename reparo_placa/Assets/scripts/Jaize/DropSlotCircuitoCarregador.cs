using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class DropSlotCircuitoCarregador : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject ImageTranformador; 
    public Transform painelFerramentas;    
    public GameObject audioTranformadorCorretoEncaixado;
    public GameObject audioFerroSolda;
    public GameObject audioEstanho;

    public SistemaPontuacao sistemaPontuacao;

    // ⭐ CONTROLE DE PONTUAÇÃO
    private bool pontoFerro = false;
    private bool pontoEstanho = false;

    private string ultimaFerramentaErro = ""; 

    [Header("Feedback")]
    public Image feedbackImage;  
    public Sprite certoSprite;   
    public Sprite erradoSprite;   

    public float tempoEstanho = 2f;
    public float tempoFerro = 2f;

    private enum Estado { SlotVazio, TranformadorInserido, EstanhoAplicado, Soldado }
    private Estado estado = Estado.SlotVazio;

    private bool dentro = false;
    private string ferramentaAtual = "";
    private Coroutine processo = null;
    private bool preenchido = false; 
    private bool objetivoRegistrado = false; 

    public void OnDrop(PointerEventData eventData)
    {
        if (preenchido) return;

        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        if (dropped.CompareTag("Tranformador"))
        {
            GameObject novoTransformador = Instantiate(ImageTranformador, transform);
            novoTransformador.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            RectTransform rect = novoTransformador.GetComponent<RectTransform>();

            rect.localScale = Vector3.one;
            rect.anchoredPosition = Vector2.zero;
            rect.sizeDelta = new Vector2(120, 120);

            GameObject preFab = Instantiate(audioTranformadorCorretoEncaixado, transform.position, Quaternion.identity);
            Destroy(preFab.gameObject, 1f);

            //Debug.Log("✅ Novo tranformador instanciado no slot!");
            MostrarFeedback(true);

            preenchido = true;
            estado = Estado.TranformadorInserido;
        }
        else
        {
            //Debug.Log("❌ Objeto errado, não é um tranformador.");
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

        // ================= ESTANHO CORRETO =================
        if (estado == Estado.TranformadorInserido && ferramentaAtual == "Estanho")
        {
            ultimaFerramentaErro = ""; 

            GameObject preFab = Instantiate(audioEstanho, transform.position, Quaternion.identity);
            Destroy(preFab.gameObject, 2f);

            if (!pontoEstanho && sistemaPontuacao != null)
                sistemaPontuacao.AdicionarPontos(20);
                pontoEstanho = true;
                TelaVitoriaJaize controlador = FindObjectOfType<TelaVitoriaJaize>();
   
                if (controlador != null)
                {
                    controlador.RegistrarFerramentaConcluido(5);
                    //Debug.Log($"🏆 transformador {name} concluído e registrado!");
                }

            if (processo != null)
                StopCoroutine(processo);

            processo = StartCoroutine(ProcessarFerramenta(
                tempoEstanho,
                Estado.EstanhoAplicado
            ));
        }

        // ================= FERRO DE SOLDA CORRETO =================
        else if (estado == Estado.EstanhoAplicado && ferramentaAtual == "FerroSolda")
        {
            ultimaFerramentaErro = ""; 

            GameObject preFab = Instantiate(audioFerroSolda, transform.position, Quaternion.identity);
            Destroy(preFab.gameObject, 2f);

            if (!pontoFerro && sistemaPontuacao != null)
                sistemaPontuacao.AdicionarPontos(20);
                pontoFerro = true;
                TelaVitoriaJaize controlador = FindObjectOfType<TelaVitoriaJaize>();
            
                if (controlador != null)
                {
                    controlador.RegistrarFerramentaConcluido(6);
                    //Debug.Log($"🏆 transformador {name} concluído e registrado!");
                }

            if (processo != null)
                StopCoroutine(processo);

            processo = StartCoroutine(ProcessarFerramenta(
                tempoFerro,
                Estado.Soldado
            ));
        }

        // ================= FERRAMENTA ERRADA =================
        else
        {
            if (ferramentaAtual != ultimaFerramentaErro)
            {
                if (sistemaPontuacao != null)
                    sistemaPontuacao.AdicionarPontos(-10);
                    pontoEstanho = false;
                    pontoEstanho = false;

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

            if (estado == Estado.Soldado && !objetivoRegistrado)
            {
                TelaVitoriaJaize controlador = FindObjectOfType<TelaVitoriaJaize>();

                if (controlador != null)
                {
                    controlador.RegistrarObjetivoConcluido();
                    objetivoRegistrado = true;

                    //Debug.Log($"🏆 transformador {name} concluído e registrado!");
                }
                else
                {
                    //Debug.LogWarning("Nenhum objeto com o script TelaVitoriaJaize foi encontrado na cena!");
                }
            }
        }
    }
}