using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator anim;
    private Rigidbody rb;
    private bool pulando = false;
    public float ForcaDoPulo = 10f;

	void Start () {

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();	
	}
	
	void Update () {

        if (Input.GetMouseButtonDown(0)) {
            anim.Play("Pulando");
            rb.useGravity = true;
            pulando = true;

        }
	}

    void FixedUpdate() {
        if (pulando) {
            pulando = false;
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * ForcaDoPulo, ForceMode.Impulse);
        }

    }

    void OnCollisionEnter(Collision outro) {
       if (outro.gameObject.tag == "Obstaculo") {
            rb.AddForce(new Vector3(-50f, 20f, 0 ), ForceMode.Impulse);
            rb.detectCollisions = false;
            anim.Play("Morrendo");

        }
    }

}
