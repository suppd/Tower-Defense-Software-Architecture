using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGoRed : MonoBehaviour
{
    //script for makign build buttons color red if you dont have enough money for the corresponding tower build!

    public Button NormalButton;
    public Button AOEButton;
    public Button DebuffButton;


    public void Update()
    {
        if (PlayerInfo.Money < 30)
        {
            DebuffButton.image.color = Color.red;
        }
        else
        {
            DebuffButton.image.color = Color.white;
        }
        if (PlayerInfo.Money < 20)
        {
            AOEButton.image.color = Color.red;
        }
        else
        {
            AOEButton.image.color = Color.white;
        }
        if (PlayerInfo.Money < 10)
        {
            NormalButton.image.color = Color.red;
        }
        else
        {
            NormalButton.image.color = Color.white;
        }
    }
}
