using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public Opcoes opt;
    int step = 0;
    [SerializeField] GameObject spotlight;
    [SerializeField] GameObject btn;
    [SerializeField] TextMeshProUGUI txt;
    [SerializeField] List<GameObject> checks;
    [SerializeField] List<GameObject> buttons;
    [SerializeField] List<Text> perguntas;
    [SerializeField] Text txtPergunta;
    [SerializeField] List<Button> Dica5050;
    [SerializeField] GameObject mask;
    Transform t;
    Transform b;
    public GameObject panelConfirma;
    public Animator Panel_anim;
    public TextMeshProUGUI panel_title;
    public TextMeshProUGUI panel_text;

    private Rect janelaDica = new Rect(0, 0, Screen.width, Screen.height);
    private bool showJanelaDica = false;

    private Rect janelaCerto = new Rect(0, 0, Screen.width, Screen.height);
    private bool showCerto = false;

    private Rect janelaErrado = new Rect(0, 0, Screen.width, Screen.height);
    private bool showErrado = false;

    // Adicionado Bonus
    public bool Acerto_Consecutivo = false;

    int quantidade_acertos = 0;

    public Text pergunta_tela;
    public Text alternativa1_tela;
    public Text alternativa2_tela;
    public Text alternativa3_tela;
    public Text alternativa4_tela;
    public Text pontuacao_tela;
    public Text valorQuestao;
    public Text dificuldade_tela;
    public Text numero_questao_tela;

    public Button botao_panel;
    public Button botao_panel_sim;
    public Button[] alternativas = new Button[4];
    public Button botao_pergunta;
    public Button confirmar;
    public Button ajuda5050;
    public Button dica;
    public Button pular;

    public GameObject[] barra_facil = new GameObject[17];
    public GameObject[] barra_medio = new GameObject[17];
    public GameObject[] barra_dificil = new GameObject[17];

    public GameObject[] risco = new GameObject[4];

    public TextAsset arquivo_texto;


    public Animator Panel_confirmar_anim;

    const int SIM = 1;
    const int NAO = 0;
    const int FACIL = 0;
    const int MEDIO = 1;
    const int DIFICIL = 2;
    const int INICIO = 0;
    const int MEIO = 1;
    const int STATUSOPCOES = 2;
    const int PROXPALAVRA = 3;
    const int MUDOU_NIVEL = 1;
    const int NAO_MUDOU_NIVEL = 0;

    const int JANELA = 1;
    const int JOGANDO = 0;

    public int quantidade_facil;
    public int quantidade_medio;
    public int quantidade_dificil;
    string[] perguntas_facil = new string[11];
    string[] perguntas_medio = new string[11];
    string[] perguntas_dificil = new string[11];
    int[] respostas_facil = new int[11];
    int[] respostas_medio = new int[11];
    int[] respostas_dificil = new int[11];
    string[,] respostas_possiveis_facil = new string[11, 4];
    string[,] respostas_possiveis_medio = new string[11, 4];
    string[,] respostas_possiveis_dificil = new string[11, 4];
    string[] dicas_facil = new string[11];
    string[] dicas_medio = new string[11];
    string[] dicas_dificil = new string[11];
    string[] audios_perguntas = new string[33];
    string[,] audios_alternativas = new string[33, 4];
    string[] audios_dicas = new string[33];

    int alternativa_correta;
    int pontos_ganhos;
    int alternativa_escolhida;
    int tirar_1;
    int tirar_2;
    public int pular_agora;

    public int questao_x_de_y;
    public int selecionou5050 = NAO;

    public int quantidade_5050 = 2;
    int quantidade_pular = 1;
    public int selecionou_pular = 0;
    public int nivel_atual = FACIL;
    int pontuacao;

    int estado;

    int Selecionado;

    float volume_efeitos, volume_musica, volume_texto;

    string txt_audio_pergunta;
    string txt_audio_a0;
    string txt_audio_a1;
    string txt_audio_a2;
    string txt_audio_a3;
    string txt_audio_dica;

    public AudioSource audio_pergunta;
    public AudioSource audio_a0;
    public AudioSource audio_a1;
    public AudioSource audio_a2;
    public AudioSource audio_a3;
    public AudioSource audio_dica;
    public AudioSource audio_transicoes;

    public AudioSource audio_botao_dica;
    public AudioSource audio_5050;
    public AudioSource audio_pular;

    public AudioSource audio_confirmar;

    System.Random random = new System.Random();

    //Utilizados para o double touch
    bool one_click = false;
    bool timer_running;
    float timer_for_double_click;

    float delay = 5.0f;

    // Declaracao de Variaveis
    public Image[] images;
    //public Shader[] shaders;

    public Sprite[] sprites_variacoes;

    public Image[] ajudas;

    public Sprite[] sprites_ajudas;

    public int pergunta_atual;

    public int[] perguntas_bool;
    public int[] ajudas_usadas;

    public int caminhos;


    private void Start()
    {
        t = spotlight.transform;
        b = btn.transform;
        foreach (GameObject b in checks)
        {
            b.GetComponent<Button>().interactable = false ;
        }
        Informacoes.setTutorial(true);

    }


    public void MostrarDica()
    {
        Panel_anim.SetBool("showPanel", true);
        panel_text.gameObject.SetActive(true);
        panel_text.text = "Um quilograma possui 1000 gramas!";

    }

    public void EscondeDica()
    {
        Panel_anim.SetBool("showPanel", false);
    }

    public void Mostrar5050()
    {
        for(int i = 2; i <= 3; i++)
        {

            checks[i].GetComponent<Button>().interactable = false;

        }

    }
    public void GoToNextStep()
    {
        int l = 1;
        switch (step)
        {
            case 0:
                spotlight.transform.position = new Vector3(t.position.x, -1.2f, 0) ;
                spotlight.transform.localScale = new Vector3(7.23f, 2f, 1);
                
                btn.transform.localScale = new Vector3(1, 1, 1);
                //btn.transform.localPosition = new Vector3(320, -158, 0);
                //btn.transform.localPosition = new Vector3(320, -124, 0);
                txtPergunta.text = "Aqui est�o as poss�veis respostas para as perguntas.";
                
                break;
            case 1:
                txtPergunta.text = "Escolha a primeira op��o clicando no quadrado.";
                //btn.transform.position = checks[0].transform.position;
                btn.transform.localScale = new Vector3(1, 1, 1);
                btn.SetActive(false);
                checks[0].GetComponent<Button>().interactable = true;
                break;

            case 2:
                txtPergunta.text = "Muito bem!";
                btn.transform.localScale = new Vector3(1, 1, 1);
                
                btn.GetComponent<Image>().enabled = true;
                btn.SetActive(true);
                checks[0].GetComponent<Button>().interactable = false;
                break;
            case 3:
                spotlight.gameObject.SetActive(false);

                btn.transform.localScale = new Vector3(1, 1, 1);
                
                //btn.transform.localPosition = new Vector3(320, -124, 0);
                txtPergunta.text = "Leia a pergunta e escolha a que voc� acha que seja a resposta!\n";
                txtPergunta.text = txtPergunta.text + "Quantos lados tem um tri�ngulo?";
                    
                
                foreach (GameObject b in checks)
                {
                    b.GetComponent<Button>().interactable = true;
                    b.transform.GetChild(0).GetComponent<Text>().text = l.ToString() ;
                    l++;
                }
                mask.SetActive(false);
                btn.SetActive(false);
                spotlight.transform.localScale = new Vector3(17.23f, 12f, 1);

                panel_text.gameObject.SetActive(true);
                break;
            case 4:
                foreach (GameObject b in checks)
                {
                    b.GetComponent<Button>().interactable = false;
                    b.transform.GetChild(0).GetComponent<Text>().text = l.ToString();
                    l++;
                }
                txtPergunta.text = "Muito bem!\n Voc� j� aprendeu a escolher a sua resposta!";
                btn.transform.localScale = new Vector3(1, 1, 1);
                
                btn.SetActive(true);
                break;

            case 5:
                spotlight.gameObject.SetActive(false);

                btn.transform.localScale = new Vector3(1, 1, 1);
                
                txtPergunta.text = "Se tiver d�vidas, basta clicar nos bot�es de ajuda abaixo!\n";

                foreach (Button b in Dica5050)
                {
                    b.gameObject.SetActive(true);
                    b.interactable = false;
                }


                foreach (GameObject b in checks)
                {
                    b.GetComponent<Button>().interactable = false;
                    b.transform.GetChild(0).GetComponent<Text>().text = "";
                    
                }
                mask.SetActive(true);

                spotlight.transform.localScale = new Vector3(17.23f, 12f, 1);

                panel_text.gameObject.SetActive(true);
                btn.SetActive(true);
                break;
            case 6:
                spotlight.gameObject.SetActive(false);

                btn.transform.localScale = new Vector3(1, 1, 1);

                txtPergunta.text = "- 50/50 ir� eliminar duas alternativas erradas!\n";
                txtPergunta.text = txtPergunta.text + "- Dica ir� dar uma pista da resposta!";
                //txtPergunta.text = txtPergunta.text + "- Se quiser, pode ir para a pr�xima pergunta com o 'Pular'!";
                foreach (Button b in Dica5050)
                {
                    b.gameObject.SetActive(true);
                    b.interactable = false;
                }


                foreach (GameObject b in checks)
                {
                    b.GetComponent<Button>().interactable = false;
                    b.transform.GetChild(0).GetComponent<Text>().text = "";

                }
                mask.SetActive(true);

                spotlight.transform.localScale = new Vector3(17.23f, 12f, 1);

                panel_text.gameObject.SetActive(true);
                btn.SetActive(true);
                break;
            case 7:

                spotlight.gameObject.SetActive(false);

                btn.transform.localScale = new Vector3(1, 1, 1);
                
                //btn.transform.localPosition = new Vector3(320, -124, 0);
                txtPergunta.text = "Quantos quilometros tem em um metro?";


                foreach (GameObject b in checks)
                {
                    b.GetComponent<Button>().interactable = true;
                    
                    l++;
                }
                checks[0].transform.GetChild(0).GetComponent<Text>().text = "60";
                checks[1].transform.GetChild(0).GetComponent<Text>().text = "1.000";
                checks[2].transform.GetChild(0).GetComponent<Text>().text = "1.024";
                checks[3].transform.GetChild(0).GetComponent<Text>().text = "10.000";
                mask.SetActive(false);
                btn.SetActive(false);
                spotlight.transform.localScale = new Vector3(17.23f, 12f, 1);

                foreach (Button b in Dica5050)
                {
                    b.gameObject.SetActive(true);
                    b.interactable = true;
                }
                panel_text.gameObject.SetActive(true);
                break;

            case 8:
                foreach (Button b in Dica5050)
                {
                    b.gameObject.SetActive(true);
                    b.interactable = false;
                }


                foreach (GameObject b in checks)
                {
                    b.GetComponent<Button>().interactable = false;
                    b.transform.GetChild(0).GetComponent<Text>().text = "";

                }

                txtPergunta.text = "�timo! Voc� j� pegou o jeito!\n J� pode finalizar o Tutorial!";
                btn.SetActive(true);
                break;
            case 9:
                sairTutorial();
                break;
        }

        step++;

    }

    private void OnDestroy()
    {
        
    }

    public void AcessarOpcoes()
    {
#if UNITY_ANDROID
            if(!one_click){
                one_click = true;
                timer_for_double_click = Time.time;

                //Audio opcoes
            }
            else{
                one_click = false;

                Informacoes.SetStatus(STATUSOPCOES);
                SalvarInfos();
                SceneManager.LoadScene("Opcoes");
            }
#else
        Informacoes.SetStatus(2);
        SalvarInfos();
        Informacoes.setTutorial(true);
        SceneManager.LoadScene("Opcoes");
        
        
#endif
    }

    private void SalvarInfos()
    {
        Informacoes.SetQuantidadeFacil(quantidade_facil);
        Informacoes.SetQuantidadeMedio(quantidade_medio);
        Informacoes.SetQuantidadeDificil(quantidade_dificil);
        Informacoes.SetNivel(nivel_atual);
        Informacoes.SetPontos(pontuacao);
        Informacoes.SetPerguntasFacil(perguntas_facil);
        Informacoes.SetRespostasFacil(respostas_facil);
        Informacoes.SetRespostasPossiveisFacil(respostas_possiveis_facil);
        Informacoes.SetDicasFacil(dicas_facil);
        Informacoes.SetPerguntasMedio(perguntas_medio);
        Informacoes.SetRespostasMedio(respostas_medio);
        Informacoes.SetRespostasPossiveisMedio(respostas_possiveis_medio);
        Informacoes.SetDicasMedio(dicas_medio);
        Informacoes.SetPerguntasDificil(perguntas_dificil);
        Informacoes.SetRespostasDificil(respostas_dificil);
        Informacoes.SetRespostasPossiveisDificil(respostas_possiveis_dificil);
        Informacoes.SetDicasDificil(dicas_dificil);
        Informacoes.SetStatus5050(selecionou5050);

        Informacoes.SetAudiosPerguntas(audios_perguntas);
        Informacoes.SetAudiosAlternativas(audios_alternativas);
        Informacoes.SetAudiosDicas(audios_dicas);

        Informacoes.SetAcertoConsecutivo(Acerto_Consecutivo);
        Informacoes.SetQuantidadeAcertos(quantidade_acertos);
        Informacoes.SetPontosGanhos(pontos_ganhos);

        Informacoes.SetNumeroQuestao(questao_x_de_y);
        Informacoes.SetPerguntaAtual(pergunta_atual);
        Informacoes.SetPerguntasRespondidas(perguntas_bool);
        Informacoes.SetAjudasUsadas(ajudas_usadas);

        Informacoes.SetQuantidade5050(quantidade_5050);
        Informacoes.SetQuantidadePular(quantidade_pular);
        Informacoes.SetStatusPular(selecionou_pular);
        Informacoes.setTutorial(true);
        if (selecionou5050 == SIM)
            Informacoes.SetTirar(tirar_1, tirar_2);

    }

    public void sairTutorial()
    {
        if(Informacoes.getTelaChamou() == "Narrativa")
        {
            SceneManager.LoadScene("Nivel");

        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
        
    }

    public void showConfirma(bool b)
    {
        panelConfirma.SetActive(b);
    }

}
