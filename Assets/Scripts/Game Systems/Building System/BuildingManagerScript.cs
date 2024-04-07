using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BuildingManagerScript : Singleton<BuildingManagerScript> //inherit from singelton class
{
    /// <summary>
    /// Building Manager script
    /// has diffrent prefabs that the player can place (right now its 3 diffrent ones)
    /// gives information to playerinfo class for the cost of each build
    /// changes the turret to build variable that the building node class can use
    /// </summary>
    //Public Lists that the HandleBuildingUI script uses to update the values of spawned UI elements
    public List<Button> InstaniatedButtons;
    public List<TowerScriptableObject> TowerDatas;
    public List<GameObject> InfoPanels;
    private TowerBase turretToBuild = null;
    [SerializeField]
    public List<TowerBase> buildableTowers;
    [SerializeField]
    private Button buttonPrefab;
    [SerializeField]
    private Canvas buttonCanvas;
    private int numberOfButtons;
    private float xOffsetRatio = 0.2f; // Ratio of screen width for X offset
    private float yOffsetRatio = 0.15f; // Ratio of screen height for Y offset
    private float buttonSpacingRatio = 0.05f; // Ratio of screen width for button spacing
    public TowerBase GetTurretToBuild()
    {
        return turretToBuild;
    }
    private void SetTurretToBuild(int index)
    {
        turretToBuild = buildableTowers[index];
        //Make sure that when changing the tower we're building the player info manager also knows the latest build cost
        PlayerInfo.BuildCost = TowerDatas[index].TowerPrice;
    }
    private void Awake()
    {
        float screenWidth = Screen.width;
        numberOfButtons = buildableTowers.Count;
        for (int i = 0; i < numberOfButtons; i++)
        {
            // Calculate the x position for the current button
            float xPos = (i + (numberOfButtons - 1) / 2.0f) * (screenWidth * xOffsetRatio + screenWidth * buttonSpacingRatio);
            // Instantiate the button prefab
            Button button = Instantiate(buttonPrefab, buttonCanvas.transform);
            // Set the position of the button
            button.transform.position = new Vector3(xPos, Screen.height * yOffsetRatio, 0f);
            //set on click event
            int currentIndex = i;
            button.onClick.AddListener(() => SetTurretToBuild(currentIndex));
            //add to public list so HandleBuildingUI can use button refrences
            InstaniatedButtons.Add((button));
            //add to public list so HandleBuildingUI knows what towers were used for example if i had 5 towers but only 3 diffrent types / 3 buttons were made it knows which of the 5
            TowerDatas.Add(buildableTowers[i].TowerDataScript);
        }
        foreach (Button button in InstaniatedButtons)
        {
            GameObject infoPanel = button.gameObject.transform.Find("InfoPanel").gameObject;
            InfoPanels.Add(infoPanel);
        }
    }
    void ButtonClicked(int index) //debug method
    {
        Debug.Log("Button " + index + " clicked!");
    }
}
