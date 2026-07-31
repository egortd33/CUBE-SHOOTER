using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
public class SliderController : MonoBehaviour
{
   public static SliderController Instance { get; private set; }

    public Slider slider;

    public int allCubes;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
           Destroy(gameObject);

            return;
        }
        Instance = this;
    }
    private void OnDestroy()
    {
      if (Instance == this)
        {
            Instance = null;
        }
    }
}
