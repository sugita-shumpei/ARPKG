using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[RequireComponent(typeof(MenuUIManager))]
public class ObjectGenerator : MonoBehaviour
{
    [SerializeField] private Transform _objParent;
    [SerializeField] private float _objectScaleScalar = 1;
    [SerializeField] private Camera _cam;
    private GameObject[] _prefabs;
    private Button[] _buttons;

    private async void Start()
    {
        _prefabs = Resources.LoadAll<GameObject>("Prefabs");
        var a = transform.GetComponent<MenuUIManager>();
        _buttons = await Task.Run(() => {
            return a.GetAssignedButtons();
        });
        
        for (int i = 0; i < _prefabs.Length; i++)
        {
            //This "ci" is necessary due to the process of AddListener
            int ci = i;
            _buttons[i].GetComponent<Button>().onClick.AddListener(() => {
                GenerateObject(_prefabs[ci]); 
            });
        }

    }


    private void GenerateObject(GameObject gb)
    {
        
        GameObject obj = Instantiate(gb);
        obj.GetComponent<Rigidbody>().constraints = 
            RigidbodyConstraints.FreezeRotationX | 
            RigidbodyConstraints.FreezeRotationY | 
            RigidbodyConstraints.FreezeRotationZ;
        obj.transform.parent = _objParent;
        obj.transform.localScale = new Vector3(
            1 * _objectScaleScalar,
            1 * _objectScaleScalar,
            1 * _objectScaleScalar
            );
        obj.transform.localPosition = new Vector3(0, 0, 0);
        Vector3 scale = _objParent.parent.localScale;
        obj.GetComponent<DrawBounds>()._boxSize = scale;

        if(obj.GetComponent<Rigidbody>() == null)
        {
            obj.AddComponent<Rigidbody>();
        }
        obj.GetComponent<Rigidbody>().useGravity = false;
        obj.GetComponent<MovableBox>()._cam = _cam;

    }
}
