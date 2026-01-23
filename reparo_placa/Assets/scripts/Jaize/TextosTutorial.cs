using UnityEngine;

public class TextosTutorial : MonoBehaviour
{
     [SerializeField] private GameObject Painel1;
    [SerializeField] private GameObject Painel2;
    [SerializeField] private GameObject Painel3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AtivarPainel2()
    {
        Painel1.SetActive(false);
        Painel2.SetActive(true);
        Painel3.SetActive(false);
    }

    public void AtivarPainel1()
    {
        Painel1.SetActive(true);
        Painel2.SetActive(false);
        Painel3.SetActive(false);
    }
    
     public void AtivarPainel3()
    {
        Painel3.SetActive(true);
        Painel1.SetActive(false);
        Painel2.SetActive(false);
    }

  
}
