using UnityEngine;

public class Test1 : MonoBehaviour, IToggle
{
    public void Toggle()
    {
       Destroy(gameObject);
    }
}
