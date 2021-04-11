using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animation : MonoBehaviour
{
    public static Animation handler;
    public abstract void handle();
    public abstract void trigger(int id);
}
