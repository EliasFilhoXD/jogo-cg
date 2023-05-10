using UnityEngine;
using System.Collections;

public class VineScript : MonoBehaviour
{
    public float forca = 10f; // Força a ser aplicada no objeto
    public float intervalo = 2f; // Intervalo de tempo entre as aplicações de força
    public Rigidbody rb; // Rigidbody do objeto


    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Obtém o Rigidbody do objeto
        rb.drag = 1f; // Aumenta a resistência ao movimento
        rb.angularDrag = 0.5f; // Aumenta a resistência à rotação
        rb.mass = 0.2f; // Reduz o peso do objeto
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f); // Define o centro de massa abaixo do objeto
        StartCoroutine(AddForceCoroutine()); // Inicia o coroutine AddForceCoroutine()
    }

    IEnumerator AddForceCoroutine()
    {
        while (true)
        {
            // Verifica se o objeto está à esquerda ou à direita do centro
            bool estaNaEsquerda = transform.position.x < 0f;
            
            // Cria um vetor de direção para baixo
            Vector3 direcaoVertical = Vector3.down;

            // Cria um vetor de direção para a esquerda ou para a direita, dependendo da posição atual do objeto
            Vector3 direcaoHorizontal = estaNaEsquerda ? Vector3.left : Vector3.right;

            // Calcula o vetor perpendicular que aponta para frente e para trás
            Vector3 forcaDirecao = Vector3.Cross(direcaoVertical, direcaoHorizontal);
            forcaDirecao.Normalize();

            // Aplica a força na direção combinada
            rb.AddForce(forcaDirecao * forca);

            // Espera o intervalo de tempo especificado
            yield return new WaitForSeconds(intervalo);
        }
    }
}
