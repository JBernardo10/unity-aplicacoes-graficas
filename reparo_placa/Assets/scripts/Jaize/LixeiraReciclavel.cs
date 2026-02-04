using UnityEngine;
using UnityEngine.EventSystems;

public class LixeiraReciclavel : MonoBehaviour, IDropHandler
{
    //public int pontos = 0;
    public int vidas = 5;
    public int vidasMaximas = 5;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject lixo = eventData.pointerDrag;

        if (lixo == null) return;

        // Verifica se o lixo é reciclável
        if (lixo.CompareTag("Reciclavel"))
        {
            // Acertou a lixeira correta
            //pontos++;

            // Regra das vidas
            if (vidas == 2)
            {
                vidas += 3;

                if (vidas > vidasMaximas)
                    vidas = vidasMaximas;
            }

            Destroy(lixo);
            Debug.Log("Reciclavel correto! Vidas:  "  + vidas);
        }
        else
        {
            // Errou a lixeira
            vidas--;
           Debug.Log("Lixeira errada! Vidas: " + vidas);
        }
    }
}
