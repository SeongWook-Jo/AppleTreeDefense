using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tree : MonoBehaviour, IPointerClickHandler
{
    public float createTime;
    public float appleDropSpeed;

    private Apple _applePref;

    private List<Apple> _appleList;

    private float _tempTime;

    public void Init()
    {
        _appleList = new List<Apple>();

        _applePref = Resources.Load<Apple>("Prefabs/Apple");
    }

    void Update()
    {
        _tempTime += Time.deltaTime;

        if (createTime < _tempTime)
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
        var ranPo = Random.insideUnitCircle;

        var apple = Instantiate(_applePref, new Vector3(ranPo.x + transform.position.x, Mathf.Abs(ranPo.y) + transform.position.y, 0), Quaternion.identity);

        apple.dropSpeed = appleDropSpeed;

        _appleList.Add(apple);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AppleDrop();
    }
}
