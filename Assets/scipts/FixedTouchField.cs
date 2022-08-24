using UnityEngine;
using UnityEngine.EventSystems;
using Lean.Touch;
public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
  //  [HideInInspector]
    public Vector2 TouchDist;
    [HideInInspector]
    public Vector2 PointerOld;
    [HideInInspector]
    protected int PointerId;
    [HideInInspector]
    public bool Pressed;
    Touch touch;
    public Vector2 oldposition;
    public Vector2 pos;
    // Use this for initialization
 
 
    // Update is called once per frame
    void Update()
    {

        //foreach (Touch touch in Input.touches)
        //{
        //    bool test = false;
        //    TouchDist = touch.position - oldposition;
        //    //If we have touched the right side of the screen.
        //    if (touch.phase == TouchPhase.Began && touch.position.x > Screen.width / 2)
        //    {
        //        test = true;
        //    }
        //    else
        //    {
        //        test = false;
        //    }
        //    if (touch.phase == TouchPhase.Moved && test)
        //    {
        //        TouchDist = touch.position - oldposition;
        //        oldposition = touch.position;
        //    }

        //    if (touch.phase == TouchPhase.Ended)
        //    {
        //        TouchDist = new Vector2();
        //    }

        //}


  



        if (Pressed)
        {

            if (PointerId >= 0 && PointerId < Input.touches.Length)
            {


                if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                    TouchDist = Input.touches[PointerId].position - PointerOld;
                    PointerOld = Input.touches[PointerId].position;
                }
                else
                {
                    TouchDist = new Vector2();
                }



            }
            else
            {
                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                PointerOld = Input.mousePosition;
            }

         //   TouchDist =touch.deltaPosition;
        }
        else
        {
            TouchDist = new Vector2();
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;

        PointerId = eventData.pointerId;
        PointerOld = eventData.position;
     //   touch = Input.GetTouch(Input.touchCount-1);
      
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
       
    }

}
