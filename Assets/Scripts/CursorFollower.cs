using UnityEngine;

public class CursorFollower : MonoBehaviour
{
	[SerializeField] private float _moveSpeed;

    void Update()
	{
		HandleMouseInput();
	}

	private void HandleMouseInput()
	{
		Vector3 newPosition = transform.position;
		newPosition.x += Input.GetAxisRaw("Horizontal") * Time.deltaTime * _moveSpeed;
		newPosition.z += Input.GetAxisRaw("Vertical") * Time.deltaTime * _moveSpeed;
		transform.position = newPosition;
	}
}
