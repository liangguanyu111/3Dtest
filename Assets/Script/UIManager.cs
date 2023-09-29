using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _Instance;

    [Header("Panel")]
    public GameObject ProcessSlider;
    public GameObject RotateIcon;


    private void Awake()
    {
        if(_Instance==null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetProcessStatus(bool status)
    {
        ProcessSlider.gameObject.SetActive(status);
    }
    public void Process(float process)
    {   
        ProcessSlider.transform.GetChild(0).GetComponent<Image>().fillAmount = process;
    }

    public void SetRotateIconStatus(bool status)
    {
        RotateIcon.gameObject.SetActive(status);
    }

    public void OnStirringPaticle()
    {
        Debug.Log("此处出现搅拌的粒子特效");
    }
    public void EndStirringPaticle()
    {
        Debug.Log("此处出现搅拌完成的粒子特效");
    }

}
