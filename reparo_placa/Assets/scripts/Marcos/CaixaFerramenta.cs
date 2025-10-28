using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CaixaDeFerramenta : MonoBehaviour, IPointerClickHandler
{
    [Header("Referências UI")]
    public GameObject painelFerramentas;
    public Button botaoAbrirFechar;

    [Header("Configurações")]
    public bool estaAberta = false;
    public bool caixaClicavel = true; // Se a caixa pode ser clicada para abrir/fechar

    void Start()
    {
        Debug.Log("🎯 CAIXA DE FERRAMENTAS - Iniciando...");
        
        // Configura o botão
        if (botaoAbrirFechar != null)
        {
            botaoAbrirFechar.onClick.RemoveAllListeners();
            botaoAbrirFechar.onClick.AddListener(AlternarCaixa);
            Debug.Log("✅ Botão configurado: " + botaoAbrirFechar.name);
        }

        // Estado inicial
        AtualizarVisual();
    }

    // ✅ MÉTODO CHAMADO QUANDO CLICA NA CAIXA
    public void OnPointerClick(PointerEventData eventData)
    {
        if (caixaClicavel)
        {
            Debug.Log("🎯 CAIXA CLICADA - Alternando...");
            AlternarCaixa();
        }
    }

    // ✅ MÉTODO PARA ABRIR/FECHAR (usado pelo botão E pela caixa)
    public void AlternarCaixa()
    {
        estaAberta = !estaAberta;
        Debug.Log("📦 Estado: " + (estaAberta ? "ABERTA" : "FECHADA"));
        AtualizarVisual();
    }

    void AtualizarVisual()
    {
        if (painelFerramentas != null)
        {
            painelFerramentas.SetActive(estaAberta);
        }

        // Atualiza texto do botão
        if (botaoAbrirFechar != null)
        {
            Text texto = botaoAbrirFechar.GetComponentInChildren<Text>();
            if (texto != null)
            {
                texto.text = estaAberta ? "FECHAR" : "ABRIR";
            }
        }
    }

    // ✅ MÉTODO PARA A CAIXA ABRIR (usado por outros scripts)
    public void AbrirCaixa()
    {
        Debug.Log("📥 Abrindo caixa...");
        estaAberta = true;
        AtualizarVisual();
    }

    // ✅ MÉTODO PARA A CAIXA FECHAR (usado por outros scripts)
    public void FecharCaixa()
    {
        Debug.Log("📤 Fechando caixa...");
        estaAberta = false;
        AtualizarVisual();
    }

    [ContextMenu("--- TESTAR: CLIQUE NA CAIXA ---")]
    public void TestarCliqueCaixa()
    {
        Debug.Log("🧪 Simulando clique na caixa...");
        AlternarCaixa();
    }

    [ContextMenu("--- VERIFICAR COMPONENTES ---")]
    public void VerificarComponentes()
    {
        Debug.Log("🔍 VERIFICANDO COMPONENTES:");
        
        // Verifica se tem Image (necessário para cliques UI)
        Image img = GetComponent<Image>();
        Debug.Log("- Tem Image: " + (img != null));
        
        // Verifica se tem Collider (necessário para cliques 3D)
        Collider2D col2D = GetComponent<Collider2D>();
        Collider col3D = GetComponent<Collider>();
        Debug.Log("- Tem Collider2D: " + (col2D != null));
        Debug.Log("- Tem Collider3D: " + (col3D != null));
        
        // Verifica se está no Canvas
        Canvas canvas = GetComponentInParent<Canvas>();
        Debug.Log("- Está no Canvas: " + (canvas != null));
    }
}