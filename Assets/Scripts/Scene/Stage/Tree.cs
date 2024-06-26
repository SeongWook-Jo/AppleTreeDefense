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
    private List<Apple> _activeAppleList;

    private float _createTime;
    private float _tempTime;

    private int _createAppleCount;

    private TreeInstance _tree;

    public void Init(TreeInstance tree, Action<Tree> createAction)
    {
        _tree = tree;

        _applePref = ResourceManager.GetPref<Apple>();

        _applePool = new ObjectPool<Apple>(CreateApple, OnGet, OnRelease, OnDestroyObj);

        _attachedAppleList = new List<Apple>();
        _activeAppleList = new List<Apple>();

        _createTime = _tree.GetGrowSpeed();
        _createAppleCount = _tree.GetAppleCount();

        createAction?.Invoke(this);
    }

    public void UpdateObj(float dt)
    {
        if (gameObject.activeSelf == false)
            return;

        _tempTime += dt;

        if (_createTime < _tempTime)
        {
            _tempTime = 0;

            while (_attachedAppleList.Count < _createAppleCount)
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
        _activeAppleList.Add(apple);

        apple.gameObject.SetActive(true);
    }

    private void OnRelease(Apple apple)
    {
        _activeAppleList.Remove(apple);

        apple.gameObject.SetActive(false);
    }

    private void ReleaseApple(Apple apple)
    {
        _applePool.Release(apple);
    }

    private void OnDestroyObj(Apple apple)
    {
        Destroy(apple);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AppleDrop();
    }

    public float GetCreateProgress()
    {
        return _tempTime / _createTime;
    }

    public void Clear()
    {
        _attachedAppleList.Clear();

        while (_activeAppleList.Count > 0)
            _applePool.Release(_activeAppleList[0]);
    }
}
