using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbrirSitesReferencias : MonoBehaviour
{
    void Start()
    {
        // Obtém o componente Button anexado ao GameObject
        Button botao = gameObject.GetComponent<Button>();

        // Adiciona o listener ao botão para detectar o clique
        botao.onClick.AddListener(OpenURLReferencias);
    }

    // Método para abrir o URL
    void OpenURLReferencias()
    {
        TextMeshProUGUI textoHyperlink = gameObject.GetComponent<TextMeshProUGUI>();

        if (textoHyperlink.text != "-")
        {
            Application.OpenURL(textoHyperlink.text);
        }
    }
}
