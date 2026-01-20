using UnityEngine;
using UnityEngine.EventSystems;

public class ArrastarTransformador : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector3 escalaOriginal;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        escalaOriginal = rectTransform.localScale;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // opcional: trazer para frente
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // se não encaixar, volta ao tamanho original
        rectTransform.localScale = escalaOriginal;
    }

    // 🔥 CHAMADO PELO SLOT
    public void EncaixarNoSlot(Transform slot, Vector3 novaEscala)
    {
        rectTransform.position = slot.position;
        rectTransform.localScale = novaEscala;
    }
}

