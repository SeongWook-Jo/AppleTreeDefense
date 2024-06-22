using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.Pool;

public class Tree : MonoBehaviour, IPointerClickHandler
{
    public float appleDropSpeed;

    private Apple _applePref;

    private ObjectPool<Apple> _applePool;

    private List<Apple> _attachedAppleList;

    private float _createTime;
    private float _tempTime;

    private int _appleCount;

    private TreeInstance _tree;

    public void Init(TreeInstance tree, Action<Tree> createAction)
    {
        _tree = tree;

        _applePref = ResourceManager.GetPref<Apple>();

        _applePool = new ObjectPool<Apple>(CreateApple, OnGet, OnRelease);

        _attachedAppleList = new List<Apple>();

        _createTime = _tree.GetGrowSpeed();
        _appleCount = _tree.GetAppleCount();

        createAction?.Invoke(this);
    }

    void Update()
    {
        _tempTime += Time.deltaTime;

        if (_createTime < _tempTime)
        {
            _tempTime = 0;

            while (_attachedAppleList.Count < _appleCount)
            {
                _applePool.Get();
            }
        }
    }

    public void AppleDrop()
    {
        foreach (var apple in _attachedAppleList)
            apple.Drop();

        _attachedAppleList.Clear();
    }

    private Apple CreateApple()
    {
        var apple = _applePref.MakeInstance(transform);

        apple.Init(ReleaseApple);

        return apple;
    }

    private void OnGet(Apple apple)
    {
        var ranPo = UnityEngine.Random.insideUnitCircle * 0.3f;

        apple.Set(new Vector3(ranPo.x, ranPo.y), appleDropSpeed);

        _attachedAppleList.Add(apple);

        apple.gameObject.SetActive(true);
    }

    private void OnRelease(Apple apple)
    {
        apple.gameObject.SetActive(false);
    }

    private void ReleaseApple(Apple apple)
    {
        _applePool.Release(apple);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AppleDrop();
    }

    public float GetCreateProgress()
    {
        return _tempTime / _createTime;
    }
}
