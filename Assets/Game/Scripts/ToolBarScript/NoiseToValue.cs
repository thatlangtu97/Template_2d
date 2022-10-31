using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseToValue : MonoBehaviour
{
    public float timeTrigger;
    public Vector3 vNoise;
    public float xSeed;
    public float ySeed;
    public float xSpeed;
    public float ySpeed;
    public Vector3 Offset;
    public float speedTime=1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeTrigger += Time.deltaTime * speedTime;
        vNoise = new Vector3(
                Mathf.PerlinNoise(timeTrigger, xSeed) *xSpeed,
                Mathf.PerlinNoise(timeTrigger, ySeed) * ySpeed,
                0f
            
            );
        transform.position = Offset + vNoise;

    }
}
