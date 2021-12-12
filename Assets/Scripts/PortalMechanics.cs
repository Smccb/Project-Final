using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalMechanics : MonoBehaviour
{
	//portal travel delay
	private float travelRate = 0.1f;
	private float nextTravel = 0f;

	public GameObject greenPortal, greenPortal2, bluePortal, bluePortal2, redPortal, redPortal2, brownPortal, brownPortal2, darkBluePortal, darkBluePortal2;


    // Start is called before the first frame update
    void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
	}

	void OnCollisionEnter(Collision collision)
	{
		//red
		Vector3 redPortalPosition = redPortal.transform.position;
		Vector3 redPortal2Position = redPortal2.transform.position;

		//green
		Vector3 greenPortalPosition = greenPortal.transform.position;
		Vector3 greenPortal2Position = greenPortal2.transform.position;

		//blue
		Vector3 bluePortalPosition = bluePortal.transform.position;
		Vector3 bluePortal2Position = bluePortal2.transform.position;

		if (Time.time > nextTravel)
		{
			//green portal 
			if (collision.gameObject == greenPortal)
			{
				transform.position = greenPortal2Position;
			}
			else if (collision.gameObject == greenPortal2)
			{
				transform.position = greenPortalPosition;
			}

			//blue portal
			else if (collision.gameObject == bluePortal)
			{
				transform.position = bluePortal2Position;
			}
			else if (collision.gameObject == bluePortal2)
			{
				transform.position = bluePortalPosition;
			}

			//red portal
			else if (collision.gameObject == redPortal)
			{
				transform.position = redPortal2Position;
			}
			else if (collision.gameObject == redPortal2)
			{
				transform.position = redPortalPosition;
			}

			if (SceneManager.GetActiveScene().buildIndex >= 2)
			{
				//brown
				Vector3 brownPortalPosition = brownPortal.transform.position;
				Vector3 brownPortal2Position = brownPortal2.transform.position;

				//darkblue
				Vector3 darkBluePortalPosition = darkBluePortal.transform.position;
				Vector3 darkBluePortal2Position = darkBluePortal2.transform.position;

				if (collision.gameObject == darkBluePortal)
				{
					transform.position = darkBluePortal2Position;
				}
				else if (collision.gameObject == darkBluePortal2)
				{
					transform.position = darkBluePortalPosition;
				}

				else if (collision.gameObject == brownPortal)
				{
					transform.position = brownPortal2Position;
				}
				else if (collision.gameObject == brownPortal2)
				{
					transform.position = brownPortalPosition;
				}
			}
			nextTravel = Time.time + travelRate;//adding time to portal delay
		}
	}
}
