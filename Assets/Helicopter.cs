using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helicopter : MonoBehaviour
{
	public float moveSpeed, gameResetTime;
	public Text txtSoldiersRescued, txtSoldiersInHeli, txtWin;
	public AudioSource audioSource;
	int soldierCount, totalRescued;

	private void Awake()
	{
		Time.timeScale = 1;
	}

	void Update()
	{
		transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * -moveSpeed * Time.deltaTime;
		if (Input.GetKeyDown(KeyCode.R)) UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.transform.gameObject.name == "Tree")
		{
			txtWin.text = "Game Over";
			Time.timeScale = 0;
			StartCoroutine(WaitForNextGame());
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (soldierCount < 3 && other.gameObject.name == "Soldier")
		{
			other.gameObject.transform.SetParent(transform);
			soldierCount++;
			txtSoldiersInHeli.text = $"Soldiers In Helicopter: {soldierCount}";
			audioSource.Play();
		}
		else if (soldierCount > 0 && other.gameObject.name == "Hospital")
		{
			foreach (Transform item in transform)
			{
				Destroy(item.gameObject);
			}
			totalRescued += soldierCount;
			txtSoldiersRescued.text = $"Soldiers Rescued: {totalRescued}";
			soldierCount = 0;
			txtSoldiersInHeli.text = $"Soldiers In Helicopter: {soldierCount}";
			if (totalRescued >= 5)
			{
				txtWin.text = "You Win!";
				Time.timeScale = 0;
				StartCoroutine(WaitForNextGame());
			}
		}
	}

	IEnumerator WaitForNextGame()
	{
		float timer = 0;
		while (timer < gameResetTime)
		{
			timer += Time.unscaledDeltaTime;
			yield return null;
		}
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}
