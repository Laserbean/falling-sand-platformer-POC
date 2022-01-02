using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2DP : MonoBehaviour
{
    [SerializeField] bool flymode; 
    public float speed = 5f;
    public Camera mainCamera;
    public float gravityScale = 1.5f;
    public float jumpSpeed = 1f;

    private Vector2 moveVelocity;

    private Rigidbody2D rb2d; ///unity thing 
    Collider2D mainCollider;
    Vector3 cameraPos;
    Transform trans;


    float moveDirection = 0f;
    float moveDirection2 = 0f;

    bool isGrounded = false;
    bool facingRight = true;
    bool goingUp = true;


    // Check every collider except Player and Ignore Raycast
    LayerMask layerMask = ~(1 << 2 | 1 << 8);
    
    void Start() // Start is called before the first frame update
    {
        trans = transform;
        rb2d = GetComponent<Rigidbody2D>(); // attaches from the thing 
        mainCollider = GetComponent<Collider2D>();
        rb2d.freezeRotation = true; 
        rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        //rb2d.gravityScale = gravityScale;
        if(mainCamera)
            cameraPos = mainCamera.transform.position;
    }

    void Update() // Update is called once per frame
    {
        // Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));
        
        moveDirection = Input.GetAxisRaw("Horizontal");
        moveDirection2 = Input.GetAxisRaw("Vertical");

        moveVelocity = new Vector2(moveDirection * speed, 0);
        // Debug.Log(moveDirection);

        if (moveDirection > 0 && !facingRight) {//going right
            facingRight = true;
            trans.localScale = new Vector3(Mathf.Abs(trans.localScale.x), trans.localScale.y, trans.localScale.z);
        } 
        if (moveDirection <0 && facingRight) {
            facingRight = false;
            trans.localScale = new Vector3(-Mathf.Abs(trans.localScale.x), trans.localScale.y, trans.localScale.z);
        }


        if (!flymode && (moveDirection2 > 0 || Input.GetKeyDown(KeyCode.W)) && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
        } else {
            // if (moveDirection2 > 0 && !goingUp) {
            //     goingUp = true;
            //     trans.localScale = new Vector3(trans.localScale.x, Mathf.Abs(trans.localScale.y), trans.localScale.z);
            // } 
            // if (moveDirection2 < 0 && goingUp) {
            //     goingUp = false; 
            //     trans.localScale = new Vector3(trans.localScale.x, -Mathf.Abs(trans.localScale.y), trans.localScale.z);
            // }

        }

        if(mainCamera) {
            mainCamera.transform.position = new Vector3(trans.position.x, trans.position.y+2, cameraPos.z);
        }
        // if (trans.position.y < -20f){
        //         trans.position = Vector3.zero;
        // }

    }

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, 0.1f, 0);
        isGrounded = Physics2D.OverlapCircle(groundCheckPos, 0.23f, layerMask);

        // rb2d.MovePosition(rb2d.position + moveVelocity * Time.fixedDeltaTime);
        if (!flymode) {
            rb2d.velocity = new Vector2((moveDirection) * speed, rb2d.velocity.y);
        } else{ 
            rb2d.velocity = new Vector2((moveDirection) * speed, (moveDirection2) * speed);
        }
        
        // Debug.DrawLine(rb2d.position, colliderBounds.min + new Vector3(colliderBounds.size.x * 0.4f, 0, 0), Color.red);
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, 0.23f, 0), isGrounded ? Color.green : Color.red);
    }

    // private void OnDrawGizmos() {
    //     Gizmos.DrawSphere(trans.position, 1);
    // }
}
