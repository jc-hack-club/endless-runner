using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class CarMovement : MonoBehaviour
{

	public float forwardForce = 10;
	public float horizontalMultiplier = 5;

	public float RightLane = -0.4400841f;
	public float CentreLane = -0f;
	public float LeftLane = 0.4400841f;

	public int CurrentLane = 0x00;
	public float laneLerper = 0f;
	public float laneSwitchCooldown = 1 / 20f;
	public float cooldownTimer = 0f;

	public float LanePosition = 0;

	public string CollisionTag = "Obstacle";
	public float TimeToDespawn = 0.2f;

	Rigidbody rb;

	public GameObject GameOverUI;

	public EventSystem evtSys;
	public Button RestartBtn;
	public GameObject SelectUI;
	public GameObject GameplayUI;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		GameOverUI.SetActive(false);
		SelectUI.SetActive(false);
		GameplayUI.SetActive(true);
	}

	// Update is called once per frame
	void Update()
	{
		cooldownTimer -= Time.deltaTime;
		//Debug.Log(Input.GetAxis("vertical") + "," + Input.GetAxis("horizontal"));
		//rb.AddForce(Input.GetAxis("horizontal") * horizontalMultiplier * Time.deltaTime, 0, 0);


		//rb.AddForce(0, 0, forwardForce * Time.deltaTime);

		if (cooldownTimer <= 0) {
			laneLerper += Input.GetAxis("horizontal");
			if (Mathf.Abs(laneLerper) > 0.9f)
			{
				CurrentLane += (int)(Mathf.Sign(laneLerper));
				cooldownTimer = laneSwitchCooldown;
				laneLerper = 0;
			}

			CurrentLane = Mathf.Clamp(CurrentLane, -1, 1);

		}

		switch(CurrentLane)
		{
			case 0:
				LanePosition = CentreLane;
				break;
			case 1:
				LanePosition = RightLane;
				break;
			case -1:
				LanePosition = LeftLane;
				break;
		}

		//LanePosition = CurrentLane == 0 ? CentreLane : CurrentLane == 1 ? RightLane : LeftLane;

		transform.position = new Vector3(LeftLane * -CurrentLane, transform.position.y, transform.position.z);
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == CollisionTag)
		{
			col.gameObject.GetComponent<CollisionHandler>().OnCollide();
		}
	}

	void OnDestroy()
	{
		ChunkHandler theHandler = GameObject.FindGameObjectWithTag("ChunkHandler").GetComponent<ChunkHandler>();
		theHandler.Speed = 0;
	}

	public void Die()
	{
		Destroy(gameObject, TimeToDespawn);
		GameOverUI.SetActive(true);
		SelectObject(evtSys, RestartBtn);
		SelectUI.SetActive(true);
		GameplayUI.SetActive(false);
	}

	public void SelectObject(EventSystem eventSystem, Button obj)
	{
		// Select the button
		obj.Select(); // Or EventSystem.current.SetSelectedGameObject(myButton.gameObject)
		// Highlight the button
		obj.OnSelect(null); // Or myButton.OnSelect(new BaseEventData(EventSystem.current))
	}

}


/*
	// Select the button
	myButton.Select(); // Or EventSystem.current.SetSelectedGameObject(myButton.gameObject)
	// Highlight the button
	myButton.OnSelect(null); // Or myButton.OnSelect(new BaseEventData(EventSystem.current))
*/
