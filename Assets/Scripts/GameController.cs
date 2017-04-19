﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Estado estado { get; private set; } 

    public float espera;
    public GameObject obstaculo;
    public float tempodeDestruicao;
    public GameObject menu;
    public GameObject painelMenu;
    private int pontos;
    public Text txtPontos;

    public static GameController instancia = null;


    private void atualizarPontos(int x)
    {
        pontos = x;
        txtPontos.text = "" + x;
    }


    void Awake() {
        if (instancia == null) {
            instancia = this;
        }

        else if (instancia != null) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start () {

        estado = Estado.AguardandoComecar;
		
	}
	
	IEnumerator GerarObstaculos () {
        while (GameController.instancia.estado == Estado.Jogando) {
            Vector3 pos = new Vector3(14f, Random.Range(3.5f, 9.5f), 0f);
            GameObject obj = Instantiate(obstaculo, pos, Quaternion.Euler(0f, -90f, 0f)) as GameObject;
            Destroy(obj, tempodeDestruicao);
            yield return new WaitForSeconds(espera);

        }
	}

    public void PlayerComecou()
    {
        estado = Estado.Jogando;
        menu.SetActive(false);
        painelMenu.SetActive(false);
        atualizarPontos(0);
        StartCoroutine(GerarObstaculos());
    }



    public void PlayerMorreu() {
        estado = Estado.GameOver;
    }
}
