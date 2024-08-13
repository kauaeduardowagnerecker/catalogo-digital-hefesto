using JetBrains.Annotations;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static LeitorDeDados1;
using Unity.VisualScripting;

public class AbrirInformacoes : MonoBehaviour
{
    // Define o dicion�rio de �ndices a partir do script LeitorDeDados, associado ao objeto LeitorDeDados.
    // Retira a lista de minerais para us�-la.
    // Define informa��o essencial para abrir os minerais.
    public static GameObject LeitorDeDados;
    public GameObject botaoAcesso;
    public GameObject itemPai;

    // Colocar uma refer�ncia vinda do LeitorDeDados aqui

    private static LeitorDeDados1 scriptLerDados;
    private GameObject cameraPrincipal;
    private Dictionary<string, int> indices;
    private Dictionary<string, string> tipos;
    private ListaDeMinerais ListaMinerais;
    private ListaDeIgneas ListaIgneas;
    private ListaDeSedimentares ListaSedimentares;
    private ListaDeMetamorficas ListaMetam�rficas;
    private GameObject abaMinerais;
    private GameObject abaIgneas;
    private GameObject abaSedimentares;
    private GameObject abaMetam�rficas;
    private GameObject contentMinerais;
    private GameObject contentIgneas;
    private GameObject contentSedimentares;
    private GameObject contentMetam�rficas;
    private GameObject informacoesMinerais;
    private GameObject informacoesIgneas;
    private GameObject informacoesSedimentares;
    private GameObject informacoesMetam�rficas;

