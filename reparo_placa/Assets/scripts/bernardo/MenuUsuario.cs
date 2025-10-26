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
        int somValor = PlayerPrefs.GetInt("SomLigado", 1);
        bool somLigado;

        if (somValor == 1)
        {
            somLigado = true;
        }
        else
        {
            somLigado = false;
        }

        if (somLigado)
        {
            Som.sprite = som_ativo;
        }
        else
        {
            Som.sprite = som_mudo;
        }

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
        PlayerPrefs.SetString("CenaAnterior", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("CenaAnterior"));
        SceneManager.LoadScene("TelaConfiguracao");
    }

    public void Sons()
    {
        int somValor = PlayerPrefs.GetInt("SomLigado", 1);
        bool somLigado;

        if (somValor == 1)
        {
            somLigado = true;
            PlayerPrefs.SetInt("SomLigado", 0);
        }
        else
        {
            somLigado = false;
            PlayerPrefs.SetInt("SomLigado", 1);
        }

        PlayerPrefs.Save();

        if (!somLigado)
        {
            Som.sprite = som_ativo;
        }
        else
        {
            Som.sprite = som_mudo;
        }

        GameObject musicaGO = GameObject.Find("Musica Fundo");
        if (musicaGO != null)
        {
            AudioSource audio = musicaGO.GetComponent<AudioSource>();
            if (audio != null)
            {
                if (!somLigado)
                {
                    audio.mute = false;
                }
                else
                {
                    audio.mute = true;
                }
            }
        }
    }
}
