﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoelhoMove : MonoBehaviour {

    public float velocidadev;
    public float velocidadeh;
    public float min;
    public float max;
    public float limite;
    public float retorno;
    public float espera;


    void Start() {
        StartCoroutine(Move(max));
    }

    IEnumerator Move(float destino) {
        while (Mathf.Abs(destino - transform.localPosition.y) > 0.2f) {
            Vector3 direcao = (destino == max) ? Vector3.up : Vector3.down;
            Vector3 velocidadeVetorial = direcao * velocidadev;
            transform.localPosition = transform.localPosition + velocidadeVetorial * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(espera);

        destino = (destino == max) ? min : max;
        StartCoroutine(Move(destino));

    }
    void Update() {
        Vector3 velocidadevetorial = Vector3.left * velocidadeh;

        transform.localPosition = transform.localPosition + velocidadevetorial * Time.deltaTime;
    }
}