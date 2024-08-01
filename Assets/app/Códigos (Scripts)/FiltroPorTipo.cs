using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FiltroPorTipo : MonoBehaviour
{

    public LeitorDeDados1 LeitorDeDados;
    public string tipo;
    public Toggle filtro1;
    public Toggle filtro2;
    public Toggle filtro3;
    public InputField inputField;
    public Pesquisa scriptPesquisa;

    private Toggle filtro;

    // Start is called before the first frame update
    void Start()
    {
        filtro = gameObject.GetComponent<Toggle>();

        filtro.onValueChanged.AddListener(delegate { Filtro(); });
    }

    void Filtro()
    {
        if (filtro.isOn)
        {
            FiltrarPorTipo(tipo);
        }
        else
        {
            ReverterFiltro(tipo);
        }

        scriptPesquisa.Pesquisar(inputField.text);
        DesativarEReposicionarBotões();
    }

    void FiltrarPorTipo(string tipo)
    {
        int quantidade_renderizada = 0;

        foreach (string key in LeitorDeDados.botaoRenderizado.Keys.ToList<string>())
        {
            if (LeitorDeDados.botaoRenderizado[key])
            {
                quantidade_renderizada = quantidade_renderizada + 1;
            }
        }

        if (quantidade_renderizada == LeitorDeDados.botoesRochasMinerais.Count)
        {
            foreach (string name in LeitorDeDados.listaDeNomesBotoesRenderizados)
            {
                LeitorDeDados.botaoRenderizado[name] = (LeitorDeDados.tiposRochasMinerais[name] == tipo);
            }
        }

        else
        {
            foreach (string name in LeitorDeDados.listaDeNomesBotoesRenderizados)
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
            }
            else
            {
                LeitorDeDados.botaoDictGameObjectRochasMinerais[key].SetActive(false);
            }
        }
    }

    void ReverterFiltro(string tipo)
    {
        bool result = (filtro.isOn && !filtro1.isOn && !filtro2.isOn && !filtro3.isOn) ||
              (!filtro.isOn && filtro1.isOn && !filtro2.isOn && !filtro3.isOn) ||
              (!filtro.isOn && !filtro1.isOn && filtro2.isOn && !filtro3.isOn) ||
              (!filtro.isOn && !filtro1.isOn && !filtro2.isOn && filtro3.isOn);

        if (result)
        {
            foreach (string name in LeitorDeDados.listaDeNomesBotoesRenderizados)
            {
                LeitorDeDados.botaoRenderizado[name] = true;
            }
        }
        else
        {
            foreach (string name in LeitorDeDados.listaDeNomesBotoesRenderizados)
            {
                LeitorDeDados.botaoRenderizado[name] = (tipo == LeitorDeDados.tiposRochasMinerais[name]);
            }
        }
    }
}
