using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour{
	
	
	private Rigidbody playerRb;
	public bool isOnGround = true;
	
	public float horizontalInput;
	private float speed = 10.0f;
	private float xRange = 25.0f;
	
	//portals
	public GameObject greenPortal;
	public GameObject greenPortal2;
	
	public GameObject bluePortal;
	public GameObject bluePortal2;
	
	public GameObject redPortal;
	public GameObject redPortal2;
	
	//portal positions
	Vector3 greenPortalPosition = new Vector3(-7, 1, -1);
	Vector3 greenPortal2Position = new Vector3(-2, 15, -1);
	
	Vector3 bluePortalPosition = new Vector3(22, 15, -1);
	Vector3 bluePortal2Position = new Vector3(-6, 15, -1);
	
	Vector3 redPortalPosition = new Vector3(-2, 2, -1);
	Vector3 redPortal2Position = new Vector3(-20, 22, -1);
	
	//player start position
	Vector3 playerStartPosition = new Vector3(-20, 1, -1);
	
	//key
	public bool keyCollected = false;
	public GameObject key;
	
	//finish door
	public GameObject finishDoor;
	
	//life counter
	public TextMeshProUGUI livesText;
	public int lives;
	
	//spikes
	public GameObject spikes;
	
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
		lives = 3;
		livesText.text = "Lives: " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround){
			playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
			isOnGround = false;
		}
		
		horizontalInput = Input.GetAxis("Horizontal");
		
		//keep player in bounds
		//left
		if (transform.position.x < -xRange){
			transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
		}
		//right
		if (transform.position.x > xRange){
			transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
		}
		horizontalInput = Input.GetAxis("Horizontal");
		transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
    }
	
	private void updateLives(){
		lives = lives - 1;
		livesText.text = "Lives: " + lives;
	}
	
	private void OnCollisionEnter(Collision collision){
		isOnGround = true;
		
		
		//green portal 
		if (collision.gameObject == greenPortal){
			transform.position = greenPortal2Position;
			
		}
		if (collision.gameObject == greenPortal2){
			transform.position = greenPortalPosition;
		}
		
		//blue portal
		if (collision.gameObject == bluePortal){
			transform.position = bluePortal2Position;
		}
		if (collision.gameObject == bluePortal2){
			transform.position = bluePortalPosition;
		}
		
		//red portal
		if (collision.gameObject == redPortal){
			transform.position = redPortal2Position;
		}
		if (collision.gameObject == redPortal2){
			transform.position = redPortalPosition;
		}
		
		//key
		if (collision.gameObject == key){
			keyCollected = true;
			Destroy(key);
		}
		
		//finishDoor
		if (collision.gameObject == finishDoor && keyCollected == true){
			Debug.Log("Level 1 complete");
		}
		
		//life lost
		if (collision.gameObject == spikes){
			updateLives();
			transform.position = playerStartPosition;
		}
	}
}




















