using UnityEngine;

public class RadiationZone : MonoBehaviour
{
    [Header("Damage")]
    public float damagePerSecond = 20f;
    public float damageInterval = 0.5f;   
    
    [Header("Audio")]
    public AudioClip zoneEnterSound;
    public AudioClip zoneExitSound;
    private AudioSource audioSource;
    
    [Header("Visual")]
    
    private RoverHealth playerHealth;
    private bool isPlayerInside = false;
    private float nextDamageTime;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.GetComponent<RoverHealth>();
            if (playerHealth != null)
            {
                isPlayerInside = true;
                if (zoneEnterSound != null && audioSource != null)
                    audioSource.PlayOneShot(zoneEnterSound);
                Debug.Log("Вход в радиационную зону!");
            }
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            playerHealth = null;
            if (zoneExitSound != null && audioSource != null)
                audioSource.PlayOneShot(zoneExitSound);
            Debug.Log("Выход из радиационной зоны.");
        }
    }
    
    void Update()
    {
        if (isPlayerInside && playerHealth != null && Time.time >= nextDamageTime)
        {
            playerHealth.TakeDamage(damagePerSecond * damageInterval);
            nextDamageTime = Time.time + damageInterval;
        }
    }
}