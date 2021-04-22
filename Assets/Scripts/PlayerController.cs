using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private bool canMove;
    private Rigidbody2D theRB2D;
    private Animator theAnimator;
    // Start is called before the first frame update
    void Start()
    {
        theRB2D = GetComponent<Rigidbody2D>();
        theAnimator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            canMove = true;
        }
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        if (canMove)
        {
            theRB2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed,
            theRB2D.velocity.y);
            theAnimator.SetFloat("Speed", Mathf.Abs(theRB2D.velocity.x));
            if(theRB2D.velocity.x > 0)
                transform.localScale = new Vector2(1f, 1f);
            else if(theRB2D.velocity.x < 0)
                transform.localScale = new Vector2(-1f, 1f);
        }
    }
}
