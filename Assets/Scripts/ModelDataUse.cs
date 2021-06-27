using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelDataUse : MonoBehaviour
{
    [SerializeField] private Transform _modelParent;

    private void Save()
    {


       
    }
    
    private void Load()
    {
        TransformDataWrapper transformDataWrapper = ModelDataManager.Load();
        
        foreach(TransformData transformData in transformDataWrapper.DataList )
        {
            //Instantiate from Path


            //Set Data


        }
    }

}
