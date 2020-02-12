using System.Collections;
using System.Collections.Generic;
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
	public float Speed = 1f;

	[HideInInspector]
	public float ChunkScale = 0;

    // Start is called before the first frame update
    void Start()
    {
		ChunkID = 0;
		ChunkScale = ChunkPrefab.transform.localScale.x;

		//Initialise the first 5 chunks
		for (int i = -RenderDistanceBack; i < RenderDistance + RenderDistanceBack; i++)
		{
			ChunkID = i;
			InstantiateChunk();
		}
	}

    // Update is called once per frame
    void Update()
    {
		//TODO: Remove auto moving player
		PlayerTransform.position = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, PlayerTransform.position.z + Speed * Time.deltaTime);

		//Calculate the current chunk
		ChunkID = (PlayerTransform.position.z / (ChunkScale * 2));
		if ((int)ChunkID > (int)PreviousChunkID)
		{
			InstantiateChunk();
			RemoveChunk();
		}

		DebugUI.text = "ChunkID: " + ChunkID;

		//Remember the previous chunk
		PreviousChunkID = ChunkID;
	}

	public void InstantiateChunk()
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
}
