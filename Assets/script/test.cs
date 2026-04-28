using Unity.VisualScripting;
using UnityEngine;

public class test : MonoBehaviour
{
    int name_one = 10;
    float name_two = 8.6f;
    bool name_three = true;
    string name_four = "Egor";

   public GameObject[]  allObgec;

    private void Start()
    {
        print(name_one);
        
        foreach (GameObject tank in allObgec)
        {
            if (tank.gameObject.GetComponent<BoxCollider>() != null) 
            {
                tank.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    private void Update()
    {
        name_one++;
        name_four = "Egor go home "+ name_one.ToString();
        print (name_four);
    }
}
