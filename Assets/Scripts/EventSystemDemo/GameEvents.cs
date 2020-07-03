using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;

    private void Awake()
    {
        instance = this;
    }

    public event Action OnNumberAchieved;

    public void NumberAchieved()
    {
        OnNumberAchieved?.Invoke();
    }

    public event Action<int> OnNumberChanged;

    public void NumberChanged(int number)
    {
        OnNumberChanged?.Invoke(number);
    }
}
