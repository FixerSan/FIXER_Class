using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    public HashSet<Setter> setters = new HashSet<Setter>();

    public void SetGridSetter(Setter _setter)
    {
        if (!setters.Contains(_setter))
            setters.Add(_setter);
    }

    public void ClearSetter()
    {
        setters.Clear();
    }

}
