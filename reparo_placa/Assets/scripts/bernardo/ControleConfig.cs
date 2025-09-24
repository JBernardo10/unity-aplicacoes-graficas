using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class ControleConfig : MonoBehaviour
{
    public Button btnSons;
    public Button btnControles;

    public Color corAtiva = Color.blue;
    public Color corInativa = Color.white;

    [SerializeField] private GameObject PainelSons;
    [SerializeField] private GameObject PainelContls;

    void Start()
    {
        // Adiciona os listeners
        btnSons.onClick.AddListener(() => AtivarAba(btnSons, btnControles));
        btnControles.onClick.AddListener(() => AtivarAba(btnControles, btnSons));

        // Aba inicial (opcional)
        AtivarAba(btnSons, btnControles);
    }

    void AtivarAba(Button ativo, Button inativo)
    {
        // Cor do botão ativo
        ativo.GetComponent<Image>().color = corAtiva;
        // Cor do botão inativo
        inativo.GetComponent<Image>().color = corInativa;
    }

    public void AbaSom()
    {
        PainelSons.SetActive(true);
        PainelContls.SetActive(false);

    }
    public void AbaConfig()
    {
        PainelSons.SetActive(false);
        PainelContls.SetActive(true);

    }
    public void Voltar()
    {
        SceneManager.LoadScene("TelaUsuario");
    }

}
