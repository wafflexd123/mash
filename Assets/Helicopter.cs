using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
	public float moveSpeed;

	void Update()
	{
		transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * -moveSpeed * Time.deltaTime;
		if(Input.GetKeyDown(KeyCode.R)) UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.transform.gameObject.name == "Tree")
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
	}
}
