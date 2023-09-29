using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableObject : MonoBehaviour,Interactable
{
    private Vector3 originPos;

    private void Awake()
    {
        originPos = this.GetComponent<Transform>().position;
    }

    public void Recover()
    {
        this.transform.position = originPos;
    }

    public virtual void Interact()
    {
        
    }

    public virtual void EndInteract()
    {
       
    }
}
