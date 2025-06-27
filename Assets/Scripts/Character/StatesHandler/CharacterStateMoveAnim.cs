using State.Character;
using System;
using UnityEngine;

public class CharacterStateMoveAnim 
{
    public Animator anim { get; private set; }
    public float speedWalkAnim { get; private set; } = 0.5f;
    public float speedRunAnim { get; private set; } = 0.8f;
    public float speedSprintAnim { get; private set; } = 1f;
    public int runningLayer { get; private set; }
}
