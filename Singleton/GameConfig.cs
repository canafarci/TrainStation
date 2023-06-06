using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Config/New Game Config", order = 0)]
public class GameConfig : ScriptableObject
{
    public float FollowerDisableDelay = 0.5f;
    public GameObject MoneyPrefab, LuggagePrefab, ConsumablePrefab;
    public Material ThunderWithEmission, ThunderWithoutEmission;
    public int StartMoney;
    public float MoneyPerStack = 20;
    public int StackPerFollowerAccept, StackPerConsumableDrop, StackPerTrainCarriage, StackPerLuggageAccept = 5;
}

[System.Serializable]
public class AudioConfig
{
    public AudioClip Audio;
    public float Volume;
}
