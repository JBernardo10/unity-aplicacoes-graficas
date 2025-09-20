using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AlteraPersonagem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created  // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static int personagem = 0;
    public Sprite P1, P2, P3, P4, P5;
    public Image avatar;


    public void MudaPersonagem(int v)
    { //1
        personagem = v;
        PlayerPrefs.SetInt("PersonagemSelecionado", personagem); // salva
        PlayerPrefs.Save(); // garante que será gravado
        Muda();
    }
    void Start()
    {//1 criar usado  o playprefs
     // Recupera o personagem salvo, se existir
        if (PlayerPrefs.HasKey("PersonagemSelecionado"))
        {
            personagem = PlayerPrefs.GetInt("PersonagemSelecionado");
        }
        Muda();
    }

    // Update is called once per frame
    void Muda()
    {
        /*if (sexo == 0)
            avatar.sprite = P1;
        else
            avatar.sprite = P2;
        //Debug.Log(sexo);*/

        switch (personagem)
        {
            case 0:
                avatar.sprite = P1;
                break;
            case 1:
                avatar.sprite = P2;
                break;
            case 2:
                avatar.sprite = P3;
                break;
            case 3:
                avatar.sprite = P4;
                break;
            case 4:
                avatar.sprite = P5;
                break;
            default:
                avatar.sprite = P1;
                break;
        }
    }

    public void Voltar()
    {
        SceneManager.LoadScene("TelaUsuario");
    }
}
