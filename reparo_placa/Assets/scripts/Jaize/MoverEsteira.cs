using UnityEngine;
using UnityEngine.UI;

public class MoverEsteira : MonoBehaviour
{
    public float velocidadeEsteira = 0.5f;
    public float velocidadeLixo = 150f;

    public Transform inicioEsteira;
    public Transform fimEsteira;

    private RawImage img;
    private Vector2 offset;
    public bool esteiraParada = false;

    void Start()
    {
        img = GetComponent<RawImage>();
    }

    void Update()
    {
        if (esteiraParada) return;
        // movimento da textura da esteira
        offset.x += velocidadeEsteira * Time.deltaTime;
        img.uvRect = new Rect(offset.x, 0, -1, 1);
    }

    public Vector2 MovimentoEsteira()
    {
        if (esteiraParada)
            return Vector2.zero;
        return Vector2.left * velocidadeLixo * Time.deltaTime;
    }
     public void PararEsteira()
    {
        esteiraParada = true;
    }

    public void VoltarEsteira()
    {
        esteiraParada = false;
    }

}