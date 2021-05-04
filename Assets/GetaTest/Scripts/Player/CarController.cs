using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
	private float horizontalInput;
	private float verticalInput;
	private float steeringAngle;

	[Header("Wheels")]
	public WheelCollider frontLeftWheel;
	public WheelCollider frontRightWheel;
	public WheelCollider rearLeftWheel;
	public WheelCollider rearRightWheel;

	[Header("Transform")]
	public Transform frontLeftTransform;
	public Transform frontRightTtransform;
	public Transform rearLeftTransform;
	public Transform rearRightTransform;

	public float steerAngle = 40;
	public float motorForce = 200;

	private void FixedUpdate()
	{
		GetInput();
		Steer();
		Accelerate();
		UpdateWheelPoses();
	}

	public void GetInput()
	{
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");
	}

	private void Steer()
	{
		steeringAngle = steerAngle * horizontalInput;
		frontLeftWheel.steerAngle = steeringAngle;
		frontRightWheel.steerAngle = steeringAngle;
	}

	private void Accelerate()
	{
		rearLeftWheel.motorTorque = verticalInput * motorForce * Time.fixedDeltaTime;
		rearRightWheel.motorTorque = verticalInput * motorForce * Time.fixedDeltaTime;
	}

	private void UpdateWheelPoses()
	{
		UpdateWheelPose(frontLeftWheel, frontLeftTransform);
		UpdateWheelPose(frontRightWheel, frontRightTtransform);
		UpdateWheelPose(rearLeftWheel, rearLeftTransform);
		UpdateWheelPose(rearRightWheel, rearRightTransform);
	}

	private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose(out _pos, out _quat);

		_transform.position = _pos;
		_transform.rotation = _quat;
	}



}
