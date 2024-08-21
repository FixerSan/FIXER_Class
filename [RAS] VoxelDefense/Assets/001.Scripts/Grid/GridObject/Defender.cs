using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : GridObject
{
    public DefenderStatus status;
    public DefenderData data;

    public override void Init(int _gridIndexX, int _gridIndexY, int _gridIndexZ, int _index)
    {
        base.Init(_gridIndexX, _gridIndexY, _gridIndexZ, _index);
        data = DataManager.Instance.GetDefenderData(_index);
    }

    public void Hit(float _damage)
    {
        status.CurrentHP -= _damage;
    }
}

public class DefenderStatus
{
    private float maxHp;
    private float currentHP;
    public float CurrentHP
    {
        get { return currentHP; }
        set
        {
            currentHP += value;
        }
    }
}
