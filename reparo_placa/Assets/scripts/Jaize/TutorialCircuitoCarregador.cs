using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialCircuitoCarregador : MonoBehaviour
{
      public void IniciarCircuitoCarregador()
    {
        SceneManager.LoadScene("CircuitoCarregador");
    }
   
     public void VoltarFases()
    {
        SceneManager.LoadScene("MenuFases");
    }
     public void VoltarMenuFases()
    {
        SceneManager.LoadScene("TutorialCircuitoCarregador");
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
