using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helicopter : MonoBehaviour
{
	public float moveSpeed, gameResetTime;
	public Text txtSoldiersRescued, txtSoldiersInHeli, txtWin;
	public AudioSource audioSource;
	public GameObject tpCam, fpCam;
	int soldierCount, totalRescued, controlInvert = -1;

	private void Awake()
	{
		Time.timeScale = 1;
	}

	void Update()
	{
		if (tpCam.activeSelf) transform.position += -moveSpeed * Time.deltaTime * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		else transform.position += moveSpeed * Time.deltaTime * new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
		if (Input.GetKeyDown(KeyCode.R)) UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			if (tpCam.activeSelf)
			{
				tpCam.SetActive(false);
				fpCam.SetActive(true);
			}
			else
			{
				tpCam.SetActive(true);
				fpCam.SetActive(false);
			}
		}
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
				if (item.name == "Soldier")
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
