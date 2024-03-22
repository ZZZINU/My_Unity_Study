using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    class Pool  
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Stack<Poolable> _poolStack = new Stack<Poolable>();
    }

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Transform _root;
    public void Init()
    {
        if(_root == null)
        {
            _root = new GameObject { name = "@Pool_Root"}.transform;
            object.DontDestroyOnLoad(_root);

        }
    }

    public void Push(Poolable poolable)
    {

    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {
        return null;
    }

    public GameObject GetOrigin(string name)
    {
        return null;
    }

}
