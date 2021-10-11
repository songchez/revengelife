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
    public GameObject build_wall; //설치할 물체
    public GameObject f_walls; //설치한 벽
    public GameObject f_Others; //설치한 물체
    Vector3 MousePosition;
    bool isDragging = false;

    enum MouseMode
    {
        SELECT,
        BUILD_WALL,
        BUILD_FURN
    }
    MouseMode currentMode = MouseMode.SELECT;

    //UI모드바꾸기
    public void mouse_buildmode_change()
    {
        currentMode = MouseMode.BUILD_WALL;
    }
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (currentMode != MouseMode.SELECT)
            {
                currentMode = MouseMode.SELECT;
            }
            else if (currentMode == MouseMode.SELECT)
            {
                Debug.Log("Show game menu?");
            }
        }
        MouseDrag_build();
        MouseDrag_deBuild();
        UpdateCameraMovement();
    }
    //Todo카메라 이동하기
    void UpdateCameraMovement()
    {
        // Handle screen panning
        if (Input.GetMouseButton(2))
        {   // Right or Middle Mouse Button
            Vector3 diff = lastFramePosition - currFramePosition;
            mainCamera.transform.Translate(diff);
        }
        mainCamera.orthographicSize -= mainCamera.orthographicSize * Input.GetAxis("Mouse ScrollWheel");
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 3f, 25f);
    }

    //todo 드래그건설
    private void OnMouseDown()
    {
        //클릭시작위치
        MousePosition = Input.mousePosition;
        Vector3 Mp = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        currFramePosition = mainCamera.ScreenToWorldPoint(Mp);
        Debug.Log("시작지점" + currFramePosition);
    }
    void OnMouseUp()
    {
        Debug.Log("완료스" + lastFramePosition);
    }
    private void MouseDrag_build()
    {
        //드래그앤 빌드맨
        if (Input.GetMouseButton(0) && currentMode == MouseMode.BUILD_WALL)
        {
            //마지막 드래그 위치
            MousePosition = Input.mousePosition;
            Vector3 Mp = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            MousePosition = mainCamera.ScreenToWorldPoint(Mp);
            RaycastHit2D Hit = Physics2D.Raycast(MousePosition, transform.forward);
            //Debug.Log(Hit.transform.gameObject);
            //누르면 리스트에 추가
            if (Hit)
            {
                //없을땐 타일, 있을땐 벽
                if (!(dragPreviewGameObjects.Contains(Hit.transform.gameObject)))
                {
                    if (Hit.transform.GetComponent<SpriteRenderer>().sprite == null)
                    {
                        Vector3 plusoffset = new Vector3(Hit.transform.position.x - 0.5f, Hit.transform.position.y + 0.5f, 0f);
                        GameObject wall = Instantiate(build_wall, plusoffset, Quaternion.identity);
                        wall.name += "(" + Hit.transform.position.x + "," + Hit.transform.position.y + ")";
                        wall.transform.parent = f_walls.transform;
                        dragPreviewGameObjects.Add(wall);
                        Hit.transform.GetComponent<Tile>().FurnitureOnTile = true; //타일판정
                    }
                    Debug.Log(dragPreviewGameObjects.Count);
                }
            }
            else if (Hit.collider != null)
            {
                Debug.Log("없쪄영");
            }
        }
    }
    private void MouseDrag_deBuild()
    {
        //드래그앤 드빌드(삭제)
        if (Input.GetMouseButtonDown(1) && currentMode == MouseMode.BUILD_WALL)
        {
            MousePosition = Input.mousePosition;
            Vector3 Mp = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            MousePosition = mainCamera.ScreenToWorldPoint(Mp);
            RaycastHit2D Hit = Physics2D.Raycast(MousePosition, transform.forward);
            if (Hit)
            {
                //없을땐 타일, 있을땐 벽
                Debug.Log("가보자" + dragPreviewGameObjects.Contains(Hit.transform.gameObject));
                if (dragPreviewGameObjects.Contains(Hit.transform.gameObject && Hit.transform.GetComponent<SpriteRenderer>().sprite != null))
                {
                    Debug.Log(Hit.transform.gameObject.name);
                    Destroy(Hit.transform.gameObject);
                    //Hit.transform.GetComponent<Tile>().FurnitureOnTile = false;
                }
            }
            else if (Hit.collider != null)
            {
                Debug.Log("없쪄영");
            }
        }
    }
}
