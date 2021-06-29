using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] private Transform _parentObject;
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private Button[] _buttons;
    public void Start()
    {
        for(int i = 0; i < _prefabs.Length; i++)
        {
            _buttons[i].GetComponent<Button>().onClick.AddListener(() => { GenerateObject(_prefabs[i]); });
        }

    }

    private void GenerateObject(GameObject gb)
    {
        GameObject obj = Instantiate(gb);
        obj.transform.parent = _parentObject;
        obj.transform.localPosition = new Vector3(0, 0, 0);
        obj.GetComponent<Rigidbody>().useGravity = false;

    }
}
