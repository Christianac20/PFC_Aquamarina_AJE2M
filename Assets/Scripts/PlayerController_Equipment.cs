using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController_Equipment : MonoBehaviour
{
    #region VARIABLES
    [Header("Variables Input System")]
    [SerializeField] InputActionAsset inputActionAsset;
    InputAction actionAttack;
    InputAction actionEquipo1Camera;
    InputAction actionEquipo2Net;
    InputAction actionEquipo3NetLauncher;

    [Header("Variables generales")]
    [SerializeField] Vector2 moveAmmount;

    [Header("Variables del Animator")]
    public bool cameraEquipped;
    [SerializeField] bool cameraTakePhoto;
    [SerializeField] bool netEquipped;
    [SerializeField] bool netLauncherEquipped;

    [Header("Variables de Componente y Scripts")]
    [SerializeField] FollowMouse followMouse;
    [SerializeField] Animator animator;

    [Header("Otras Variables")]
    Vector2 movement;
    [SerializeField] GameObject cameraEquipment;
    [SerializeField] GameObject cameraEquipmentBasePosition;

    #endregion

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        //ASIGNO LAS VARIABLES DE ACCIONES DEL INPUT SYSTEM
        actionAttack = InputSystem.actions.FindAction("Attack");
        actionEquipo1Camera = InputSystem.actions.FindAction("Equipo1_Camera");
        actionEquipo2Net = InputSystem.actions.FindAction("Equipo2_Net");
        actionEquipo3NetLauncher = InputSystem.actions.FindAction("Equipo3_NetLauncher");

        //ASIGNO LAS VARIABLES DE COMPONENTES
        animator = GetComponent<Animator>();
        followMouse = GetComponentInChildren<FollowMouse>();
        cameraEquipment = GameObject.FindWithTag("CameraPhotos");
        cameraEquipmentBasePosition = GameObject.FindWithTag("CameraPhotosBasePoint");
    }

    // Update is called once per frame
    void Update()
    {
        //EQUIPMENT FUNCTIONS
        CameraEquip();
        TakePhoto();
        NetEquip();
        NetLauncherEquip();

        //ANIMATOR VARIABLES SETTINGS
        animator.SetBool("CameraEquipped", cameraEquipped);
        animator.SetBool("NetEquipped", netEquipped);
        animator.SetBool("NetLauncherEquipped", netLauncherEquipped);
    }

    #region EQUIPMENT
    void CameraEquip()
    {
        if (actionEquipo1Camera.WasPressedThisFrame() && cameraEquipped == false)
        {
            cameraEquipment.transform.position = cameraEquipmentBasePosition.transform.position;
            cameraEquipped = true;
            followMouse.enabled = true;
        }
        else if (actionEquipo1Camera.WasPressedThisFrame() && cameraEquipped == true)
        {
            cameraEquipped = false;
            followMouse.enabled = false;
            cameraEquipment.transform.position = cameraEquipmentBasePosition.transform.position;
        }
        else if (moveAmmount != Vector2.zero)
        {
            cameraEquipped = false;
            followMouse.enabled = false;
            cameraEquipment.transform.position = cameraEquipmentBasePosition.transform.position;
        }
    }

    void TakePhoto()
    {
        if (cameraEquipped && actionAttack.WasPressedThisFrame())
        {
            animator.SetTrigger("CameraTakePhoto");
        }
    }

    void NetEquip()
    {
        if (actionEquipo2Net.WasPressedThisFrame())
        {
            netEquipped = true;
        }
    }

    void NetLauncherEquip()
    {
        if (actionEquipo3NetLauncher.WasPressedThisFrame())
        {
            netLauncherEquipped = true;
        }
    }
    #endregion
}
