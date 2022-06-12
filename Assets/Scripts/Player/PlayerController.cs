using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
	Player playerModel;

	Controller _moveController;
	Controller _lookAndFireController;

	Vector3 dirToMove;
	Vector3 dirToLook;

	public PlayerController(Player m, PlayerView v, PlayerHealth ph)
	{
		playerModel = m;

		playerModel.onMovement += v.Movement;
		playerModel.onDeath += v.Death;
		playerModel.onKnockBack += v.KnockBack;
		playerModel.onShoot += v.Shoot;
		playerModel.cancelShoot += v.CancelShoot;
		ph.onLifeChanged += v.SetHealthBar;

		_moveController = m.moveController;
		_lookAndFireController = m.lookAndFireController;
	}
	public void OnUpdate()
    {
		playerModel.Move(MoveController());
		playerModel.Look(ShootController());
	}
	Vector3 MoveController()
	{
		dirToMove = _moveController.GetDir();
		dirToMove.Normalize();

		return dirToMove;
	}

	Vector3 ShootController()
    {
		dirToLook = _lookAndFireController.GetDir();

		return dirToLook;
    }
}
