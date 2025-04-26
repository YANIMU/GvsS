using System.Collections.Generic;
using UnityEngine;

public class PlantSommonerManager : MonoBehaviour
{
    public List<string> mobsName = new();
    public List<GameObject> buttons;
    public GameObject prefabButton;
    public GameObject shadow;
    void Start()
    {
        prefabButton.GetComponent<PlantSommoner>().sommonMobShadow = shadow;
        GameObject newob;
        foreach (var name in mobsName)
        {
            newob = Resources.Load<GameObject>($"Plant/{name}");
            newob.SetActive(false);
            var btn = Instantiate<GameObject>(prefabButton);
            btn.transform.parent = transform;
            btn.GetComponent<PlantSommoner>().mobName = name;
            btn.GetComponent<PlantSommoner>().sommonMob = newob;
            btn.SetActive(true);
            buttons.Add(btn);
        }
    }
}