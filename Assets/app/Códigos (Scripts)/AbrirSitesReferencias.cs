using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbrirSitesReferencias : MonoBehaviour
{
    // URL do website para redirecionar
    public string url;

    // Refer�ncia ao link
    public GameObject textoHyperlink;

    void Start()
    {
        // Obt�m o componente Button anexado ao GameObject
        Button botao = textoHyperlink.GetComponent<Button>();

        // Adiciona o listener ao bot�o para detectar o clique
        botao.onClick.AddListener(OpenURLReferencias);
    }

    // M�todo para abrir o URL
    void OpenURLReferencias()
    {
        // Abre o URL no navegador padr�o
        Application.OpenURL(url);
    }
}
