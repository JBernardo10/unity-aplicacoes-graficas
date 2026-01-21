using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialCircuitoCarregador : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void VoltarMenuFases()
    {
        SceneManager.LoadScene("MenuFases");
         Screen.orientation = ScreenOrientation.Portrait;
    }
}
