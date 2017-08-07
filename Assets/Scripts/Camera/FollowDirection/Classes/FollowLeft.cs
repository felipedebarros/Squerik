using UnityEngine;

public class FollowLeft : FollowDirection {
	public override Vector2 Follow(Vector2 pos, Vector2 target, Vector2 beginPos)
	{
		Vector2 newPos = pos;
		if(target.x < beginPos.x)
			newPos.x = target.x; 
		return newPos;
	}
}
