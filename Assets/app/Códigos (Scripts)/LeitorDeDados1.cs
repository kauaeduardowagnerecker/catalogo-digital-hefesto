using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeitorDeDados1 : MonoBehaviour
{
    /* Inicializa as variáveis necessárias apra o funcionamento do código.
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

    public TextAsset dadosIgneas;
    //public GameObject abaIgneas;
    public int num_colunasIgneas;
    public GameObject prefabIgneas;
    //public GameObject igneasInformacoes;

    public TextAsset dadosMetamorficas;
    //public GameObject abaMetamorficas;
    public int num_colunasMetamorficas;
    public GameObject prefabMetamorficas;
    //public GameObject metamorficasInformacoes;

    public TextAsset dadosSedimentares;
    //public GameObject abaSedimentares;
    public int num_colunasSedimentares;
    public GameObject prefabSedimentares;
    //public GameObject sedimentaresInformacoes;

    /* A linha seguinte cria uma estrutura chamada dicionário, que cria uma associação entre um valor "chave"
     * e um valor a ser retirado por essa "chave". Nesse caso, utilizamos ela no código de leitura de dadosMinerais
     * para associar o nomeTecnico da rocha/mineral ao seu índice na planilha e usar isso para retirar suas informações
     * no código AbrirMineral/AbrirRochaÍgnea/etc.
     */

    public Dictionary<string, int> indicesRochasMinerais = new Dictionary<string, int>();

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
        public string classificacao;
        public string tambemConhecidoPor;
        public string variedades;
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
        public string classificacao;
        public string tambemConhecidoPor;
        public string variedades;
        public string sedimentosOriginarios;
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
        public string tambemConhecidoPor;
        public string variedades;
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

        // ordenar
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

            ListaMinerais.catalogoMinerais[i].nomeTecnico = listaDeDadosMinerais[num_colunasMinerais * (i + 1)].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].classe = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 1].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].grupo = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 2].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].formula = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 3].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].tipo = "Mineral";
            ListaMinerais.catalogoMinerais[i].tambemConhecidoPor = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 4].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].variedades = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 5].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].clivagem = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 6].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].dureza = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 7].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].fratura = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 8].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].densidade = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 9].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].tenacidade = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 10].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].diafaneidade = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 11].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].traco = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 12].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].brilho = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 13].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].cor = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 14].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].habito = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 15].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].rochaCotidiano = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 16].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].outrasInfo = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 17].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].classeCristalina = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 18].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].curiosidades = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 19].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].doacao = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 20].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].museuhe = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 21].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].uspgeociencias = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 22].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].minmicro = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 23].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].wikipedia = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 24].Replace('"', ' ');
            ListaMinerais.catalogoMinerais[i].outro = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 25].Replace('"', ' ');

            Debug.Log(i + ListaMinerais.catalogoMinerais[i].nomeTecnico);
            indicesRochasMinerais.Add(ListaMinerais.catalogoMinerais[i].nomeTecnico, i);
        }

        // Preenchendo as informações de rochas ígneas.
        for (int i = 0; i < tamanhoDaTabelaIgneas; i++)
        {
            ListaIgneas.catalogoIgneas[i] = new RochaIgnea();

            ListaIgneas.catalogoIgneas[i].nomeTecnico = listaDeDadosIgneas[num_colunasIgneas * (i + 1)];
            ListaIgneas.catalogoIgneas[i].tipo = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 1];
            ListaIgneas.catalogoIgneas[i].variacao = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 2];
            ListaIgneas.catalogoIgneas[i].tambemConhecidoPor = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 3];
            ListaIgneas.catalogoIgneas[i].familia = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 4];
            ListaIgneas.catalogoIgneas[i].magmaOriginario = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 5];
            ListaIgneas.catalogoIgneas[i].comoForma = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 6];
            ListaIgneas.catalogoIgneas[i].rochaCotidiano = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 7];
            ListaIgneas.catalogoIgneas[i].curiosidades = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 8];
            ListaIgneas.catalogoIgneas[i].doacao = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 9];
            ListaIgneas.catalogoIgneas[i].museuhe = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 10];
            ListaIgneas.catalogoIgneas[i].uspgeociencias = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 11];
            ListaIgneas.catalogoIgneas[i].wikipedia = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 12];
            ListaIgneas.catalogoIgneas[i].outro = listaDeDadosIgneas[num_colunasIgneas * (i + 1) + 13];
        }

        // Preenchendo as informações de rochas metamórficas.
        for (int i = 0; i < tamanhoDaTabelaMetamorficas; i++)
        {
            ListaMetamorficas.catalogoMetamorficas[i] = new RochaMetamorfica();

            ListaMetamorficas.catalogoMetamorficas[i].nomeTecnico = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1)];
            ListaMetamorficas.catalogoMetamorficas[i].variacao = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 1];
            ListaMetamorficas.catalogoMetamorficas[i].tambemConhecidoPor = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 2];
            ListaMetamorficas.catalogoMetamorficas[i].familia = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 3];
            ListaMetamorficas.catalogoMetamorficas[i].tipoEGrau = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 4];
            ListaMetamorficas.catalogoMetamorficas[i].comoForma = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 5];
            ListaMetamorficas.catalogoMetamorficas[i].condicoesDeFormacao = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 6];
            ListaMetamorficas.catalogoMetamorficas[i].rochaCotidiano = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 7];
            ListaMetamorficas.catalogoMetamorficas[i].curiosidades = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 8];
            ListaMetamorficas.catalogoMetamorficas[i].doacao = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 9];
            ListaMetamorficas.catalogoMetamorficas[i].museuhe = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 10];
            ListaMetamorficas.catalogoMetamorficas[i].uspgeociencias = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 11];
            ListaMetamorficas.catalogoMetamorficas[i].wikipedia = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 12];
            ListaMetamorficas.catalogoMetamorficas[i].outro = listaDeDadosMetamorficas[num_colunasMetamorficas * (i + 1) + 13];
        }

        // Preenchendo as informações de rochas sedimentares.
        for (int i = 0; i < tamanhoDaTabelaSedimentares; i++)
        {
            ListaSedimentares.catalogoSedimentares[i] = new RochaSedimentar();

            ListaSedimentares.catalogoSedimentares[i].nomeTecnico = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1)];
            ListaSedimentares.catalogoSedimentares[i].tipo = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 1];
            ListaSedimentares.catalogoSedimentares[i].tambemConhecidoPor = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 2];
            ListaSedimentares.catalogoSedimentares[i].sedimentosOriginarios = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 3];
            ListaSedimentares.catalogoSedimentares[i].comoForma = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 4];
            ListaSedimentares.catalogoSedimentares[i].texturas = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 5];
            ListaSedimentares.catalogoSedimentares[i].mineraisEssenciais = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 6];
            ListaSedimentares.catalogoSedimentares[i].mineraisAcessorios = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 7];
            ListaSedimentares.catalogoSedimentares[i].rochaCotidiano = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 8];
            ListaSedimentares.catalogoSedimentares[i].curiosidades = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 9];
            ListaSedimentares.catalogoSedimentares[i].doacao = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 10];
            ListaSedimentares.catalogoSedimentares[i].museuhe = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 11];
            ListaSedimentares.catalogoSedimentares[i].uspgeociencias = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 12];
            ListaSedimentares.catalogoSedimentares[i].wikipedia = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 13];
            ListaSedimentares.catalogoSedimentares[i].outro = listaDeDadosSedimentares[num_colunasSedimentares * (i + 1) + 14];
        }

        // Ajustando o tamanho do ScrolLView (Content).
        RectTransform tamanhoDaLista = containerDosItens.GetComponent<RectTransform>();
        float tamanhoEscalonado = 235f * (tamanhoDaTabelaMinerais + tamanhoDaTabelaMetamorficas + tamanhoDaTabelaIgneas + tamanhoDaTabelaSedimentares);
        tamanhoDaLista.sizeDelta = new Vector2(tamanhoDaLista.sizeDelta.x, tamanhoEscalonado);

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
            
            Transform formula = item1.transform.GetChild(3);
            TextMeshProUGUI textoFormula = formula.GetComponent<TextMeshProUGUI>();

            Transform foto = item1.transform.GetChild(4);
            Image componenteImagemFoto = foto.GetComponent<Image>();

            RectTransform retanguloItem = item1.GetComponent<RectTransform>();
            retanguloItem.localPosition = new Vector2(272.775f, (-z * 230)-114.5f);

            ultimaPosicao = retanguloItem.localPosition;

            // Preenchendo as informações.

            textoNome.SetText(ListaMinerais.catalogoMinerais[z].nomeTecnico);
            textoTipo.SetText(ListaMinerais.catalogoMinerais[z].tipo);
            textoFormula.SetText(ListaMinerais.catalogoMinerais[z].formula);
            componenteImagemFoto.sprite = Resources.Load<Sprite>("Amianto Crisotila");

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

            Debug.Log(z + ListaMetamorficas.catalogoMetamorficas[z].nomeTecnico);
            Debug.Log(item2.name);
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
        }
    }
}