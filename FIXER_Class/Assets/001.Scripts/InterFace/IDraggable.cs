using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDraggable 
{
    public abstract void StartDrag();
    public abstract void Drag();
    public abstract void EndDrag();
}
