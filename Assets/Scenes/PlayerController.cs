using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Reference to Cutter Saw Components")] 
    [SerializeField]  Transform pointA;
    [SerializeField]  Transform pointB;
    [SerializeField]  LineRenderer rope;
    
    [Header("Settings for cutter")]
    [Range(50, 150)] [SerializeField]  float speedOfOverallRotation;
    [Range(200, 500)] [SerializeField]  float speedOfIndividualRotation;
    [SerializeField]  bool growCutter;
    [SerializeField]  int growValue;
    [Header("Settings for camera")] 
    [SerializeField] new Camera camera;
    [SerializeField]  Vector3 cameraOffset;
    [Range(1, 5)] [SerializeField]  float speedOfCamera;
    
    private Transform _currentPoint;
    private int layerMask = 8;

    // Start is called before the first frame update
    void Start()
    {
        _currentPoint = pointA;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSaw();
        RotateComponents();
        DrawRope();
        UpdateCameraPosition();
        if(growCutter) Grow(1);
    }

    private void ChangeSaw()
    {
        if (Input.GetMouseButtonDown(0) && growCutter!=true)
        {
            speedOfOverallRotation = -1 * speedOfOverallRotation;
            if (_currentPoint == pointA)
                _currentPoint = pointB;
            else if(_currentPoint == pointB)
                _currentPoint = pointA;
            CheckForGround();
        }
    }
    private void RotateComponents()
    {
        pointA.Rotate(Vector3.forward * Time.deltaTime * speedOfIndividualRotation);
        pointB.Rotate(Vector3.forward * Time.deltaTime * speedOfIndividualRotation);
        if(growCutter!=true)
            transform.RotateAround(_currentPoint.position,Vector3.up, speedOfOverallRotation*Time.deltaTime);
    }

    private void DrawRope()
    {
        rope.SetPosition(0,pointA.position);
        rope.SetPosition(1,pointB.position);
    }

    private void UpdateCameraPosition()
    {
        if (camera.transform.position != _currentPoint.position)
            camera.transform.position = Vector3.Lerp(camera.transform.position,_currentPoint.position+cameraOffset,speedOfCamera*Time.deltaTime);
    }

    private void Grow(int dir)
    {
        if (_currentPoint != pointA)
            pointA.transform.Translate(Vector3.right*dir*Time.deltaTime,Space.Self);
        else
            pointB.transform.Translate(Vector3.right*-dir*Time.deltaTime,Space.Self);
    }

    void CheckForGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(_currentPoint.position, Vector3.down,out hit, 20,layerMask))
            Debug.Log("Safe");
        else
        {
            Debug.Log("GameOver");
            Debug.Log(hit.transform.gameObject.name);
        }
            
    }
}
