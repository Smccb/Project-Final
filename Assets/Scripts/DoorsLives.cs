using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DoorsLives : MonoBehaviour
{
	private Rigidbody playerRb;


	//portal travel delay
	private float travelRate = 0.1f;
	private float nextTravel = 0f;

	//keys
	public bool keyCollected = false;
	public GameObject key;
	public bool key1Collected = false;
	public GameObject key1;

	//finish door
	public GameObject finishDoor;

	//finish door
	public GameObject lockedDoor;

	public GameObject portalOut;

	//life counter
	public TextMeshProUGUI livesText;
	public int lives;

	//spikes
	public GameObject spikes;
	public GameObject spikes1;


	Vector3 playerStartPosition = new Vector3(-20, 0, -1);
	Vector3 playerStartPosition2 = new Vector3(-23, 50, -1);


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
		//game over scene plays when lives go below zero
		if (lives < 0)
		{
			SceneManager.LoadScene(4);
		}
	}

	private void updateLives()
	{
		lives = lives - 1;
		livesText.text = "Lives: " + lives;
	}
	private void OnCollisionEnter(Collision collision)
	{

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
		if (collision.gameObject == spikes || collision.gameObject == spikes1)
		{
			
			updateLives();
			if (SceneManager.GetActiveScene().buildIndex == 1)
			{
				transform.position = playerStartPosition;
			}
			else if (SceneManager.GetActiveScene().buildIndex == 2)
			{
				transform.position = playerStartPosition2;
			}
			
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
