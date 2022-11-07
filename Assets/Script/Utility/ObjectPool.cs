using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public interface IPool<T> where T : MonoBehaviour
{
    void Get(Action<T> onLoad);
    void Return(T obj);
}

public class ObjectPool<T> : IPool<T> where T : MonoBehaviour
{
    private AssetReference assetRef = null;
    private Queue<T> poolQueue = new Queue<T>();
    private int loadWaitCnt = 0;
    private Action LoadComplete = null;
    private Transform parent;

    public void Init(AssetReference _ref, Transform _parent, int size)
    {
        parent = _parent;
        assetRef = _ref;
        Create(size);
    }

    public void Create(int size)
    {
        if(assetRef != null)
        {
            loadWaitCnt = size;
            for(var i = 0; i < size; ++i)
            {
                assetRef.InstantiateAsync(parent, false).Completed += OnAssetLoadComp;
            }            
        }
    }

    private void OnAssetLoadComp(AsyncOperationHandle<GameObject> handle)
    {
        poolQueue.Enqueue(handle.Result.GetComponent<T>());
        handle.Result.SetActive(false);
        --loadWaitCnt;
        if(loadWaitCnt <= 0)
        {
            LoadComplete?.Invoke();
            LoadComplete = null;
        }
    }

    public void Get(Action<T> onLoad)
    {
        Action Complete = () => {
            var obj = poolQueue.Dequeue();      
            obj.gameObject.SetActive(true);
            onLoad?.Invoke(obj);
        };
        if(poolQueue.Count < 1)
        {
            LoadComplete = Complete;
            Create(5);
        }            
        else
            Complete();
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        poolQueue.Enqueue(obj);
    }

    public void AllObjectActives()
    {
        foreach (var obj in poolQueue)
            if (obj != null && obj.gameObject != null)
                obj.gameObject.SetActive(false);

    }

    public void Clear()
    {
        foreach(var obj in poolQueue)
            if(obj != null && obj.gameObject != null)
                Addressables.ReleaseInstance(obj.gameObject);
        
        poolQueue.Clear();
    }
}
