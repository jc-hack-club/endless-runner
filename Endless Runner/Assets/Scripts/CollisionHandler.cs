using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnCollide()
    {
        Camera.main.GetComponent<CameraShake>().enabled = true;
        Camera.main.GetComponent<CameraShake>().shakeDuration = 2.5f;
        ChunkHandler theHandler = GameObject.FindGameObjectWithTag("ChunkHandler").GetComponent<ChunkHandler>();
        theHandler.SpeedIncreaseFactor = 0;
        GameObject.FindGameObjectWithTag("Car").GetComponent<CarMovement>().Die();
        Time.timeScale = 0.1f;
    }
}
