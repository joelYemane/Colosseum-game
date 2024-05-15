using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Ellement
{
    None,
    Fire,
    Water,
    Wind,
    Electro
}

[CreateAssetMenu(fileName = "EllementStone", menuName = "Ellement", order = 1)]
public class EllementStoneScripObject : ScriptableObject
{
    public Color _EmissionColor;

    public Ellement _ellement = new Ellement();

    public Drop _drop;

    [SerializeField]
    public class Drop
    {
        public GameObject _stoneObj;
    }
}
