using UnityEngine;
using System.Collections;

public class Pencil : MonoBehaviour 
{
    bool IsDrawing;
    Vector3 ScreenPoint;
    Vector3 ScanPos;
    Vector3 Offset;
    Drawline LineDrawer;

    void Start()
    {
        LineDrawer = GetComponent<Drawline>();
    }

    void Draw()
    {
        IsDrawing = true;
        LineDrawer.Restart();
        ScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenPoint.z));
    }

	// Update is called once per frame
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            //IsDrawing = true;
            //LineDrawer.Restart();
            //ScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
            //Offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenPoint.z));
            Draw();
        }
        if (Input.GetMouseButtonUp(0))
        {
            IsDrawing = false;
        }
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                //IsDrawing = true;
                //LineDrawer.Restart();
                //ScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
                //Offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenPoint.z));
                Draw();
            }
            if (Input.GetTouch(i).phase == TouchPhase.Ended)
            {
                IsDrawing = false;
            }
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    IsDrawing = true;
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    IsDrawing = false;
        //}
        if (IsDrawing)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, ScreenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
            curPosition.x = 0;
            transform.position = curPosition;
        }
	}
}
