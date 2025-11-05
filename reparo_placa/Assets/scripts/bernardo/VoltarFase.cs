using UnityEngine;
using UnityEngine.SceneManagement;

public class VoltarFase : MonoBehaviour
{
    public string nomeCena; // Nome da cena a ser carregada

    public void Voltar()
    {
        SceneManager.LoadScene(nomeCena);
    }
}
