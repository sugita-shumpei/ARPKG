using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ModelDataUse : MonoBehaviour
{
    [SerializeField] private Transform _rootParentBox;
    [SerializeField] private ObjectGenerator _objGen;


    private string prefabPath = "Prefabs/";

    public void Save()
    {
        TransformDataWrapper transformDataWrapper = new TransformDataWrapper();
        TransformData transformData = new TransformData();

        foreach (Transform child in _rootParentBox.transform)
        {
            //Prefab Name
            transformData.name = child.name;
            transformData.position = child.position;
            transformData.scale = child.localScale;

            //PrefabUtility.SaveAsPrefabAsset(child.gameObject, "Assets/Resources/" + prefabPath + child.name + ".prefab");

            transformDataWrapper.DataList.Add(transformData);
        }

        ModelDataManager.Save(transformDataWrapper);
    }

    public void Load()
    {
        Reset();

        TransformDataWrapper transformDataWrapper = ModelDataManager.Load();
        
        foreach(TransformData transformData in transformDataWrapper.DataList )
        {
            GameObject loadedModel = Resources.Load<GameObject>(prefabPath + transformData.name);
            _objGen.GenerateObject(loadedModel, transformData.position, transformData.scale);
        }
    }

    public void Reset()
    {
        foreach(Transform model in _rootParentBox.transform)
        {
            Destroy(model.gameObject);
        }
    }

}
