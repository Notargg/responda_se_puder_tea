using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class FimJogo : MonoBehaviour
{
    public Text pontos;
    public Text bonus_tela;
    public Text x5050;
    public Text pular;
    public Text pontos_final;
    public Button botao;
    public Button prosseguir;

    public Text texto_prosseguir;

    public CanvasGroup canvasGroup;


    public Image imagem;
    public Sprite[] spritearray;

    double bonus_d;
    int pontuacao;
    int bonus;

    int bonus_5050;
    int bonus_pular;

    private bool fadein;

    void Start()
    {
        
        // imagem.SetActive(false);

        canvasGroup.alpha = 0;
        fadein = false;
        pontuacao = Informacoes.GetPontos();
        Informacoes.SetCaminhos(1);
        CalcularBonus();
        botao.onClick.AddListener(() => Voltar());
    }

    void Voltar(){
		SceneManager.LoadScene("Menu");
	}
    
    private void ColocarPontuacao()
    {
        pontos.text = pontuacao.ToString();
        
    }

    void Update()
    {

        if (fadein)
        {
            canvasGroup.alpha += Time.deltaTime;
        }

        if (canvasGroup.alpha >= 1)
        {
            fadein = false;
        }

        ColocarPontuacao();

    }

    public void Finalizar()
    {

        fadein = true;

        pontuacao = Informacoes.GetPontos();
        pontuacao = 0;

        if (pontuacao == 0)
        {
            imagem.sprite = spritearray[0];
        }
        else if (pontuacao == 1)
        {
            imagem.sprite = spritearray[1];
        }
        else if (pontuacao == 2)
        {
            imagem.sprite = spritearray[2];
        }
        else if (pontuacao == 3)
        {
            imagem.sprite = spritearray[3];
        }

        prosseguir.enabled = false;
        texto_prosseguir.text = "";
    }

    public void MenuVoltar(){
        Informacoes.SetStatus(0);
        SceneManager.LoadScene("Menu");
    
    }


    private void CalcularBonus()
    {
        // bonus_d = (Informacoes.GetQuantidadeFacil() + Informacoes.GetQuantidadeMedio() + Informacoes.GetQuantidadeDificil()) * 0.2;
        // bonus = Convert.ToInt32(bonus_d) * 10;
        bonus = 10;

        //Teste
        // Informacoes.SetQuantidade5050(2);
        //  Informacoes.SetQuantidadePular(1);

        bonus_5050 = Informacoes.GetQuantidade5050() * bonus;
        bonus_pular = Informacoes.GetQuantidadePular() * bonus;

        if (Informacoes.GetQuantidade5050() > 0 || Informacoes.GetQuantidadePular() > 0)
        {
            bonus_tela.text = "Você desbloqueou conquistas e ganhou bônus na pontuação.\n\n";
            bonus_tela.text += "Bônus acumulados: ";

            if (Informacoes.GetQuantidade5050() != 0)
            {
                x5050.text = "Não usou a ajuda 5050: + " + bonus_5050.ToString() + " pontos!";
                pontuacao = pontuacao + bonus_5050;
            }
            if (Informacoes.GetQuantidadePular() != 0)
            {
                pular.text = "Não usou a ajuda Pular: + " + bonus_pular.ToString() + " pontos!";
                pontuacao = pontuacao + bonus_pular;
            }
            pontos_final.text = "Pontuação atualizada: " + pontuacao.ToString();
            Informacoes.SetPontos(pontuacao);
        }
    }
}
