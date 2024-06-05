using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public int index;
    public int gridIndexX, gridIndexY, gridIndexZ;

    public virtual void Init(int _gridIndexX, int _gridIndexY, int _gridIndexZ)
    {
        gridIndexX = _gridIndexX;
        gridIndexY = _gridIndexY;
        gridIndexZ = _gridIndexZ;
    }
}
