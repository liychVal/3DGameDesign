using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    IUserAction userAction;
    public string gameMessage ;
    public int time;
    GUIStyle style, bigstyle;
    // Start is called before the first frame update
    void Start()
    {
        time = 100;
        userAction = SSDirector.GetInstance().CurrentSceneController as IUserAction;

        style = new GUIStyle();
        style.normal.textColor = Color.black;
        style.fontSize = 15;

        bigstyle = new GUIStyle();
        bigstyle.normal.textColor = Color.black;
        bigstyle.fontSize =25;

        
        
    }

    // Update is called once per frame
    void OnGUI() {
        userAction.Check();
        GUI.Label(new Rect(200, Screen.height * 0.1f, 50, 200), "Preists and Devils", bigstyle);
        GUI.Label(new Rect(250, 100, 50, 200), gameMessage, style);
        GUI.Label(new Rect(0,0,100,50), "Time: " + time, style);
    }
    
}
