using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteoro : MonoBehaviour
{   
    [SerializeField]private AudioClip deathSound;
    private Rigidbody2D rb;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= -6){
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D colidingObjct) {
        if(colidingObjct.gameObject.tag == "Laser"){
            Destroy(colidingObjct.gameObject);
            StartCoroutine(Death());
        }
    }
    IEnumerator Death ()
    {
            ScoreUpdater.addValueToCurrentScore(1);
            rb.simulated = false;
            anim.SetBool("isDead",true);
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            yield return new WaitForSeconds(0.61f);    
            Destroy(gameObject);
    }

}
