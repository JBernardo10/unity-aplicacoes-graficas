using UnityEngine;

public class ControlaEfeitoSonoro : MonoBehaviour
{
    [Header("Fonte do Som de Efeito")]
    public AudioSource audioefeito;  // arraste o AudioSource aqui no Inspector

    private float volumeEfeitos;
    private float volumeGeral;

    private void Start()
    {
        AtualizarVolume();
    }

    /// <summary>
    /// Atualiza o volume do áudio conforme os valores salvos nos PlayerPrefs.
    /// </summary>
    public void AtualizarVolume()
    {
        volumeEfeitos = PlayerPrefs.GetFloat("VolumeEfeitos", 1f);
        volumeGeral = PlayerPrefs.GetFloat("VolumeGeral", 1f);

        float volumeFinal = volumeEfeitos * volumeGeral;

        if (audioefeito != null)
            audioefeito.volume = volumeFinal;
    }

    /// <summary>
    /// Toca o efeito sonoro aplicando o volume configurado.
    /// </summary>
    public void TocarEfeito()
    {
        if (audioefeito != null)
        {
            AtualizarVolume();
            audioefeito.Play();
        }
        else
        {
            Debug.LogWarning($"⚠️ Nenhum AudioSource atribuído no objeto {name}");
        }
    }

    /// <summary>
    /// Toca o som apenas se não estiver tocando.
    /// </summary>
    public void TocarUnico()
    {
        if (audioefeito != null && !audioefeito.isPlaying)
        {
            AtualizarVolume();
            audioefeito.Play();
        }
    }
}
