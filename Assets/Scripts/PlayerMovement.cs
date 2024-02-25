using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D PlayerRigid;
    [SerializeField] private float x_velocity = 6.5f;
    //private float x_force = 5.0f;
    private float dirX = 0f;
    private CapsuleCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;

    private Animator anim;
    private SpriteRenderer spriterend;
    private enum MovementState { idle, running, jumping, falling }
    //private MovementState state = MovementState.idle; Don't need this no more, we use animator

    [SerializeField] private float jumpForce = 7.5f; /* With SerializeField, further changes made
                                              in initial value WON'T be reflected in editor, 
                                              to fix this, simply right-click and Reset the Script component.*/

    private void Start()
    {
        /* This is the Start function 
         for initialization and shite*/
        Debug.Log("\tGame is up and running.");
        PlayerRigid = this.gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent <Animator>();
        spriterend = GetComponent<SpriteRenderer>();
        coll = GetComponent<CapsuleCollider2D>();
    }

    private bool IsGrounded()
    {
        return Physics2D.CapsuleCast(coll.bounds.center, coll.bounds.size, CapsuleDirection2D.Vertical,  0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void Update()
    {
        //GetAxisRaw doesn't scale up or down from its value in an ease-in-ease-out manner.
        //Instead it snaps to value
        dirX = Input.GetAxisRaw("Horizontal");
        PlayerRigid.velocity = new Vector2(dirX * x_velocity, PlayerRigid.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            //PlayerRigid.velocity += new Vector2(0, jumpForce);
            PlayerRigid.velocity = new Vector2(PlayerRigid.velocity.x, jumpForce);
            //Debug.Log("Player velocity is "+ PlayerRigid.velocity.ToString());
        }

        AnimationUpdate();
        
        //void homebrewX()
        //{
        //    //This was a 'Martin Attempt' to do horizontal movement,
        //    //before being introduced to input manager, used above and the GetAxis func.
        //    if (Input.GetKeyDown("right"))
        //    {
        //        //PlayerRigid.velocity += new Vector2(4, 0);
        //        PlayerRigid.velocity += Vector2.right * 4;
        //    }
        //    if (Input.GetKeyDown("left"))
        //    {
        //        PlayerRigid.velocity += new Vector2(-4, 0);
        //    }
        //}

    }

    private void AnimationUpdate()
    {
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.running;
            spriterend.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            spriterend.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (PlayerRigid.velocity.y > 0.05f) //In order to account for imprecision
        {
            state = MovementState.jumping;
        }
        else if (PlayerRigid.velocity.y < -0.05f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private void FixedUpdate()
    {
    //    
    //    PlayerRigid.AddForce(new Vector2(dirX * x_force, 0));

    }
}
