﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class LivingAnimator : MonoBehaviour {
	public float animationSpeed = 0.1f;
	public List<Sprite> upSprites, downSprites, sideSprites, hairDownSprites, hairUpSprites, hairSideSprites;
	public SpriteRenderer hairRenderer;
	
	private SpriteRenderer sr;
	private Rigidbody2D rb;

	private Direction direction = Direction.Down;
	private bool moving;

	private void Awake() {
		sr = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
	}

	private void Start() {
		StartCoroutine(Animate());
	}

	private void Update() {
		Vector2 movementVector = rb.velocity.normalized;
		moving = !movementVector.Equals(Vector2.zero);
		if (moving) { //First check if movement is even happening
			direction = DirectionUtility.VectorToDirection(movementVector);

			if (direction == Direction.Left) {
				transform.localScale = new Vector3(-1, 1, 1);
			} else {
				transform.localScale = new Vector3(1, 1, 1);
			}
		}
	}

	private List<Sprite> GetAnimation(Direction direction) {
		switch (direction) {
			case Direction.Down:
				return downSprites;
			case Direction.Up:
				return upSprites;
			default:
				return sideSprites;
		}
	}

	private List<Sprite> GetHairAnimation(Direction direction) {
		switch (direction) {
			case Direction.Down:
				return hairDownSprites;
			case Direction.Up:
				return hairUpSprites;
			default:
				return hairSideSprites;
		}
	}

	private IEnumerator Animate() {
		int index = 0;
		while (true) {
			List<Sprite> animation = GetAnimation(direction);
			if (index >= animation.Count || !moving) index = 0;
			
			//Set main sprite
			sr.sprite = animation[index];

			//Set additional sprites
			if (hairRenderer != null) hairRenderer.sprite = GetHairAnimation(direction)[index];

			index++;
			yield return new WaitForSeconds(animationSpeed);
		}
	}
}