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

    private GameObject _virtualSphereParent;
    private float _sideXmaxSize = 4;
    private float _sideZmaxSize = 4;
    private float _boxHeight = 4;

    void Start()
    {
        _virtualSphereParent = new GameObject();
        _virtualSphereParent.name = "VirtualSphereParentObjects";
        _genObjects = new GameObject[_numMaxObjects];

        float virtualBoxScaleX = transform.parent.localScale.x;
        float virtualBoxScaleZ = transform.parent.localScale.z;

        foreach (Transform child in transform)
        {
            if(child.name == "Side_Xp")
            {
                _sideXmaxSize = child.position.x;// * virtualBoxScaleX;
                print("X: " + _sideXmaxSize);

            }
            else if(child.name == "Side_Zp")
            {
                _sideZmaxSize = child.position.z;// * virtualBoxScaleZ;
                print("Z: " + child.position.z);
            }
        }
        _sideXmaxSize -= _localScale.x * 3.5f;
        _sideZmaxSize -= _localScale.z * 3.5f;
        _boxHeight = transform.parent.localScale.y;
    }
    public void GenerateBall(){

        int numGenObject = System.Math.Min(_numMaxObjects-_numObjects,_numGenObjects);
        for(int i=0;i<numGenObject;++i){
            Vector3 randomPosition;
            randomPosition.x                                   = Random.Range(-_sideXmaxSize, _sideXmaxSize);
            randomPosition.y                                   = Random.Range(_boxHeight + _localScale.y, _boxHeight + _localScale.y * 20);
            randomPosition.z                                   = Random.Range(- _sideZmaxSize, _sideZmaxSize);
            _genObjects[_numObjects+i]                         = GameObject.Instantiate(_baseObject) as GameObject;
            _genObjects[_numObjects+i].transform.parent        = _virtualSphereParent.transform;
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
