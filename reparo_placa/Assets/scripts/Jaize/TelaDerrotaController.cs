using UnityEngine;
using TMPro;

public class TelaDerrotaController : MonoBehaviour
{
    public TMP_Text textoAcertos;
    public TMP_Text textoErros;

    void Start()
    {
        textoAcertos.text = textoAcertos.text + " " + TesteLixeira.acertos;
        textoErros.text   = textoErros.text + " " + TesteLixeira.erros;

    }
}