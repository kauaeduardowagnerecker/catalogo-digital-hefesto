using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiltroAlfabéticoDescendente : MonoBehaviour
{
    Button filtro;
    public LeitorDeDados1 leitorDeDados;
    public GameObject botaoZA;

    // Start is called before the first frame update
    void Start()
    {
        filtro = this.gameObject.GetComponent<Button>();
        filtro.onClick.AddListener(FiltrarAlfabéticoDescendente);
    }

    // Update is called once per frame
    void FiltrarAlfabéticoDescendente()
    {
        leitorDeDados.listaDeNomesDeRochasEMinerais.Sort();
        int i = 0;

        foreach (string nome in leitorDeDados.listaDeNomesDeRochasEMinerais)
        {
            if (leitorDeDados.botaoRenderizado[nome])
            {
                RectTransform botao = leitorDeDados.botoesRochasMinerais[nome];
                botao.localPosition = new Vector2(botao.localPosition.x, (-i * 230) - 114.5f);
                i++;
            }
            else
            {
                continue;
            }
        }

        botaoZA.SetActive(true);
        i = 0;
        gameObject.SetActive(false);
    }
}
