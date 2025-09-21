using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;



public class MenuUsuario : MonoBehaviour
{

    [SerializeField] private GameObject PainelCreditos;
    [SerializeField] private GameObject painelMenuPrincipal;

    public void TelaInicial()
    {
        SceneManager.LoadScene("TelaEntrar");
    }
    public void Jogar()
    {
        SceneManager.LoadScene("MenuFases");
    }
    public void AbrirCreditos()
    {
        painelMenuPrincipal.SetActive(false);
        PainelCreditos.SetActive(true);
    }
    public void FecharCreditos()
    {   
        painelMenuPrincipal.SetActive(true);
         PainelCreditos.SetActive(false);
    }
    public void Configuracao()
    {
        //aaa
    }

    public void Sons()
    {
        //sons
    }
}
