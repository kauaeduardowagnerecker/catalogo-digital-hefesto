using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FiltroPorTipo : MonoBehaviour
{

    public LeitorDeDados1 LeitorDeDados;
    private Dictionary<string, bool> botaoRenderizado;
    private Dictionary<string, string> tiposRochasMinerais;
    private Dictionary<string, GameObject> botoesGameObjectRochasMinerais;
    private Dictionary<string, RectTransform> botoesRectsRochasMinerais;
    private List<string> nomesBotoesRochasMineraisRenderizados;

    // Start is called before the first frame update
    void Start()
    {
        botaoRenderizado = LeitorDeDados.botaoRenderizado;
        tiposRochasMinerais = LeitorDeDados.tiposRochasMinerais;
        nomesBotoesRochasMineraisRenderizados = LeitorDeDados.listaDeNomesBotoesRenderizados;
        botoesGameObjectRochasMinerais = LeitorDeDados.botaoDictGameObjectRochasMinerais;
        botoesRectsRochasMinerais = LeitorDeDados.botoesRochasMinerais;
    }

    void FiltrarPorTipo(string tipo)
    {
        foreach (string name in nomesBotoesRochasMineraisRenderizados)
        {
            botaoRenderizado[name] = (tiposRochasMinerais[name] != tipo);
        }
    }
    
    void DesativarEReposicionarFiltroTipo()
    {
        int i = 0;
        foreach (string key in nomesBotoesRochasMineraisRenderizados)
        {
            if (botaoRenderizado[key])
            {
                botoesGameObjectRochasMinerais[key].SetActive(true);

                RectTransform rectBotao = botoesRectsRochasMinerais[key];
                rectBotao.localPosition = new Vector3(rectBotao.localPosition.x, (-i * 230) - 114.5f);

                i++;
            }
            else
            {
                botoesGameObjectRochasMinerais[key].SetActive(false);
            }
        }
    }
}
