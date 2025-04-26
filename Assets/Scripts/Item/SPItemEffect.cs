using UnityEngine;

public class SPItemEffect : ItemEffect
{
    public int sPowerObject = 25;
    public override void OnExecute()
    {
        CostManager.Instance.sPower += sPowerObject;
    }
}