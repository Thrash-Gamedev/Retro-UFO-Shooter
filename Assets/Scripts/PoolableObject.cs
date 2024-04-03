using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableObject : MonoBehaviour
{
    protected ObjectPooler _pooler;
    public void SetPool(ObjectPooler pooler)
    {
        _pooler = pooler;
    }

    public void ReleaseToPool()
    {
        _pooler.ReleaseToPool(this);
    }

    public abstract void Initialize();
}
