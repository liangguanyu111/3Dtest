using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bowl : MonoBehaviour
{
    private Material batterMaterial;
    public float StirringTime; //完成搅拌的时间
    public float process = 0;

    private float timer =0;

    public UnityEvent OnStrringEnd = new UnityEvent();
    private void Awake()
    {
        batterMaterial = this.transform.GetChild(0).GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        
    }

    public void Startstirring()
    {
        UIManager._Instance.OnStirringPaticle();
        batterMaterial.SetInt("_IsActive",1);
    }

    public void BeStrried(float timer)
    {
        process = timer / StirringTime;
        batterMaterial.SetFloat("_Process", process);
        UIManager._Instance.Process(process);
        if(process>=1)
        {
            UIManager._Instance.SetProcessStatus(false);
            UIManager._Instance.EndStirringPaticle();
            OnStrringEnd.Invoke();
        }
    }

}
