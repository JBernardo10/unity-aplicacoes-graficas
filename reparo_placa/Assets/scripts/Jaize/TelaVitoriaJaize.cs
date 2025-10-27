using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaVitoriaJaize : MonoBehaviour
{
    [SerializeField] string victoryScene = "TelaVitoria";
    [SerializeField] int totalObjetivos = 2;   // qtd de capacitores/slots a corrigir
    [SerializeField] float delayAntesDeIr = 1.0f;

    int concluido;

     // Variável para armazenar o tempo total da fase
    private float tempoInicio;
    public float tempoTotalFase; 

      private void Start()
    {
        // Marca o tempo inicial da fase
        tempoInicio = Time.time;
    }

    // chame isto quando UM capacitor for finalizado (removido queimado + instalado novo)
    public void RegistrarObjetivoConcluido()
    {
        concluido++;
        if (concluido >= totalObjetivos)
            StartCoroutine(GoVictory());
    }

    System.Collections.IEnumerator GoVictory()
    {
        tempoTotalFase = Time.time - tempoInicio;
        Debug.Log($"⏱️ Tempo total da fase: {tempoTotalFase:F2} segundos");

        yield return new WaitForSeconds(delayAntesDeIr);

        PlayerPrefs.SetFloat("UltimoTempoFase", tempoTotalFase);
        
        SceneManager.LoadScene(victoryScene);
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
