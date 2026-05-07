using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowMouse : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] GameObject mainCamera;
    [SerializeField] Camera mainCameraComponent;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] GameObject player;
    [SerializeField] PlayerControllerWater playerControllerWater;

    #endregion

    #region METHODS
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        player = GameObject.FindWithTag("Player");
        mainCameraComponent = mainCamera.GetComponent<Camera>();
        playerControllerWater = player.GetComponent<PlayerControllerWater>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowMousePositionDelayed();
    }

    void FollowMousePositionDelayed()
    {
        transform.position = Vector2.MoveTowards(transform.position, GetWorldPositionFromMouse(), maxSpeed * Time.deltaTime);
    }

    private Vector2 GetWorldPositionFromMouse()
    {
        return mainCameraComponent.ScreenToWorldPoint(Input.mousePosition);
    }

    /*void FlipPlayer()
    {
        if (this.transform.position.x < -1 && playerControllerWater.cameraEquipped == true)
        {
            playerControllerWater.Flip();
        }
    }*/ 

    #endregion
}
