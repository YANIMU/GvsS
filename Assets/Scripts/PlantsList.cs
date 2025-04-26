using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlantsList : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] allList;
    public List<GameObject> list = new();
    public List<int> listIndexs;
    public GameObject prefab_Btn;
    void Start()
    {
        prefab_Btn.SetActive(false);
        allList = Resources.LoadAll<GameObject>("Plant");
        foreach (var item in listIndexs)
        {
            list.Add(allList[item - 1]);
        }
        foreach (var item in list)
        {
            var newob = Instantiate<GameObject>(prefab_Btn);
            newob.transform.parent = this.transform;
            var ps = newob.GetComponent<PlantSommoner>();
            ps.sommonMob = item;
            ps.isSpriteTreaking = false;

            ps.text.text = $"{ps.mobName.Split('_')[1]}\n{ps.sommonMob.GetComponent<PUnit>().cost}";
            newob.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
