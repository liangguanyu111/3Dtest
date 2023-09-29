using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class StirringEvent : UnityEvent<float> { }

public class Stirring : InteractableObject
{
    float StirringTimer = 0;
    bool strring = false;
    public StirringEvent OnStirring = new StirringEvent();

    KeyCode lastKeyCode;

    Animator an;

    private void Start()
    {
        an = this.GetComponent<Animator>();
    }
    IEnumerator StirringPorcess()
    {
        while (strring)
        {
            StirringTimer += ReturnADKey();
            yield return null;
            OnStirring.Invoke(StirringTimer);
        }

        yield return null;
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonUp(0) && other.gameObject.name == "bowl"&&!strring)
        {
 
            Bowl bowl;
            bowl = other.gameObject.GetComponent<Bowl>();
            strring = true;

            //UI…Ë÷√
            UIManager._Instance.SetProcessStatus(true);

            bowl.Startstirring();
            bowl.OnStrringEnd.AddListener(EndStirring);

            an.SetBool("Active", true);
            OnStirring.AddListener(bowl.BeStrried);
            StartCoroutine("StirringPorcess");
        }
    }

    float ReturnADKey()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (lastKeyCode==KeyCode.D)
            {
                lastKeyCode = KeyCode.A;
                return 0.5f;
            }
            lastKeyCode = KeyCode.A;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            if (lastKeyCode == KeyCode.A)
            {
                lastKeyCode = KeyCode.D;
                return 0.5f;
            }
            lastKeyCode = KeyCode.D;
        }
        return 0f;
    }
    void EndStirring()
    {
        an.SetBool("Active",false);
        Recover();
        strring = false;
    }

    public override void Interact()
    {
        UIManager._Instance.SetRotateIconStatus(true);
    }

    public override void EndInteract()
    {
        UIManager._Instance.SetRotateIconStatus(false);
    }
}
