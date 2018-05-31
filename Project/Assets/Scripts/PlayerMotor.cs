using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private Camera maincamera;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation  = Vector3.zero;
    private Vector3 camerarotation = Vector3.zero;
    private Vector3 thrusterforce = Vector3.zero;
    [SerializeField]
    private Rigidbody rb;

    void Start () {

        //get rigidbody component
        rb = GetComponent<Rigidbody>();
	}

    //move it from controller script
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void CameraRotate(Vector3 _camrotation)
    {
        camerarotation = _camrotation;
    }
    public void ApplyThruster(Vector3 _thrusterforce)
    {
        thrusterforce = _thrusterforce;
    }
    void FixedUpdate()
    {
        Movement();
        Rotation();
    }
    void Movement()
    {
        if (velocity == null || velocity == Vector3.zero)
            return;
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        if (thrusterforce != Vector3.zero || thrusterforce == null)
            rb.AddForce(thrusterforce * Time.fixedDeltaTime, ForceMode.Acceleration);
    }
    void Rotation()
    {
        if (rotation == null || rotation == Vector3.zero)
            return;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        maincamera.transform.Rotate(-camerarotation);
    }
}
