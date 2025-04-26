using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeControler : BasicButton
{
    public int MaxTimeScale;
    public override void OnClick()
    {
        base.OnClick();
        if (MaxTimeScale <= Time.timeScale) Time.timeScale = 1;
        else Time.timeScale++;
        transform.GetChild(0).GetComponent<Text>().text = GetText();
    }
    public string GetText() => Time.timeScale switch
    {
        1 => ">",
        2 => ">>",
        3 => ">>>",
        _ => ">"
    };
}