using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiltroAlfabético : MonoBehaviour
{
    public LeitorDeDados1 leitorDeDados;
    public Sprite spriteAZ;
    public Sprite spriteZA;

    private Toggle toggle;
    private Image imagemToggle;
    // Start is called before the first frame update
    void Start()
    {
        toggle = gameObject.GetComponent<Toggle>();
        imagemToggle = gameObject.GetComponent<Image>();

        toggle.onValueChanged.AddListener(delegate { FiltroAlfa(); });
    }

    // Update is called once per frame
    void FiltroAlfa()
    {
        if (toggle.isOn)
        {
            FiltrarAlfabéticoAZ();
            imagemToggle.sprite = spriteZA;
        }
        else
        {
            FiltrarAlfabéticoZA();
            imagemToggle.sprite = spriteAZ;
        }
    }

    void FiltrarAlfabéticoZA()
    {
        leitorDeDados.listaDeNomesDeRochasEMinerais.Reverse();
        int i = 0;

        foreach (string nome in leitorDeDados.listaDeNomesDeRochasEMinerais)
        {
            if (leitorDeDados.botaoRenderizado[nome] && leitorDeDados.botaoDictGameObjectRochasMinerais[nome].activeSelf)
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

        i = 0;
    }

    void FiltrarAlfabéticoAZ()
    {
        leitorDeDados.listaDeNomesDeRochasEMinerais.Sort();
        int i = 0;

        foreach (string nome in leitorDeDados.listaDeNomesDeRochasEMinerais)
        {
            if (leitorDeDados.botaoRenderizado[nome] && leitorDeDados.botaoDictGameObjectRochasMinerais[nome].activeSelf)
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

        i = 0;
    }
}
