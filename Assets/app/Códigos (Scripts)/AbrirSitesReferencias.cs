using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbrirSitesReferencias : MonoBehaviour
{
    // URL do website para redirecionar
    public string url;

    // Referência ao link
    public GameObject textoHyperlink;

    void Start()
    {
        // Obtém o componente Button anexado ao GameObject
        Button botao = textoHyperlink.GetComponent<Button>();

        // Adiciona o listener ao botão para detectar o clique
        botao.onClick.AddListener(OpenURLReferencias);
    }

    // Método para abrir o URL
    void OpenURLReferencias()
    {
        // Abre o URL no navegador padrão
        Application.OpenURL(url);
    }
}
