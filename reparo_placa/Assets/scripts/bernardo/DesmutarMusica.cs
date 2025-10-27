using UnityEngine;

public class DesmutarMusica : MonoBehaviour
{
    [Header("Nome do objeto da música de fundo")]
    public string nomeMusicaFundo = "Musica Fundo";

    private void Start()
    {
        GameObject musicaGO = GameObject.Find(nomeMusicaFundo);
        if (musicaGO != null)
        {
            AudioSource audio = musicaGO.GetComponent<AudioSource>();
            if (audio != null)
            {
                float volumeMusica = PlayerPrefs.GetFloat("VolumeMusica", 1f);
                float volumeGeral = PlayerPrefs.GetFloat("VolumeGeral", 1f);
                audio.volume = volumeMusica * volumeGeral;

                Debug.Log($"🔊 Música de fundo restaurada pelo script '{name}'.");
            }
        }
        else
        {
            Debug.LogWarning($"⚠️ GameObject '{nomeMusicaFundo}' não encontrado na cena!");
        }
    }
}
