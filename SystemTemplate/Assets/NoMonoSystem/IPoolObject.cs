using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    public abstract void SetUp();
    public abstract void Clear();
}