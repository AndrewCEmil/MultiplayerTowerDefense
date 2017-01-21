using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class MessageBox : MonoBehaviour {

	public GameObject enemyPrefab;
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
	private Queue<Message> toSend;
	private long lastMessageId;

	// Use this for initialization
	void Start () {
		waiting = false;
		messages = new Queue<Message> ();
		toSend = new Queue<Message> ();
	}

	public bool HasMoreMessages() {
		return messages.Count > 0;
	}

	public Message GetNextMessage() {
		return messages.Dequeue ();
	}

	public void SendMessage(Message message) {
		toSend.Enqueue (message);
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

		if (toSend.Count > 0) {
			SendWaitingMessages ();
		}

		while (HasMoreMessages()) {
			HandleMessage (GetNextMessage ());
		}
	}

	void HandleMessage(Message message) {
		if (message.type == "enemy_create") {
			HandleEnemyCreate (message);
		} else if (message.type == "enemy_destroy") {
			HandleEnemyDestroy (message);
		} else if (message.type == "enemy_destroy") {
			HandleEnemyDamage (message);
		}
	}

	void HandleEnemyCreate(Message message) {
		GameObject newEnemy = Instantiate (enemyPrefab);
		newEnemy.transform.position = new Vector3 (0, 0, 0);
	}

	void HandleEnemyDestroy(Message message) {
		//find enemy
		//destroy it
	}

	void HandleEnemyDamage(Message message) {
		//find enemy
		//damage it
	}

	void LoadNewMessages() {
		WWW www = new WWW(GetMessageUrl());
		StartCoroutine(WaitForRequest(www));
	}

	void SendWaitingMessages() {
		Messages messages = new Messages ();
		List<Message> msgs = new List<Message> ();
		while (toSend.Count > 0) {
			msgs.Add (toSend.Dequeue ());
		}
		messages.messages = msgs;
		WWW www = new WWW (SendMessagesUrl (),  Encoding.ASCII.GetBytes(JsonUtility.ToJson(messages)));
	}

	string GetMessageUrl() {
		return getRootUrl() + "/get_messages";
	}

	string SendMessagesUrl () {
		return getRootUrl() + "/send_messages";
	}

	string getRootUrl() {
		return "localhost:4000";
	}
}
