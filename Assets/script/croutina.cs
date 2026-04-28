using System.Collections;
using UnityEngine;

public class croutina : MonoBehaviour
{
    MeshRenderer Mesh_red;

    private void Start()
    {
        Mesh_red = GetComponent<MeshRenderer>();
        StartCoroutine(Mama());
    }

    public IEnumerator Mama()
    {
        for (int i = 0; i < 3; i++)
        {
            Mesh_red.enabled = false;

            yield return new WaitForSeconds(1f);

            Mesh_red.enabled = true;

            yield return new WaitForSeconds(1f);
        }
        Destroy(gameObject);
    }
}