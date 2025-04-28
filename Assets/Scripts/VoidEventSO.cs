using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Void Event", menuName = "Game Event/Void Event")]
public class VoidEventSO : GameEventSO<EmptySendData> { }

public struct EmptySendData { }
