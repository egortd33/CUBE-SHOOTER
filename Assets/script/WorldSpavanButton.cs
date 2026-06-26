using UnityEngine;

public class WorldSpavanButton : MonoBehaviour 
{
    public PersonRowManager personRowManager;

    private void OnMouseDown()
    {
        if (personRowManager != null)
        {
           if (personRowManager.Perons[0].name == gameObject.name)
            {
                Destroy(gameObject);
            }
        }
       
    }


    
    
}
