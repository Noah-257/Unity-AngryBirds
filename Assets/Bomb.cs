using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float impactField;
    public float force;
    public LayerMask lmToHit;
    public GameObject explosionPrefab;

    
    void Update()
    {
        if(Input.GetMouseButton(0)){
            Explosion();
        }
       
    }

    void Explosion(){
         Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, impactField, lmToHit);
        foreach(Collider2D obj in objects){
            Vector2 dir = obj.transform.position - transform.position;

            obj.GetComponent<Rigidbody2D>().AddForce(dir * force);
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, impactField);
    }
}
