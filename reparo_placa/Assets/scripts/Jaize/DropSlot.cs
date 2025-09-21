using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public GameObject prefabImagemCorreta; // arraste o prefab do capacitor aqui
    public Transform painelFerramentas;    // onde está o original (opcional, se quiser manter referência visual)

    [Header("Feedback")]
    public Image feedbackImage;  // arraste aqui uma UI Image
    public Sprite certoSprite;   // sprite do ✔
    public Sprite erradoSprite;  // sprite do ✖

    private bool preenchido = false; // controla se já foi usado corretamente
    public void OnDrop(PointerEventData eventData)
    {
         // Se já foi preenchido corretamente, não faz nada
        if (preenchido) return;
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        // Verifica se o objeto arrastado tem o mesmo nome ou tag do prefab
        if (dropped.CompareTag("Capacitor")) // certifique-se de que o prefab e os clones têm essa tag
        {
            // Instancia uma nova cópia do capacitor no slot
            GameObject novoCapacitor = Instantiate(prefabImagemCorreta, transform);
            novoCapacitor.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            Debug.Log("✅ Novo capacitor instanciado no slot!");
            MostrarFeedback(true);
            // Travar o slot para não repetir
            preenchido = true;
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

            // Ocultar depois de 1.5 segundos
            CancelInvoke(nameof(EsconderFeedback));
            Invoke(nameof(EsconderFeedback), 1.5f);
        }
    }

    void EsconderFeedback()
    {
        if (feedbackImage != null)
            feedbackImage.gameObject.SetActive(false);
    }

}