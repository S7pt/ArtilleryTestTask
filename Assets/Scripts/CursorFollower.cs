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
		Vector3 position = transform.position;
		position.x += Input.GetAxisRaw("Horizontal") * Time.deltaTime * _moveSpeed;
		position.z += Input.GetAxisRaw("Vertical") * Time.deltaTime * _moveSpeed;
		transform.position = position;
	}
}
