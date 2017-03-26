using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMove : MonoBehaviour
{
	public float speed = 0.4f;
	Vector2 dest = Vector2.zero;
	Vector2 nextdir = Vector2.zero;
	int touchdir = 0;

	void Start ()
	{
		dest = transform.position;
	}

	void FixedUpdate ()
	{
		// Move closer to Destination
		if ((Vector2)transform.position == dest) {
		} else {
			Vector2 p = Vector2.MoveTowards (transform.position, dest, speed);
			GetComponent<Rigidbody2D> ().MovePosition (p);
		}
		Vector2 w = (Vector2)transform.position;
		touchdir = judueFinger();
		// Check for Input if not moving
		//if (Mathf.Abs(w.x - dest.x)<0.01 &&Mathf.Abs(w.y - dest.y)<0.01) {
		if (w == dest) {
			if ((touchdir == -2 || Input.GetKey (KeyCode.UpArrow)) && valid (Vector2.up))
				nextdir = Vector2.up;
			else if ((touchdir == -1 || Input.GetKey (KeyCode.RightArrow)) && valid (Vector2.right))
				nextdir = Vector2.right;
			else if ((touchdir == 2 || Input.GetKey (KeyCode.DownArrow)) && valid (-Vector2.up))
				nextdir = -Vector2.up;
			else if ((touchdir == 1 || Input.GetKey (KeyCode.LeftArrow)) && valid (-Vector2.right))
				nextdir = -Vector2.right;
			else if (!valid (nextdir))
				nextdir = Vector2.zero;
			dest = (Vector2)transform.position + nextdir;
		}
		// Animation Parameter
		Vector2 dir = dest - (Vector2)transform.position;
		GetComponent<Animator> ().SetFloat ("DirX", dir.x);
		GetComponent<Animator> ().SetFloat ("DirY", dir.y);
	}

	bool valid (Vector2 dir)
	{
		// Cast Line from 'next to Pac-Man' to 'Pac-Man'
		Vector2 pos = transform.position;
		RaycastHit2D hit = Physics2D.Linecast (pos + dir, pos);
		return (hit.collider == GetComponent<Collider2D> ());
	}

	bool startPosFlag = true;
	Vector2 startFingerPos;
	Vector2 nowFingerPos;
	float xMoveDistance;
	float yMoveDistance;

	int judueFinger ()
	{
		if (Input.touchCount == 0)
			return 0;
		int backValue;
		if (Input.GetTouch (0).phase == TouchPhase.Began && startPosFlag == true) {
			startFingerPos = Input.GetTouch (0).position;
			startPosFlag = false;
		}
		if (Input.GetTouch (0).phase == TouchPhase.Ended) {
			startPosFlag = true;
		}
		nowFingerPos = Input.GetTouch (0).position;
		xMoveDistance = Mathf.Abs (nowFingerPos.x - startFingerPos.x);
		yMoveDistance = Mathf.Abs (nowFingerPos.y - startFingerPos.y);
		if (xMoveDistance > yMoveDistance) {
			if (nowFingerPos.x - startFingerPos.x > 0) {
				backValue = -1;
			} else {
				backValue = 1;    
			}
		} else {
			if (nowFingerPos.y - startFingerPos.y > 0) {
				backValue = -2;    
			} else {
				backValue = 2;  
			}
		}
		return backValue;
	}
}
