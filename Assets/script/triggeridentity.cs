using UnityEngine;

public class triggeridentity : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerShoot>() != null)
        {
            other.GetComponent<PlayerShoot>().IsActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.GetComponent<PlayerShoot>() != null)
        {
            other.GetComponent<PlayerShoot>().IsActive = false;
        }
    }
}
