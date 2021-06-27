using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject _panel;
    private bool isVisible = false;
    void Start()
    {
        _panel.SetActive(isVisible);
    }
    public void VisiblePanel(){
        isVisible = !isVisible;
        _panel.SetActive(isVisible);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
