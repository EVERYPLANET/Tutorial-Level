using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player Visual Model
    private GameObject playerModel;
    
    //Player Rigid Body
    private Rigidbody playerRB;

    //Player Speed Variables
    public float speed = 0f;
    //public float moveX = 0f;

    //Player Jump Variables
    public float jumpStrength = 0f;
    private bool hasDoubleJump = false;
    private int totalJumps = 0;
    private bool isGrounded = true;
    
    // Start is called before the first frame update
    void Start()
    {
        playerModel = GameObject.FindGameObjectWithTag("PlayerModel");
        playerRB = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        JumpHandler();
    }
    
    // Player AD Movement
    private void PlayerMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        moveX = Input.GetAxisRaw("Horizontal");

        Vector3 scale = transform.localScale;

        
        //Player Direction
        if (moveX > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }

        if (moveX < 0)
        {
            scale.x = -Mathf.Abs(scale.x);
        }

        if (Mathf.Abs(moveX) > 0)
        {
            playerModel.transform.localScale = scale;
        }

        transform.position += new Vector3(moveX, 0f, 0f) * (speed) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider col)
    {
        //print(col.tag);
        if (col.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (col.CompareTag("Bounce"))
        {
            print("collision");
            Physics.IgnoreCollision(GetComponent<Collider>(),col , true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //print(other.tag);
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    
    // Jump Handler
    private void JumpHandler()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            playerRB.AddForce(new Vector2(0f, jumpStrength), ForceMode.Impulse);
        }
    }
}
