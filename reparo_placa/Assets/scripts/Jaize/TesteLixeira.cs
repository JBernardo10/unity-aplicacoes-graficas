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
    
    [Header("Contador de acertos e erros")]
    public static int acertos = 0;
    public static int erros = 0;

    [Header("Controle de vitória")]
    public static int totalLixosNaMesa = 0; 
    public static bool todosLixosCorretos = false;

    [Header("Sons")]
    public AudioClip somAcerto;   // som de objeto caindo na lixeira
    public AudioClip somErro;     // som de erro (opcional)
    private AudioSource audioSource;

    private void Start()
    {
        // Conta todos os objetos que possuem o script Lixo
        Lixo[] lixos = GameObject.FindObjectsOfType<Lixo>();
        totalLixosNaMesa = lixos.Length;

        todosLixosCorretos = false;
        acertos = 0;
        erros = 0;

        // Configura o AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject lixo = eventData.pointerDrag;
        if (lixo == null) return;

        // Ativa o painel para mostrar a mensagem
        //painelMensagem.SetActive(true);

        // Verifica se o lixo tem a mesma tag da lixeira atual
        if (lixo.tag == this.gameObject.tag)
        {
            acertos++;

            if (vidas < vidasMaximas)
                vidas++;

            Destroy(lixo);
            mensagemFeedback.text = "Acertou!";
            mensagemFeedback.color = Color.green;
            Debug.Log("Acertou! Vidas: " + vidas + " | Acertos: " + acertos);

            // 🔊 Toca som de acerto
            if (somAcerto != null)
                audioSource.PlayOneShot(somAcerto);

            // 👉 Atualiza o HUD de objetivos
            SistemaPontuacao sistema = FindObjectOfType<SistemaPontuacao>();
            if (sistema != null)
            {
                sistema.RegistrarDescarteCorreto();
            }

            // Verifica se já acertou todos os lixos
            if (acertos == totalLixosNaMesa)
            {
                todosLixosCorretos = true;
                Debug.Log("Todos os lixos foram colocados corretamente!");
            }
        }
        else
        {
            erros++;
            vidas--;
            mensagemFeedback.text = "Errou!";
            mensagemFeedback.color = Color.red;
            Debug.Log("Errou! Vidas: " + vidas + " | Erros: " + erros);

            // 🔊 Toca som de erro (opcional)
            if (somErro != null)
                audioSource.PlayOneShot(somErro);
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