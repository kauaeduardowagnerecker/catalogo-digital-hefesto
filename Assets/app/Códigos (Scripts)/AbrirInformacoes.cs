using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

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
    private GameObject contentMinerais;
    private GameObject contentIgneas;

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
        contentMinerais = scriptLerDados.minContent;

        // Itens das ígneas
        ListaIgneas = scriptLerDados.ListaIgneas;
        abaIgneas = scriptLerDados.abaIgneas;
        contentIgneas = scriptLerDados.igneasContent;

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
                textos[44].text = ListaMinerais.catalogoMinerais[indice].outrasInfo;
                textos[46].text = ListaMinerais.catalogoMinerais[indice].rochaCotidiano;
                textos[48].text = ListaMinerais.catalogoMinerais[indice].curiosidades;
                textos[50].text = ListaMinerais.catalogoMinerais[indice].doacao;
                textos[53].text = ListaMinerais.catalogoMinerais[indice].museuhe;
                textos[55].text = ListaMinerais.catalogoMinerais[indice].uspgeociencias;
                textos[57].text = ListaMinerais.catalogoMinerais[indice].minmicro;
                textos[59].text = ListaMinerais.catalogoMinerais[indice].wikipedia;
                textos[61].text = ListaMinerais.catalogoMinerais[indice].outro;

                for (int i = 53; i <= 61; i = i + 2)
                {
                    if (textos[i].text == "-")
                    {
                        textos[i].color = new Color(7f / 255f, 0f / 255f, 0f / 255f, 255f / 255f);
                        textos[i].fontStyle = FontStyles.Normal;
                    }
                    else
                    {
                        textos[i].color = Color.blue;
                        textos[i].fontStyle = FontStyles.Underline;
                    }
                }

                AjustarTextos(scriptLerDados.minInformacoes);
                AdjustSizeScrollview(contentMinerais, scriptLerDados.minInformacoes);

                cameraPrincipal.transform.localPosition = new Vector3(-1501f, cameraPrincipal.transform.localPosition.y, cameraPrincipal.transform.localPosition.z);
                break;

            case "Ígnea":

                abaIgneas.SetActive(true);
                textos = scriptLerDados.igneasInformacoes.GetComponentsInChildren<TextMeshProUGUI>();
                indice = indices[itemPai.name];
                var rochaIgnea = ListaIgneas.catalogoIgneas[indice];

                textos[0].text = rochaIgnea.nomeTecnico;
                textos[1].text = rochaIgnea.variacao;
                textos[4].text = rochaIgnea.nomeTecnico;
                textos[6].text = rochaIgnea.tambemConhecidoPor;
                textos[8].text = rochaIgnea.variedades;
                textos[10].text = rochaIgnea.variacao;
                textos[12].text = rochaIgnea.familia;
                textos[14].text = rochaIgnea.magmaOriginario;
                textos[16].text = rochaIgnea.comoForma;
                textos[18].text = rochaIgnea.rochaCotidiano;
                textos[20].text = rochaIgnea.curiosidades;
                textos[22].text = rochaIgnea.doacao;
                textos[25].text = rochaIgnea.museuhe;
                textos[27].text = rochaIgnea.uspgeociencias;
                textos[29].text = rochaIgnea.wikipedia;
                textos[31].text = rochaIgnea.outro;

                for (int i = 25; i <= 31; i = i + 2)
                {
                    if (textos[i].text == "-")
                    {
                        textos[i].color = new Color(7f, 0f, 0f, 255f);
                        textos[i].fontStyle = FontStyles.Normal;
                    }
                    else
                    {
                        textos[i].color = Color.blue;
                        textos[i].fontStyle = FontStyles.Underline;
                    }
                }

                AdjustSizeScrollview(contentIgneas, scriptLerDados.prefabIgneas);
                cameraPrincipal.transform.localPosition = new Vector3(-2940f, cameraPrincipal.transform.localPosition.y, cameraPrincipal.transform.localPosition.z);
                break;
        }
    }

    void AdjustSizeScrollview(GameObject content, GameObject modelo)
    {
        int index = 0;
        string type = tipos[itemPai.name];

        index = modelo.transform.childCount - 1;

        RectTransform contentRect = content.GetComponent<RectTransform>();
        float top = contentRect.localPosition.y;

        Transform ultimoObjetoModelo = modelo.transform.GetChild(index);
        float bottom = ultimoObjetoModelo.localPosition.y;

        float distance = top - bottom;

        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, distance);

    }

    void AjustarTextos(GameObject informacoes)
    {
        int espacamentoVertical = 4;
        List<RectTransform> listaObjetos = new List<RectTransform>();
        
        foreach (RectTransform child in informacoes.GetComponent<RectTransform>())
        {
            listaObjetos.Add(child);
        }

        TMP_Text[] textos = informacoes.GetComponentsInChildren<TMP_Text>();

        for (int i = 6; i < listaObjetos.Count; i++)
        {
            RectTransform objetoAnterior = listaObjetos[i - 1];
            AjustarRectParaTexto(objetoAnterior);
            Vector3 posiçãoAnterior = objetoAnterior.localPosition;
            float posiçãoAnteriorY = posiçãoAnterior.y;
            float alturaAnterior = objetoAnterior.rect.height;
            float limiteAnterior = posiçãoAnteriorY - alturaAnterior;

            RectTransform objetoAtual = listaObjetos[i];
            AjustarRectParaTexto(objetoAtual);
            Vector3 posiçãoAtual = objetoAtual.localPosition;
            float posiçãoAtualY = posiçãoAtual.y;
            float alturaAtual = objetoAtual.rect.height;
            float larguraAtual = objetoAtual.rect.width;

            float posiçãoNovaY = posiçãoAnteriorY - (alturaAnterior / 2) - espacamentoVertical - (alturaAtual/2);
            float posiçãoNovaX = (larguraAtual / 2) - 199.41f;
            objetoAtual.localPosition = new Vector3(posiçãoNovaX, posiçãoNovaY, posiçãoAtual.z);
        }
    }

    void AjustarRectParaTexto(RectTransform text)
    {
        TMP_Text textoReal = text.GetComponent<TMP_Text>();
        if (textoReal != null)
        {
            RectTransform rectTexto = text.GetComponent<RectTransform>();

            textoReal.ForceMeshUpdate();

            Vector2 textSize = textoReal.GetRenderedValues(false);
            
            text.sizeDelta = new Vector2(text.sizeDelta.x, textSize.y);
        }
    }
}
