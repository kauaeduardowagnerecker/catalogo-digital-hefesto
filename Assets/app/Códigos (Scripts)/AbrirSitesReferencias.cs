using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbrirSitesReferencias : MonoBehaviour
{
    // URL do website para redirecionar
    public string url;

    // Refer�ncia ao �cone
    public Button �cone;

    void Start()
    {
        // Obt�m o componente Button anexado ao GameObject
        Button botao = �cone.GetComponent<Button>();

        // Verifica se o componente Button est� presente
        if (botao != null)
        {
            // Adiciona o listener ao bot�o para detectar o clique
            botao.onClick.AddListener(OpenURLReferencias);
        }
        else
        {
            Debug.LogError("O componente Button n�o est� presente no GameObject.");
        }
    }

    // M�todo para abrir o URL
    void OpenURLReferencias()
    {
        // Abre o URL no navegador padr�o
        Application.OpenURL(url);
    }
}
