using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour{
	
	
	private Rigidbody playerRb;
	public bool isOnGround = true;
	
	public float horizontalInput;
	private float speed = 10.0f;
	private float xRange = 25.0f;
	
	//sound effects
	private AudioSource playerAudio;
	public AudioClip jumpSound;


	//player start position
	Vector3 playerStartPosition = new Vector3(-20, 1, -1);

	//portal travel delay
	private float travelRate = 0.1f;
	private float nextTravel = 0f;//	-	-	-	-	-	-	-	-	delay-goes with key and door event managment

	//keys
	public bool keyCollected = false;//	-	-	-	-	-	-	-	-	delay-goes with key and door event managment
	public GameObject key;
	public bool key1Collected = false;
	public GameObject key1;

	//finish door
	public GameObject finishDoor;//	-	-	-	-	-	-	-	-	delay-goes with key and door event managment

	//finish door
	public GameObject lockedDoor;//	-	-	-	-	-	-	-	-	delay-goes with key and door event managment

	public GameObject portalOut;//	-	-	-	-	-	-	-	-	delay-goes with key and door event managment

	//life counter
	public TextMeshProUGUI livesText;//	-	-	-	-	-	-	-	-	delay-goes with key and door event managment
	public int lives;
	
	//spikes
	public GameObject spikes;//	-	-	-	-	-	-	-	-	delay-goes with key and door event managment

	// Start is called before the first frame update
	void Start()
    {

		playerRb = GetComponent<Rigidbody>();
		lives = 3;
		livesText.text = "Lives: " + lives;
		
		playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround){
			playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
			isOnGround = false;
			
			playerAudio.PlayOneShot(jumpSound, 1.0f);
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

		//game over scene plays when lives go below zero
		if (lives < 0) {
			SceneManager.LoadScene(4);
		}
    }
	
	private void updateLives(){
		lives = lives - 1;
		livesText.text = "Lives: " + lives;
	}
	private void OnCollisionEnter(Collision collision)
	{
		isOnGround = true;

		//key
		if (collision.gameObject == key)
		{
			keyCollected = true;
			Destroy(key);
		}
		//key1
		if (collision.gameObject == key1)
		{
			key1Collected = true;
			Destroy(key1);
		}

		//finishDoor
		if (collision.gameObject == finishDoor && keyCollected == true)
		{
			Debug.Log("Level complete");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//sends player to next level when current one complete
		}

		//life lost
		if (collision.gameObject == spikes)
		{
			updateLives();
			transform.position = playerStartPosition;
		}

		//level2 locked door and level 2 exit door
		if (Time.time > nextTravel)
		{
			if (SceneManager.GetActiveScene().buildIndex >= 2)
			{
				Vector3 lockedDoorPosition = lockedDoor.transform.position;
				Vector3 portalOutPosition = portalOut.transform.position;

				if (collision.gameObject == lockedDoor && key1Collected == true)
				{
					transform.position = portalOutPosition;
				}
				if (collision.gameObject == portalOut)
				{
					transform.position = lockedDoorPosition;
				}
			}
			nextTravel = Time.time + travelRate;//adding time to portal delay
		}
	}
}