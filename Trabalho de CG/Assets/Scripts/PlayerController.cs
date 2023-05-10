using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;

    private Vector3 moveDirection;
    public float gravityScale;

    public Animator anim;
    public Transform pivot;
    public float rotateSpeed;

    public GameObject playerModel;
    public AudioSource vinhaSound;

    Transform currentSwingable;
    Transform ladderUp;

    bool swinging = false; 
    bool climbingLadder = false;

    public AudioSource jumpSound;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if(controller.isGrounded){
            moveDirection.y = 0f;
            if(Input.GetButtonDown("Jump")){
                moveDirection.y = jumpForce;
                jumpSound.Play();
            }
        }

        if (!controller.isGrounded && transform.position.y < -15) {
            FindObjectOfType<GameManager>().RemovePoint(1000000);
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){ 
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));

        if (swinging == true){
            transform.position = currentSwingable.position;
            if(Input.GetKeyDown(KeyCode.Space)){
                swinging = false;
                moveDirection.y = jumpForce;
            }
        }

        if (climbingLadder)
        {
            moveDirection.y = moveSpeed;
        }

        if (!controller.isGrounded)
        {
            // Check if the player is still touching the ladder
            if (ladderUp != null && transform.position.x > ladderUp.position.x - 1 && transform.position.x < ladderUp.position.x + 1)
            {
                climbingLadder = true;
            }
            else
            {
                climbingLadder = false;
            }
        }

        
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Vine"){
            swinging = true;
            currentSwingable = other.transform;
            vinhaSound.Play();
        }

        if(other.gameObject.tag == "Ladder"){
            ladderUp = other.transform;
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.tag == "Ladder"){
            ladderUp = null;
        }
    }
}