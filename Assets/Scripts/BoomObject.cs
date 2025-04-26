using System.Collections.Generic;
using UnityEngine;

public class BoomObject : MonoBehaviour
{
    public List<GameObject> list;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Mob"){
            list.Add(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        for (int i = 0; i < list.Count; i++)
        {
            if(list[i].Equals(other.gameObject)){
                list.RemoveAt(i);
                break;
            }
        }
    }
}