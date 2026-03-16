using UnityEngine;
using UnityEngine.EventSystems;

public class DragDescarteLixo : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Vector3 startPosition;
    private Transform startParent;

    public MoverEsteira moverEsteira;

    public bool estaSendoArrastado;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
{
    if (!estaSendoArrastado && moverEsteira != null)
    {
        rectTransform.anchoredPosition += moverEsteira.MovimentoEsteira();

        // Detecta se chegou no fim da esteira
        if (!moverEsteira.esteiraParada &&
            rectTransform.position.x > moverEsteira.fimEsteira.position.x)
        {
            moverEsteira.PararEsteira();
        }
    }
}

    public void OnBeginDrag(PointerEventData eventData)
    {
        estaSendoArrastado = true;

        startPosition = rectTransform.position;
        startParent = transform.parent;

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        estaSendoArrastado = false;

        canvasGroup.blocksRaycasts = true;

        if (transform.parent == startParent)
        {
            rectTransform.position = startPosition;
        }
    }
     void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("FimEsteira"))
        {
            moverEsteira.PararEsteira();
        }
        Debug.Log("obj detectado" + col.tag);
    }
     public void LixoDescartado()
    {
        moverEsteira.VoltarEsteira();
    }

}