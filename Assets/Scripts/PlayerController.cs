using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float dfltSpeed;
    public float jumpForce;
    private bool canMove;
    private Rigidbody2D theRB2D;
    public bool grounded;
    public LayerMask whatIsGrd;
    public Transform grdChecker;
    public float grdCheckerRad;
    public float airTime;
    public float airTimeCounter;
    private bool ctrlActive;
    private bool isDead;
    private Collider2D playerCol;
    public GameObject[] childObjs;
    public float shockForce;
    private Animator theAnimator;
    public GameManager theGM;
    private LivesManager theLM;
    // Start is called before the first frame update
    void Start()
    {
        theLM = FindObjectOfType<LivesManager>();
        theRB2D = GetComponent<Rigidbody2D>();
        theAnimator = GetComponent<Animator>();
        playerCol = GetComponent<Collider2D>();
        airTimeCounter = airTime;
        dfltSpeed = speed;
        ctrlActive = true;
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
        grounded = Physics2D.OverlapCircle(grdChecker.position, grdCheckerRad, whatIsGrd);
        if (ctrlActive == true)
        {

            MovePlayer();
            Jump();
        }
    }
    void MovePlayer()
    {
        if (canMove)
        {
            theRB2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed,
            theRB2D.velocity.y);
            theAnimator.SetFloat("Speed", Mathf.Abs(theRB2D.velocity.x));
            if (theRB2D.velocity.x > 0)
                transform.localScale = new Vector2(1f, 1f);
            else if (theRB2D.velocity.x < 0)
                transform.localScale = new Vector2(-1f, 1f);
        }
    }
    void Jump()
    {
        if (grounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                theRB2D.velocity = new Vector2(theRB2D.velocity.x, jumpForce);
            }
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if (airTimeCounter > 0)
            {
                theRB2D.velocity = new Vector2(theRB2D.velocity.x, jumpForce);
                airTimeCounter -= Time.deltaTime;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            airTimeCounter = 0;
        }
        if (grounded)
        {
            airTimeCounter = airTime;
        }
        theAnimator.SetBool("Grounded", grounded);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "Spike") || (other.gameObject.tag == "Enemy"))
        {
            Debug.Log("Ouch!");
            theLM.TakeLife();
            PlayerDeath();
        }
    }
    void PlayerDeath()
    {

        isDead = true;
        theAnimator.SetBool("Dead", isDead);
        ctrlActive = false;
        playerCol.enabled = false;
        foreach (GameObject child in childObjs)
            child.SetActive(false);
        theRB2D.gravityScale = 0f;
        theRB2D.AddForce(transform.up * shockForce, ForceMode2D.Impulse);
        StartCoroutine("PlayerRespawn");
    }
    IEnumerator PlayerRespawn()
    {
        yield return new WaitForSeconds(1.5f);
        isDead = false;
        theAnimator.SetBool("Dead", isDead);
        playerCol.enabled = true;
        foreach (GameObject child in childObjs)
            child.SetActive(true);
        theRB2D.gravityScale = 1f;
        yield return new WaitForSeconds(0.1f);
        ctrlActive = true;
        theGM.Reset();
    }
}