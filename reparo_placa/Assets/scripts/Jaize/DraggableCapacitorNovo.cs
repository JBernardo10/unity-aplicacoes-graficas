using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class DraggableCapacitorNovo : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text mensagemUI; // arraste o Text do Canvas
    [SerializeField] private GameObject PainelCampoTexto;

    public float tempoEstanho = 2f;
    public float tempoFerro = 2f;

    private enum Estado { SlotVazio, CapacitorInserido, EstanhoAplicado, Soldado }
    private Estado estado = Estado.SlotVazio;

    private bool dentro = false;
    private string ferramentaAtual = "";
    private Coroutine processo = null;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Capacitor"))
        {
            // Jogador colocou o capacitor novo no slot
            estado = Estado.CapacitorInserido;
            mensagemUI.text = "Capacitor adicionado corretamente! Agora utilize o estanho.";
            if (PainelCampoTexto != null)
                PainelCampoTexto.SetActive(true);

            // Coloca o capacitor como filho do slot
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.transform.localPosition = Vector3.zero;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        ferramentaAtual = eventData.pointerDrag.tag;
        dentro = true;

        if (PainelCampoTexto != null)
            PainelCampoTexto.SetActive(true);

        // Passo 1: aplicar estanho
        if (estado == Estado.CapacitorInserido && ferramentaAtual == "Estanho")
        {
            processo = StartCoroutine(ProcessarFerramenta(
                "Aplicando estanho...", tempoEstanho,
                Estado.EstanhoAplicado,
                "Agora utilize o ferro de solda."
            ));
        }
        // Passo 2: aplicar ferro de solda
        else if (estado == Estado.EstanhoAplicado && ferramentaAtual == "FerroSolda")
        {
            processo = StartCoroutine(ProcessarFerramenta(
                "Soldando capacitor...", tempoFerro,
                Estado.Soldado,
                "Capacitor inserido com sucesso na placa mãe!"
            ));
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        dentro = false;
        if (processo != null)
        {
            StopCoroutine(processo);
            processo = null;
        }

        if (PainelCampoTexto != null)
            PainelCampoTexto.SetActive(false);
    }

    private IEnumerator ProcessarFerramenta(string msgDurante, float tempo, Estado proximo, string msgDepois)
    {
        float elapsed = 0f;
        while (elapsed < tempo && dentro)
        {
            mensagemUI.text = msgDurante + $" ({elapsed:F1}/{tempo:F1}s)";
            if (PainelCampoTexto != null)
                PainelCampoTexto.SetActive(true);

            elapsed += Time.deltaTime;
            yield return null;
        }

        if (dentro)
        {
            estado = proximo;
            mensagemUI.text = msgDepois;
            if (PainelCampoTexto != null)
                PainelCampoTexto.SetActive(true);
        }
    }
}