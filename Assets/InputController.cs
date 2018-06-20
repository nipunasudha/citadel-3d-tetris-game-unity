using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Vector3 startPosition = Vector3.zero;
    private Vector3 endPosition = Vector3.zero;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // swipe begins
        {
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0)) // swipe ends
        {
            endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (startPosition != endPosition && startPosition != Vector3.zero && endPosition != Vector3.zero)
        {
            float deltaX = endPosition.x - startPosition.x;
            float deltaY = endPosition.y - startPosition.y;
            if ((deltaX > 5.0f || deltaX < -5.0f) && (deltaY >= -1.0f || deltaY <= 1.0f))
            {
                if (startPosition.x < endPosition.x) // swipe LTR
                {
                    print("LTR");
                }
                else // swipe RTL
                {
                    print("RTL");
                }
            }

            startPosition = endPosition = Vector3.zero;
        }
    }
}