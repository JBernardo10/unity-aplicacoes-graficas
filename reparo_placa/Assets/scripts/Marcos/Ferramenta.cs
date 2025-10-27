using UnityEngine;
using UnityEngine.UI;

public class Ferramenta : MonoBehaviour
{
    public string tipoFerramenta;
    private Image imagemFerramenta;
    private static Ferramenta ferramentaAtual;

    void Start()
    {
        imagemFerramenta = GetComponent<Image>();
        Debug.Log("✅ Ferramenta " + tipoFerramenta + " carregada");
    }

    public void Selecionar()
    {
        Debug.Log("🎯 CLIQUE NA FERRAMENTA: " + tipoFerramenta);
        
        if (ferramentaAtual != null)
        {
            ferramentaAtual.imagemFerramenta.color = Color.white;
        }

        ferramentaAtual = this;
        imagemFerramenta.color = Color.yellow;
        Debug.Log("⭐ FERRAMENTA SELECIONADA: " + tipoFerramenta);
        
        // ✅ BUSCA DETALHADA DO FUSÍVEL
        Fusivel[] todosFusiveis = FindObjectsByType<Fusivel>(FindObjectsSortMode.None);
        Debug.Log("🔍 Procurando fusíveis... Encontrados: " + todosFusiveis.Length);

        if (todosFusiveis.Length > 0)
        {
            foreach (Fusivel fusivel in todosFusiveis)
            {
                Debug.Log("✅ Fusível encontrado: " + fusivel.gameObject.name);
                
                // ✅ MÉTODO CORRIGIDO: "ReceberFerramentaSelecionada"
                fusivel.ReceberFerramentaSelecionada(tipoFerramenta);
                
                Debug.Log("📤 Ferramenta enviada para: " + fusivel.gameObject.name);
            }
        }
        else
        {
            Debug.LogError("❌ NENHUM FUSÍVEL ENCONTROU NA CENA!");
            Debug.LogError("❌ Verifique se existe um objeto com script Fusivel.cs");
        }
    }
}