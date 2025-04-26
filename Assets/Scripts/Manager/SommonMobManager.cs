using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SommonMobManager : MonoBehaviour
{
    public GameObject sommoners;
    public List<MobSommoner> sommonerList = new();
    public List<string> sommonMobKeyList = new();
    public SerializableDictionary<string, GameObject> sommonMobList = new();
    public List<GameObject> nowSommonMobs = new();
    public GameObject endButton;
    List<List<string>> sommonMobSequenceList;
    public List<int> sommonerIndexList = new();
    public string sommonMobSequence;
    public int indexNumber = 0;
    public float nowListDelayTime = 10f;
    public float sommonDelayTime = 1f;
    public float n_dTime;
    public float s_dTime;
    public bool isGetNowList;
    public bool isSommon;
    public bool isGameEnd;
    public int orderinlayer = 0;
    private void Start()
    {
        for (int i = 0; i < sommoners.transform.childCount; i++)
        {
            sommonerList.Add(sommoners.transform.GetChild(i).GetComponent<MobSommoner>());
        }
        n_dTime = 30f;
        s_dTime = 0f;
        isGetNowList = false;
        isSommon = false;
        foreach (var item in sommonMobKeyList)
        {
            SetSommonMobList(item);
        }
        sommonMobSequence = Resources.Load<TextAsset>("SommonSequenceTest").text;
        sommonMobSequenceList = GetSequenceList();
    }
    private void Update()
    {
        if (isGameEnd) return;
        if (OnEnd())
        {
            isGameEnd = true;
            StartCoroutine("E_button", 1f);
            return;
        }
        n_dTime -= Time.deltaTime;
        s_dTime -= Time.deltaTime;
        nowSommonMobs = nowSommonMobs.Where(x => x != null).ToList();
        if (s_dTime <= 0f && isSommon)
        {
            if (sommonMobSequenceList[indexNumber].Count == 0) isSommon = false;
            else if (sommonMobList.ContainsKey(sommonMobSequenceList[indexNumber][0]))
            {
                var newob = sommonerList[GetsommonerIndex()].GetComponent<MobSommoner>().OnExecute(sommonMobList[sommonMobSequenceList[indexNumber][0]]);
                if (newob.transform.childCount > 0)
                {
                    for (int i = 0; i < newob.transform.childCount; i++)
                    {
                        orderinlayer--;
                        newob.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = orderinlayer;
                    }
                }
                orderinlayer--;
                newob.GetComponent<SpriteRenderer>().sortingOrder = orderinlayer;
                nowSommonMobs.Add(newob);
                sommonMobSequenceList[indexNumber].RemoveAt(0);
                if (sommonMobSequenceList[indexNumber].Count == 0) isSommon = false;
            }
            s_dTime = sommonDelayTime;
        }
        if (nowSommonMobs.Count == 0 && n_dTime > 11f)
        {
            n_dTime = 10f;
        }
        if (n_dTime <= 0f && indexNumber < sommonMobSequenceList?.Count)
        {
            isGetNowList = true;
            indexNumber++;
            if (indexNumber < sommonMobSequenceList.Count)
            {
                n_dTime = nowListDelayTime * sommonMobSequenceList[indexNumber].Count;
            }
        }
        if (isGetNowList || (nowSommonMobs.Count == 0 && sommonMobSequenceList[indexNumber].Count == 0))
        {
            isGetNowList = false;
            isSommon = true;
        }
    }
    public void E_button() => endButton.SetActive(true);
    public bool OnEnd()
    {
        //Debug.Log($"{indexNumber}:{sommonMobSequenceList.Count}");
        return indexNumber >= sommonMobSequenceList?.Count && nowSommonMobs.Count == 0;
    }
    public void EndButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void SetSommonMobList(string str)
    {
        GameObject newob = Instantiate(Resources.Load<GameObject>($"Mob/{str}"));
        newob.SetActive(false);
        sommonMobList.Add(str, newob);
    }
    public List<List<string>> GetSequenceList()
    {
        var list = new List<List<string>>();
        var newlist = sommonMobSequence.Split("\n").ToList();
        for (int i = 0; i < newlist.Count; i++)
        {
            List<string> strlist = new();
            var newnewlist = newlist[i].Split(",").ToList();
            Debug.Log(newnewlist.Count + "COunt");
            for (int j = 0; j < newnewlist.Count; j += 2)
            {
                for (int k = 0; k < int.Parse(newnewlist[j + 1]); k++)
                {
                    strlist.Add(newnewlist[j]);
                }
            }
            list.Add(strlist);
        }
        return list;
    }
    public int GetsommonerIndex()
    {
        if (sommonerIndexList.Count == 0)
        {
            for (int i = 0; i < sommonerList.Count; i++)
            {
                sommonerIndexList.Add(i);
            }
        }
        int rand = Random.Range(0, sommonerIndexList.Count);
        int ind = sommonerIndexList[rand];
        sommonerIndexList.RemoveAt(rand);
        return ind;
    }
    public void OnClick()
    {
        Debug.Log($"Debug : test {nameof(SommonMobManager)} {nameof(OnClick)}");
        n_dTime = 0.5f;
    }
}