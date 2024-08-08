using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbrirBarraDePesquisa : MonoBehaviour
{
    // Assign these in the Inspector
    public GameObject barraDePesquisa;
    public GameObject filtroAlfab�tico;
    public GameObject filtroGeral;

    // Reference to the Toggle component
    private Toggle toggle;

    void Start()
    {
        toggle = gameObject.GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        SetElementsActive(isOn);
    }

    private void SetElementsActive(bool isActive)
    {
        barraDePesquisa.SetActive(isActive);

        // Manage the states of filtroAZ, filtroZA, and filtroGeral
        if (isActive)
        {
            filtroAlfab�tico.SetActive(false);
            filtroGeral.SetActive(false);
        }
        else
        {
            filtroAlfab�tico.SetActive(true);
            filtroGeral.SetActive(true);
        }
    }
}
