using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
ECS or MVC conception is applied on a simple calculator.
Firstly, entities and their states are defined as the game model.
And then, give some components/controls which can modify the model.
Finally, system handler OnGUI provides a UI view for the calculator.
*/

public class SimpleCalculator : MonoBehaviour {

    // Entities and their states / Model
    private string input = "";
    private string result = "";
    private float firstOperand;
    private float secondOperand;
    private string operation;
    private bool resultDisplayed = false;
    // System Handlers
    void Start () {
        Init();
    }

    // View to render entities / models
    // Here! you cannot modify the model directly, use components/controls to do it
    void OnGUI() {
        GUI.Box(new Rect(150, 25, 400, 500), "");
        // 背景
        GUI.Label(new Rect(220, 40, 280, 50), input == "" ? result : input, new GUIStyle() { fontSize = 24 });

        // 按钮
        string[] buttons = { "7", "8", "9", "/", "4", "5", "6", "*", "1", "2", "3", "-", "0", ".", "=", "+" };
        int count = 0;
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 4; j++) {
                if (GUI.Button(new Rect(230 + j * 70, 160 + i * 70, 60, 60), buttons[count])) {
                    ButtonPressed(buttons[count]);
                }
                count++;
            }
        }

        // 清除按钮
        if (GUI.Button(new Rect(230, 430, 260, 40), "Clear")) Init();
    }

    // Components /controls
    void Init() {
        input = "";
        result = "";
        firstOperand = 0;
        secondOperand = 0;
        operation = "";
        resultDisplayed = false;
    }

    void ButtonPressed(string button) {
        if (button == "=") {
            CalculateResult();
        } else if ("+-*/".Contains(button)) {
            SetOperation(button);
        } else {
            if (resultDisplayed) {
                input = button;  
                resultDisplayed = false;
            } else {
                input += button;
            }
        }
    }

    void SetOperation(string op) {
        if (input != "" || result != "") {
              // 在原有答案上继续操作
            if (resultDisplayed) {
                firstOperand = float.Parse(result);
                resultDisplayed = false;
            } else if (input != "") {
                firstOperand = float.Parse(input);
            }
            operation = op;
            input = ""; 
        }
    }

    void CalculateResult() {
        if (input != "" && operation != "") {
            secondOperand = float.Parse(input);
            switch (operation) {
                case "+":
                    result = (firstOperand + secondOperand).ToString();
                    break;
                case "-":
                    result = (firstOperand - secondOperand).ToString();
                    break;
                case "*":
                    result = (firstOperand * secondOperand).ToString();
                    break;
                case "/":
                    result = (secondOperand != 0) ? (firstOperand / secondOperand).ToString() : "Error";
                    break;
            }
            input = ""; 
            operation = "";  
            resultDisplayed = true; 
        }
    }
}
