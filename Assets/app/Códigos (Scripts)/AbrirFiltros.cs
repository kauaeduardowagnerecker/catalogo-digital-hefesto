using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbrirFiltros : MonoBehaviour
{
    public GameObject caixa;
    Toggle toggleAbrirFiltros;
    // Start is called before the first frame update
    void Start()
    {
        toggleAbrirFiltros = gameObject.GetComponent<Toggle>();

        toggleAbrirFiltros.onValueChanged.AddListener(delegate { AbrirToggle(); });
    }

    void AbrirToggle()
    {
        if (toggleAbrirFiltros.isOn)
        {
            caixa.SetActive(true);
        }
        else
        {
            {
                caixa.SetActive(false);
            }
        }
    }
}
