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
    public Animator animadorMenu;
    public float coordenadaX;
    public GameObject mainCamera;

    void Start()
    {
        Button trocador = botao.GetComponent<Button>();
        trocador.onClick.AddListener(TrocaDePagina);
    }

    void TrocaDePagina()
    {
        if (paginaAlvo != paginaAtual)
        {
            paginaAlvo.SetActive(true);
            paginaAtual.SetActive(false);
        }
        else
        {
            animadorMenu.Play("FecharMenu");
        }
    }
}
