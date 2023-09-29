using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager _Instance;



    [Header("Drag")]

    private Vector3 mousePos;
    private Vector3 orignPos;
    InteractableObject interactObj;
    private bool isDrag;
    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Interactable interactable;
                if (hit.collider.TryGetComponent<Interactable>(out interactable))
                {
                    interactable.Interact();
                    mousePos = Input.mousePosition;
                    isDrag = true;
                    interactObj = hit.collider.gameObject.GetComponent<InteractableObject>();
                    orignPos = Camera.main.WorldToScreenPoint(interactObj.transform.position);
                }
            }
        }

        //On Drag
        if (Input.GetMouseButton(0))
        {
            if (isDrag && interactObj)
            {
                Vector3 newPos = orignPos + Input.mousePosition - mousePos;
                Vector3 newWorldPos = Camera.main.ScreenToWorldPoint(newPos);
                interactObj.transform.position = newWorldPos;
            }
        }

        //End Drag

        if (Input.GetMouseButtonUp(0))
        {
            isDrag = false;
            if(interactObj)
            interactObj.GetComponent<Interactable>().EndInteract();
            interactObj = null;
        }
    }


}
