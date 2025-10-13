using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class Lixeira : MonoBehaviour, IDropHandler
{
    public GameObject painelMensagem; // arraste o painel do Canvas
    public TMP_Text mensagemUI;       // arraste o Text que está dentro do painel
    public GameObject audioCapacitorQLixeira;

    [Header("Controle de descarte")]
    public int totalDeCapacitoresQueimados = 2; // defina quantos precisam ser descartados
    private int capacitoresDescartados = 0;

    public float tempoMensagem = 6f; // tempo que cada mensagem ficará visível

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

                if (capacitoresDescartados >= totalDeCapacitoresQueimados)
                {
                    // Mostra primeiro "Objeto descartado!", depois "Adicione os capacitores bons."
                    StopAllCoroutines();
                    StartCoroutine(MostrarMensagensSequenciais());
                }
                else
                {
                    MostrarMensagem("Objeto descartado!");
                }
            }
        }
    }

    private void MostrarMensagem(string mensagem)
    {
        StopAllCoroutines();
        StartCoroutine(MostrarMensagemTempo(mensagem, tempoMensagem));
    }

    private IEnumerator MostrarMensagemTempo(string mensagem, float tempo)
    {
        painelMensagem.SetActive(true);
        mensagemUI.text = mensagem;

        yield return new WaitForSeconds(tempo);

        painelMensagem.SetActive(false);
    }

    private IEnumerator MostrarMensagensSequenciais()
    {
        // Mensagem 1
        painelMensagem.SetActive(true);
        mensagemUI.text = "Objeto descartado!";
        yield return new WaitForSeconds(tempoMensagem);

        // Mensagem 2
        mensagemUI.text = "Adicione os capacitores bons.";
        yield return new WaitForSeconds(tempoMensagem);

        painelMensagem.SetActive(false);
    }
}