using UnityEngine;
using UnityEngine.SceneManagement;

public class TelaFase : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AbrirFase1()
    {
        SceneManager.LoadScene("TutorialReparoPlaca");
    }
    public void AbrirFase2()
    {
        SceneManager.LoadScene("0.backupMinhaCena_RECUPERADA");
    }
    public void AbrirFase3()
    {
        SceneManager.LoadScene("ColocarProcessador");
    }
     public void Voltar()
    {
        SceneManager.LoadScene("TelaUsuario");
   }
     public void AbrirFase4()
    {
        SceneManager.LoadScene("CircuitoCarregador");
    }
     public void AbrirFase5()
    {
        SceneManager.LoadScene("DescarteLixo");
    }
}
