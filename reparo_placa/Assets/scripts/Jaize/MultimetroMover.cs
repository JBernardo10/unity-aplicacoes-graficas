using UnityEngine;
using UnityEngine.EventSystems;


public class MultimetroMover : MonoBehaviour, IPointerClickHandler
{
    public Transform posicaoNaPlaca;   // alvo
    public float velocidade = 5f;
    public Vector3 escalaFinal = new Vector3(2f, 2f, 2f); // tamanho desejado
    public AbriFechar painelFerramentas; // referência ao script AbriFechar

    private bool mover = false;
    private bool chegou = false;

    void Update()
    {
        if (mover && !chegou)
        {
            transform.position = Vector3.MoveTowards(transform.position, posicaoNaPlaca.position, velocidade * Time.deltaTime);

            // Verifica se chegou
            if (Vector3.Distance(transform.position, posicaoNaPlaca.position) < 0.01f)
            {
                chegou = true;
                transform.localScale = escalaFinal;

                // Fecha o painel usando a animação
                if (painelFerramentas != null && painelFerramentas.gameObject.activeSelf)
                {
                    painelFerramentas.Alternar();
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        mover = true; // ao clicar, começa a mover
    }
}