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
            string lowercaseName = name.ToLower();
            string lowercaseInput = input.ToLower();

            RectTransform rectBotao = LeitorDeDados.botoesRochasMinerais[name];

            if (LeitorDeDados.botaoRenderizado[name] && lowercaseName.Contains(lowercaseInput))
            {
                LeitorDeDados.botaoDictGameObjectRochasMinerais[name].SetActive(true);

                i++;
            }
            else
            {
                LeitorDeDados.botaoDictGameObjectRochasMinerais[name].SetActive(false);
            }
        }
        float offset = 235f;
        float tamanhoEscalonado = 235f * i + offset;
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
}
