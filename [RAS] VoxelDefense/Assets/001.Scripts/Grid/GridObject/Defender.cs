using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : GridObject
{
    public DefenderData data;

    public override void Init(int _gridIndexX, int _gridIndexY, int _gridIndexZ, int _index)
    {
        base.Init(_gridIndexX, _gridIndexY, _gridIndexZ, _index);
        data = DataManager.Instance.GetDefenderData(_index);
    }
}
