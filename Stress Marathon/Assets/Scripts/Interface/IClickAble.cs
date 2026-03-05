using System;
using UnityEngine;

public interface IClickAble
{
    public event Action<IClickAble> OnClickSound;
}
