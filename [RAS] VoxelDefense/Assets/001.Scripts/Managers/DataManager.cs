using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public Dictionary<int, DefenderData> defenderDatas = new Dictionary<int, DefenderData>();
    public DefenderDataProfile defenderDataProfile;

    private void Start()
    {
        LoadDefenderData();
    }

    private void LoadDefenderData()
    {
        for (int i = 0; i < defenderDataProfile.datas.Length; i++)
        {
            defenderDatas.Add(defenderDataProfile.datas[i].index, defenderDataProfile.datas[i]);
        }
    }

    public DefenderData GetDefenderData(int _index)
    {
        if (defenderDatas.TryGetValue(_index, out DefenderData data)) return data;
        return null;
    }
}

[System.Serializable]
public class DefenderData
{
    public int index;
    public string name;
}

[CreateAssetMenu(menuName = "Container/DefenderDatas", fileName = "DefenderDataProfile")]
public class DefenderDataProfile : ScriptableObject
{
    public DefenderData[] datas;
}

[System.Serializable]
public class EnemyData
{
    public int index;
    public string name;
    public float attackForce;
}

[CreateAssetMenu(menuName = "Container/EnemyDatas", fileName = "EnemyDataProfile")]
public class EnemyDataProfile : ScriptableObject
{
    public EnemyData[] datas;
}
