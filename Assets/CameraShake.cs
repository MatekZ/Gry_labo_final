using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.3f;
    public float shakeAmplitude = 8f;
    public float shakeFrequency = 2.0f;

    private float shakeTimer = 0f;
    public float spawnInterval;
    private float spawnTimer = 0f;
    public float spawnDuration = 5f;



    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;
    public GameObject fallingObjectPrefab;
    public Transform[] objectSpawnpoints;


    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void Shake()
    {
        shakeTimer = shakeDuration;
        spawnTimer = spawnDuration;
        Debug.Log("SHAKING");
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            float noiseX = Random.Range(-1f, 1f) * shakeAmplitude;
            float noiseY = Random.Range(-1f, 1f) * shakeAmplitude;

            noise.m_AmplitudeGain = noiseX;
            noise.m_FrequencyGain = noiseY;

            shakeTimer -= Time.deltaTime * shakeFrequency;
            if(spawnTimer <= spawnDuration)
            {
                spawnTimer += Time.deltaTime;
                if (spawnTimer >= spawnInterval)
                {
                    SpawnFallingObjects();
                    spawnTimer = 0f;
                }
            }
            
        }
        else
        {
            noise.m_AmplitudeGain = 0f;
            noise.m_FrequencyGain = 0f;
        }
    }

    public void SpawnFallingObjects()
    {
        int rnd = Random.Range(0, (objectSpawnpoints.Length));
        Debug.Log(rnd);
        Instantiate(fallingObjectPrefab, objectSpawnpoints[rnd].position, Quaternion.identity);
    }
}