using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayTouchManager : MonoBehaviour
{
    public GameManager gM;
    //public Text testTouch;
    public Vector2[] TouchPOS = new Vector2[2];
    public bool[] isTouch = new bool[2];
    public GameObject cursorObj;
    public GameObject cursorZero, cursorOne;
    RectTransform rT;
    [SerializeField]
    //List<Touch> touches = new List<Touch>();
    Touch[] touches;
    public TextMeshProUGUI demoText;
    // Start is called before the first frame update
    void Start()
    {
        Input.multiTouchEnabled = true;
        gM = FindObjectOfType<GameManager>();
        rT = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        TouchDetection();
        //TouchDetectionTwo();
    }

    void TouchDetection()
    {
        //demoText.text = Input.touchCount.ToString();
        if (Input.touchCount > 0 && (gM.touchedObjects == null || gM.touchedObjects == this.gameObject))
        {
            touches = Input.touches;
            if (isTouchInField(touches[0]))
            {
                isTouch[0] = true;
                TouchPOS[0] = touches[0].position;
                gM.touchedObjects = this.gameObject;
                
                Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(TouchPOS[0]);
                cursorPosition += new Vector2(0, 1f);
                if (cursorZero == null)
                {
                    cursorZero = (GameObject)Instantiate(cursorObj, cursorPosition, transform.rotation);
                }
                else
                {
                    cursorZero.transform.position = cursorPosition;
                }
            }
            
            //For Second Touch
            if (Input.touchCount > 1)
            {
                if (isTouchInField(touches[1]))
                { 
                        isTouch[1] = true;
                        //gM.touchedObjects = this.gameObject;
                        //TouchPOS[1] = Input.GetTouch(1).position;
                        TouchPOS[1] = touches[1].position;
                       
                        Vector2 cursorPositionOne = Camera.main.ScreenToWorldPoint(TouchPOS[1]);
                        cursorPositionOne += new Vector2(0, 1f);
                        if (cursorOne == null)
                        {
                            cursorOne = (GameObject)Instantiate(cursorObj, cursorPositionOne, transform.rotation);
                            cursorOne.GetComponent<CursorGUI>().colorNum = 1;
                        }
                        else
                        {
                            cursorOne.transform.position = cursorPositionOne;
                        }
                 }
            }
            else
            {
                isTouch[1] = false;
                TouchPOS[1] = Vector2.zero;
                Destroy(cursorOne);
            }
        }
        else
        {
            
            
            isTouch[0] = false;
            isTouch[1] = false;
            TouchPOS[0] = Vector2.zero;
            TouchPOS[1] = Vector2.zero;
            Destroy(cursorZero);
            Destroy(cursorOne);
            if (gM.touchedObjects == this.gameObject)
            {
                gM.touchedObjects = null;
            }
        }
    }
    
    void TouchDetectionTwo()
    {
        for(int i = 0; i < Input.touchCount; i++)
        {
            Vector2 touchPOS = Input.touches[i].position;
            //Touch Phase Began

            //Touch Phase Moved
        }
    }
    
    bool isTouchInField(Touch touchArea)
    {
        /*
        float MaxX = (GetComponent<RectTransform>().rect.width * transform.parent.GetComponent<Canvas>().scaleFactor);
        float MaxY = (GetComponent<RectTransform>().rect.height * transform.parent.GetComponent<Canvas>().scaleFactor);
        float MinY = (GetComponent<RectTransform>().rect.y);
        */
        //if((touchArea.position.x < GetComponent<RectTransform>().rect.width && touchArea.position.x < GetComponent<RectTransform>().rect.x) && (touchArea.position.y > GetComponent<RectTransform>().rect.height && touchArea.position.y < GetComponent<RectTransform>().rect.y))
        //if ((touchArea.position.x < GetComponent<RectTransform>().rect.width && touchArea.position.x < GetComponent<RectTransform>().rect.x))
        //
        //if ((touchArea.position.x < MaxX && touchArea.position.x > 0) && (touchArea.position.y > MaxX && touchArea.position.y < MinY))

        //Vector2 screenPOS = Camera.main.ScreenToWorldPoint(touchArea.position) ;
        //Vector2 localPosition = transform.InverseTransformPoint(touchArea.position);
        //if (RectTransformUtility.RectangleContainsScreenPoint(rT, touchArea.position, Camera.main))
        if (RectTransformUtility.RectangleContainsScreenPoint(rT, touchArea.position, Camera.main))
        //if(rT.rect.Contains(localPosition))
        //if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rT, localPosition, Camera.main, out screenPOS))
        {
            //testTouch.text = "In Area";
            return true;
        }
        else
        {

            //testTouch.text = "Not In Area";
            //testTouch.text = touchArea.position.y + MinY.ToString();
            //testTouch.text = touchArea.position.x.ToString() + test.ToString();
            return false;

        }
    }
}
