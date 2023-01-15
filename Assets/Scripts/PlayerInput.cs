using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    
   public Vector2 GetPlayerInput()
   {
        if(Input.GetKeyDown(KeyCode.LeftArrow))  return Vector2.left;
        if (Input.GetKeyDown(KeyCode.RightArrow)) return Vector2.right;
        if (Input.GetKeyDown(KeyCode.UpArrow)) return Vector2.up;
        if (Input.GetKeyDown(KeyCode.DownArrow)) return Vector2.down;

        return Vector2.zero;
   }
    
}
