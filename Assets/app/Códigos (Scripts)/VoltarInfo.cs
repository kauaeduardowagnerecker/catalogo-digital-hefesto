using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoltarInfo : MonoBehaviour
{
    public Button botaoVoltar;
    public Camera cameraMinerais;
    public GameObject abaDosMinerais;

    void Start()
    {
        botaoVoltar.onClick.AddListener(Voltar);
    }

    void Voltar()
    {
        abaDosMinerais.SetActive(false);
        cameraMinerais.transform.localPosition = new Vector3(0, cameraMinerais.transform.localPosition.y, cameraMinerais.transform.localPosition.z);
    }
}
