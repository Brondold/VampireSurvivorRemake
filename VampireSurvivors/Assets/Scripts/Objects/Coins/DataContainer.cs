using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DataContainer : ScriptableObject
{
    public int coins;

    public List<bool> stageUnlock;  

    public void StageUnlock(int i)
    {
        stageUnlock[i] = true;
    }




}
