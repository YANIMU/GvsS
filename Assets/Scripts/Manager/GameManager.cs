using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlantSommonerManager P_Sommoner_M;
    public SommonMobManager E_Sommoner_M;
    public SystemMassageManager Sys_Msg_M;
    void Start()
    {
        if (Instance == null) Instance = this;
    }
}
