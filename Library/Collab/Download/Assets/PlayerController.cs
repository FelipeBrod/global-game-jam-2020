using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // "Public" variables
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpForce = 500.0f;
    // [SerializeField] private Transform groundCheckPos;
    // [SerializeField] private LayerMask whatIsGround;
    // [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] bool mirrorring;

    // Private variables
    // private bool isGrounded = false;
    private Rigidbody rBody;
    // private Animator anim;
    private bool isFacingRight = true;
    // private bool isCrouching = false;
    public int direction = 1;
    
    public GameObject playerCam;

    // Start is called before the first frame update
    void Start()
    {
        if(mirrorring)
        {
            direction = -1;
        }

        rBody = GetComponent<Rigidbody>();
        // anim = GetComponent<Animator>();
    }

    // Physics
    void FixedUpdate()
    {
        Vector2 movement = Vector2.zero;
        float horiz = direction * Input.GetAxis("Horizontal");

        // isGrounded = GroundCheck();

        // Jumping code will go here!
        // if(isGrounded && Input.GetAxis("Jump") > 0)
        // {
        //     rBody.AddForce(new Vector2(0.0f, jumpForce));
        //     isGrounded = false;
        // }

        rBody.velocity = new Vector3(horiz * speed, rBody.velocity.y);


        // // Check if sprite is crouching
        // if(isGrounded && rBody.velocity.x == 0 && Input.GetAxis("Vertical") < 0)
        // {
        //     isCrouching = true;
        // }
        // else
        // {
        //     isCrouching = false;
        // }

        // Check if sprite needs to be flipped
        // if(isFacingRight && rBody.velocity.x < 0)
        // {
        //     Flip();
        // }
        // else if (!isFacingRight && rBody.velocity.x > 0)
        // {
        //     Flip();
        // }


        // anim.SetFloat("xSpeed", Mathf.Abs(rBody.velocity.x));
        // anim.SetFloat("ySpeed", rBody.velocity.y);
        // anim.SetBool("isGrounded", isGrounded);
        // anim.SetBool("isCrouching", isCrouching);
    }

    // private bool GroundCheck()
    // {
    //     return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround); ;
    // }

    // private void Flip()
    // {
    //     Vector3 temp = transform.localScale;
    //     temp.x *= -1;
    //     transform.localScale = temp;

    //     isFacingRight = !isFacingRight;
    // }
}