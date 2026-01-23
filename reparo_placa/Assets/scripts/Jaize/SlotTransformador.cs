using UnityEngine;
using UnityEngine.EventSystems;

public class SlotTransformador : MonoBehaviour, IDropHandler
{
    // Escala do transformador quando encaixar
    public Vector3 escalaAoEncaixar = new Vector3(3.0f, 3.0f, 3.0f);

    public void OnDrop(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerDrag;

        if (obj != null)
        {
            ArrastarTransformador transformador =
                obj.GetComponent<ArrastarTransformador>();

            if (transformador != null)
            {
                transformador.EncaixarNoSlot(transform, escalaAoEncaixar);
            }
        }
    }
}
