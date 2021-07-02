using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackingRatioViewer : MonoBehaviour
{
    [SerializeField] private Text _packingRatioText;
    [SerializeField] private Slider _packRatioSlider;
    [SerializeField] private Transform _modelManager;
    private Transform _arBoxTransform;
    private ObjectGenerator _objGenerator;
    float _packingRatio = 0;
    float _sizeOfARBox = .1f;

    private void Start()
    {
        _objGenerator = _modelManager.GetComponent<ObjectGenerator>();
        _arBoxTransform = _modelManager.parent;
        _sizeOfARBox = _arBoxTransform.localScale.x
            * _arBoxTransform.localScale.y 
            * _arBoxTransform.localScale.z;
        print(_sizeOfARBox);
    }

    private void Update()
    {
        float sizeOfObjects = 0;
        int totalObjectsCount = _modelManager.childCount;
        for(int i = 0; i < totalObjectsCount; i++)
        {
            Vector3 objectSize = _modelManager.GetChild(i).GetComponent<Collider>().bounds.size;
            sizeOfObjects += objectSize.x * objectSize.y * objectSize.z;
        }
        _packingRatio = sizeOfObjects / _sizeOfARBox;
        print(sizeOfObjects);

        _packingRatioText.text = (_packingRatio).ToString("P0");
        _packRatioSlider.value = _packingRatio;
    }
}
