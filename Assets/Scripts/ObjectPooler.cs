
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class ObjectPooler : MonoBehaviour
{

    [SerializeField] protected int minPoolSize = 250, maxPoolSize = 500;

    [SerializeField] protected List<PoolableObject> objPrefabList;

    protected ObjectPool<PoolableObject> _pool;


    // server only
    public void Start()
    {


        if (objPrefabList == null || objPrefabList.Count == 0)
        {
            Debug.LogWarning($"Failed to initialize pool on {gameObject.name}, prefabList is empty!");
            return;
        }

        _pool = new ObjectPool<PoolableObject>(() =>
        //create
        {
            var newObj = Instantiate(objPrefabList[Random.Range(0, objPrefabList.Count)]);
            newObj.SetPool(this);

            return newObj;
        },
        //get
        netObj => {
            netObj.gameObject.SetActive(true);

        },
        //release
        netObj => {
            netObj.gameObject.SetActive(false);
        },
        //destroy
        netObj => {
            Destroy(netObj.gameObject);
        },
        false, minPoolSize, maxPoolSize);

    }

    public virtual void ReleaseToPool(PoolableObject objToRelease)
    {
        _pool.Release(objToRelease);
    }

    public virtual PoolableObject SpawnAt(Vector3 position)
    {
        var newObj = _pool.Get();
        newObj.transform.position = position;
        newObj.Initialize();
        return newObj;
    }

    public PoolableObject SpawnAt(Vector3 position, Quaternion rotation)
    {
        var newObj = SpawnAt(position);
        newObj.transform.rotation = rotation;
        return newObj;
    }
}
