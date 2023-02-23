using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helicopter : MonoBehaviour
{
	public float moveSpeed;
	public Text txtSoldiers;
	int soldierCount;

	void Update()
	{
		transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * -moveSpeed * Time.deltaTime;
		if (Input.GetKeyDown(KeyCode.R)) UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.transform.gameObject.name == "Tree")
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (soldierCount < 3 && other.gameObject.name == "Soldier")
		{
			other.gameObject.transform.SetParent(transform);
			soldierCount++;
		}
		else if (soldierCount > 0 && other.gameObject.name == "Hospital")
		{
			foreach (Transform item in transform)
			{
				Destroy(item.gameObject);
			}
			soldierCount = 0;
		}
	}
}
