using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class ConclusaoManager : MonoBehaviour
{
    [Header("UI Elements - Conclusão")]
    public GameObject painelConclusao;
    public TextMeshProUGUI textoParabens;
    public TextMeshProUGUI textoTempo;
    public Image estrela1;
    public Image estrela2;
    public Image estrela3;
    public Button botaoProximaFase;
    public Button botaoRepetir;
    public Button botaoMenu;

    [Header("Configurações")]
    public float tempoParaConcluir = 60f;
    
    [Header("Sons da Conclusão")]
    public AudioClip somConclusao;
    public AudioClip somEstrela;
    public AudioClip somCliqueBotao;
    public AudioClip somVitoria;
    
    private float tempoDecorrido = 0f;
    private bool faseConcluida = false;
    private bool cronometroAtivo = true;
    private AudioSource audioSource;

    void Start()
    {
        // ✅ CONFIGURAÇÃO DE ÁUDIO ADICIONADA
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.volume = 0.7f;
        }
        
        // CONFIGURA SONS NOS BOTÕES
        ConfigurarSonsBotoes();
        
        cronometroAtivo = true;
        tempoDecorrido = 0f;
        faseConcluida = false;
        
        if (painelConclusao != null)
        {
            painelConclusao.SetActive(false);
        }

        if (botaoProximaFase != null) botaoProximaFase.interactable = false;
        if (botaoRepetir != null) botaoRepetir.interactable = false;
        if (botaoMenu != null) botaoMenu.interactable = false;

        Debug.Log("⏱️ Cronômetro iniciado!");
    }

    void Update()
    {
        if (cronometroAtivo && !faseConcluida)
        {
            tempoDecorrido += Time.deltaTime;
        }
    }

    // ✅ MÉTODO NOVO: CONFIGURA SONS NOS BOTÕES
    void ConfigurarSonsBotoes()
    {
        ConfigurarSomBotao(botaoProximaFase);
        ConfigurarSomBotao(botaoRepetir);
        ConfigurarSomBotao(botaoMenu);
    }

    // ✅ MÉTODO NOVO: CONFIGURA SOM EM UM BOTÃO
    void ConfigurarSomBotao(Button botao)
    {
        if (botao != null)
        {
            botao.onClick.AddListener(() => TocarSom(somCliqueBotao));
        }
    }

    // ✅ MÉTODO NOVO: TOCAR SOM
    void TocarSom(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void ConcluirFase()
    {
        if (faseConcluida) return;

        faseConcluida = true;
        cronometroAtivo = false;
        
        Debug.Log("FASE CONCLUÍDA! Tempo: " + tempoDecorrido.ToString("F1") + "s");
        
        // ✅ SOM DE CONCLUSÃO ADICIONADO
        TocarSom(somConclusao);
        
        StartCoroutine(MostrarTelaConclusao());
    }

    IEnumerator MostrarTelaConclusao()
    {
        yield return new WaitForSeconds(1f);

        if (painelConclusao != null)
        {
            painelConclusao.SetActive(true);
            Debug.Log("Tela de conclusão ativada");
        }

        int estrelas = CalcularEstrelas();
        yield return StartCoroutine(AnimacaoTextoParabens());

        if (textoTempo != null)
        {
            textoTempo.text = "Tempo: " + tempoDecorrido.ToString("F1") + "s";
            Debug.Log("Tempo exibido: " + tempoDecorrido.ToString("F1") + "s");
        }

        yield return StartCoroutine(AnimacaoEstrelas(estrelas));

        if (botaoProximaFase != null) botaoProximaFase.interactable = true;
        if (botaoRepetir != null) botaoRepetir.interactable = true;
        if (botaoMenu != null) botaoMenu.interactable = true;

        Debug.Log("Tela de conclusão completamente carregada");
    }

    IEnumerator AnimacaoTextoParabens()
    {
        if (textoParabens == null) yield break;

        textoParabens.text = "";
        // TEXTO SEM EMOJIS - usa símbolos ASCII
        string mensagem = "PARABENS! VOCE CONCLUIU A FASE!";
        
        // ✅ SOM DE VITÓRIA DURANTE O TEXTO
        TocarSom(somVitoria);
        
        foreach (char letra in mensagem)
        {
            textoParabens.text += letra;
            yield return new WaitForSeconds(0.05f); // Mais rápido
        }

        // Efeito de piscar com cores
        for (int i = 0; i < 3; i++)
        {
            textoParabens.color = Color.yellow;
            yield return new WaitForSeconds(0.2f);
            textoParabens.color = Color.white;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator AnimacaoEstrelas(int quantidadeEstrelas)
    {
        Debug.Log("Mostrando " + quantidadeEstrelas + " estrelas");

        if (estrela1 != null) estrela1.color = Color.gray;
        if (estrela2 != null) estrela2.color = Color.gray;
        if (estrela3 != null) estrela3.color = Color.gray;

        yield return new WaitForSeconds(0.3f);

        if (quantidadeEstrelas >= 1 && estrela1 != null)
        {
            yield return StartCoroutine(AnimacaoEstrelaIndividual(estrela1));
        }
        if (quantidadeEstrelas >= 2 && estrela2 != null)
        {
            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(AnimacaoEstrelaIndividual(estrela2));
        }
        if (quantidadeEstrelas >= 3 && estrela3 != null)
        {
            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(AnimacaoEstrelaIndividual(estrela3));
        }
    }

    IEnumerator AnimacaoEstrelaIndividual(Image estrela)
    {
        Debug.Log("Animando estrela: " + estrela.name);

        // ✅ SOM DA ESTRELA ADICIONADO
        TocarSom(somEstrela);

        float duracao = 0.4f;
        float tempo = 0f;
        Vector3 escalaInicial = Vector3.zero;
        Vector3 escalaFinal = Vector3.one * 1.2f;

        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            float progresso = tempo / duracao;
            estrela.transform.localScale = Vector3.Lerp(escalaInicial, escalaFinal, progresso);
            estrela.color = Color.Lerp(Color.gray, Color.yellow, progresso);
            yield return null;
        }

        estrela.transform.localScale = Vector3.one * 1.2f;
        yield return new WaitForSeconds(0.1f);
        estrela.transform.localScale = Vector3.one;

        Debug.Log("Estrela animada: " + estrela.name);
    }

    int CalcularEstrelas()
    {
        if (tempoDecorrido <= 20f) 
        {
            Debug.Log("3 estrelas - Tempo excelente!");
            return 3;
        }
        if (tempoDecorrido <= 40f) 
        {
            Debug.Log("2 estrelas - Tempo bom!");
            return 2;
        }
        
        Debug.Log("1 estrela - Tempo normal");
        return 1;
    }

    public void ProximaFase()
    {
        // ✅ SOM ADICIONADO
        TocarSom(somCliqueBotao);
        SceneManager.LoadScene("ColocarProcessador");
        Debug.Log("Proxima Fase clicado");
    }

    public void RepetirFase()
    {
        // ✅ SOM ADICIONADO
        TocarSom(somCliqueBotao);
        
        Debug.Log("Repetir Fase clicado");
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void VoltarMenu()
    {
        // ✅ SOM ADICIONADO
        TocarSom(somCliqueBotao);
        SceneManager.LoadScene("MenuFases");
        Debug.Log("Menu clicado");
    }

    public void TesteConclusao()
    {
        // ✅ SOM ADICIONADO NO TESTE
        TocarSom(somConclusao);
        
        Debug.Log("TESTE MANUAL - Concluindo fase...");
        ConcluirFase();
    }
}