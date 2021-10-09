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
	List<GameObject> dragPreviewGameObjects = new List<GameObject>();
	GameObject selectedTile; //해당타일
	Vector3 MousePosition;
	bool isDragging = false;

	enum MouseMode {
		SELECT,
		BUILD
	}
	MouseMode currentMode = MouseMode.SELECT;  

	//UI모드바꾸기
	public void mouse_buildmode_change(){
		currentMode = MouseMode.BUILD;
	} 
    void Start()
    {
        
    }
    void Update()
    {
		if( Input.GetKeyUp(KeyCode.Escape) ) {
			if(currentMode == MouseMode.BUILD) {
				currentMode = MouseMode.SELECT;
			}
			else if( currentMode == MouseMode.SELECT ) {
				Debug.Log("Show game menu?");
			}
		}
		MouseDrag_build();
        UpdateCameraMovement();
    }
	//Todo카메라 이동하기
    void UpdateCameraMovement() {
		// Handle screen panning
		if( Input.GetMouseButton(2) ) {	// Right or Middle Mouse Button
			Vector3 diff = lastFramePosition - currFramePosition;
			mainCamera.transform.Translate( diff );
		}
		mainCamera.orthographicSize -= mainCamera.orthographicSize * Input.GetAxis("Mouse ScrollWheel");
		mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 3f, 25f);
	}

	//todo 드래그건설
    private void OnMouseDown(){ 
		 //클릭시작위치
		MousePosition = Input.mousePosition;
		Vector3 Mp = new Vector3(Input.mousePosition.x,Input.mousePosition.y,10);
        currFramePosition = mainCamera.ScreenToWorldPoint(Mp);
		Debug.Log("시작지점"+ currFramePosition);
    } 
	void OnMouseUp()
	{
		Debug.Log("완료스"+ lastFramePosition);
	}
	private void MouseDrag_build() {
		//빌드모드
		if(Input.GetMouseButton(0) && currentMode == MouseMode.BUILD)
		{
			//마지막 드래그 위치
			MousePosition = Input.mousePosition;
			Vector3 Mp = new Vector3(Input.mousePosition.x,Input.mousePosition.y,10);
			MousePosition = mainCamera.ScreenToWorldPoint(Mp);
			RaycastHit2D Hit = Physics2D.Raycast(MousePosition, transform.forward);
			//Debug.Log(Hit.transform.gameObject);
			//누르면 리스트에 추가
			if(Hit){
				if(Hit.transform.gameObject && !(dragPreviewGameObjects.Contains(Hit.transform.gameObject)))
				{
					Hit.transform.GetComponent<SpriteRenderer>().color = Color.gray;
					dragPreviewGameObjects.Add(Hit.transform.gameObject);
					Debug.Log(dragPreviewGameObjects.Count);
				}
			}else if (Hit.collider != null){
                Debug.Log("없쪄영");
            }
			//currentMode = MouseMode.SELECT;
        }
	}
}
