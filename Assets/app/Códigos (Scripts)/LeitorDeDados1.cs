using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class LeitorDeDados1 : MonoBehaviour
{
    /* Inicializa as variáveis necessárias para o funcionamento do código.
     * dadosMinerais se refere ao arquivo .csv da planilha do Catálogo de Rochas e Minerais, convertido através de 
     * websites ou outras ferramentas para ser delimitado por colunas.
     * num_colunasMinerais é o número de colunas da planilha em questão.
     * prefabMinerais é o molde (prefabMinerais) dos botões da categoria de rochas específica/minerais.
     * containerDosItens é o Content do Scroll View que controla a lista de rochas e minerais.
     */
    public GameObject containerDosItens;
    public GameObject cameraMain;

    public TextAsset dadosMinerais;
    public GameObject abaMinerais;
    public int num_colunasMinerais;
    public GameObject prefabMinerais;
    public GameObject minInformacoes;
    public GameObject minContent;

    public TextAsset dadosIgneas;
    public GameObject abaIgneas;
    public int num_colunasIgneas;
    public GameObject prefabIgneas;
    public GameObject igneasInformacoes;
    public GameObject igneasContent;

    public TextAsset dadosMetamorficas;
    public GameObject abaMetamorficas;
    public int num_colunasMetamorficas;
    public GameObject prefabMetamorficas;
    public GameObject metamorficasInformacoes;
    public GameObject metamorficasContent;

    public TextAsset dadosSedimentares;
    public GameObject abaSedimentares;
    public int num_colunasSedimentares;
    public GameObject prefabSedimentares;
    public GameObject sedimentaresInformacoes;
    public GameObject sedimentaresContent;

    /* A linha seguinte cria uma estrutura chamada dicionário, que cria uma associação entre um valor "chave"
     * e um valor a ser retirado por essa "chave". Nesse caso, utilizamos ela no código de leitura de dadosMinerais
     * para associar o nomeTecnico da rocha/mineral ao seu índice na planilha e usar isso para retirar suas informações
     * no código AbrirMineral/AbrirRochaÍgnea/etc.
     */

    public Dictionary<string, int> indicesRochasMinerais = new Dictionary<string, int>();

    public Dictionary<string, string> tiposRochasMinerais = new Dictionary<string, string>();

    public Dictionary<string, RectTransform> botoesRochasMinerais = new Dictionary<string, RectTransform>();

    public Dictionary<string, bool> botaoRenderizado = new Dictionary<string, bool>();

    public Dictionary<string, GameObject> botaoDictGameObjectRochasMinerais = new Dictionary<string, GameObject>();

    public List<string> listaDeNomesDeRochasEMinerais;
    public List<string> listaDeNomesBotoesRenderizados;


    /* As linhas seguintes criam objetos especiais específicos para o aplicativo através de classes. Em geral, 
     * as classes são um ''grupo'' de objetos similares. Nesse caso, agrupamos as rochas em uma única classe,
     * pois todas elas tem propriedades semelhantes.
     */

    [System.Serializable]
    public class RochaMetamorfica
    {
        public string nomeFoto;
        public string nomeFotoAplicacoes;
        public string nomeTecnico;
        public string tambemConhecidoPor;
        public string variacao;
        public string familia;
        public string tipoEGrau;
        public string comoForma;
        public string condicoesDeFormacao;
        public string texturas;
        public string mineraisEssenciais;
        public string mineraisAcessorios;
        public string rochaCotidiano;
        public string curiosidades;
        public string doacao;
        public string museuhe;
        public string uspgeociencias;
        public string wikipedia;
        public string museuUnesp;
        public string outro;
        // Classificação = bio, quimio, clast, org
    }

    [System.Serializable]
    public class RochaSedimentar
    {
        public string nomeFoto;
        public string nomeFotoAplicacoes;
        public string nomeTecnico;
        public string tipo;
        public string tambemConhecidoPor;
        public string sedimentosOriginarios;
        public string familia;
        public string comoForma;
        public string texturas;
        public string mineraisEssenciais;
        public string mineraisAcessorios;
        public string rochaCotidiano;
        public string curiosidades;
        public string doacao;
        public string museuhe;
        public string uspgeociencias;
        public string wikipedia;
        public string outro;
        public string outro2;
        // Classificação = bio, quimio, clast, org
    }

    [System.Serializable]
    public class RochaIgnea
    {
        public string nomeFoto;
        public string nomeFotoAplicacoes;
        public string nomeTecnico;
        public string tipo;
        public string variacao;
        public string correspondenteExtrusivoIntrusivo;
        public string tambemConhecidoPor;
        public string familia;
        public string magmaOriginario;
        public string comoForma;
        public string rochaCotidiano;
        public string curiosidades;
        public string doacao;
        public string museuhe;
        public string uspgeociencias;
        public string wikipedia;
        public string outro;
    }

    [System.Serializable]
    public class Mineral
    {
        public string nomeTecnico;
        public string classe;
        public string grupo;
        public string formula;
        public string tambemConhecidoPor;
        public string tipo;
        public string variedades;
        public string clivagem;
        public string dureza;
        public string fratura;
        public string densidade;
        public string tenacidade;
        public string diafaneidade;
        public string traco;
        public string brilho;
        public string cor;
        public string habito;
        public string rochaCotidiano;
        public string outrasInfo;
        public string classeCristalina;
        public string curiosidades;
        public string doacao;
        public string museuhe;
        public string uspgeociencias;
        public string minmicro;
        public string wikipedia;
        public string outro;
        // Adicionar as categorias de referências.
        // Organizar a ordem.
    }

    [System.Serializable]
    public class ListaDeMinerais
    {
        public Mineral[] catalogoMinerais;
    }

    [System.Serializable]
    public class ListaDeIgneas
    {
        public RochaIgnea[] catalogoIgneas;
    }

    [System.Serializable]
    public class ListaDeMetamorficas
    {
        public RochaMetamorfica[] catalogoMetamorficas;
    }

    [System.Serializable]
    public class ListaDeSedimentares
    {
        public RochaSedimentar[] catalogoSedimentares;
    }

    public ListaDeMinerais ListaMinerais = new ListaDeMinerais();
    public ListaDeIgneas ListaIgneas = new ListaDeIgneas();
    public ListaDeMetamorficas ListaMetamorficas = new ListaDeMetamorficas();
    public ListaDeSedimentares ListaSedimentares = new ListaDeSedimentares();

    void Start()
    {
        LerDados();
    }

    void LerDados()
    {
        string[] listaDeDadosMinerais = dadosMinerais.text.Split(new string[] {"*", "@"}, StringSplitOptions.None);
        string[] listaDeDadosIgneas = dadosIgneas.text.Split(new string[] { "*", "@" }, StringSplitOptions.None);
        string[] listaDeDadosMetamorficas = dadosMetamorficas.text.Split(new string[] {"*", "@"}, StringSplitOptions.None);
        string[] listaDeDadosSedimentares = dadosSedimentares.text.Split(new string[] { "*", "@" }, StringSplitOptions.None);

        int tamanhoDaTabelaMinerais = (listaDeDadosMinerais.Length / num_colunasMinerais) - 1;
        Debug.Log($@"Número de minerais: {tamanhoDaTabelaMinerais}");
        int tamanhoDaTabelaIgneas = (listaDeDadosIgneas.Length / num_colunasIgneas) - 1;
        Debug.Log($@"Número de rochas ígneas: {tamanhoDaTabelaIgneas}");
        int tamanhoDaTabelaMetamorficas = (listaDeDadosMetamorficas.Length / num_colunasMetamorficas) - 1;
        Debug.Log($@"Número de rochas metamórficas: {tamanhoDaTabelaMetamorficas}");
        int tamanhoDaTabelaSedimentares = (listaDeDadosSedimentares.Length / num_colunasSedimentares) - 1;
        Debug.Log($@"Número de rochas sedimentares: {tamanhoDaTabelaSedimentares}");

        ListaMinerais.catalogoMinerais = new Mineral[tamanhoDaTabelaMinerais];
        ListaIgneas.catalogoIgneas = new RochaIgnea[tamanhoDaTabelaIgneas];
        ListaMetamorficas.catalogoMetamorficas = new RochaMetamorfica[tamanhoDaTabelaMetamorficas];
        ListaSedimentares.catalogoSedimentares = new RochaSedimentar[tamanhoDaTabelaSedimentares];

        // Preenchendo as informações de minerais.
        for (int i = 0; i < tamanhoDaTabelaMinerais; i++)
        {
            ListaMinerais.catalogoMinerais[i] = new Mineral();
            Mineral mineral = ListaMinerais.catalogoMinerais[i];

            mineral.nomeTecnico = listaDeDadosMinerais[num_colunasMinerais * (i + 1)].Replace('"', ' ');
            mineral.classe = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 1].Replace('"', ' ');
            mineral.grupo = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 2].Replace('"', ' ');
            mineral.formula = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 3].Replace('"', ' ');
            mineral.tipo = "Mineral";
            mineral.tambemConhecidoPor = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 4].Replace('"', ' ');
            mineral.variedades = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 5].Replace('"', ' ');
            mineral.clivagem = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 6].Replace('"', ' ');
            mineral.dureza = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 7].Replace('"', ' ');
            mineral.fratura = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 8].Replace('"', ' ');
            mineral.densidade = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 9].Replace('"', ' ');
            mineral.tenacidade = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 10].Replace('"', ' ');
            mineral.diafaneidade = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 11].Replace('"', ' ');
            mineral.traco = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 12].Replace('"', ' ');
            mineral.brilho = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 13].Replace('"', ' ');
            mineral.cor = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 14].Replace('"', ' ');
            mineral.habito = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 15].Replace('"', ' ');
            mineral.rochaCotidiano = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 16].Replace('"', ' ');
            mineral.outrasInfo = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 17].Replace('"', ' ');
            mineral.classeCristalina = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 18].Replace('"', ' ');
            mineral.curiosidades = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 19].Replace('"', ' ');
            mineral.doacao = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 20].Replace('"', ' ');
            mineral.museuhe = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 21].Replace('"', ' ');
            mineral.uspgeociencias = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 22].Replace('"', ' ');
            mineral.minmicro = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 23].Replace('"', ' ');
            mineral.wikipedia = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 24].Replace('"', ' ');
            mineral.outro = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 25].Replace('"', ' ');

            Debug.Log(i + mineral.nomeTecnico);
            indicesRochasMinerais.Add(mineral.nomeTecnico, i);
            tiposRochasMinerais.Add(mineral.nomeTecnico, "Mineral");
        }

        // Preenchendo as informações de rochas ígneas.
        for (int i = 0; i < tamanhoDaTabelaIgneas; i++)
        {
            ListaIgneas.catalogoIgneas[i] = new RochaIgnea();
            RochaIgnea rochaIgnea = ListaIgneas.catalogoIgneas[i];

            rochaIgnea.nomeTecnico = listaDeDadosIgneas[num_colunasIgneas * (i + 1)];
            rochaIgnea.tipo = "Ígnea";
            rochaIgnea.variacao = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 2];
            rochaIgnea.tambemConhecidoPor = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 3];
            rochaIgnea.familia = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 4];
            rochaIgnea.magmaOriginario = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 5];
            rochaIgnea.comoForma = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 6];
            rochaIgnea.correspondenteExtrusivoIntrusivo = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 7];
            rochaIgnea.rochaCotidiano = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 8];
            rochaIgnea.curiosidades = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 9];
            rochaIgnea.doacao = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 10];
            rochaIgnea.museuhe = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 11];
            rochaIgnea.uspgeociencias = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 12];
            rochaIgnea.wikipedia = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 13];
            rochaIgnea.outro = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 14];

            indicesRochasMinerais.Add(rochaIgnea.nomeTecnico, i);
            tiposRochasMinerais.Add(rochaIgnea.nomeTecnico, "Ígnea");
        }

        // Preenchendo as informações de rochas metamórficas.
        for (int i = 0; i < tamanhoDaTabelaMetamorficas; i++)
        {
            ListaMetamorficas.catalogoMetamorficas[i] = new RochaMetamorfica();
            RochaMetamorfica rochaMetamorfica = ListaMetamorficas.catalogoMetamorficas[i];

            rochaMetamorfica.nomeTecnico = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1)];
            rochaMetamorfica.variacao = "Metamórfica";
            rochaMetamorfica.tambemConhecidoPor = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 2];
            rochaMetamorfica.familia = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 3];
            rochaMetamorfica.tipoEGrau = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 4];
            rochaMetamorfica.comoForma = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 5];
            rochaMetamorfica.condicoesDeFormacao = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 6];
            rochaMetamorfica.rochaCotidiano = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 7];
            rochaMetamorfica.curiosidades = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 8];
            rochaMetamorfica.doacao = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 9];
            rochaMetamorfica.museuhe = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 10];
            rochaMetamorfica.uspgeociencias = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 11];
            rochaMetamorfica.wikipedia = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 12];
            rochaMetamorfica.museuUnesp = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 13];
            rochaMetamorfica.outro = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 14];

            indicesRochasMinerais.Add(rochaMetamorfica.nomeTecnico, i);
            tiposRochasMinerais.Add(rochaMetamorfica.nomeTecnico, "Metamórfica");
        }

        // Preenchendo as informações de rochas sedimentares.
        for (int i = 0; i < tamanhoDaTabelaSedimentares; i++)
        {
            ListaSedimentares.catalogoSedimentares[i] = new RochaSedimentar();
            RochaSedimentar rochaSedimentar = ListaSedimentares.catalogoSedimentares[i];

            rochaSedimentar.nomeTecnico = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1)];
            rochaSedimentar.tipo = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 1];
            rochaSedimentar.tambemConhecidoPor = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 2];
            rochaSedimentar.familia = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 3];
            rochaSedimentar.sedimentosOriginarios = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 4];
            rochaSedimentar.comoForma = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 5];
            rochaSedimentar.texturas = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 6];
            rochaSedimentar.mineraisEssenciais = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 7];
            rochaSedimentar.mineraisAcessorios = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 8];
            rochaSedimentar.rochaCotidiano = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 9];
            rochaSedimentar.curiosidades = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 10];
            rochaSedimentar.doacao = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 11];
            rochaSedimentar.museuhe = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 12];
            rochaSedimentar.uspgeociencias = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 13];
            rochaSedimentar.wikipedia = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 14];
            rochaSedimentar.outro = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 15];
            rochaSedimentar.outro2 = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 16];

            indicesRochasMinerais.Add(rochaSedimentar.nomeTecnico, i);
            tiposRochasMinerais.Add(rochaSedimentar.nomeTecnico, "Sedimentar");
        }

        // Ajustando o tamanho do ScrollView (Content).
        RectTransform tamanhoDaLista = containerDosItens.GetComponent<RectTransform>();
        float offset = 235f;
        float tamanhoEscalonado = 235f * (tamanhoDaTabelaMinerais + tamanhoDaTabelaMetamorficas + tamanhoDaTabelaIgneas + tamanhoDaTabelaSedimentares) + offset;
        tamanhoDaLista.sizeDelta = new Vector2(tamanhoDaLista.sizeDelta.x, tamanhoEscalonado);
        Debug.Log("Quantidade de rochas e minerais: " + (tamanhoDaTabelaMinerais + tamanhoDaTabelaMetamorficas + tamanhoDaTabelaIgneas + tamanhoDaTabelaSedimentares));

        Transform listaDosItens = containerDosItens.GetComponent<Transform>();

        Vector2 ultimaPosicao = Vector2.zero;

        // Preenchendo a lista de itens.
        for (int z = 0; z < tamanhoDaTabelaMinerais; z++)
        {
            GameObject item1 = Instantiate(prefabMinerais, Vector3.zero, Quaternion.identity, listaDosItens);
            item1.name = ListaMinerais.catalogoMinerais[z].nomeTecnico;

            // Criando as variáveis que representam nomeTecnico, tipo, fórmula e imagem.

            Transform nome = item1.transform.GetChild(0);
            TextMeshProUGUI textoNome = nome.GetComponent<TextMeshProUGUI>();

            Transform tipo = item1.transform.GetChild(2);
            TextMeshProUGUI textoTipo = tipo.GetComponent<TextMeshProUGUI>();

            Transform foto = item1.transform.GetChild(3);
            Image componenteImagemFoto = foto.GetComponent<Image>();

            Transform botaoAbertura = item1.transform.GetChild(4);
            AbrirInformacoes scriptAbertura = botaoAbertura.GetComponent<AbrirInformacoes>();

            RectTransform retanguloItem = item1.GetComponent<RectTransform>();
            retanguloItem.localPosition = new Vector2(272.775f, (-z * 230)-114.5f);

            ultimaPosicao = retanguloItem.localPosition;

            // Preenchendo as informações.

            textoNome.SetText(ListaMinerais.catalogoMinerais[z].nomeTecnico);
            textoTipo.SetText(ListaMinerais.catalogoMinerais[z].tipo);
            componenteImagemFoto.sprite = Resources.Load<Sprite>("Amianto Crisotila");

            botoesRochasMinerais.Add(item1.name, retanguloItem);
            botaoRenderizado.Add(item1.name, true);
            botaoDictGameObjectRochasMinerais.Add(item1.name, item1);
            // Descobrir se é possível carregar imagens dinamicamente. (sim, falta implementar) (deixar pra depois)
        }

        for (int z = 0; z < tamanhoDaTabelaMetamorficas; z++)
        {
            GameObject item2 = Instantiate(prefabMetamorficas, Vector3.zero, Quaternion.identity, listaDosItens);
            item2.name = ListaMetamorficas.catalogoMetamorficas[z].nomeTecnico;

            Transform nome = item2.transform.GetChild(0);
            TextMeshProUGUI textoNome = nome.GetComponent<TextMeshProUGUI>();

            textoNome.text = ListaMetamorficas.catalogoMetamorficas[z].nomeTecnico;

            RectTransform rectPosicao = item2.GetComponent<RectTransform>();
            rectPosicao.localPosition = ultimaPosicao + new Vector2(0, 230 * -(z+1));

            if (z == tamanhoDaTabelaMetamorficas - 1)
            {
                ultimaPosicao = rectPosicao.localPosition;
            }

            botaoDictGameObjectRochasMinerais.Add(item2.name, item2);
            botoesRochasMinerais.Add(item2.name, rectPosicao);
            botaoRenderizado.Add(item2.name, true);
        }

        for (int z = 0; z < tamanhoDaTabelaIgneas; z++)
        {
            GameObject item3 = Instantiate(prefabIgneas, Vector3.zero, Quaternion.identity, listaDosItens);
            item3.name = ListaIgneas.catalogoIgneas[z].nomeTecnico;

            Transform nome = item3.transform.GetChild(0);
            TextMeshProUGUI textoNome = nome.GetComponent<TextMeshProUGUI>();

            textoNome.text = ListaIgneas.catalogoIgneas[z].nomeTecnico;

            RectTransform rectPosicao = item3.GetComponent<RectTransform>();
            rectPosicao.localPosition = ultimaPosicao + new Vector2(0, 230 * -(z+1));

            if (z == tamanhoDaTabelaIgneas - 1)
            {
                ultimaPosicao = rectPosicao.localPosition;
            }

            Debug.Log(z + ListaIgneas.catalogoIgneas[z].nomeTecnico);
            Debug.Log(item3.name);

            botoesRochasMinerais.Add(item3.name, rectPosicao);
            botaoRenderizado.Add(item3.name, true);
            botaoDictGameObjectRochasMinerais.Add(item3.name, item3);
        }

        for (int z = 0; z < tamanhoDaTabelaSedimentares; z++)
        {
            GameObject item4 = Instantiate(prefabSedimentares, Vector3.zero, Quaternion.identity, listaDosItens);
            item4.name = ListaSedimentares.catalogoSedimentares[z].nomeTecnico;

            Transform nome = item4.transform.GetChild(0);
            TextMeshProUGUI textoNome = nome.GetComponent<TextMeshProUGUI>();

            textoNome.text = ListaSedimentares.catalogoSedimentares[z].nomeTecnico;

            RectTransform rectPosicao = item4.GetComponent<RectTransform>();
            rectPosicao.localPosition = ultimaPosicao + new Vector2(0, 230 * -(z + 1));

            if (z == tamanhoDaTabelaSedimentares - 1)
            {
                ultimaPosicao = rectPosicao.localPosition;
            }

            Debug.Log(z + ListaSedimentares.catalogoSedimentares[z].nomeTecnico);
            Debug.Log(item4.name);

            botoesRochasMinerais.Add(item4.name, rectPosicao);
            botaoRenderizado.Add(item4.name, true);
            botaoDictGameObjectRochasMinerais.Add(item4.name, item4);
        }

        listaDeNomesDeRochasEMinerais = botoesRochasMinerais.Keys.ToList();
        listaDeNomesBotoesRenderizados = listaDeNomesDeRochasEMinerais;
    }
}