using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrocarPagina : MonoBehaviour
{
    public GameObject paginaAlvo;
    public GameObject paginaAtual;
    public GameObject botao;
    public GameObject menu;
    public float coordenadaX;
    public GameObject mainCamera;

    private Animator animadorMenu;

    void Start()
    {
        animadorMenu = menu.GetComponent<Animator>();
        Button trocador = botao.GetComponent<Button>();
        trocador.onClick.AddListener(TrocaDePagina);
    }

    void TrocaDePagina()
    {
        if (paginaAlvo != paginaAtual)
        {
            paginaAlvo.SetActive(true);
            mainCamera.transform.localPosition = new Vector3(coordenadaX, mainCamera.transform.localPosition.y, mainCamera.transform.localPosition.z);
            menu.transform.Translate(new Vector3(372, 0, 0));
            paginaAtual.SetActive(false);
        }
        else
        {
            animadorMenu.Play("FecharMenu");
        }
    }
}
