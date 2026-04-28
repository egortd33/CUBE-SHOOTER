using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class test2 : MonoBehaviour
{
    public Color color = Color.white;
    public Color insideColor = Color.black;
    public int power = -7;
    public string display;


    public void OpenTheDoor(int time)
    {
        power = 8;
        display = "Hello world";

        if (time > 21 && time < 6)
        {
            TurnlightOn(GetColor(225, 10, 10));
            TurnlightOn();
        }
        else
        {
            TurnlightOn();
        }
    }

    private void TurnlightOn()
    {

    }

    private void TurnlightOn(Color _color)
    {

    }

    private Color GetColor(int color1, int color2, int color3)
    {
        return new Color(color1, color2, color3);
    }
}
