using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MaterialChanger : MonoBehaviour
{
    string _maskedMaterialsPath = "Materials/MaskedMaterials/";
    string _normalMaterialsPath = "Materials/NormalMaterials/";
    string _prefixMasked = "Masked_";
    Material[] _maskedMat;
    Material[] _normalMat;

    bool _useMaskedMat = true;
    bool _updatedMat = true;
    Renderer[] _objectsRenderer;
    int _totalChild = 0;


    public void UpdateChangingMaterialObjects()
    {
        _totalChild = transform.childCount;
        _objectsRenderer = new Renderer[_totalChild];
        _maskedMat = new Material[_totalChild];
        _normalMat = new Material[_totalChild];
        for (int i = 0; i < _totalChild; i++)
        {
            Transform model = transform.GetChild(i);
            _objectsRenderer[i] = model.GetComponent<Renderer>();
            _maskedMat[i] = Resources.Load<Material>(_maskedMaterialsPath 
                + _prefixMasked 
                + model.name 
                );
            _normalMat[i] = Resources.Load<Material>(_normalMaterialsPath
                + model.name
                );
            print(_maskedMat[i]);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            print("M pressed");
            _useMaskedMat = !_useMaskedMat;
            _updatedMat = false;
        }

        if (!_updatedMat)
        {
            if (_useMaskedMat)
            {
                for (int i = 0; i < _totalChild; i++)
                {
                    print("masked");

                    _objectsRenderer[i].material = _maskedMat[i];
                }
            }
            else
            {
                for (int i = 0; i < _totalChild; i++)
                {
                    print("normal");
                    _objectsRenderer[i].material = _normalMat[i];
                }
            }
            _updatedMat = true;
        }
        
    }
}
