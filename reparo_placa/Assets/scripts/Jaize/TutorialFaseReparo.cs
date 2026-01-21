using UnityEngine;
using UnityEngine.SceneManagement;


public class TutorialFaseReparo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Iniciar()
    {
        SceneManager.LoadScene("FaseReparoPlaca");
    }

    // Update is called once per frame
    public void VoltarMenuFases()
    {
        SceneManager.LoadScene("MenuFases");
    }

    public void VoltarTutorialFaseReparo()
    {

        SceneManager.LoadScene("TutorialReparoPlaca");
        Screen.orientation = ScreenOrientation.Portrait;
        
    }
    
}
