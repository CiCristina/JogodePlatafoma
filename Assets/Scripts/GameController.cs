using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


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
    private List<GameObject> obstaculos;



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
        obstaculos = new List<GameObject>();
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
            Vector3 pos = new Vector3(12.37f, Random.Range(4.5f, 9f), 0f);
            GameObject obj = Instantiate(obstaculo, pos, Quaternion.Euler(0f, -90f, 0f)) as GameObject;
            obstaculos.Add(obj);
            StartCoroutine(DestruirObstaculo(obj));
            yield return new WaitForSeconds(espera);
        }
    }


    IEnumerator DestruirObstaculo(GameObject obj)
    {
        yield return new WaitForSeconds(tempodeDestruicao);
        if (obstaculos.Remove(obj))
        {
            Destroy(obj);
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
        while (obstaculos.Count > 0)
        {
            GameObject obj = obstaculos[0];
            if (obstaculos.Remove(obj))
            {
                Destroy(obj);
            }
        }
        estado = Estado.AguardandoComecar;
        menuCamera.SetActive(true);
        menuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        pontosPanel.SetActive(false);
        GameObject.Find("pu").GetComponent<PudimController>().recomecar();
    }


}