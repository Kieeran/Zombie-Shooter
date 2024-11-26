using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace InputSystem
{
	public class PlayerInputSystem : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool aim;
		public bool shoot;
		public bool reload;
		public bool openInventory;
		public bool interact;
		public bool shortcut1;
		public bool shortcut2;
		public bool shortcut3;
		public bool escapeUI;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnAim(InputValue value)
		{
			AimInput(value.isPressed);
		}

		public void OnShoot(InputValue value)
		{
			ShootInput(value.isPressed);
		}

		public void OnReload(InputValue value)
		{
			ReloadInput(value.isPressed);
		}

		public void OnInteract(InputValue value)
		{
			InteractInput(value.isPressed);
		}

		public void OnOpenInventory(InputValue value)
		{
			OpenInventoryInput(value.isPressed);
		}

		public void OnShortcut1(InputValue value)
		{
			Shortcut1Input(value.isPressed);
		}

		public void OnShortcut2(InputValue value)
		{
			Shortcut2Input(value.isPressed);
		}

		public void OnShortcut3(InputValue value)
		{
			Shortcut3Input(value.isPressed);
		}

		public void OnEscapeUI(InputValue value)
		{
			EscapeUIInput(value.isPressed);
		}
#endif

		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void AimInput(bool newAimState)
		{
			aim = newAimState;
		}

		public void ShootInput(bool newShootState)
		{
			shoot = newShootState;
		}

		public void ReloadInput(bool newReloadState)
		{
			reload = newReloadState;
		}

		public void InteractInput(bool newInteractState)
		{
			interact = newInteractState;
		}

		public void OpenInventoryInput(bool newOpenInventoryState)
		{
			openInventory = newOpenInventoryState;
		}

		public void Shortcut1Input(bool newShortcut1State)
		{
			shortcut1 = newShortcut1State;
		}

		public void Shortcut2Input(bool newShortcut2State)
		{
			shortcut2 = newShortcut2State;
		}

		public void Shortcut3Input(bool newShortcut3State)
		{
			shortcut3 = newShortcut3State;
		}

		public void EscapeUIInput(bool newEscapeUIState)
		{
			escapeUI = newEscapeUIState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		public void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}

}