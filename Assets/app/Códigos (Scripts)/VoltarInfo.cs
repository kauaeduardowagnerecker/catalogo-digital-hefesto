using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoltarInfo : MonoBehaviour
{
    public Button botaoVoltar;
    public Camera cameraMinerais;
    public GameObject aba;

    void Start()
    {
        botaoVoltar.onClick.AddListener(Voltar);
    }

    void Voltar()
    {
        aba.SetActive(false);
        cameraMinerais.transform.localPosition = new Vector3(0f, cameraMinerais.transform.localPosition.y, cameraMinerais.transform.localPosition.z);
    }
}
