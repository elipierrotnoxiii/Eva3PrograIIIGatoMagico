using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] RaspeGenerator raspeGenerator;
    [SerializeField] TMPro.TextMeshProUGUI textInstruction;

    CatStateType gameState = CatStateType.Instantiate;

    private void Start()
    {
        raspeGenerator.GenerateCats();
    }

    private void Update()
    {
        if(Cat.state != gameState)
        {
            gameState = Cat.state;
            ChangeState(gameState);
        }
    }

    private void ChangeState(CatStateType gameState)
    {
        switch(gameState)
        {
            case CatStateType.Instantiate:
                break;
            case CatStateType.Selection:
                textInstruction.text = "Seleccion a tu gato objetivo";
                break;
            case CatStateType.Gaming:
                textInstruction.text = "Clickea al gato objetivo, para quitarle sus decimas";
                break;
            case CatStateType.Finish:
                textInstruction.text = "El juego ha terminado";
                break;
        }
    }

}
