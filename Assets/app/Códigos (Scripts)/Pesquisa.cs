using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Pesquisa : MonoBehaviour
{
    public LeitorDeDados1 LeitorDeDados;
    public FiltroPorTipo scriptFiltros;
    public GameObject barraDePesquisa;

    private TMP_InputField inputPesquisa;
    // Start is called before the first frame update
    void Start()
    {
        inputPesquisa = gameObject.GetComponent<TMP_InputField>();
        inputPesquisa.onValueChanged.AddListener(delegate { Pesquisar(inputPesquisa.text); });
    }

    // Update is called once per frame
    public void Pesquisar(string input)
    {
        int i = 0;
        RectTransform rectContainerDosItens = LeitorDeDados.containerDosItens.GetComponent<RectTransform>();

        foreach (string name in LeitorDeDados.listaDeNomesDeRochasEMinerais)
        {
            string processedName = RemoverAcentua��o(name.ToLower());
            string processedInput = RemoverAcentua��o(input.ToLower());

            RectTransform rectBotao = LeitorDeDados.botoesRochasMinerais[name];

            if (LeitorDeDados.botaoRenderizado[name] && processedName.Contains(processedInput))
            {
                LeitorDeDados.botaoDictGameObjectRochasMinerais[name].SetActive(true);

                i++;
            }
            else
            {
                LeitorDeDados.botaoDictGameObjectRochasMinerais[name].SetActive(false);
            }
        }
        float tamanhoEscalonado = (226f * i) - 200f;
        rectContainerDosItens.sizeDelta = new Vector2(rectContainerDosItens.sizeDelta.x, tamanhoEscalonado);
        
        int z = 0;
        foreach (string name in LeitorDeDados.listaDeNomesDeRochasEMinerais)
        {
            if (LeitorDeDados.botaoDictGameObjectRochasMinerais[name].activeSelf)
            {
                RectTransform rectBotao = LeitorDeDados.botoesRochasMinerais[name];
                rectBotao.localPosition = new Vector3(rectBotao.localPosition.x, (-z * 230) - 114.5f, rectBotao.localPosition.z);
                z++;
            }
        }
    }

    string RemoverAcentua��o(string input)
    {
        string correctedInput = "";

        if (input == null)
        {
            return null;
        }

        Dictionary<char, char> AccentMap = new Dictionary<char, char>
        {
        {'�', 'a'}, {'�', 'a'}, {'�', 'a'}, {'�', 'a'}, {'�', 'a'}, {'�', 'a'},
        {'�', 'e'}, {'�', 'e'}, {'�', 'e'}, {'�', 'e'},
        {'�', 'i'}, {'�', 'i'}, {'�', 'i'}, {'�', 'i'},
        {'�', 'o'}, {'�', 'o'}, {'�', 'o'}, {'�', 'o'}, {'�', 'o'},
        {'�', 'u'}, {'�', 'u'}, {'�', 'u'}, {'�', 'u'},
        {'�', 'c'},
        {'�', 'n'}
        };

        HashSet<char> keysSet = AccentMap.Keys.ToHashSet();

        for (int i = 0; i < input.Length; i++)
        {
            if (keysSet.Contains(input[i]))
            {
                correctedInput += AccentMap[input[i]];
            }
            else
            {
                correctedInput += input[i];
            }
        }

        return correctedInput;
}
}
