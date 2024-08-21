using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public UIScene sceneUI;

    public UIScene ShowSceneUI<T>() where T : UIScene
    {
        sceneUI = ResourceManager.Instance.Instantiate(typeof(T).Name).GetComponent<UIScene>();
        sceneUI.Init();
        return sceneUI;
    }
}
