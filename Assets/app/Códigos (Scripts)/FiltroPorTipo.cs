using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FiltroPorTipo : MonoBehaviour
{

    public LeitorDeDados1 LeitorDeDados;
    public string tipo;
    public Toggle filtro1;
    public Toggle filtro2;
    public Toggle filtro3;
    public GameObject barraDePesquisa;
    public Pesquisa scriptPesquisa;

    private Toggle filtro;
    private TMP_InputField inputPesquisa;

    void OnEnable()
    {
        filtro = gameObject.GetComponent<Toggle>();
        inputPesquisa = barraDePesquisa.GetComponent<TMP_InputField>();
    }

    public void Filtro(string tipo)
    {
        if (filtro.isOn)
        {
            FiltrarPorTipo(tipo);
            DesativarEReposicionarBotões();
            scriptPesquisa.Pesquisar(inputPesquisa.text);
        }
        else
        {
            ReverterFiltro(tipo);
            DesativarEReposicionarBotões();
            scriptPesquisa.Pesquisar(inputPesquisa.text);
        }
    }

    public void FiltrarPorTipo(string tipo)
    {
        Debug.Log("A");
        int quantidade_renderizada = 0;

        foreach (string key in LeitorDeDados.botaoRenderizado.Keys.ToList<string>())
        {
            if (LeitorDeDados.botaoRenderizado[key])
            {
                quantidade_renderizada = quantidade_renderizada + 1;
            }
        }
        Debug.Log(quantidade_renderizada);
        if (quantidade_renderizada == LeitorDeDados.listaDeNomesDeRochasEMinerais.Count)
        {
            Debug.Log("Condição satisfeita");
            foreach (string name in LeitorDeDados.botaoRenderizado.Keys.ToList<string>())
            {
                LeitorDeDados.botaoRenderizado[name] = (LeitorDeDados.tiposRochasMinerais[name] == tipo);
                Debug.Log(LeitorDeDados.botaoRenderizado[name] + tipo);
            }
        }

        else
        {
            foreach (string name in LeitorDeDados.botaoRenderizado.Keys.ToList<string>())
            {
                if (LeitorDeDados.tiposRochasMinerais[name] == tipo)
                {
                    LeitorDeDados.botaoRenderizado[name] = true;
                }
            }
        }
    }
    
    public void DesativarEReposicionarBotões()
    {
        int i = 0;
        foreach (string key in LeitorDeDados.botaoRenderizado.Keys.ToList<string>())
        {
            if (LeitorDeDados.botaoRenderizado[key])
            {
                LeitorDeDados.botaoDictGameObjectRochasMinerais[key].SetActive(true);

                RectTransform rectBotao = LeitorDeDados.botoesRochasMinerais[key];
                rectBotao.localPosition = new Vector3(rectBotao.localPosition.x, (-i * 230) - 114.5f);

                i++;
                Debug.Log("Ativado");
            }
            else
            {
                Debug.Log("Desativado");
                LeitorDeDados.botaoDictGameObjectRochasMinerais[key].SetActive(false);
            }
        }
    }

    public void ReverterFiltro(string tipo)
    {
        bool result = (!filtro.isOn && !filtro1.isOn && !filtro2.isOn && !filtro3.isOn);

        if (result)
        {
            Debug.Log("A");
            foreach (string name in LeitorDeDados.listaDeNomesDeRochasEMinerais)
            {
                LeitorDeDados.botaoRenderizado[name] = true;
            }
        }
        else
        {
            foreach (string name in LeitorDeDados.listaDeNomesDeRochasEMinerais)
            {
                if (LeitorDeDados.tiposRochasMinerais[name] == tipo)
                {
                    LeitorDeDados.botaoRenderizado[name] = !(tipo == LeitorDeDados.tiposRochasMinerais[name]);
                }
            }
        }
    }
}
