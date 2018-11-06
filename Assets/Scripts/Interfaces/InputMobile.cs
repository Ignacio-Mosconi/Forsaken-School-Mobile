﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class InputMobile : IInput
{
	public float GetHorizontalAxis()
	{
		return CrossPlatformInputManager.GetAxis("Horizontal");
	}

	public float GetVerticalAxis()
	{
		return CrossPlatformInputManager.GetAxis("Vertical");
	}

	public float GetHorizontalViewAxis()
	{
		return CrossPlatformInputManager.GetAxis("Mouse X");
	}

	public float GetVerticalViewAxis()
	{
		return CrossPlatformInputManager.GetAxis("Mouse Y");
	}

	public float GetSwapWeaponAxis()
	{
		return (CrossPlatformInputManager.GetButtonDown("SwapWeapon")) ? 1.0f : 0f;
	}

	public bool GetFireButton()
	{
		return CrossPlatformInputManager.GetButton("Fire");
	}

	public bool GetReloadButton()
	{
		return CrossPlatformInputManager.GetButtonDown("Reload");
	}

	public bool GetJumpButton()
	{
		return CrossPlatformInputManager.GetButton("Jump");
	}

	public bool GetSprintButton()
	{
		return false;
	}

	public bool GetSprintButtonModifier()
	{
		return (CrossPlatformInputManager.GetAxis("Vertical") > 0.7f);
	}

	public bool GetInteractButton()
	{
		return false;
	}

	public bool GetInteractHoldButton()
	{
		return false;
	}

	public bool GetPauseButton()
	{
		return CrossPlatformInputManager.GetButtonDown("Cancel");
	}
}
