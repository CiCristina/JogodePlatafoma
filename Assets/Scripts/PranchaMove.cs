
using UnityEngine;

public class PranchaMove : MonoBehaviour {

    private Animator anim;
    private Rigidbody rb;
    private AudioSource audiosource;
    public AudioClip somMorte;

    public float velocidade;
    public float limite;
    public float retorno;

    void Start() {

    }

    void Update() {
        Vector3 velocidadevetorial = Vector3.left * velocidade;

        transform.localPosition = transform.localPosition + velocidadevetorial * Time.deltaTime;

        if (transform.localPosition.x <= limite) {
            transform.localPosition = new Vector3(retorno, transform.localPosition.y, transform.localPosition.z);
        }
    }
    void OnCollisionEnter(Collision outro) {
        if (GameController.instancia.estado == Estado.Jogando) {
            if (outro.gameObject.tag == "Obstaculo") {
                rb.AddForce(new Vector3(-50f, 20f, 0), ForceMode.Impulse);
                rb.detectCollisions = false;
                anim.Play("Morrendo");
                audiosource.PlayOneShot(somMorte);
                GameController.instancia.PlayerMorreu();
            }
        }
    }
}