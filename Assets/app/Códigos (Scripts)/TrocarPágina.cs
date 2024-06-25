using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrocarPagina : MonoBehaviour
{
    public string paginaAlvo;
    public string paginaAtual;
    public GameObject botao;
    public Animator animadorMenu;
    public Animator animadorCamera;

    void Start()
    {
        Button trocador = botao.GetComponent<Button>();
        trocador.onClick.AddListener(TrocaDePagina);
    }

    void TrocaDePagina()
    {
        if (paginaAlvo != paginaAtual)
        {
            SceneManager.LoadSceneAsync(paginaAlvo);
            SceneManager.UnloadSceneAsync(paginaAtual);
        }
        else
        {
            animadorMenu.Play("FecharMenu");
            animadorCamera.Play("ClarearFundo");
        }
    }
}
