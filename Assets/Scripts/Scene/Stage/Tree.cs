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

    private float _createAppleTime;
    private float _createAppleRemainTime;

    private int _gardenId;
    private int _createAppleCount;

    private TreeInstance _tree;

    private Action<int> _lobbyStateClickAction;

    private Action _showHudAction;

    private Action _hideHudAction;

    public void Init(int gardenId, TreeInstance tree, Action<Tree> createAction, Action<int> lobbyStateClickAction)
    {
        _gardenId = gardenId;

        _tree = tree;

        _lobbyStateClickAction = lobbyStateClickAction;

        _applePref = ResourceManager.GetPref<Apple>();

        _applePool = new ObjectPool<Apple>(CreateApple, OnGet, OnRelease, OnDestroyObj);

        _attachedAppleList = new List<Apple>();
        _activeAppleList = new List<Apple>();

        createAction?.Invoke(this);
    }

    public void Refresh()
    {
        _createAppleRemainTime = 0f;
        _showHudAction?.Invoke();
        _createAppleTime = _tree.GetGrowSpeed();
        _createAppleCount = _tree.GetAppleCount();
    }

    public void UpdateObj(float dt)
    {
        if (gameObject.activeSelf == false)
            return;

        _createAppleRemainTime += dt;

        if (_createAppleTime < _createAppleRemainTime)
        {
            _hideHudAction?.Invoke();

            _createAppleRemainTime = 0;

            while (_attachedAppleList.Count < _createAppleCount)
            {
                _applePool.Get();
            }
        }
    }

    public void AppleDrop()
    {
        if (_attachedAppleList.Count <= 0)
            return;

        _createAppleRemainTime = 0f;

        _showHudAction?.Invoke();

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
        if (StageManager.CurrGameState == GameState.Playing)
        {
            AppleDrop();
        }
        else if (StageManager.CurrGameState == GameState.Lobby)
        {
            _lobbyStateClickAction?.Invoke(_gardenId);
        }
    }

    public float GetCreateProgress()
    {
        return _createAppleRemainTime / _createAppleTime;
    }

    public void Clear()
    {
        _attachedAppleList.Clear();

        while (_activeAppleList.Count > 0)
            _applePool.Release(_activeAppleList[0]);
    }

    public void SetHideHudAction(Action action)
    {
        _hideHudAction = action;
    }

    public void SetShowHudAction(Action action)
    {
        _showHudAction = action;
    }
}
