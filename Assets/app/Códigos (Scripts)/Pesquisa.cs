using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pesquisa : MonoBehaviour
{
    public LeitorDeDados1 LeitorDeDados;
    private List<string> listaDeBotoesRenderizados;
    public Dictionary<string, bool> dictBotaoRenderizado;
    private Dictionary<string, GameObject> botoesGameObjectDict;
    private Dictionary<string, RectTransform> botoesRectDict;

    public FiltroPorTipo scriptFiltros;
    // Start is called before the first frame update
    void Start()
    {
        listaDeBotoesRenderizados = LeitorDeDados.listaDeNomesBotoesRenderizados;
        botoesGameObjectDict = LeitorDeDados.botaoDictGameObjectRochasMinerais;
        botoesRectDict = LeitorDeDados.botoesRochasMinerais;
        dictBotaoRenderizado = LeitorDeDados.botaoRenderizado;
    }

    // Update is called once per frame
    public void Pesquisar(string input)
    {
        foreach (string name in listaDeBotoesRenderizados)
        {
            string lowercaseName = name.ToLower();
            string lowercaseInput = input.ToLower();
            dictBotaoRenderizado[name] = lowercaseName.Contains(lowercaseInput);
            scriptFiltros.DesativarEReposicionarBotões();
        }
    }
}
