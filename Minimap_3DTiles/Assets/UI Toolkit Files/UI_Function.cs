using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Function : MonoBehaviour
{
    private Button option1;
    private Button option2;
    public GameObject SeaLevel2050;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        SeaLevel2050.SetActive(false);

        option1 = root.Q<Button>("BM_BB_1");
        option2 = root.Q<Button>("BM_BB_2");

        option1.clicked += () => Option1Clicked();
        option2.clicked += () => Option2Clicked();
    }

    public void Option1Clicked()
    {
        SeaLevel2050.SetActive(false);
    }

    public void Option2Clicked()
    {
        SeaLevel2050.SetActive(true);
    }
}
