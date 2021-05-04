﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform objectToFollow;
	public Vector3 offset;
	public float followSpeed = 10;
	public float lookSpeed = 10;

	private Vector3 lookDirection;
	private Quaternion rot;
	private Vector3 targetPos;

	private void FixedUpdate()
	{
		LookAtTarget();
		MoveToTarget();
	}

	public void LookAtTarget()
	{
		lookDirection = objectToFollow.position - transform.position;
		rot = Quaternion.LookRotation(lookDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, rot, lookSpeed * Time.deltaTime);
	}

	public void MoveToTarget()
	{
		targetPos = objectToFollow.position + 
					objectToFollow.forward * offset.z + 
					objectToFollow.right * offset.x + 
					objectToFollow.up * offset.y;

		transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
	}

}
