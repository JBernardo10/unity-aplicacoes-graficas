using UnityEngine;

public class AbriFechar : MonoBehaviour
{
    public RectTransform painel;
    public Vector2 posFechado = new Vector2(-358, 0); // posição fora da tela
    public Vector2 posAberto = new Vector2(0, 0);      // posição visível
    public float velocidade = 10f;

    private bool aberto = false;
    private Vector2 destino;

    void Start()
    {
        painel.anchoredPosition = posFechado;
        destino = posFechado;
    }

    void Update()
    {
        painel.anchoredPosition = Vector2.Lerp(
            painel.anchoredPosition,
            destino,
            Time.deltaTime * velocidade
        );
    }

    public void Alternar()
    {
        aberto = !aberto;
        destino = aberto ? posAberto : posFechado;
    }
}