using UnityEngine;

public class MobSommoner : MonoBehaviour
{
    public GameObject obj;
    public string objName;
    private void Start()
    {
        obj = Resources.Load<GameObject>($"Mob/{objName}");
        obj.SetActive(false);
        obj.transform.position = this.gameObject.transform.position;
    }
    private void Update()
    {

    }
    public void OnExecute()
    {
        var newob = Instantiate(obj, this.gameObject.transform.position, Quaternion.identity);
        newob.SetActive(true);
    }
    public GameObject OnExecute(GameObject ob)
    {
        var newob = Instantiate(ob, this.gameObject.transform.position, Quaternion.identity);
        newob.SetActive(true);
        return newob;
    }
}