    // Start is called before the first frame update
    void Start()
    {
        LeitorDeDados = GameObject.Find("LeitorDeDados");
        // Remover esse find utilizando setagem din�mica de vari�veis

        // Refer�ncias essenciais.
        scriptLerDados = LeitorDeDados.GetComponent<LeitorDeDados1>();

        // Camera
        cameraPrincipal = scriptLerDados.cameraMain;

        // Dicion�rios
        indices = scriptLerDados.indicesRochasMinerais;
        tipos = scriptLerDados.tiposRochasMinerais;

        // Itens dos minerais
        ListaMinerais = scriptLerDados.ListaMinerais;
        abaMinerais = scriptLerDados.abaMinerais;
        contentMinerais = scriptLerDados.minContent;
        informacoesMinerais = scriptLerDados.minInformacoes;

        // Itens das �gneas
        ListaIgneas = scriptLerDados.ListaIgneas;
        abaIgneas = scriptLerDados.abaIgneas;
        contentIgneas = scriptLerDados.igneasContent;
        informacoesIgneas = scriptLerDados.igneasInformacoes;

        // Itens das sedimentares
        ListaSedimentares = scriptLerDados.ListaSedimentares;
        abaSedimentares = scriptLerDados.abaSedimentares;
        contentSedimentares = scriptLerDados.sedimentaresContent;
        informacoesSedimentares = scriptLerDados.sedimentaresInformacoes;

        // Itens das metam�rficas

        ListaMetam�rficas = scriptLerDados.ListaMetamorficas;
        abaMetam�rficas = scriptLerDados.abaMetamorficas;
        contentMetam�rficas = scriptLerDados.metamorficasContent;
        informacoesMetam�rficas = scriptLerDados.metamorficasInformacoes;

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
                textos = informacoesMinerais.GetComponentsInChildren<TextMeshProUGUI>();
                indice = indices[itemPai.name];
                Mineral mineral = ListaMinerais.catalogoMinerais[indice];

                Transform foto4 = informacoesMinerais.transform.GetChild(3);
                Image imagem4 = foto4.GetComponent<Image>();

                // Preenche as caixas de texto.
                textos[0].text = mineral.nomeTecnico;
                textos[1].text = mineral.formula;
                textos[4].text = mineral.nomeTecnico;
                textos[6].text = mineral.tambemConhecidoPor;
                textos[8].text = mineral.variedades;
                textos[10].text = mineral.tipo;
                textos[12].text = mineral.formula;
                textos[14].text = mineral.classe;
                textos[16].text = mineral.grupo;
                textos[20].text = mineral.cor;
                textos[22].text = mineral.brilho;
                textos[24].text = mineral.traco;
                textos[26].text = mineral.diafaneidade;
                textos[29].text = mineral.densidade;
                textos[31].text = mineral.dureza;
                textos[33].text = mineral.tenacidade;
                textos[35].text = mineral.fratura;
                textos[37].text = mineral.clivagem;
                textos[40].text = mineral.habito;
                textos[42].text = mineral.classeCristalina;
                textos[44].text = mineral.outrasInfo;
                textos[46].text = mineral.rochaCotidiano;
                textos[48].text = mineral.curiosidades;
                textos[50].text = mineral.doacao;
                textos[53].text = mineral.museuhe;
                textos[55].text = mineral.uspgeociencias;
                textos[57].text = mineral.minmicro;
                textos[59].text = mineral.outro;

                for (int i = 53; i <= 59; i = i + 2)
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

                imagem4.sprite = Resources.Load<Sprite>(mineral.nomeTecnico);

                AjustarTextos(informacoesMinerais, contentMinerais, "Mineral");
                AjustarScrollView(contentMinerais, informacoesMinerais);

                cameraPrincipal.transform.localPosition = new Vector3(-2000f, cameraPrincipal.transform.localPosition.y, cameraPrincipal.transform.localPosition.z);
                break;

            case "�gnea":

                abaIgneas.SetActive(true);
                textos = informacoesIgneas.GetComponentsInChildren<TextMeshProUGUI>();
                indice = indices[itemPai.name];
                RochaIgnea rochaIgnea = ListaIgneas.catalogoIgneas[indice];

                Transform foto3 = informacoesIgneas.transform.GetChild(3);
                Image imagem3 = foto3.GetComponent<Image>();

                textos[0].text = rochaIgnea.nomeTecnico;
                textos[1].text = rochaIgnea.variacao;
                textos[4].text = rochaIgnea.nomeTecnico;
                textos[6].text = rochaIgnea.tambemConhecidoPor;
                textos[8].text = rochaIgnea.correspondenteExtrusivoIntrusivo;
                textos[10].text = rochaIgnea.variacao;
                textos[12].text = rochaIgnea.familia;
                textos[14].text = rochaIgnea.magmaOriginario;
                textos[16].text = rochaIgnea.comoForma;
                textos[18].text = rochaIgnea.rochaCotidiano;
                textos[20].text = rochaIgnea.curiosidades;
                textos[22].text = rochaIgnea.doacao;
                textos[25].text = rochaIgnea.museuhe;
                textos[27].text = rochaIgnea.uspgeociencias;
                textos[29].text = rochaIgnea.outro;

                for (int i = 25; i <= 29; i = i + 2)
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

                imagem3.sprite = Resources.Load<Sprite>(rochaIgnea.nomeTecnico);

                AjustarTextos(informacoesIgneas, contentIgneas, "�gnea");
                AjustarScrollView(contentIgneas, informacoesIgneas);

                cameraPrincipal.transform.localPosition = new Vector3(-4000f, cameraPrincipal.transform.localPosition.y, cameraPrincipal.transform.localPosition.z);
                break;

            case "Sedimentar":

                abaSedimentares.SetActive(true);
                textos = informacoesSedimentares.GetComponentsInChildren<TextMeshProUGUI>();
                indice = indices[itemPai.name];
                RochaSedimentar rochaSedimentar = ListaSedimentares.catalogoSedimentares[indice];

                Transform foto2 = informacoesSedimentares.transform.GetChild(3);
                Image imagem2 = foto2.GetComponent<Image>();

                textos[0].text = rochaSedimentar.nomeTecnico;
                textos[1].text = rochaSedimentar.tipo;
                textos[4].text = rochaSedimentar.nomeTecnico;
                textos[6].text = rochaSedimentar.tambemConhecidoPor;
                textos[8].text = rochaSedimentar.tipo;
                textos[10].text = rochaSedimentar.familia;
                textos[12].text = rochaSedimentar.sedimentosOriginarios;
                textos[15].text = rochaSedimentar.texturas;
                textos[17].text = rochaSedimentar.mineraisEssenciais;
                textos[19].text = rochaSedimentar.mineraisAcessorios;
                textos[21].text = rochaSedimentar.comoForma;
                textos[23].text = rochaSedimentar.rochaCotidiano;
                textos[25].text = rochaSedimentar.curiosidades;
                textos[27].text = rochaSedimentar.doacao;
                textos[30].text = rochaSedimentar.museuhe;
                textos[32].text = rochaSedimentar.uspgeociencias;
                textos[34].text = rochaSedimentar.outro;
                textos[35].text = rochaSedimentar.outro2;

                for (int i = 30; i <= 34; i = i + 2)
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

                imagem2.sprite = Resources.Load<Sprite>(rochaSedimentar.nomeTecnico);

                AjustarTextos(informacoesSedimentares, contentSedimentares, "Sedimentar");
                AjustarScrollView(contentSedimentares, informacoesSedimentares);

                cameraPrincipal.transform.localPosition = new Vector3(-6000f, cameraPrincipal.transform.localPosition.y, cameraPrincipal.transform.localPosition.z);
                break;

            case "Metam�rfica":

                abaMetam�rficas.SetActive(true);
                indice = indices[itemPai.name];
                textos = informacoesMetam�rficas.GetComponentsInChildren<TextMeshProUGUI>();
                RochaMetamorfica metamorfica = ListaMetam�rficas.catalogoMetamorficas[indice];

                Transform foto = informacoesMetam�rficas.transform.GetChild(2);
                Image imagem = foto.GetComponent<Image>();

                textos[0].text = metamorfica.nomeTecnico;
                textos[3].text = metamorfica.nomeTecnico;
                textos[5].text = metamorfica.tambemConhecidoPor;
                textos[7].text = metamorfica.familia;
                textos[9].text = metamorfica.tipoEGrau;
                textos[11].text = metamorfica.condicoesDeFormacao;
                textos[13].text = metamorfica.comoForma;
                textos[15].text = metamorfica.rochaCotidiano;
                textos[17].text = metamorfica.curiosidades;
                textos[19].text = metamorfica.doacao;
                textos[22].text = metamorfica.museuhe;
                textos[24].text = metamorfica.uspgeociencias;
                textos[26].text = metamorfica.museuUnesp;
                textos[28].text = metamorfica.outro; //31?

                for (int i = 22; i <= 28; i = i + 2)
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

                imagem.sprite = Resources.Load<Sprite>(metamorfica.nomeTecnico);

                AjustarTextos(informacoesMetam�rficas, contentMetam�rficas, "Metam�rfica");
                AjustarScrollView(contentMetam�rficas, informacoesMetam�rficas);

                cameraPrincipal.transform.localPosition = new Vector3(-8000f, cameraPrincipal.transform.localPosition.y, cameraPrincipal.transform.localPosition.z);
                break;
        }
    }

    void AjustarScrollView(GameObject content, GameObject modelo)
    {
        int index = 0;

        index = modelo.transform.childCount - 1;

        RectTransform contentRect = content.GetComponent<RectTransform>();

        RectTransform primeiroRect = content.transform.GetChild(0).GetComponent<RectTransform>();
        float top = primeiroRect.localPosition.y + (primeiroRect.rect.height/2);

        Transform ultimoObjetoModelo = modelo.transform.GetChild(index);
        RectTransform rectUltimoObjetoModelo = ultimoObjetoModelo.GetComponent<RectTransform>();
        float bottom = ultimoObjetoModelo.localPosition.y - (rectUltimoObjetoModelo.rect.height/2);

        float distance = Math.Abs(top - bottom);

        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, distance - 100);

    }

    void AjustarTextos(GameObject informacoes, GameObject content, string tipo)
    {
        int espacamentoVertical = 4;
        int espa�amentoImagem = 28;
        List<RectTransform> listaObjetos = new List<RectTransform>();

        RectTransform rectContent = content.GetComponent<RectTransform>();
        
        foreach (RectTransform child in informacoes.GetComponent<RectTransform>())
        {
            listaObjetos.Add(child);
        }

        TMP_Text[] textos = informacoes.GetComponentsInChildren<TMP_Text>();

        RectTransform primeiroItem = listaObjetos[1];
        AjustarRectParaTexto(primeiroItem);
        float posicaoPrimeiroItem = 0;
        switch (tipo)
        {
            case "Mineral":
                posicaoPrimeiroItem = 50f - (primeiroItem.rect.height / 2f);
                break;
            case "Sedimentar":
                posicaoPrimeiroItem = 435f - (primeiroItem.rect.height / 2f);
                break;
            case "�gnea":
                posicaoPrimeiroItem = 443.1f - (primeiroItem.rect.height / 2f);
                break;
            case "Metam�rfica":
                posicaoPrimeiroItem = 426.81f - (primeiroItem.rect.height / 2f);
                break;
        }
        primeiroItem.localPosition = new Vector3(primeiroItem.localPosition.x, posicaoPrimeiroItem, primeiroItem.localPosition.z);

        for (int i = 2; i < listaObjetos.Count; i++)
        {
            RectTransform objetoAnterior = listaObjetos[i - 1];
            AjustarRectParaTexto(objetoAnterior);
            Vector3 posi��oAnterior = objetoAnterior.localPosition;
            float posi��oAnteriorY = posi��oAnterior.y;
            float alturaAnterior = objetoAnterior.rect.height;
            float limiteAnterior = posi��oAnteriorY - alturaAnterior;

            RectTransform objetoAtual = listaObjetos[i];
            AjustarRectParaTexto(objetoAtual);
            Vector3 posi��oAtual = objetoAtual.localPosition;
            float posi��oAtualY = posi��oAtual.y;
            float alturaAtual = objetoAtual.rect.height;
            float larguraAtual = objetoAtual.rect.width;

            if (listaObjetos[i-1].name == "Image" || listaObjetos[i].name == "Image")
            {
                float posi��oNovaY = posi��oAnteriorY - (alturaAnterior / 2) - espa�amentoImagem - (alturaAtual / 2);
                objetoAtual.localPosition = new Vector3(posi��oAtual.x, posi��oNovaY, posi��oAtual.z);
            }
            else
            {
                float posi��oNovaY = posi��oAnteriorY - (alturaAnterior / 2) - espacamentoVertical - (alturaAtual / 2);
                objetoAtual.localPosition = new Vector3(posi��oAtual.x, posi��oNovaY, posi��oAtual.z);
            }
        }
    }

    void AjustarRectParaTexto(RectTransform text)
    {
        TMP_Text textoReal = text.GetComponent<TMP_Text>();
        if (textoReal != null)
        {
            RectTransform rectTexto = text.GetComponent<RectTransform>();

            textoReal.ForceMeshUpdate();

            Vector2 textSize = textoReal.GetRenderedValues(true);
            
            text.sizeDelta = new Vector2(text.sizeDelta.x, textSize.y);
        }
    }
}
