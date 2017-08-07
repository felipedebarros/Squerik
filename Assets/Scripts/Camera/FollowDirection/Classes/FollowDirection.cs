using UnityEngine;

public abstract class FollowDirection : ScriptableObject {
	public abstract Vector2 Follow(Vector2 pos, Vector2 target, Vector2 beginPos);
}
