using UnityEngine;
using UnityEngine.UI;

public class PersonagemManage : MonoBehaviour
{
    public static PersonagemManage instancia;

    public Sprite P1, P2, P3, P4, P5; // arraste os sprites na ordem correta no Inspector

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject); // persiste entre cenas
        }
        else
        {
            Destroy(gameObject); // evita duplicatas
        }
    }

    public Sprite GetPersonagemSprite(int id)
    {
        switch (id)
        {
            case 0:
                return P1;
            case 1:
                return P2;
            case 2:
                return P3;
            case 3:
                return P4;
            case 4:
                return P5;
            default:
                return P1;
        }
    }
}
