using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TesteLixeira : MonoBehaviour, IDropHandler
{
    [Header("Vidas do Jogador")]
    public static int vidas = 5;   // Global para todas as lixeiras
    public int vidasMaximas = 5;

    [Header("Mensagem de Feedback")]
    public GameObject painelMensagem;   // arraste o painel (Image com sprite)
    public TMP_Text mensagemFeedback;   // arraste o TextMeshPro dentro do painel

    public void OnDrop(PointerEventData eventData)
    {
        GameObject lixo = eventData.pointerDrag;
        if (lixo == null) return;

        // Ativa o painel para mostrar a mensagem
        painelMensagem.SetActive(true);

        // Verifica se o lixo tem a mesma tag da lixeira atual
        if (lixo.tag == this.gameObject.tag)
        {
            if (vidas < vidasMaximas)
                vidas++;

            Destroy(lixo);
            mensagemFeedback.text = "Acertou!";
            mensagemFeedback.color = Color.green;
            Debug.Log("Acertou! Vidas: " + vidas);
        }
        else
        {
            vidas--;
            mensagemFeedback.text = "Errou!";
            mensagemFeedback.color = Color.red;
            Debug.Log("Errou! Vidas: " + vidas);
        }

        // Esconde o painel depois de 2 segundos
        CancelInvoke();
        Invoke("EsconderMensagem", 2f);
    }

    void EsconderMensagem()
    {
        painelMensagem.SetActive(false);
    }
}