using UnityEngine;

public class SoilSample : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoverInventory inv = other.GetComponent<RoverInventory>();
            if (inv != null && inv.CollectSample(this))
            {
                Destroy(gameObject);
            }
        }
    }
}