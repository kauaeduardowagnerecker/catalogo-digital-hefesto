using UnityEngine;
using UnityEngine.SceneManagement;

public class Carregamento : MonoBehaviour
{
    /* Start cont�m o c�digo que � iniciado junto com o objeto, ou seja, assim que a interface � carregada,
     * o programa Start � executado.*/
    public string primeiraP�gina;
    public string telaDeCarregamento;
    public int waitTime = 0;
    //Permite definir qual a p�gina inicial que aparecer� ap�s o carregamento.
    void Start()
    {
        Invoke("CarregarP�gina", waitTime);
        //Invoke executa CarregarP�gina ap�s esperar waitTime em segundos.
    }
    public void CarregarP�gina()
    {
        SceneManager.LoadSceneAsync(primeiraP�gina);
        SceneManager.UnloadSceneAsync(telaDeCarregamento);
        //Ambos comandos Load e Unload devem ser Async para um carregamento mais suave.
    }
}
