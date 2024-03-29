using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[RequireComponent(typeof(MenuUIManager))]
public class ButtonObjectGenerator : MonoBehaviour
{
    [SerializeField] private ObjectGenerator _objGen;
    private GameObject[] _prefabs;
    private Button[] _buttons;

    private async void Start()
    {
        _prefabs = Resources.LoadAll<GameObject>("Prefabs");
        var a = transform.GetComponent<MenuUIManager>();
        _buttons = await Task.Run(() => {
            return a.GetAssignedButtons();
        });
        

        for (int i = 0; i < _prefabs.Length; i++)
        {
            //This "ci" is necessary due to the process of AddListener
            int ci = i;
            _buttons[i].GetComponent<Button>().onClick.AddListener(() => {
                _objGen.GenerateObject(_prefabs[ci]); 
            });
        }
    }
}
