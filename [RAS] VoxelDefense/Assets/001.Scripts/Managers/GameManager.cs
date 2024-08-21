using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.Burst.CompilerServices;
using Unity.IO.LowLevel.Unsafe;
using Unity.Services.Analytics.Internal;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public IngameSystem ingame;

    public void Start()
    {
        ResourceManager.Instance.LoadAllAsync<Object>("default",_completeCallback: StartIngame);
        GridManager.Instance.EnGridOnScene();
    }

    public void StartIngame()
    {
        ingame = new IngameSystem();
        ingame.Init();
    }

    private void Update()
    {
        ingame?.Update();
    }
}

public class IngameSystem
{
    public List<int> cardIndexes = new List<int>();
    private Setter selectSetter;

    private bool isSelecting = false;

    public IngameSystem()
    {
        cardIndexes = new List<int>();
        cardIndexes.Add(1000001);
    }
    public void Init()
    {
        UIManager.Instance.ShowSceneUI<UIScene_Main>();
    }

    public void Update()
    {
        if (!isSelecting) return;
        SelectSetter();
    }

    public void SelectCard()
    {
        //Time.timeScale = 0.1f;
        isSelecting = true;
        foreach (var setter in ObjectManager.Instance.setters)
        {
            setter.SetSelectState(isSelecting);
        }
    }

    public void SelectSetter()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            ChangeSelectSetter(hit.transform.GetComponent<Setter>());
        }
    }

    public void ChangeSelectSetter(Setter _setter)
    {
        if (_setter == selectSetter) return;
        selectSetter?.UnSelect();
        selectSetter = _setter;
        selectSetter?.Select();
    }

    public void DropCard(UICard _card)
    {
        CancleCard();
        if (selectSetter != null)
        {
            selectSetter.UseSetter(_card.defenderIndex);
            return;
        }
    }

    public void CancleCard()
    {
        isSelecting = false;
        foreach (var setter in ObjectManager.Instance.setters)
        {
            setter.SetSelectState(isSelecting);
        }
    }
}
