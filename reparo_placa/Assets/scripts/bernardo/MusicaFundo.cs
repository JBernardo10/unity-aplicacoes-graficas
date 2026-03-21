using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class MusicaFundo : MonoBehaviour
{
   
    private static MusicaFundo musicaFundo;

    void Awake()
    {
        if (musicaFundo == null)
        {
            musicaFundo = this;
           // Verifica se SomLigado já existe
            if (!PlayerPrefs.HasKey("SomLigado"))
            {
                PlayerPrefs.SetInt("SomLigado", 1); // 1 = som ligado
            }

            // Verifica se VolumeMusica já existe
            if (!PlayerPrefs.HasKey("VolumeMusica"))
            {
                PlayerPrefs.SetFloat("VolumeMusica", 1f); // volume máximo
            }
           else
            {
                // Aplica o volume salvo ao AudioSource da música de fundo
                GameObject musicaGO = GameObject.Find("Musica Fundo");
                if (musicaGO != null)
                {
                    AudioSource audio = musicaGO.GetComponent<AudioSource>();
                    if (audio != null)
                    {
                        float volumeSalvo = PlayerPrefs.GetFloat("VolumeMusica", 1f);
                        float volumeGeral = PlayerPrefs.GetFloat("VolumeGeral", 1f);
                        audio.volume =  volumeSalvo * volumeGeral;
                        int somLigado = PlayerPrefs.GetInt("SomLigado", 1);
                        if(somLigado == 0)
                        {
                            audio.mute = true;
                        }
                    }
                }
            }
            if (!PlayerPrefs.HasKey("VolumeEfeitos"))
            {
                PlayerPrefs.SetFloat("VolumeEfeitos", 1f); // volume máximo
            }
                if (!PlayerPrefs.HasKey("VolumeGeral"))
            {
                PlayerPrefs.SetFloat("VolumeGeral", 1f); // volume máximo
            }

            PlayerPrefs.Save();
            DontDestroyOnLoad(musicaFundo);

        }
        else
        {
            Destroy(gameObject);
        }
    }
}