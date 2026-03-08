using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaVitoriaJaize : MonoBehaviour
{
    [SerializeField] string victoryScene = "TelaVitoria";
    [SerializeField] int totalObjetivos = 4;   // qtd de 
    // capacitores/slots a corrigir
    public int totalFerramenta = 6;
    [SerializeField] float delayAntesDeIr = 1.0f;
    [SerializeField] int numeroFase;

    int concluido = 0;

     // Variável para armazenar o tempo total da fase
    private float tempoInicio;
    public float tempoTotalFase; 
    public SistemaPontuacao sistemaPontuacao;

      private void Start()
    {
        // Marca o tempo inicial da fase
        tempoInicio = Time.time;

        // Atualiza o HUD logo no início da fase
        string Objetivo = string.Format("Objetivos: {0}/{1}", concluido, totalObjetivos);
        sistemaPontuacao.AtualizaObj(Objetivo);

    }

     private void Update()
    {
        // Atualiza o cronômetro em tempo real
        tempoTotalFase = Time.time - tempoInicio;
        // Converte para minutos e segundos
        int minutos = Mathf.FloorToInt(tempoTotalFase / 60f);
        int segundos = Mathf.FloorToInt(tempoTotalFase % 60f);

        // Formata no estilo 00:00
        string cronometro = string.Format("{0:00}:{1:00}", minutos, segundos);
        sistemaPontuacao.AtualizaTempo(cronometro); 

         //Debug.Log("⏱️ Cronômetro: " + segundos);
    }


    // chame isto quando UM capacitor for finalizado (removido queimado + instalado novo)
    public void RegistrarObjetivoConcluido()
    {    
        concluido++;
        //string Objetivo = string.Format("Objetivos: {0}/{0}",concluido, totalObjetivos);
        string Objetivo = string.Format("Objetivos: {0}/{1}", concluido, totalObjetivos);
        sistemaPontuacao.AtualizaObj(Objetivo);
       
        if (concluido >= totalObjetivos)
            StartCoroutine(GoVictory());
    }

    System.Collections.IEnumerator GoVictory()
    {
        tempoTotalFase = Time.time - tempoInicio;
        //Debug.Log($"⏱️ Tempo total da fase: {tempoTotalFase:F2} segundos");

        yield return new WaitForSeconds(delayAntesDeIr);

        PlayerPrefs.SetFloat("UltimoTempoFase", tempoTotalFase);
        PlayerPrefs.SetInt("UltimoFaseConcluida", numeroFase);
        
        SceneManager.LoadScene(victoryScene);
        Screen.orientation = ScreenOrientation.Portrait;
    }

     public void RegistrarFerramentaConcluido(int ferraConcluido)
    {    
        string ferramentasUsadas = string.Format("Ferramentas: {0}/{1}",ferraConcluido, totalFerramenta);
        sistemaPontuacao.AtualizarFerramentas(ferramentasUsadas);
       
    }
}
