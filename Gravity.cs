using Godot;
using System;

public class Gravity : RigidBody2D
{
    [Export] public float gravityForce = 40;

    // Called when the node enters the scene tree for the first time.

    public float GetGravity() {
        return gravityForce;
    }
}
