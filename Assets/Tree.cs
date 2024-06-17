using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tree : MonoBehaviour, IPointerClickHandler
{
    public float appleDropSpeed;

    private Apple _applePref;

    private List<Apple> _appleList;

    private float _createTime;
    private float _tempTime;

    private int _appleCount;

    private TreeInstance _tree;

    public void Init(TreeInstance tree)
    {
        _tree = tree;

        _appleList = new List<Apple>();

        _applePref = Resources.Load<Apple>("Prefabs/Apple");

        _createTime = _tree.GetGrowSpeed();
        _appleCount = _tree.GetAppleCount();
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
            var ranPo = Random.insideUnitCircle;

            var apple = Instantiate(_applePref, new Vector3(ranPo.x + transform.position.x, Mathf.Abs(ranPo.y) + transform.position.y, 0), Quaternion.identity);

            apple.dropSpeed = appleDropSpeed;

            _appleList.Add(apple);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AppleDrop();
    }
}
