using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiltroAlfabetico : MonoBehaviour
{

    public LeitorDeDados1 leitorDeDados;

    private List<string> listaDeNomesRochasMinerais;
    private List<string> listaDeRochasMineraisRenderizados;
    private Dictionary<string, RectTransform> botoesRochasMinerais;

    // Start is called before the first frame update
    void Start()
    {
        Button filtro = gameObject.GetComponent<Button>();
        filtro.onClick.AddListener(FiltrarAlfabético);
    }

    // Update is called once per frame
    void FiltrarAlfabético()
    {
        listaDeRochasMineraisRenderizados = leitorDeDados.listaDeNomesBotoesRenderizados;
        botoesRochasMinerais = leitorDeDados.botoesRochasMinerais;

        listaDeRochasMineraisRenderizados.Sort();

        for (int i = 0; i < listaDeRochasMineraisRenderizados.Count; i++)
        {
            string nome = listaDeRochasMineraisRenderizados[i];
            RectTransform botao = botoesRochasMinerais[nome];

            botao.localPosition = new Vector2(botao.localPosition.x, (-i * 230) - 114.5f);

            Debug.Log("Working");
        }
    }
}
