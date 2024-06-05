using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public void Awake()
    {
        //ΩÃ±€≈Ê
        if (GameManager.Instance == null)
            GameManager.Instance = this;
        else Destroy(this);
    }

    private void Update()
    {
        CheckClickableObject();
    }

    private void CheckClickableObject()
    {

    }
}
