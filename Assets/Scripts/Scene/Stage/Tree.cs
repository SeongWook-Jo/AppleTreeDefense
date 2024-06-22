using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.Pool;

public class Tree : MonoBehaviour, IPointerClickHandler
{
    public float appleDropSpeed;

    private Apple _applePref;

    private List<Apple> _appleList;

    private float _createTime;
    private float _tempTime;

    private int _appleCount;

    private TreeInstance _tree;

    public void Init(TreeInstance tree, Action<Tree> createAction)
    {
        _tree = tree;

        _appleList = new List<Apple>();

        _applePref = ResourceManager.GetPref<Apple>();

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

            CreateApple();
        }
    }

    public void AppleDrop()
    {
        while(_appleList.Count > 0)
        {
            var apple = _appleList[0];
            apple.Drop();
            _appleList.RemoveAt(0);
        }
    }

    private void CreateApple()
    {
        while(_appleCount > _appleList.Count)
        {
            var ranPo = UnityEngine.Random.insideUnitCircle * 0.3f;

            var apple = _applePref.MakeInstance(transform);

            apple.transform.localPosition += new Vector3(ranPo.x, ranPo.y);

            apple.dropSpeed = appleDropSpeed;

            _appleList.Add(apple);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AppleDrop();
    }

    public float GetCreateTime()
    {
        return _tempTime / _createTime;
    }
}
