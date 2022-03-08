using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cell : MonoBehaviour
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public bool Alive { get; private set; }

    private MeshRenderer _mr;

    private void SetAliveColor()
    {
        _mr.material.color = Alive ? Color.white : Color.black;
    }
    public void SetCoords(int x, int y)
    {
        X = x;
        Y = y;
    }
    public void ChangeState()
    {
        Alive = !Alive;
        SetAliveColor();
    }

    private void Start()
    {
        Alive = false;
        if (TryGetComponent(out _mr)) { SetAliveColor(); }
    }
}
