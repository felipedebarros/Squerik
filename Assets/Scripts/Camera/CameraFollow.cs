using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField] private Transform _target;
	[SerializeField] private Transform _beginFollowPoint;
	[SerializeField] private FollowDirection _direction;

	void Start()
	{
		#if UNITY_EDITOR 
			if(!_target) Util.Error.ShowError("CameraFollow: Target not set");
		#endif
	}

	void Update()
	{
		Vector3 newPos = transform.position;
		newPos = _direction.Follow(transform.position, _target.position, _beginFollowPoint.position);
		newPos.z = transform.position.z;
		transform.position = newPos;
	}
}
