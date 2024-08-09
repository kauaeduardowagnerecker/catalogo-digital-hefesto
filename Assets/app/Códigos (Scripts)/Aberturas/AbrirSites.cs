using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbrirSites : MonoBehaviour
{
    // URL do website para redirecionar
    public string url;

    void Start()
    {
        // Obtém o componente Button anexado ao GameObject
        Button botao = gameObject.GetComponent<Button>();

        // Verifica se o componente Button está presente
        if (botao != null)
        {
            // Adiciona o listener ao botão para detectar o clique
            botao.onClick.AddListener(OpenURL);
        }
        else
        {
            Debug.LogError("O componente Button não está presente no GameObject.");
        }
    }

    // Método para abrir o URL
    void OpenURL()
    {
        // Abre o URL no navegador padrão
        Application.OpenURL(url);
    }
}
