using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IClickable 
{
    public Transform selectTransform { get; }
    public abstract void Select();
    public abstract void Click();
    public abstract void DeSelect();
}
