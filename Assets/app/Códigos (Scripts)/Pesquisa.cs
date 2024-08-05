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
        foreach (string name in LeitorDeDados.listaDeNomesDeRochasEMinerais)
        {
            string lowercaseName = name.ToLower();
            string lowercaseInput = input.ToLower();
            if (LeitorDeDados.botaoRenderizado[name] && lowercaseName.Contains(lowercaseInput))
            {
                LeitorDeDados.botaoDictGameObjectRochasMinerais[name].SetActive(true);

                RectTransform rectBotao = LeitorDeDados.botoesRochasMinerais[name];
                rectBotao.localPosition = new Vector3(rectBotao.localPosition.x, (-i * 230) - 114.5f);

                i++;
            }
            else
            {
                LeitorDeDados.botaoDictGameObjectRochasMinerais[name].SetActive(false);
            }
        }
    }
}
