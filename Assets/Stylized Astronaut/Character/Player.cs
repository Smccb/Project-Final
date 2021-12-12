using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private Animator anim;
	private CharacterController controller;

	public float jumpSpeed = 1400.0f;
	public float turnSpeed = 1.0f;

	public float horizontalInput;
	private float speed = 10.0f;
	private float xRange = 25.0f;

	private Vector3 moveDirection = Vector3.zero;
	public float gravity = 20.0f;


	void Start () {
			controller = GetComponent <CharacterController>();
			anim = gameObject.GetComponentInChildren<Animator>();
	}

	void Update (){
		if (Input.GetKey("d"))
		{
			anim.SetInteger("AnimationPar", 1);
			transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
			transform.forward = Vector3.right * turnSpeed * Time.deltaTime;
		}

		else if (Input.GetKey("a")) {
			anim.SetInteger("AnimationPar", 1);
			transform.Translate(Vector3.left * horizontalInput * Time.deltaTime * speed);
			transform.forward = Vector3.left * turnSpeed * Time.deltaTime;
		}

		else
		{
			anim.SetInteger("AnimationPar", 0);
		}

		if (controller.isGrounded)
		{
			//Feed moveDirection with input.
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);



			//Multiply it by speed.
			moveDirection *= speed;
			//Jumping
			if (Input.GetKey(KeyCode.Space))
			{
				moveDirection.y -= jumpSpeed;
				//Applying gravity to the controller
				moveDirection.y -= gravity * Time.deltaTime;
				//Making the character move
				controller.Move(moveDirection * Time.deltaTime);
			}


		}

		//keep player in bounds
		//left
		if (transform.position.x < -xRange)
		{
			transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
		}
		//right
		if (transform.position.x > xRange)
		{
			transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
		}

		Vector3.right.Normalize();
		Vector3.left.Normalize();
		transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
		transform.Translate(Vector3.left * horizontalInput * Time.deltaTime * speed);
	}
}
