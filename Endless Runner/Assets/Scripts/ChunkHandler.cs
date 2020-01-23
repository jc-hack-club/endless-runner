using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChunkHandler : MonoBehaviour
{
	public GameObject ChunkPool;
	public GameObject ChunkPrefab;

	public ushort RenderDistance;

	public Transform PlayerTransform;
	public long ChunkID = 0x00;
	public long PreviousChunkID = 0x00;

	public Text DebugUI;
	public float Speed = 1f;

	public float ChunkScale = 0;

    // Start is called before the first frame update
    void Start()
    {
		ChunkID = 0;
		ChunkScale = ChunkPrefab.transform.localScale.x;
	}

    // Update is called once per frame
    void Update()
    {
		//TODO: Remove auto moving player
		PlayerTransform.position = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, PlayerTransform.position.z + Speed * Time.deltaTime);

		//Calculate the current chunk
		ChunkID = (long) (PlayerTransform.position.z / 16L);
		if (ChunkID > PreviousChunkID)
		{
			InstantiateChunk();
		}

		DebugUI.text = "ChunkID: " + ChunkID;

		//Remember the previous chunk
		PreviousChunkID = ChunkID;
	}

	public void InstantiateChunk()
	{
		GameObject ChunkTmp = Instantiate(ChunkPrefab);
		ChunkTmp.name = "Chunk-" + (ChunkID + RenderDistance);
		ChunkTmp.transform.parent = ChunkPool.transform;
		ChunkTmp.transform.position = new Vector3(0, 0, ChunkScale * (ChunkID + 1 + RenderDistance));
		Debug.Log("ChunkScale * (" + ChunkID + " + 1 + " + RenderDistance + ")");
	}

	public void RemoveChunk()
	{
		Transform[] chunks = ChunkPool.transform.GetComponentsInParent<Transform>();
		for (int i = 0; i < chunks.Length; i++)
		{
			if (chunks[i].name == "Chunk-" + ChunkID)
			{
				Destroy(chunks[i]);
			}
		}
	}
}
