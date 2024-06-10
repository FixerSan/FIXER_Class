using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class UIScene_Main : UIScene
{
    public List<UICard> cardUIs = new List<UICard>();
    public Transform cardTrans;

    public override void Init()
    {
        Redraw();
    }

    public override void Redraw()
    {
        CreateCardUI();
    }

    public void CreateCardUI()
    {
        for (int i = 0; i < GameManager.Instance.ingame.cardIndexes.Count; i++)
        {
            UICard card = ResourceManager.Instance.Instantiate(typeof(UICard).Name).GetComponent<UICard>();
            card.transform.SetParent(cardTrans);
            card.Init(GameManager.Instance.ingame.cardIndexes[i]); 
        }
    }
}
