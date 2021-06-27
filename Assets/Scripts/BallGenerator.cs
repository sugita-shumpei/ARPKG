using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject    _parent;
    private GameObject [] _genObjects;
    private int           _numObjects    = 0;
    [SerializeField]
    GameObject            _baseObject;
    [SerializeField]
    private Vector3       _localScale;
    [SerializeField]
    private int           _numMaxObjects = 0;
    [SerializeField]
    private int           _numGenObjects = 0;
    [SerializeField]
    private Vector3       _minRange;
    [SerializeField]
    private Vector3       _maxRange;
    void Start()
    {
        _genObjects = new GameObject[_numMaxObjects];
    }
    public void GenerateBall(){
        int numGenObject = System.Math.Min(_numMaxObjects-_numObjects,_numGenObjects);
        for(int i=0;i<numGenObject;++i){
            Vector3 randomPosition;
            randomPosition.x                                   = Random.Range(_minRange.x,_maxRange.x);
            randomPosition.y                                   = Random.Range(_minRange.x,_maxRange.y);
            randomPosition.z                                   = Random.Range(_minRange.x,_maxRange.z);
            _genObjects[_numObjects+i]                         = GameObject.Instantiate(_baseObject) as GameObject;
            _genObjects[_numObjects+i].transform.parent        = _baseObject.transform.parent;
            _genObjects[_numObjects+i].transform.localPosition = randomPosition;
            _genObjects[_numObjects+i].transform.localScale    = _localScale;
            Debug.Log(randomPosition);
        }
        _numObjects = _numObjects + numGenObject;
        _parent     = transform.root.gameObject;
        Debug.Assert(_baseObject!=null);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
