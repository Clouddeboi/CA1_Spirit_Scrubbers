// Author: Anastasia McCormac
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script to set panel background colours to transparent on game launch. 
/// </summary>
public class CS_UI_PanelColour : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Colour set here changes background image tint to that colour, default is to alpha 0 resulting in transparent and non-visible blocks.")]
    private Color colourSet= new Color(0,0,0,0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start () {

        this.gameObject.GetComponent<Image>().color = colourSet;
    }
}
