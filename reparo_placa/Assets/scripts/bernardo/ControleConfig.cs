using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ControleConfig : MonoBehaviour
{    [Header("Botões")]
    public Button btnSons;
    public Button btnControles;

    [Header("Cores das Abas")]
    public Color corAtiva = Color.blue;
    public Color corInativa = Color.white;

    [Header("Painéis")]
    [SerializeField] private GameObject PainelSons;
    [SerializeField] private GameObject PainelContls;

    [Header("Sliders de Volume")]
    public Slider sliderVolumeMusica;
    public Slider sliderEfeitoSonoro;
    public Slider sliderVolumeGeral;


    void Start()
    {
        // Adiciona os listeners
        btnSons.onClick.AddListener(() => AtivarAba(btnSons, btnControles));
        btnControles.onClick.AddListener(() => AtivarAba(btnControles, btnSons));

        // Aba inicial (opcional)
        AtivarAba(btnSons, btnControles);

        float volumeMusicaSalvo = PlayerPrefs.GetFloat("VolumeMusica", 1f);
        float volumeEfeitosSalvo = PlayerPrefs.GetFloat("VolumeEfeitos", 1f);
        float volumeGeralsSalvo = PlayerPrefs.GetFloat("VolumeGeral", 1f);

        sliderVolumeMusica.value = volumeMusicaSalvo;
        sliderEfeitoSonoro.value = volumeEfeitosSalvo;
        sliderVolumeGeral.value = volumeGeralsSalvo;
    }

    void AtivarAba(Button ativo, Button inativo)
    {
        // Cor do botão ativo
        ativo.GetComponent<Image>().color = corAtiva;
        // Cor do botão inativo
        inativo.GetComponent<Image>().color = corInativa;
    }

    public void AbaSom()
    {
        PainelSons.SetActive(true);
        PainelContls.SetActive(false);

    }
    public void AbaConfig()
    {
        PainelSons.SetActive(false);
        PainelContls.SetActive(true);

    }
    public void Voltar()
    {
        string cenaAnterior = PlayerPrefs.GetString("CenaAnterior", "TelaUsuario"); // valor padrão se não existir
        SceneManager.LoadScene("TelaUsuario");
        //Debug.Log(cenaAnterior);
    }

    public void AtualizarVolumeMusica(float valor)
    {
        GameObject musicaGO = GameObject.Find("Musica Fundo");
        if (musicaGO != null)
        {
            AudioSource audio = musicaGO.GetComponent<AudioSource>();
            if (audio != null)
            {
                float geral = PlayerPrefs.GetFloat("VolumeGeral", 1f);
                 float vfinal = valor * geral;
                audio.volume = vfinal;
                PlayerPrefs.SetFloat("VolumeMusica", valor);
                PlayerPrefs.Save();
            }
        }
    }
    public void AtualizarVolumeEfeitoSonoro(float valor)
    {
        float geral = PlayerPrefs.GetFloat("VolumeGeral", 1f);
        float vfinal = valor * geral;
        PlayerPrefs.SetFloat("VolumeEfeitos", valor);
        PlayerPrefs.Save();
    }
    public void AtualizarVolumeGeral(float valorGeral)
    {
        // Salva o novo volume geral
        PlayerPrefs.SetFloat("VolumeGeral", valorGeral);
        PlayerPrefs.Save();
        // Atualiza volumes em tempo real (música e efeitos)
        AtualizarVolumeMusica(sliderVolumeMusica.value);
        AtualizarVolumeEfeitoSonoro(sliderEfeitoSonoro.value);

        
    }

}
