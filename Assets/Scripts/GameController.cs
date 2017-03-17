using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public float espera;
    public GameObject obstaculo;
    public float tempodeDestruicao;

    public static GameController instancia = null;


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

        StartCoroutine(GerarObstaculos());
		
	}
	
	IEnumerator GerarObstaculos () {
        while (true) {
            Vector3 pos = new Vector3(14f, Random.Range(7f, 1.0f), 0f);
            GameObject obj = Instantiate(obstaculo, pos, Quaternion.Euler(0f, -90f, 0f)) as GameObject;
            Destroy(obj, tempodeDestruicao);
            yield return new WaitForSeconds(espera);

        }
		
	}
}
