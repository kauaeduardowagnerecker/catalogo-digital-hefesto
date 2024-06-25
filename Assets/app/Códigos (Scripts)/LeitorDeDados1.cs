using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// TESTAR USAR .TSV, pra isso precisa tirar o \n

public class LeitorDeDados1 : MonoBehaviour
{
    /* Inicializa as vari�veis necess�rias apra o funcionamento do c�digo.
     * dadosMinerais se refere ao arquivo .csv da planilha do Cat�logo de Rochas e Minerais, convertido atrav�s de 
     * websites ou outras ferramentas para ser delimitado por colunas.
     * num_colunasMinerais � o n�mero de colunas da planilha em quest�o.
     * prefabMinerais � o molde (prefabMinerais) dos bot�es da categoria de rochas espec�fica/minerais.
     * containerDosItens � o Content do Scroll View que controla a lista de rochas e minerais.
     */

    public TextAsset dadosMinerais;
    public TextAsset dadosIgneas;
    public int num_colunasMinerais;
    public int num_colunasIgneas;
    public GameObject prefabMinerais;
    public GameObject prefabIgneas;
    public GameObject containerDosItens;
    public GameObject minInformacoes;
    public GameObject cameraMin;

    /* A linha seguinte cria uma estrutura chamada dicion�rio, que cria uma associa��o entre um valor "chave"
     * e um valor a ser retirado por essa "chave". Nesse caso, utilizamos ela no c�digo de leitura de dadosMinerais
     * para associar o nomeTecnico da rocha/mineral ao seu �ndice na planilha e usar isso para retirar suas informa��es
     * no c�digo AbrirMineral/AbrirRocha�gnea/etc.
     */

    public Dictionary<string, int> indicesRochasMinerais = new Dictionary<string, int>();

    /* As linhas seguintes criam objetos especiais espec�ficos para o aplicativo atrav�s de classes. Em geral, 
     * as classes s�o um ''grupo'' de objetos similares. Nesse caso, agrupamos as rochas em uma �nica classe,
     * pois todas elas tem propriedades semelhantes.
     */

    [System.Serializable]
    public class RochaMetamorfica
    {
        public string nomeFoto;
        public string nomeFotoAplicacoes;
        public string nomeTecnico;
        public string tipo;
        public string classificacao;
        public string tambemConhecidoPor;
        public string variedades;

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
        // Classifica��o = bio, quimio, clast, org
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
        // Classifica��o = bio, quimio, clast, org
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

        // ordenar
    }

    // Classe dos Minerais.
    [System.Serializable]
    public class Mineral
    {
        public string nomeFoto;
        public string nomeFotoAplicacoes;
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
        // Adicionar as categorias de refer�ncias.
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

    public ListaDeMinerais ListaMinerais = new ListaDeMinerais();
    public ListaDeIgneas ListaIgneas = new ListaDeIgneas();

    // Start is called before the first frame update
    void Start()
    {
        LerDados();
    }

    void LerDados()
    {
        string[] listaDeDadosMinerais = dadosMinerais.text.Split(new string[] {"_", "*"}, StringSplitOptions.None);
        // string[] listaDeDadosIgneas = dadosIgneas.text.Split(new string[] { "*", "\n" }, StringSplitOptions.None);

        int tamanhoDaTabelaMinerais = (listaDeDadosMinerais.Length / num_colunasMinerais) - 1;
        Debug.Log(tamanhoDaTabelaMinerais);
        // int tamanhoDaTabelaIgneas = listaDeDadosIgneas.Length / num_colunasMinerais - 1;

        ListaMinerais.catalogoMinerais = new Mineral[tamanhoDaTabelaMinerais];
        // ListaIgneas.catalogoIgneas = new RochaIgnea[tamanhoDaTabelaIgneas];

        for (int i = 0; i < tamanhoDaTabelaMinerais; i++)
        {
            listaDeDadosMinerais[i] = listaDeDadosMinerais[i].Replace('"', ' ');
        }

        /* for (int i = 0; i < tamanhoDaTabelaIgneas; i++)
        {
             listaDeDadosMinerais[i] = listaDeDadosIgneas[i].Replace('"', ' ');
        }*/

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

            Debug.Log(i + ListaMinerais.catalogoMinerais[i].nomeTecnico);
            indicesRochasMinerais.Add(ListaMinerais.catalogoMinerais[i].nomeTecnico, i);
        }

        /*
         * for (int i = 0; i < tamanhoDaTabelaMinerais; i++)
        {
            ListaIgneas.catalogoIgneas[i] = new RochaIgnea();

            ListaIgneas.catalogoIgneas[i].nomeTecnico = listaDeDadosMinerais[num_colunasMinerais * (i + 1)].Replace('"', ' ');
            ListaIgneas.catalogoIgneas[i].tipo = "Mineral";
            ListaIgneas.catalogoIgneas[i].formula = listaDeDadosMinerais[num_colunasMinerais * (i + 1) + 2].Replace('"', ' ');

            indicesRochasMinerais.Add(ListaMinerais.catalogoMinerais[i].nomeTecnico, i);
        }
        */

        RectTransform tamanhoDaLista = containerDosItens.GetComponent<RectTransform>();

        float termoDeEscala = 235f * tamanhoDaTabelaMinerais;
        tamanhoDaLista.sizeDelta = new Vector2(0, termoDeEscala);

        for (int z = 0; z < tamanhoDaTabelaMinerais; z++)
        {
            // Instanciando o item.

            GameObject item = Instantiate(prefabMinerais, Vector3.zero, Quaternion.identity, containerDosItens.GetComponent<Transform>());
            item.name = ListaMinerais.catalogoMinerais[z].nomeTecnico;

            // Criando as vari�veis que representam nomeTecnico, tipo, f�rmula e imagem.

            Transform nomeTrans = item.transform.GetChild(0);
            TextMeshProUGUI textoNome = nomeTrans.GetComponent<TextMeshProUGUI>();

            Transform tipo = item.transform.GetChild(2);
            TextMeshProUGUI textoTipo = tipo.GetComponent<TextMeshProUGUI>();
            
            Transform formula = item.transform.GetChild(3);
            TextMeshProUGUI textoFormula = formula.GetComponent<TextMeshProUGUI>();

            Transform foto = item.transform.GetChild(4);
            Image componenteImagemFoto = foto.GetComponent<Image>();

            RectTransform rectTransform = item.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(-6, -z * 230 + 7043);

            // Preenchendo as informa��es.

            textoNome.SetText(ListaMinerais.catalogoMinerais[z].nomeTecnico);
            textoTipo.SetText(ListaMinerais.catalogoMinerais[z].tipo);
            textoFormula.SetText(ListaMinerais.catalogoMinerais[z].formula);
            componenteImagemFoto.sprite = Resources.Load<Sprite>("Amianto Crisotila");

            // Testar os tamanhos. >>> Perguntar pro Alfredo se posso botar a varia��o pra outro campo de texto
            // Descobrir se � poss�vel carregar imagens dinamicamente. (sim, falta implementar) (deixar pra depois)
            // Descobrir como criar as setinhas/outlines para qualquer bloco arbitr�rio de c�digo.
        }
    }
}