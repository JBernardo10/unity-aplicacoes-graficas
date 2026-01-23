using UnityEngine;
using UnityEngine.EventSystems;

public class ArrastarTransformador : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    private Vector3 escalaOriginal;
    private bool encaixado = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        // Guarda a escala original para restaurar se não encaixar
        escalaOriginal = rectTransform.localScale;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Traz o objeto para frente na UI
        transform.SetAsLastSibling();

        // Sempre começa como "não encaixado"
        encaixado = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move corretamente dentro do Canvas
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Se não encaixou em nenhum slot, volta ao tamanho original
        if (!encaixado)
        {
            rectTransform.localScale = escalaOriginal;
        }
    }

    // 🔥 CHAMADO PELO SlotTransformador
   public void EncaixarNoSlot(Transform slot, Vector3 novaEscala)
{
    encaixado = true;

    RectTransform slotRect = slot.GetComponent<RectTransform>();

    rectTransform.SetParent(slotRect, false);

    rectTransform.localScale = Vector3.one; // zera herança
    rectTransform.anchoredPosition = Vector2.zero;

    rectTransform.sizeDelta = new Vector2(120, 120); // 👈 CONTROLE REAL DO TAMANHO
}
}
