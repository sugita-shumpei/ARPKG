using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    [SerializeField] GameObject _scrollViewContent;
    [SerializeField] GameObject _addObjectScrollView;
    [SerializeField] GameObject _addObjectButton;

    private GameObject[] _prefabs;
    private Sprite[] _prefabImages;
    private bool _isVisible = false;

    void Start()
    {
        _prefabs = Resources.LoadAll<GameObject>("Prefabs");
        _prefabImages = Resources.LoadAll<Sprite>("Images/Objects");

        Transform list = _scrollViewContent.transform;
        for (int i = 0; i < _prefabs.Length; i++)
        {
            GameObject listButton = Instantiate(_addObjectButton) as GameObject;

            listButton.transform.SetParent(list);
            listButton.transform.Find("Object Name Text").GetComponent<Text>().text = _prefabs[i].name;
        }
        _addObjectScrollView.SetActive(_isVisible);
        _panel.SetActive(_isVisible);

        this.GetComponent<Button>().onClick.AddListener(() => VisiblePanel() );
    }

    public void VisiblePanel(){
        print("VisivlePanel ran: _isVisible = " + _isVisible);

        _isVisible = !_isVisible;

        _addObjectScrollView.SetActive(_isVisible);
        _panel.SetActive(_isVisible);
    }

}
