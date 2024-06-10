using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setter : GridObject
{
    public MeshRenderer selectEffect;
    public Material unSelectEffectMaterial;
    public Material selectEffectMaterial;
    private bool isSelected = false;

    public override void Init(int _gridIndexX, int _gridIndexY, int _gridIndexZ)
    {
        base.Init(_gridIndexX, _gridIndexY, _gridIndexZ);
        selectEffect.enabled = false;
        ObjectManager.Instance.SetGridSetter(this);
    }

    public void SetSelectState(bool _isSelecting)
    {
        if (isSelected) return;
        selectEffect.enabled = _isSelecting;
    }

    public void Select()
    {
        selectEffect.material = selectEffectMaterial;
    }

    public void UnSelect()
    {
        selectEffect.material = unSelectEffectMaterial;
    }

    public void UseSetter(int _defenderIndex)
    {
        GridObject gridObject = ResourceManager.Instance.Instantiate($"Defender_{_defenderIndex}").GetComponent<GridObject>();
        GridManager.Instance.EnGrid(gridObject, gridIndexX, gridIndexY + 1, gridIndexZ, _defenderIndex);
    }
}
