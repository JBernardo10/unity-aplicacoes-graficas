using UnityEngine;
using UnityEngine.EventSystems;

public class SlotTransformador : MonoBehaviour, IDropHandler
{
    public Vector3 escalaAoEncaixar = new Vector3(20.0f, 20.0f, 20.0f);

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
