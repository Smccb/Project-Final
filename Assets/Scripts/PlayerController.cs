using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour{


	private Animator anim;

	private Rigidbody playerRb;
	public bool isOnGround = true;

	private float turnSpeed = 3.0f;

	private float horizontalInput;
	private float speed = 10.0f;
	private float xRange = 25.0f;

	//sound effects
	private AudioSource playerAudio;
	public AudioClip jumpSound;

	// Start is called before the first frame update
	void Start()
    {
		gameObject.transform.eulerAngles = new Vector3(
		gameObject.transform.eulerAngles.x,
		gameObject.transform.eulerAngles.y +90,
		gameObject.transform.eulerAngles.z);
		anim = gameObject.GetComponentInChildren<Animator>();
		playerRb = GetComponent<Rigidbody>();

		playerAudio = GetComponent<AudioSource>();

	}

    // Update is called once per frame
    void Update()
    {
		//jump
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround){
			playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
			isOnGround = false;
			
			playerAudio.PlayOneShot(jumpSound, 1.0f);
		}
		
		horizontalInput = Input.GetAxis("Horizontal");

		//if d is pressed run animation + turn player to face direction
		if (Input.GetKey("d"))
		{
			anim.SetInteger("AnimationPar", 1);
			transform.forward = Vector3.right * turnSpeed * Time.deltaTime;

			//playerMovement
			transform.Translate(Vector3.forward * horizontalInput * Time.deltaTime * speed);
		}

		//if a is pressed run animation + turn player to face direction
		else if (Input.GetKey("a"))
		{
			anim.SetInteger("AnimationPar", 1);
			transform.forward = Vector3.left * turnSpeed * Time.deltaTime;

			//playerMovement
			transform.Translate(Vector3.back * horizontalInput * Time.deltaTime * speed);
		}

		//idel character animation if character not moving
		else
		{
			anim.SetInteger("AnimationPar", 0);
		}

		//keep player in bounds
		//left
		if (transform.position.x < -xRange){
			transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
		}
		//right
		if (transform.position.x > xRange){
			transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
		}
    }

	private void OnCollisionEnter(Collision collision)
	{
		isOnGround = true;
	}
}