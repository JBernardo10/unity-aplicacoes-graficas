using UnityEngine;
using UnityEngine.EventSystems;

public class LixeiraOrganicos : MonoBehaviour, IDropHandler
{
    //public int pontos = 0;
    public int vidas = 5;
    public int vidasMaximas = 5;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject lixo = eventData.pointerDrag;

        if (lixo == null) return;

        if (lixo.CompareTag("Organico"))
        {
            // Acertou a lixeira correta
            //pontos++;
            
            if (vidas == 2)
            {
                vidas += 3;
                if (vidas > vidasMaximas)
                    vidas = vidasMaximas;
            }

            Destroy(lixo);
            Debug.Log("Orgânico correto! Vidas: " + vidas);
        }
        else
        {
            // Errou a lixeira
            //pontos--;
            vidas --;
            Debug.Log("Lixeira errada!  Vidas: " + vidas);
        }
    }
}

