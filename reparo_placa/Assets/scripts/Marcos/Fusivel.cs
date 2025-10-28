using UnityEngine;
using UnityEngine.UI;

public class Fusivel : MonoBehaviour
{
    [Header("Configurações do Fusível")]
    public Sprite fusivelQueimado;
    public Sprite fusivelNovo;
    
    [Header("Sons do Fusível")]
    public AudioClip somRemoverFusivel;
    public AudioClip somColocarFusivel;
    public AudioClip somSoldar;
    public AudioClip somErro;
    public AudioClip somConclusao;
    
    [Header("Debug - Apenas Visualização")]
    public string ferramentaAtual;
    public string estadoAtual;
    
    private Image fusivelImage;
    private AudioSource audioSource;
    private bool estaQueimado = true;
    private bool temFusivel = true;
    private bool estaSoldado = false;
    private bool faseJaConcluida = false;
    private string ferramentaSelecionada;

    void Start()
    {
        fusivelImage = GetComponent<Image>();
        
        // ✅ CONFIGURAÇÃO DO ÁUDIO ADICIONADA
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.volume = 0.7f;
        }
        // ✅ FIM DA CONFIGURAÇÃO DE ÁUDIO
        
        if (fusivelImage == null)
        {
            Debug.LogError("Componente Image não encontrado no objeto Fusivel!");
            return;
        }
        
        if (fusivelQueimado != null)
        {
            fusivelImage.sprite = fusivelQueimado;
            fusivelImage.color = Color.white;
        }
        else
        {
            Debug.LogWarning("Sprite fusivelQueimado não atribuído no inspector!");
        }
        
