using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;



public class MenuUsuario : MonoBehaviour
{

    [SerializeField] private GameObject PainelCreditos;
    [SerializeField] private GameObject painelMenuPrincipal;
    public Sprite som_ativo, som_mudo;
    public Image Som;
    public static bool somLigado = true;



    void Start()
    {
    bool somLigado = PlayerPrefs.GetInt("SomLigado", 1) == 1;
    Som.sprite = somLigado ? som_ativo : som_mudo;
    AudioListener.volume = somLigado ? 1f : 0f;
    }
    public void TelaInicial()
    {
        SceneManager.LoadScene("TelaEntrar");
    }
    public void Jogar()
    {
        SceneManager.LoadScene("MenuFases");
    }
    public void AbrirCreditos()
    {
        painelMenuPrincipal.SetActive(false);
        PainelCreditos.SetActive(true);
    }
    public void FecharCreditos()
    {   
        painelMenuPrincipal.SetActive(true);
         PainelCreditos.SetActive(false);
    }
    public void Configuracao()
    {
        SceneManager.LoadScene("TelaConfiguracao");
    }

    public void Sons()
    {
// Recupera o estado atual do som (1 = ligado, 0 = desligado)
        int somValor = PlayerPrefs.GetInt("SomLigado", 1); // 1 como padrão

        // Inverte o estado
        bool somLigado = somValor == 1 ? false : true;

        // Salva o novo estado
        PlayerPrefs.SetInt("SomLigado", somLigado ? 1 : 0);
        PlayerPrefs.Save();

        // Atualiza o sprite e o volume
        if (somLigado)
        {
            Som.sprite = som_ativo;
            AudioListener.volume = 1f;
        }
        else
        {
            Som.sprite = som_mudo;
            AudioListener.volume = 0f;
        }

    }
}
