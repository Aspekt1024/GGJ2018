using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageInteract : MonoBehaviour {

    public Sprite[] Icons;

    Button[] UI_Btns;
    

	// Use this for initialization
	void Start () {
        UI_Btns = GetComponentsInChildren<Button>();
        print(UI_Btns.Length);

        foreach (Button btn in UI_Btns)
        {
            btn.onClick.AddListener(ButtonClicked);
        }
    }
	

    void ButtonClicked()
    {
        print("You clicked");

        //Make call to Message Instantiate script
        //to create msg gameobject


        //pick new random icon from Icons list to be the image
        //TODO: Address duplicate icons
        foreach(Button btn in UI_Btns)
        {
            Image temp = btn.GetComponent<Image>();
            temp.sprite = Icons[Random.Range(0, Icons.Length)];
        }
    }



}
