using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] GameObject _panel;
    [SerializeField] GameObject _scrollViewContent;
    [SerializeField] GameObject _addObjectScrollView;
    [SerializeField] GameObject _addObjectButton;

    private GameObject[] _prefabs;
    private Sprite[] _prefabImages;
    private Button[] _listbuttons;
    private bool _isVisible = false;

    public Button[] GetAssignedButtons()
    {
        while (_listbuttons.Any()) ;
        return _listbuttons;
    }

    void Start()
    {
        _prefabs = Resources.LoadAll<GameObject>("Prefabs");
        _prefabImages = Resources.LoadAll<Sprite>("Images/Objects");

        Transform list = _scrollViewContent.transform;
        _listbuttons = new Button[_prefabs.Length];
        
        for (int i = 0; i < _prefabs.Length; i++)
        {
            GameObject listButtonGb = Instantiate(_addObjectButton) as GameObject;
            _listbuttons[i] = listButtonGb.GetComponent<Button>();

            listButtonGb.transform.SetParent(list);
            listButtonGb.transform.Find("Object Name Text").GetComponent<Text>().text = _prefabs[i].name;
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
