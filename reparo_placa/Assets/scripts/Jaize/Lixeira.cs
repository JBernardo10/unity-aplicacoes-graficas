using UnityEngine;
using UnityEngine.EventSystems;

public class Lixeira : MonoBehaviour, IDropHandler
{
    public GameObject audioCapacitorQLixeira;

    [Header("Controle de descarte")]
    public int totalDeCapacitoresQueimados = 2; // defina quantos precisam ser descartados
    private int capacitoresDescartados = 0;   

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            var capacitor = eventData.pointerDrag.GetComponentInChildren<DraggableCapacitorQueimadoUI>();

            if (capacitor != null)
            {
                GameObject preFab = Instantiate(audioCapacitorQLixeira, transform.position, Quaternion.identity);
                Destroy(preFab.gameObject, 2f);

                capacitor.Descartar();
                capacitoresDescartados++;

                // Aqui você pode colocar lógica adicional caso queira
                // verificar se todos os capacitores foram descartados
                if (capacitoresDescartados >= totalDeCapacitoresQueimados)
                {
                    Debug.Log("Todos os capacitores queimados foram descartados.");
                }
            }
        }
    }
}