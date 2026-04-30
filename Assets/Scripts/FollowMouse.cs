using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] GameObject mainCamera;
    [SerializeField] Camera mainCameraComponent;
    [SerializeField] float maxSpeed = 10f;
    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        mainCameraComponent = mainCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowMousePosition();
    }

    void FollowMousePosition()
    {
        transform.position = GetWorldPositionFromMouse();
    }

    private Vector2 GetWorldPositionFromMouse()
    {
        return mainCameraComponent.ScreenToWorldPoint(Input.mousePosition);
    }
    #endregion
}
