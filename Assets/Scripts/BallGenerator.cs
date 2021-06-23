using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject    _parent;
    private GameObject [] _genObjects;
    
    [SerializeField]
    GameObject            _baseObject;
    [SerializeField]
    private Vector3       _localScale;
    [SerializeField]
    private int           _numObjects = 0;
    [SerializeField]
    private Vector3       _minRange;
    [SerializeField]
    private Vector3       _maxRange;
    void Start()
    {
        _genObjects = new GameObject[_numObjects];
        for(int i=0;i<_numObjects;++i){
            Vector3 randomPosition;
            randomPosition.x                       = Random.Range(_minRange.x,_maxRange.x);
            randomPosition.y                       = Random.Range(_minRange.x,_maxRange.y);
            randomPosition.z                       = Random.Range(_minRange.x,_maxRange.z);
            _genObjects[i]                         = GameObject.Instantiate(_baseObject) as GameObject;
            _genObjects[i].transform.parent        = _baseObject.transform.parent;
            _genObjects[i].transform.localPosition = randomPosition;
            _genObjects[i].transform.localScale    = _localScale;
            Debug.Log(randomPosition);
        }
        _parent                                    = transform.root.gameObject;
        Debug.Assert(_baseObject!=null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
