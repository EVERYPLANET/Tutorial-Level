using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player Visual Model
    public GameObject playerModel;
    private Animator modelAnimator;
    
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
    
    //Audio Manager
    public AudioManager AM;
    public string clipName;

    // Start is called before the first frame update
    void Start()
    {
        playerModel = GameObject.FindGameObjectWithTag("PlayerModel");
        playerRB = GetComponent<Rigidbody>();
        modelAnimator = playerModel.GetComponent<Animator>();

        cannon = GetComponentInChildren<Cannon>();

    }

    // Update is called once per frame
    void Update()
    {
        if (controlsActive)
        {
            PlayerMovement();
            JumpHandler();

            //print(currentSpeed);
            modelAnimator.SetFloat("speed", Mathf.Abs(currentSpeed));
        }
    }
    
    // Player AD Movement
    private void PlayerMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        moveX = Input.GetAxisRaw("Horizontal");

        Vector3 rotation = playerModel.transform.eulerAngles;

        //Player Direction
        if (moveX > 0)
        {
            rotation.y = 110;
        }

        if (moveX < 0)
        {
            rotation.y = 250;
        }

        if (Mathf.Abs(moveX) > 0)
        {
            playerModel.transform.eulerAngles = rotation;
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
        AM.playClip(clipName);
        print("You got " +quantity.ToString() + " "+ itemID.ToString()+ "!");
        for (int i = 0; i < quantity; i++)
        {
            inventory.Add(itemID);
        }
        
    }

    public bool CheckInventory(string itemID)
    {
        print(inventory.Contains(itemID));
        return inventory.Contains(itemID);
    }
}
