using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DataSO : ScriptableObject
{
    [SerializeField]
    private int _coins;
    [SerializeField]
    private int _coinsLevel1;
    [SerializeField]
    private string _level;
    [SerializeField]
    private float _vida;
    
    public int Value
    {
        get { return _coins; }
        set { _coins = value; }
    }
    public int Value1
    {
        get { return _coinsLevel1; }
        set { _coinsLevel1 = value; }
    }

    public string Level
    {
        get { return _level;  }
        set { _level = value;  }
    }

    public float Vida
    {
        get { return _vida; }
        set { _vida = value; }
    }
}
