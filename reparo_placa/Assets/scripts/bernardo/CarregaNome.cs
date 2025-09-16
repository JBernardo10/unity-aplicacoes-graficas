using UnityEngine;
using TMPro;

public class CarregaNome : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_Text textoNome;
    void Start()
    {
        string nome = PlayerPrefs.GetString("NomeDoUsuario");
        textoNome.text = nome;
        //Debug.Log("Nome recuperdo: " + nome);
    }

    
    void TrocaAvatar()
    {
        
    }
}
