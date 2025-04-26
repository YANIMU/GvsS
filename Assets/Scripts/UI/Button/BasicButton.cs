using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BasicButton : MonoBehaviour
{
    public Button Btn => GetComponent<Button>();
    void Start()
    {
        OnStart();
    }
    protected virtual void OnStart(){
        Btn.onClick.AddListener(OnClick);
    }
    public virtual void OnClick()
    {
        Debug.Log($"OnClicked : {gameObject.name}");
    }
}