using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSetter : GridObject
{
    private Material material;

    public override void Init(int _gridIndexX, int _gridIndexY, int _gridIndexZ)
    {
        base.Init(_gridIndexX, _gridIndexY, _gridIndexZ);
        material = GetComponent<MeshRenderer>().material;
    }

    public void Select()
    {
        material.color = Color.red;
    }

    public void DeSelect()
    {
        material.color = Color.white;
    }
}
