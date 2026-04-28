using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System;
using UnityEditor.Rendering;

public class Interaction : MonoBehaviour
{
    [SerializeField] private float distance;

    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private Camera camera;
    private Door carentDoor;


    private void Start()
    {
        camera = GetComponent<Camera>();
        if(textMeshProUGUI != null)
        {
            textMeshProUGUI.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        Ray ray = new Ray( camera.transform.position, camera.transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, distance))
        {
            IToggle Interact = hit.collider.GetComponent<IToggle>();


            if(Interact != null)
            {
               
                ShowText();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Interact.Toggle();
                }
            }   
            else
            {
                Cleartext ();
            }
        }
        else
        {
            Cleartext ();
        }
    }

    private void Cleartext()
    {
        if (textMeshProUGUI != null)
        {
            textMeshProUGUI.gameObject.SetActive(false);
            
        }
        carentDoor = null;
    }

    private void ShowText()
    {
        if (textMeshProUGUI != null)
        {
            textMeshProUGUI.gameObject.SetActive(true);
            textMeshProUGUI.text = "press E";
        }
    }
}
