using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MessageBox : MonoBehaviour {

	private static MessageBox instance = null;
	public static MessageBox Instance {
		get { return instance; }
	}
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	private bool waiting;
	private Queue<Message> messages;
	private long lastMessageId;

	// Use this for initialization
	void Start () {
		waiting = false;
		messages = new Queue<Message> ();
	}

	public bool HasMoreMessages() {
		return messages.Count > 0;
	}

	public Message GetNextMessage() {
		return messages.Dequeue ();
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		// check for errors
		if (www.error == null)
		{
			Messages newMessages = JsonUtility.FromJson<Messages> (www.text);
			foreach(Message newMessage in newMessages.messages) {
				messages.Enqueue(newMessage);
			}
			Debug.Log ("enqued messages, " + www.text);
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!waiting) {
			LoadNewMessages ();
		}
	}

	void LoadNewMessages() {
		string url = "localhost:4000/get_messages?last_id=" + lastMessageId;
		WWW www = new WWW(url);
		StartCoroutine(WaitForRequest(www));
	}

}
