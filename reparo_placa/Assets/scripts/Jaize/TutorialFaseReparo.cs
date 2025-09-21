using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialFaseReparo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Iniciar()
    {
        //SceneManager.LoadScene("fase1");
    }

    // Update is called once per frame
    public void VoltarMenuFases()
    {
        SceneManager.LoadScene("MenuFases");
    }
}
