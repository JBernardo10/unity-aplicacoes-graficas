using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EntrarJogo : MonoBehaviour
{
    
    public TMP_InputField inputNome;

    public void Start()
    {
        if (PlayerPrefs.HasKey("NomeDoUsuario"))
        {
            string nome = PlayerPrefs.GetString("NomeDoUsuario");
            inputNome.text = nome;
        }
    }

    public void EntrarESalvarNome()
    {   
        string nome = inputNome.text.Trim();
        if (string.IsNullOrEmpty(nome))
            nome = "jogador";
        PlayerPrefs.SetString("NomeDoUsuario", nome);
        PlayerPrefs.Save();
        //Debug.Log("Nome salvo: " + nome);
        //SceneManager.LoadScene("Menu");
    }
}
