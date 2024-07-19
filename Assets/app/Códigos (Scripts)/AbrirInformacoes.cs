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
    private GameObject cameraPrincipal;
    private Dictionary<string, int> indices;
    private Dictionary<string, string> tipos;
    private LeitorDeDados1.ListaDeMinerais ListaMinerais;
    private LeitorDeDados1.ListaDeIgneas ListaIgneas;
    private GameObject abaMinerais;
    private GameObject abaIgneas;

    // Start is called before the first frame update
    void Start()
    {
        LeitorDeDados = GameObject.Find("LeitorDeDados");
        // Remover esse find utilizando setagem dinâmica de variáveis

        // Referências essenciais.
        scriptLerDados = LeitorDeDados.GetComponent<LeitorDeDados1>();

        // Camera
        cameraPrincipal = scriptLerDados.cameraMain;

        // Dicionários
        indices = scriptLerDados.indicesRochasMinerais;
        tipos = scriptLerDados.tiposRochasMinerais;

        // Itens dos minerais
        ListaMinerais = scriptLerDados.ListaMinerais;
        abaMinerais = scriptLerDados.abaMinerais;

        // Itens das ígneas
        ListaIgneas = scriptLerDados.ListaIgneas;
        abaIgneas = scriptLerDados.abaIgneas;

        // Adiciona a interatividade de clique.
        Button abrirPagina = botaoAcesso.GetComponent<Button>();
        abrirPagina.onClick.AddListener(Abrir);
    }

    void Abrir()
    {
        string type = tipos[itemPai.name];
        TextMeshProUGUI[] textos;
        int indice;

        switch (type)
        {
            case "Mineral": 
                abaMinerais.SetActive(true);
                textos = scriptLerDados.minInformacoes.GetComponentsInChildren<TextMeshProUGUI>();
                indice = indices[itemPai.name];

                break;
            case "Ígnea":
                abaIgneas.SetActive(true);
                textos = scriptLerDados.igneasInformacoes.GetComponentsInChildren<TextMeshProUGUI>();
                break;
        }

        // Define uma lista de componentes para editá-los.
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

        cameraPrincipal.transform.localPosition = new Vector3(-1503, cameraPrincipal.transform.localPosition.y, cameraPrincipal.transform.localPosition.z);
    }
}
