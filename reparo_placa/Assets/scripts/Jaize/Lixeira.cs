using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Lixeira : MonoBehaviour, IDropHandler
{
    public TMP_Text mensagemUI; // arraste o Text do Canvas
     public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            var capacitor = eventData.pointerDrag.GetComponentInChildren<DraggableCapacitorQueimadoUI>();

            if (capacitor != null)
            {
                capacitor.Descartar();
                mensagemUI.text = "objeto descartado!";

            }
        }
    }

}
