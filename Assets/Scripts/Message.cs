using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;

[System.Serializable]
public class Message {
	public Message() {}

	public string type;
	public string action;
	public int timestamp;
}
