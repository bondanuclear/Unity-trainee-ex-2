using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
     Vector2 startPos;
     Vector2 endPos;
     public Vector2 GetMobileInput()
     {
          Vector2 dir = Vector2.zero;
          if(Input.touchCount > 0)
          {
               Debug.Log("touching the screen");
               if(Input.GetTouch(0).phase == TouchPhase.Began)
               {
                    startPos = Input.GetTouch(0).position;
               }
               if(Input.GetTouch(0).phase == TouchPhase.Ended)
               {
                    endPos = Input.GetTouch(0).position;
                    dir = (endPos - startPos).normalized;
               }
          }

          return dir;
     }
     public Vector2 BlocksDirection(Vector2 inputDir)
     {
          if(Vector2.Dot(Vector2.up, inputDir) > 0.9f) return Vector2.up;
          if (Vector2.Dot(Vector2.down, inputDir) > 0.9f) return Vector2.down;
          if (Vector2.Dot(Vector2.right, inputDir) > 0.9f) return Vector2.right;
          if (Vector2.Dot(Vector2.left, inputDir) > 0.9f) return Vector2.left;

          return Vector2.zero;
     }
    // mobile input
   public Vector2 GetPlayerInput()
   {
    
        if(Input.GetKeyDown(KeyCode.LeftArrow))  return Vector2.left;
        if (Input.GetKeyDown(KeyCode.RightArrow)) return Vector2.right;
        if (Input.GetKeyDown(KeyCode.UpArrow)) return Vector2.up;
        if (Input.GetKeyDown(KeyCode.DownArrow)) return Vector2.down;

        return Vector2.zero;
   }
    
}
