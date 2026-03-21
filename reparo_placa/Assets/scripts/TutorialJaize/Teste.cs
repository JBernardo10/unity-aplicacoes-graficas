using UnityEngine;

public class Teste : MonoBehaviour
{
    public static bool jogoIniciado = false;
    public TipoLixo tipo;
    public float velocidade = 300f;

    private Transform destino;

    void Start()
    {
        // Procura todas as lixeiras na cena
       LixTutorial[] lixeiras = FindObjectsOfType<LixTutorial>();

        foreach (LixTutorial lixeira in lixeiras)
        {
            if (lixeira.tipo == tipo)
            {
                destino = lixeira.transform;
                break;
            }
        }
    }

    void Update()
    {
        if (!jogoIniciado)
            return;

        if (destino != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                destino.position,
                velocidade * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, destino.position) < 20f)
            {
                Destroy(gameObject);
            }
        }
    }
}