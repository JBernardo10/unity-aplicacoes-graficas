using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableJaize : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    private Transform startParent;

    // Referência ao PanelFerramentas (arraste o objeto no Inspector)
    public Transform panelFerramentas;  


    public bool segurado = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.position;
        startParent = transform.parent;
        // Durante o drag, muda o parent para o PanelFerramentas
        transform.SetParent(panelFerramentas);
        canvasGroup.blocksRaycasts = false; // Permite detectar o drop
    }

    public void OnDrag(PointerEventData eventData)
    {
        //rectTransform.position = Input.mousePosition;
        rectTransform.position = eventData.position;
        segurado = true;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        segurado = false;

        // Se não encaixou no slot, volta para a posição original
        if (transform.parent == panelFerramentas)
        {
            transform.SetParent(startParent);
            //rectTransform.position = startPosition;
            rectTransform.localPosition = Vector3.zero;
        }
    }
}
