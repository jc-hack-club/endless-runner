using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkHandler : MonoBehaviour
{
	public GameObject ChunkPool;
	public GameObject ChunkPrefab;

	public ushort RenderDistance;

	public Transform PlayerTransform;
	public long ChunkID = 0x00;
	public long PreviousChunkID = 0x00;

    // Start is called before the first frame update
    void Start()
    {
		ChunkID = 0;
    }

    // Update is called once per frame
    void Update()
    {
		//TODO: Remove auto moving player
		PlayerTransform.position = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, PlayerTransform.position.z + 1f * Time.deltaTime);

		//Calculate the current chunk


		//Remember the previous chunk
		PreviousChunkID = ChunkID;
	}
}
