using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WalkerMove : MonoBehaviour
{
    public float moveSpeed;
    public float rayLength;
    private bool moveLeft;
    public Transform contactChecker;
    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        int layerMask = 1 << 9;
        layerMask = ~layerMask;
        RaycastHit2D contactCheck = Physics2D.Raycast(contactChecker.position, Vector2.down,
        rayLength, layerMask);
        Debug.DrawRay(contactChecker.position, Vector2.down * rayLength, Color.red);
        if (contactCheck == false)
        {
            if (moveLeft == true)
            {
                transform.eulerAngles = new Vector2(0, -180);
                moveLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 0);
                moveLeft = true;
            }
        }
    }
}