using UnityEngine;
using UnityEngine.UI;

public class Processador : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject processador = null;
    public Image socket;
    void Start()
    {

    }

    // Update is called once per frame
    public void ClicouNoProcessador(GameObject p)
    {
        processador = p;
        Debug.Log("processador clicado");
    }

    public void ClicouNoSocket()
    {
        Debug.Log("ClicouNoSocket");
        if (processador != null)
        {
            Debug.Log("Processador Normal");
            if (socket != null)
            {
                Debug.Log("Socket Normal");
                Image spriteProcessador = processador.GetComponent<Image>();
                if (spriteProcessador != null)
                {
                    Debug.Log("Finalizado");
                    socket.sprite = spriteProcessador.sprite;
                    socket.color = new Color(255f, 255f, 255f, 255f);
                    processador.SetActive(false);

                    #if UNITY_2023_1_OR_NEWER
                        ConclusaoManager conclusao = FindFirstObjectByType<ConclusaoManager>();
                    #else
                        ConclusaoManager conclusao = FindObjectOfType<ConclusaoManager>();
                    #endif
                    
                    
                    conclusao.ConcluirFase();
                }
            }
        }
    }
    void Update()
    {
        
    }
}
