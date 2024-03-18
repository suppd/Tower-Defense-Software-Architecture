using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnterEvent : EventArgs
{
    public BaseEnterEvent(Collider enemyCollider)
    {
        boxCollider = enemyCollider;
    }
    public Collider boxCollider = null;
}
