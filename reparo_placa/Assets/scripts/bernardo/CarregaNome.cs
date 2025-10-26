using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarregaNome : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_Text textoNome;
    public Image avatar;
    void Start()
    {  

        string nome = PlayerPrefs.GetString("NomeDoUsuario");
        textoNome.text = nome;
        //Debug.Log("Nome recuperdo: " + nome); 
        // // Recupera o ID do personagem salvo
        int personagemID = PlayerPrefs.GetInt("PersonagemSelecionado", 0);

        // Verifica se o gerenciador está disponível
        if (PersonagemManage.instancia != null)
        {
            // Recupera o sprite correspondente e aplica na imagem
            avatar.sprite = PersonagemManage.instancia.GetPersonagemSprite(personagemID);
        }

    }
    public void AlteraPersonagem()
    {
        PlayerPrefs.SetString("CenaAnterior", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
    
        SceneManager.LoadScene("EscolhaPersonagem");
        
    }


}
