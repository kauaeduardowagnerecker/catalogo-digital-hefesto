using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbrirInformacoes : MonoBehaviour
{
    // Define o dicionário de índices a partir do script LeitorDeDados, associado ao objeto LeitorDeDados.
    // Retira a lista de minerais para usá-la.
    // Define informação essencial para abrir os minerais.
    public static GameObject LeitorDeDados;
    public GameObject botaoAcesso;
    public GameObject itemPai;

    // Colocar uma referência vinda do LeitorDeDados aqui

    private static LeitorDeDados1 scriptLerDados;
    private GameObject cameraMinerais;
    private Dictionary<string, int> indices;
    private LeitorDeDados1.ListaDeMinerais ListaMinerais;
    private GameObject abaMinerais;

    // Start is called before the first frame update
    void Start()
    {
        // Extrai alguns dos GameObjects necessários.
        LeitorDeDados = GameObject.Find("LeitorDeDados");
        // Remover esse find utilizando setagem dinâmica de variáveis
        scriptLerDados = LeitorDeDados.GetComponent<LeitorDeDados1>();
        cameraMinerais = scriptLerDados.cameraMain;
        indices = scriptLerDados.indicesRochasMinerais;
        ListaMinerais = scriptLerDados.ListaMinerais;
        abaMinerais = scriptLerDados.abaMinerais;

        // Adiciona a interatividade de clique.
        Button abrirPagina = botaoAcesso.GetComponent<Button>();
        abrirPagina.onClick.AddListener(Abrir);
    }

    void Abrir()
    {
        abaMinerais.SetActive(true);

        // Define uma lista de componentes para editá-los.
        TextMeshProUGUI[] textos = scriptLerDados.minInformacoes.GetComponentsInChildren<TextMeshProUGUI>();
        Debug.Log(textos.Length);
        // Guarda o índice do mineral, através do nome do GameObject e do dicionário indicesRochasMinerais.
        int indice = indices[itemPai.name];

        var listaTemp = ListaMinerais.catalogoMinerais;

        // Preenche as caixas de texto.
        textos[0].text = ListaMinerais.catalogoMinerais[indice].nomeTecnico;
        textos[1].text = ListaMinerais.catalogoMinerais[indice].formula;
        textos[4].text = ListaMinerais.catalogoMinerais[indice].nomeTecnico;
        textos[6].text = ListaMinerais.catalogoMinerais[indice].tambemConhecidoPor;
        textos[8].text = ListaMinerais.catalogoMinerais[indice].variedades;
        textos[10].text = ListaMinerais.catalogoMinerais[indice].tipo;
        textos[12].text = ListaMinerais.catalogoMinerais[indice].formula;
        textos[14].text = ListaMinerais.catalogoMinerais[indice].classe;
        textos[16].text = ListaMinerais.catalogoMinerais[indice].grupo;
        textos[20].text = ListaMinerais.catalogoMinerais[indice].cor;
        textos[22].text = ListaMinerais.catalogoMinerais[indice].brilho;
        textos[24].text = ListaMinerais.catalogoMinerais[indice].traco;
        textos[26].text = ListaMinerais.catalogoMinerais[indice].diafaneidade;
        textos[29].text = ListaMinerais.catalogoMinerais[indice].densidade;
        textos[31].text = ListaMinerais.catalogoMinerais[indice].dureza;
        textos[33].text = ListaMinerais.catalogoMinerais[indice].tenacidade;
        textos[35].text = ListaMinerais.catalogoMinerais[indice].fratura;
        textos[37].text = ListaMinerais.catalogoMinerais[indice].clivagem;
        textos[40].text = ListaMinerais.catalogoMinerais[indice].habito;
        textos[42].text = ListaMinerais.catalogoMinerais[indice].classeCristalina;

        cameraMinerais.transform.localPosition = new Vector3(-1503, cameraMinerais.transform.localPosition.y, cameraMinerais.transform.localPosition.z);
    }
}
