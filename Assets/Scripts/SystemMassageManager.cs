using UnityEngine;
using UnityEngine.UI;

public class SystemMassageManager : MonoBehaviour
{
    public static SystemMassageManager Instance;
    Text text;
    public float viewTime;
    public float delayTime;
    public float v_Time;
    public float d_Time;
    public bool isView = false;
    private void Start()
    {
        if (Instance == null) Instance = this;
        text = GetComponent<Text>();
    }
    private void Update()
    {
        if (!isView) return;
        d_Time -= Time.deltaTime;
        if (d_Time <= 0f)
        {
            v_Time -= Time.deltaTime;
            if (v_Time <= 0f)
            {
                isView = false;
            }
            SetColor((v_Time / viewTime) <= 0f ? 0f : v_Time / viewTime);
        }
    }
    public void SystemMassage(string massage)
    {
        SetColor(1f);
        if (!System.IO.File.Exists("C:testDebugLog.txt"))
            System.IO.File.WriteAllText("C:testDebugLog.txt", "debug");
        System.IO.File.AppendAllText("C:testDebugLog.txt", $"\n{System.DateTime.Now}" + GetMassage(massage));
        d_Time = delayTime;
        v_Time = viewTime;
        isView = true;
    }
    public string GetMassage(string massage)
    {
        text.text = massage;
        return massage;
    }
    Color SetColor(float a)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, a);
        return text.color;
    }
}