        AtualizarDebug();
        Debug.Log("FUSIVEL INICIADO - Estado: QUEIMADO");
    }

    void Update()
    {
        AtualizarDebug();
    }

    void AtualizarDebug()
    {
        ferramentaAtual = ferramentaSelecionada;
        
        if (estaSoldado)
            estadoAtual = "SOLDADO";
        else if (estaQueimado)
            estadoAtual = "QUEIMADO";
        else if (temFusivel)
            estadoAtual = "NOVO";
        else
            estadoAtual = "VAZIO";
    }

    // ✅ MÉTODO DE ÁUDIO ADICIONADO
    private void TocarSom(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void ClicarFusivel()
    {
        Debug.Log($"CLIQUE NO FUSIVEL - Ferramenta: {ferramentaSelecionada}, Estado: {estadoAtual}");

        if (fusivelImage == null)
        {
            Debug.LogError("fusivelImage é null! Não é possível processar clique.");
            return;
        }

        // ALICATE - REMOVE FUSIVEL QUEIMADO
        if (ferramentaSelecionada == "Alicate")
        {
            if (estaQueimado && temFusivel && !estaSoldado)
            {
                Debug.Log("REMOVENDO fusivel queimado com alicate...");
                fusivelImage.sprite = null;
                fusivelImage.color = new Color(0.3f, 0.3f, 0.3f, 0.3f);
                temFusivel = false;
                estaQueimado = false;
                estaSoldado = false;
                
                // ✅ SOM ADICIONADO
                TocarSom(somRemoverFusivel);
                Debug.Log("Fusivel removido! Agora esta VAZIO");
            }
            else if (!temFusivel)
            {
                // ✅ SOM DE ERRO ADICIONADO
                TocarSom(somErro);
                Debug.LogWarning("Não há fusível para remover!");
            }
            else if (estaSoldado)
            {
                // ✅ SOM DE ERRO ADICIONADO
                TocarSom(somErro);
                Debug.LogWarning("Fusível está soldado! Não pode ser removido com alicate.");
            }
            else
            {
                // ✅ SOM DE ERRO ADICIONADO
                TocarSom(somErro);
                Debug.LogWarning("Alicate só remove fusíveis QUEIMADOS!");
            }
        }
        // FUSIVEL NOVO - COLOCA FUSIVEL NOVO
        else if (ferramentaSelecionada == "FusivelNovo")
        {
            if (!temFusivel && fusivelNovo != null && !estaSoldado)
            {
                Debug.Log("COLOCANDO fusivel novo...");
                fusivelImage.sprite = fusivelNovo;
                fusivelImage.color = Color.white;
                temFusivel = true;
                estaQueimado = false;
                estaSoldado = false;
                
                // ✅ SOM ADICIONADO
                TocarSom(somColocarFusivel);
                Debug.Log("Fusivel novo colocado! Agora esta NOVO");
            }
            else if (temFusivel)
            {
                // ✅ SOM DE ERRO ADICIONADO
                TocarSom(somErro);
                Debug.LogWarning("Já existe um fusível no lugar!");
            }
            else if (estaSoldado)
            {
                // ✅ SOM DE ERRO ADICIONADO
                TocarSom(somErro);
                Debug.LogWarning("Não pode colocar fusível novo em base soldada!");
            }
            else
            {
                // ✅ SOM DE ERRO ADICIONADO
                TocarSom(somErro);
                Debug.LogWarning("Só pode colocar fusível novo quando estiver VAZIO!");
            }
        }
        // FERRO DE SOLDA - SOLDAR O FUSIVEL
        else if (ferramentaSelecionada == "FerroSolda")
        {
            if (temFusivel && !estaQueimado && !estaSoldado)
            {
                Debug.Log("SOLDANDO fusivel com ferro de solda...");
                
                // EFEITO DOURADO
                fusivelImage.color = new Color(1.0f, 0.85f, 0.2f);
                
                estaSoldado = true;
                
                // ✅ SOM ADICIONADO
                TocarSom(somSoldar);
                Debug.Log("FUSIVEL SOLDADO COM SUCESSO!");
                
                // CHAMA A CONCLUSÃO DA FASE após 1 segundo
                Invoke(nameof(VerificarConclusao), 1f);
            }
            else if (estaSoldado)
            {
                Debug.Log("Fusivel já está SOLDADO!");
                fusivelImage.color = new Color(1.0f, 0.9f, 0.3f);
            }
            else if (!temFusivel)
            {
                // ✅ SOM DE ERRO ADICIONADO
                TocarSom(somErro);
                Debug.LogWarning("Não há fusível para soldar!");
            }
            else if (estaQueimado)
            {
                // ✅ SOM DE ERRO ADICIONADO
                TocarSom(somErro);
                Debug.LogWarning("Não pode soldar fusível queimado!");
            }
            else
            {
                // ✅ SOM DE ERRO ADICIONADO
                TocarSom(somErro);
                Debug.LogWarning("Ferro de solda só solda fusíveis NOVOS!");
            }
        }
        else
        {
            // ✅ SOM DE ERRO ADICIONADO
            TocarSom(somErro);
            Debug.LogWarning("Selecione uma ferramenta primeiro!");
        }
    }

    // METODO PARA VERIFICAR CONCLUSÃO DA FASE
    public void VerificarConclusao()
    {
        Debug.Log("Verificando conclusão da fase...");
        
        if (estaSoldado && !faseJaConcluida)
        {
            Debug.Log("Fusivel soldado - chamando conclusão da fase!");
            
            // ✅ SOM DE CONCLUSÃO ADICIONADO
            TocarSom(somConclusao);
            
            // CORREÇÃO: Usando o método não-obsoleto
            #if UNITY_2023_1_OR_NEWER
            ConclusaoManager conclusao = FindFirstObjectByType<ConclusaoManager>();
            #else
            ConclusaoManager conclusao = FindObjectOfType<ConclusaoManager>();
            #endif
            
            if (conclusao != null)
            {
                conclusao.ConcluirFase();
                faseJaConcluida = true;
                Debug.Log("ConclusaoManager encontrado e acionado!");
            }
            else
            {
                Debug.LogError("ConclusaoManager não encontrado na cena!");
                
                // Fallback: tenta encontrar um gerenciador de fase alternativo
                #if UNITY_2023_1_OR_NEWER
                GameObject managerObj = GameObject.FindFirstObjectByType<GameObject>();
                #else
                GameObject managerObj = GameObject.FindObjectOfType<GameObject>();
                #endif
                
                if (managerObj != null)
                {
                    Debug.Log("GameManager encontrado como alternativa!");
                    // Adicione aqui a chamada para seu gerenciador alternativo
                }
            }
        }
        else if (faseJaConcluida)
        {
            Debug.Log("Fase já foi concluída anteriormente");
        }
        else
        {
            Debug.Log("Fusivel não está soldado - não pode concluir fase");
        }
    }

    public void ReceberFerramentaSelecionada(string ferramenta)
    {
        ferramentaSelecionada = ferramenta;
        Debug.Log($"Ferramenta recebida: {ferramenta}");
    }

    public void ResetarFusivel()
    {
        if (fusivelImage == null)
        {
            Debug.LogError("Não é possível resetar: fusivelImage é null!");
            fusivelImage = GetComponent<Image>();
            if (fusivelImage == null) return;
        }

        if (fusivelQueimado != null)
        {
            fusivelImage.sprite = fusivelQueimado;
            fusivelImage.color = Color.white;
        }
        
        estaQueimado = true;
        temFusivel = true;
        estaSoldado = false;
        faseJaConcluida = false;
        ferramentaSelecionada = "";
            
        Debug.Log("FUSIVEL RESETADO para estado inicial");
    }

    // METODO PARA TESTE RAPIDO
    public void TesteConclusaoRapida()
    {
        Debug.Log("TESTE RAPIDO - Simulando conclusão...");
        
        if (fusivelImage == null)
        {
            Debug.LogError("fusivelImage é null no teste rápido!");
            return;
        }

        // Simula o fusivel sendo soldado
        fusivelImage.sprite = fusivelNovo;
        fusivelImage.color = new Color(1.0f, 0.85f, 0.2f);
        estaSoldado = true;
        estaQueimado = false;
        temFusivel = true;
        
        // ✅ SOM ADICIONADO NO TESTE
        TocarSom(somSoldar);
        
        // Chama a conclusão imediatamente
        VerificarConclusao();
    }

    // NOVO: Método para verificar o estado atual (útil para outros scripts)
    public bool EstaConcluido()
    {
        return estaSoldado && faseJaConcluida;
    }

    // NOVO: Método para forçar conclusão (útil para testes)
    public void ForcarConclusao()
    {
        if (!estaSoldado)
        {
            estaSoldado = true;
            estaQueimado = false;
            temFusivel = true;
            
            if (fusivelImage != null && fusivelNovo != null)
            {
                fusivelImage.sprite = fusivelNovo;
                fusivelImage.color = new Color(1.0f, 0.85f, 0.2f);
            }
        }
        
        // ✅ SONS ADICIONADOS
        TocarSom(somSoldar);
        TocarSom(somConclusao);
        
        VerificarConclusao();
    }

    void OnValidate()
    {
        if (fusivelImage == null)
            fusivelImage = GetComponent<Image>();
            
        // ✅ VERIFICAÇÃO DE AUDIOSOURCE ADICIONADA
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }
}