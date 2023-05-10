using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player Visual Model
    public GameObject playerModel;
    
    //Player Rigid Body
    private Rigidbody playerRB;

    //Player Speed Variables
    public float speed = 0f;

    public float currentSpeed = 0f;
    //public float moveX = 0f;

    //Player Jump Variables
    public float jumpStrength = 0f;
    private bool hasDoubleJump = false;
    private int totalJumps = 0;
    private bool isGrounded = true;
    
    //Controls on?
    public bool controlsActive = true;
    
    //Cannon Script
    public Cannon cannon;
    
    //Inventory
    public List<string> inventory = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        playerModel = GameObject.FindGameObjectWithTag("PlayerModel");
        playerRB = GetComponent<Rigidbody>();

        cannon = GetComponentInChildren<Cannon>();

    }

    // Update is called once per frame
    void Update()
    {
        if (controlsActive)
        {
            PlayerMovement();
            JumpHandler();
        }
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
        
        currentSpeed = moveX * speed * Time.deltaTime;

        if (Mathf.Abs(currentSpeed) > 0)
        {
            cannon.isStill = false;
        }
        else
        {
            cannon.isStill = true;
        }

        transform.position += new Vector3(currentSpeed, 0f, 0f);
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

    public void addInventory(string itemID, int quantity)
    {
        for (int i = 0; i < quantity; i++)
        {
            inventory.Add(itemID);
        }
        
    }

    public bool CheckInventory(string itemID)
    {
        return inventory.Contains(itemID);
    }
}
