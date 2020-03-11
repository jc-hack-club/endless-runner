using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ChunkHandler : MonoBehaviour
{
	//Chunk References
	public GameObject ChunkPool;
	public GameObject ChunkPrefab;

	//Render Distance
	public ushort RenderDistance;
	public ushort RenderDistanceBack;

	//References
	public Transform PlayerTransform;
	public float ChunkID = 0x00;
	public float PreviousChunkID = 0x00;

	//Debug Shit
	public Text DebugUI;
	public GameObject DebugFrame;
	public float Speed = 1f;

	public int Score = 0;
	private int Highscore;

	public Text ScoreUI;
	public Text HighscoreUI;

	[HideInInspector]
	public float ChunkScale = 0;

	public float SpeedIncreaseFactor = 0.1f;
	public int avgFrameRate;

	// Start is called before the first frame update
	void Start()
	{
		DebugFrame.SetActive(false);
		Time.timeScale = 1f;
		ChunkID = 0;
		ChunkScale = ChunkPrefab.transform.localScale.x;

		//Initialise the first 5 chunks
		for (int i = -RenderDistanceBack; i < RenderDistance + RenderDistanceBack; i++)
		{
			ChunkID = i;
			InstantiateChunk(!(i < RenderDistanceBack / 2));
		}
		PlayerTransform.position = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, 4.5f);
		GetHighscore();
	}

    // Update is called once per frame
    void Update()
	{
		//FPS counter
		float current = 0;
		current = (int)(1f / Time.unscaledDeltaTime);
		avgFrameRate = (int)current;

		//TODO: Remove auto moving player
		PlayerTransform.position = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, PlayerTransform.position.z + Speed * Time.deltaTime);

		//Calculate the current chunk
		ChunkID = (PlayerTransform.position.z / (ChunkScale * 2));
		if ((int)ChunkID > (int)PreviousChunkID)
		{
			InstantiateChunk(true);
			RemoveChunk();
			Speed += SpeedIncreaseFactor;
		}

		DebugUI.text = avgFrameRate + " FPS\nChunkID: " + ChunkID + "\nSpeed: " + Speed + "\nHighscore: " + Highscore;

		if (Input.GetKeyUp(KeyCode.F3))
		{
			DebugFrame.SetActive(!DebugFrame.activeInHierarchy);
		}

		if (Input.GetKeyUp(KeyCode.F4))
		{
			if (PlayerPrefs.HasKey("Highscore"))
			{
				PlayerPrefs.DeleteKey("Highscore");
			}
		}
		
		SpeedIncreaseFactor += Input.GetAxis("brackets") * 0.25f;

		//Remember the previous chunk
		PreviousChunkID = ChunkID;
		Score = (int)(ChunkID / 5);
		ScoreUI.text = "Score: " + Score;

		Highscore = Mathf.Max(Highscore, Score);
		HighscoreUI.text = "Highscore: " + Highscore;

		if (SpeedIncreaseFactor == 0)
		{
			PlayerPrefs.SetInt("Highscore", Highscore);
		}
	}

	public void InstantiateChunk(bool ObstaclesEnabled)
	{
		bool exists = false;
		Transform[] chunks = ChunkPool.transform.GetComponentsInChildren<Transform>();
		for (int i = 0; i < chunks.Length; i++)
		{
			//Render Distance behind check
			if (chunks[i].name == "Chunk-" + ((int)ChunkID + RenderDistance))
			{
				exists = true;
			}
		}
		if (exists == false)
		{
			GameObject ChunkTmp = Instantiate(ChunkPrefab);
			ChunkTmp.name = "Chunk-" + ((int)ChunkID + RenderDistance);
			ChunkTmp.transform.parent = ChunkPool.transform;
			ChunkTmp.transform.position = new Vector3(0, 0, ChunkScale * 2 * ((int)ChunkID + 1 + RenderDistance));
			ChunkTmp.GetComponent<Randomiser>().ObstaclesEnabled = ObstaclesEnabled;
		}
	}

	public void RemoveChunk()
	{
		Transform[] chunks = ChunkPool.transform.GetComponentsInChildren<Transform>();
		for (int i = 0; i < chunks.Length; i++)
		{
			//Render Distance behind check
			if (chunks[i].name == "Chunk-" + ((int)ChunkID - RenderDistanceBack))
			{
				chunks[i].GetComponent<Destroyer>().Remove();
			}
		}
	}

	public void GetHighscore()
	{
		if (PlayerPrefs.HasKey("Highscore"))
		{
			Highscore = PlayerPrefs.GetInt("Highscore", 0);
		}
	}
}
