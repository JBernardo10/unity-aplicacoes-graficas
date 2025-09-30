using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class Lixeira : MonoBehaviour, IDropHandler
{
    public float tempoDescarte = 1f;
    public GameObject painelMensagem; // arraste o painel do Canvas
    public TMP_Text mensagemUI;       // arraste o Text que está dentro do painel

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            var capacitor = eventData.pointerDrag.GetComponentInChildren<DraggableCapacitorQueimadoUI>();

            if (capacitor != null)
            {
                capacitor.Descartar();
                StartCoroutine(MostrarMensagem("objeto descartado!"));
            }
        }
    }

    private IEnumerator MostrarMensagem(string mensagem)
    {
        painelMensagem.SetActive(true);  // mostra o painel
        mensagemUI.text = mensagem;

        yield return new WaitForSeconds(tempoDescarte);

        painelMensagem.SetActive(false); // esconde painel + texto
        
    }
}
