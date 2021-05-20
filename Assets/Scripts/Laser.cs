using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Laser : MonoBehaviour
{
    public GameObject hitFX;

    public int damageToDeal;
    private Rigidbody2D theRB2D;
    
        // Start is called before the first frame update
        void Start()
        {
            theRB2D = transform.parent.GetComponent<Rigidbody2D>();
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Hurtbox")
            {
                other.gameObject.GetComponent<EnemyHP>().TakeDamage(damageToDeal);
                
            }
            GameObject effect = Instantiate(hitFX, transform.position, Quaternion.identity);
            Destroy(effect, 0.3f);
            Destroy(gameObject);
        }
        
}