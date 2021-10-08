using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
     	// The world-position of the mouse last frame.
	Vector3 lastFramePosition;
	Vector3 currFramePosition;
    public Camera mainCamera;
	// The world-position start of our left-mouse drag operation
	Vector3 dragStartPosition;
	List<GameObject> dragPreviewGameObjects;
	GameObject selectedTile; //해당타일
	Vector2 MousePosition;
	bool isDragging = false;

	enum MouseMode {
		SELECT,
		BUILD
	}
	MouseMode currentMode = MouseMode.SELECT;   // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //클릭시작위치
        currFramePosition = mainCamera.ScreenToWorldPoint( Input.mousePosition );

		if( Input.GetKeyUp(KeyCode.Escape) ) {
			if(currentMode == MouseMode.BUILD) {
				currentMode = MouseMode.SELECT;
			}
			else if( currentMode == MouseMode.SELECT ) {
				Debug.Log("Show game menu?");
			}
		}
		//빌드모드
		if(Input.GetMouseButton(0) && currentMode == MouseMode.BUILD)
		{
			MousePosition = Input.mousePosition;
			Vector3 Mp = new Vector3(Input.mousePosition.x,Input.mousePosition.y,10);
			MousePosition = mainCamera.ScreenToWorldPoint (Mp);
			Debug.Log(MousePosition);
			RaycastHit2D Hit = Physics2D.Raycast(MousePosition, transform.forward);
			if(Hit){
				Hit.transform.GetComponent<SpriteRenderer>().color = Color.gray;
			}
            if (Hit.collider != null){
                Debug.Log("없쪄영");
            }
			currentMode = MouseMode.SELECT;
        }
        UpdateCameraMovement();
        
        //마지막 드래그 위치
        lastFramePosition = mainCamera.ScreenToWorldPoint( Input.mousePosition );
    }
    void UpdateCameraMovement() {
		// Handle screen panning
		if( Input.GetMouseButton(1) || Input.GetMouseButton(2) ) {	// Right or Middle Mouse Button
			Vector3 diff = lastFramePosition - currFramePosition;
			mainCamera.transform.Translate( diff );
		}

		mainCamera.orthographicSize -= mainCamera.orthographicSize * Input.GetAxis("Mouse ScrollWheel");
		mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 3f, 25f);
	}
	public void mouse_buildmode_change(){
		currentMode = MouseMode.BUILD;
	}
}
