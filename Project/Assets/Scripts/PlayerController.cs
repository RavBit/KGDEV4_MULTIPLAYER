using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    [Header("Speed of the player")]
    private float speed = 5f;
    [Header("Speed of the Look Sensitivity")]
    [SerializeField]
    private float lookSensitivity = 3f;
    [Header("Player his motor")]
    [SerializeField]
    private PlayerMotor playerMotor;

    [Header("Thruster from Player")]
    [SerializeField]
    private float thrusterForce = 1000f;

    void Start () {
        LockCursor();
        playerMotor = GetComponent<PlayerMotor>();

    }
	
	void Update () {
        if (PauseMenu.isOn)
            return;
        //getting x and y movement
        float _xMov = Input.GetAxis("Horizontal");
        float _zMov = Input.GetAxis("Vertical");


        //Get the movement values
        Vector3 _moveHorizontal = transform.right * _xMov;
        Vector3 _moveVertical = transform.forward * _zMov;

        //normalize the values
        Vector3 _velocity = (_moveHorizontal + _moveVertical) * speed;

        //Give value to the movement script
        playerMotor.Move(_velocity);

        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0, _yRot, 0) * lookSensitivity;
        playerMotor.Rotate(_rotation);

        float _xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRotation = new Vector3(_xRot, 0, 0) * lookSensitivity;
        

        playerMotor.CameraRotate(_cameraRotation);

        Vector3 _thrusterForce = Vector3.zero;
        if(Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
        }

        playerMotor.ApplyThruster(_thrusterForce);
    }
    public static void LockCursor()
    {
        Cursor.visible = !Cursor.visible;
        if (Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
            return;
        }
        if (!Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.None;
            return;
        }
    }
}
