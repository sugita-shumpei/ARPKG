using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ModelDataUse : MonoBehaviour
{
    [SerializeField] private Transform _modelParent;
    private string prefabPath = "Prefabs/";

    public void Save()
    {
        TransformDataWrapper transformDataWrapper = new TransformDataWrapper();
        TransformData transformData = new TransformData();

        foreach (Transform child in _modelParent.transform)
        {
            //Prefab Name
            transformData.name = child.name;
            transformData.position = child.position;
            transformData.Scale = child.localScale;

            PrefabUtility.SaveAsPrefabAsset(child.gameObject, "Assets/Resources/" + prefabPath + child.name + ".prefab");

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
            //Instantiate from Path
            GameObject loadedModel = Resources.Load<GameObject>(prefabPath + transformData.name);
            GameObject prefab = Instantiate(loadedModel);
            //Set Data
            prefab.transform.position = transformData.position;
            prefab.transform.localScale = transformData.Scale;


            prefab.transform.parent = _modelParent;
        }
    }

    public void Reset()
    {
        foreach(Transform model in _modelParent.transform)
        {
            Destroy(model.gameObject);
        }
    }

}
