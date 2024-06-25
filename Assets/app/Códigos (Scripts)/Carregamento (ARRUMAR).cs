using UnityEngine;
using UnityEngine.SceneManagement;

public class Carregamento : MonoBehaviour
{
    /* Start contém o código que é iniciado junto com o objeto, ou seja, assim que a interface é carregada,
     * o programa Start é executado.*/
    public string primeiraPágina;
    public string telaDeCarregamento;
    public int waitTime = 0;
    //Permite definir qual a página inicial que aparecerá após o carregamento.
    void Start()
    {
        Invoke("CarregarPágina", waitTime);
        //Invoke executa CarregarPágina após esperar waitTime em segundos.
    }
    public void CarregarPágina()
    {
        SceneManager.LoadSceneAsync(primeiraPágina);
        SceneManager.UnloadSceneAsync(telaDeCarregamento);
        //Ambos comandos Load e Unload devem ser Async para um carregamento mais suave.
    }
}
