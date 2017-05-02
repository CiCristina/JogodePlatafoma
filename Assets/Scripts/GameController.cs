﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public Estado estado { get; private set; }


    public float espera;
    public GameObject obstaculo;
    public float tempodeDestruicao;
    public GameObject menuCamera;
    public GameObject menuPanel;
    private int pontos;
    public GameObject gameOverPanel;
    public GameObject pontosPanel;
    public Text txtPontos;
    public Text txtMaiorPontuacao;


    public static GameController instancia = null;


    private void atualizarPontos(int x)
    {
        pontos = x;
        txtPontos.text = "" + x;
    }

    public void incrementarPontos(int x)
    {
        atualizarPontos(pontos + x);
    }


    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }

        else if (instancia != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        pontos = 0;
        estado = Estado.AguardandoComecar;
        PlayerPrefs.SetInt("HighScore", 0);
        menuCamera.SetActive(true);
        menuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        pontosPanel.SetActive(false);


    }

    IEnumerator GerarObstaculos()
    {
        while (GameController.instancia.estado == Estado.Jogando)
        {
            Vector3 pos = new Vector3(14f, Random.Range(3.5f, 9.5f), 0f);
            GameObject obj = Instantiate(obstaculo, pos, Quaternion.Euler(0f, -90f, 0f)) as GameObject;
            Destroy(obj, tempodeDestruicao);
            yield return new WaitForSeconds(espera);

        }
    }


    public void PlayerComecou()
    {
        estado = Estado.Jogando;
        menuCamera.SetActive(false);
        menuPanel.SetActive(false);
        pontosPanel.SetActive(true);
        atualizarPontos(0);
        StartCoroutine(GerarObstaculos());
    }



    public void PlayerMorreu()
    {
        estado = Estado.GameOver;
        if (pontos > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", pontos);
            txtMaiorPontuacao.text = "" + pontos;
        }
        gameOverPanel.SetActive(true);
    }



    public void PlayerVoltou()
    {
        estado = Estado.AguardandoComecar;
        menuCamera.SetActive(true);
        menuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        pontosPanel.SetActive(false);
        GameObject.Find("pu").GetComponent<PudimController>().recomecar();
    }


}