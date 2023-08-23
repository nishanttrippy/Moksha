using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{

	public string NPCname;
	public string playerName;

	[TextArea(3, 10)]
	public string[] sentences;

}