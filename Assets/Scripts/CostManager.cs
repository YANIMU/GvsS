using UnityEngine;
using UnityEngine.UI;
public class CostManager : MonoBehaviour
{
    public static CostManager Instance;
    public int sPower = 300;
    public Text text;
    private void Start()
    {
        if (Instance == null) Instance = this;
    }
    private void Update()
    {
        text.text = $"SP : {sPower}";
        sPower = sPower > 10000 ? 9999 : sPower;
    }
    public void OnClick()
    {
        sPower += 25;
    }
}