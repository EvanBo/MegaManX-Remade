using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LifeUp : MonoBehaviour
{
    private LivesManager theLM;
    // Start is called before the first frame update
    void Start()
    {
        theLM = FindObjectOfType<LivesManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            theLM.AddLife();
            Destroy(gameObject);
            ScoreManager.instance.AddPoint();
        }
    }
}