using UnityEngine;

public class MutarMusica : MonoBehaviour
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
                audio.volume = 0f;
                Debug.Log($"🔇 Música de fundo mutada pelo script '{name}'.");
            }
        }
        else
        {
            Debug.LogWarning($"⚠️ GameObject '{nomeMusicaFundo}' não encontrado na cena!");
        }
    }
}
