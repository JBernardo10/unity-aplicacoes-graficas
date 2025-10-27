using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject painelTutorial;
    public Image imagemTutorial;
    public Text textoInstrucao;
    public Button botaoProximo;
    public Button botaoPular;
    public Button botaoIniciar;

    [Header("Conteúdo do Tutorial")]
    public Sprite[] spritesTutorial;
    public string[] instrucoesTutorial;

    [Header("Referências do Jogo")]
    public Button botaoFusivel;
    public Button botaoAlicate;
    public Button botaoFusivelNovo;
    public Button botaoFerroSolda;

    [Header("Sons dos Botões do Tutorial")]
    public AudioClip somCliqueBotao;
    public AudioClip somHoverBotao;
    
    private int passoAtual = 0;
    private bool tutorialAtivo = false;
    private AudioSource audioSource;

    void Start()
    {
        Debug.Log("🔧 TUTORIAL MANAGER INICIADO");
        
        // ✅ CONFIGURAÇÃO DE ÁUDIO ADICIONADA
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.volume = 0.7f;
        }
        
        // CONFIGURA SONS NOS BOTÕES DO TUTORIAL
        ConfigurarSonsBotoes();
        
        // VERIFICA SE O CONTEÚDO ESTÁ CONFIGURADO
        if (spritesTutorial == null || spritesTutorial.Length == 0)
        {
            Debug.LogError("❌ Sprites Tutorial não configurados!");
        }
        
        if (instrucoesTutorial == null || instrucoesTutorial.Length == 0)
        {
            Debug.LogError("❌ Instruções Tutorial não configuradas!");
        }
        
        IniciarTutorial();
    }

    // ✅ MÉTODO NOVO: CONFIGURA SONS NOS BOTÕES
    void ConfigurarSonsBotoes()
    {
        ConfigurarSomBotao(botaoProximo);
        ConfigurarSomBotao(botaoPular);
        ConfigurarSomBotao(botaoIniciar);
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

    public void IniciarTutorial()
    {
        tutorialAtivo = true;
        Debug.Log("🎓 TUTORIAL INICIADO");
        
        painelTutorial.SetActive(true);
        DesativarInteracoesJogo();
        MostrarPasso(0);
    }

    public void ProximoPasso()
    {
        // ✅ SOM ADICIONADO
        TocarSom(somCliqueBotao);
        
        passoAtual++;
        if (passoAtual < spritesTutorial.Length)
        {
            MostrarPasso(passoAtual);
        }
    }

    public void PularTutorial()
    {
        // ✅ SOM ADICIONADO
        TocarSom(somCliqueBotao);
        
        Debug.Log("⏩ PULANDO TUTORIAL");
        FinalizarTutorial();
    }

    public void IniciarJogo()
    {
        // ✅ SOM ADICIONADO
        TocarSom(somCliqueBotao);
        
        Debug.Log("🎮 INICIANDO JOGO!");
        FinalizarTutorial();
    }

    void MostrarPasso(int passo)
    {
        Debug.Log($"📖 MOSTRANDO PASSO {passo + 1}");
        
        // VERIFICA IMAGEM
        if (imagemTutorial != null)
        {
            if (passo < spritesTutorial.Length && spritesTutorial[passo] != null)
            {
                imagemTutorial.sprite = spritesTutorial[passo];
                Debug.Log($"🖼️ Imagem definida: {spritesTutorial[passo].name}");
            }
            else
            {
                Debug.LogWarning($"❌ Sprite do passo {passo} está NULL ou não configurado");
                imagemTutorial.sprite = null;
            }
        }

        // VERIFICA TEXTO
        if (textoInstrucao != null)
        {
            if (passo < instrucoesTutorial.Length && !string.IsNullOrEmpty(instrucoesTutorial[passo]))
            {
                textoInstrucao.text = instrucoesTutorial[passo];
                Debug.Log($"📝 Texto definido: {instrucoesTutorial[passo]}");
            }
            else
            {
                Debug.LogWarning($"❌ Instrução do passo {passo} está vazia");
                textoInstrucao.text = "Instrução não configurada";
            }
        }

        // CONFIGURA BOTÕES
        bool ehUltimoPasso = (passo == spritesTutorial.Length - 1);
        
        if (botaoProximo != null) botaoProximo.gameObject.SetActive(!ehUltimoPasso);
        if (botaoIniciar != null) botaoIniciar.gameObject.SetActive(ehUltimoPasso);
        if (botaoPular != null) botaoPular.gameObject.SetActive(!ehUltimoPasso);
    }

    void DesativarInteracoesJogo()
    {
        if (botaoFusivel != null) botaoFusivel.interactable = false;
        if (botaoAlicate != null) botaoAlicate.interactable = false;
        if (botaoFusivelNovo != null) botaoFusivelNovo.interactable = false;
        if (botaoFerroSolda != null) botaoFerroSolda.interactable = false;
    }

    void AtivarInteracoesJogo()
    {
        if (botaoFusivel != null) botaoFusivel.interactable = true;
        if (botaoAlicate != null) botaoAlicate.interactable = true;
        if (botaoFusivelNovo != null) botaoFusivelNovo.interactable = true;
        if (botaoFerroSolda != null) botaoFerroSolda.interactable = true;
    }

    void FinalizarTutorial()
    {
        tutorialAtivo = false;
        painelTutorial.SetActive(false);
        AtivarInteracoesJogo();
    }

    public bool IsTutorialAtivo()
    {
        return tutorialAtivo;
    }

    // ✅ MÉTODO ADICIONADO: Tocar som manualmente (útil para outros scripts)
    public void TocarSomClique()
    {
        TocarSom(somCliqueBotao);
    }
}