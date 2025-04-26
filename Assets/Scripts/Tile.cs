using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isCanSommon;
    public GameObject sommonMob;
    private void Update()
    {
        if (!isCanSommon)
        {
            if (sommonMob == null)
            {
                isCanSommon = true;
            }
        }
    }
}