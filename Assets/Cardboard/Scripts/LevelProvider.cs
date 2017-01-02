using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LevelProvider : MonoBehaviour {

	public static List<int> GetLevelLocks() {
		string levelLocksJson = PlayerPrefs.GetString ("LevelLocks");
		if (levelLocksJson.Equals ("") || levelLocksJson.Equals("{}")) {
			InitLevelLocks ();
			levelLocksJson = PlayerPrefs.GetString ("LevelLocks");
		}
		LevelLocks levelLocks = JsonUtility.FromJson<LevelLocks>(levelLocksJson);
		return levelLocks.levelLocks;
	}

	public static void SetLevelLocks(List<int> levelLocks) {
		LevelLocks lls = new LevelLocks ();
		lls.levelLocks = levelLocks;
		string levelLocksJson = JsonUtility.ToJson (lls);
		PlayerPrefs.SetString ("LevelLocks", levelLocksJson);
		string compare = PlayerPrefs.GetString ("LevelLocks");
		PlayerPrefs.Save ();
	}

	public static void OpenLevelLock (int level) {
		List<int> levelLocks = GetLevelLocks ();
		if (!levelLocks.Contains (level)) {
			levelLocks.Add (level);
		}
		SetLevelLocks (levelLocks);
	}

	public static string InitLevelLocks() {
		List<int> levelLocks = new List<int> ();
		levelLocks.Add (1);
		SetLevelLocks (levelLocks);
		return JsonUtility.ToJson (levelLocks);
	}

	public static bool IsLevelLocked(int level) {
		return !GetLevelLocks ().Contains (level);
	}

	public static Level[] GetLevels() {
		Level[] levels = new Level[NumLevels ()];
		for (int i = 1; i <= NumLevels (); i++) {
			levels [i - 1] = GetLevel (i);
		}
		return levels;
	}


	public static Level GetLevel(int level) {
		if (level == 0) {
			return Ring ();
		} if (level == 1) {
			return Ring ();
		} else if (level == 2) {
			return SlingShot ();
		} else if (level == 3) {
			return Throwback ();
		} else if (level == 4) {
			return SplitShot ();
		} else if (level == 5) {
			return BigRing ();
		}
		return null;
	}

	static int NumLevels() {
		return 5;
	}

	public static Level SlingShot() {
		Vector3[] positions = new Vector3[1];
		positions [0] = new Vector3 (0, 2, 5);
		Vector3[] drifts = new Vector3[4];
		drifts [0] = new Vector3 (10, 0, 1);
		drifts [1] = new Vector3 (-10, 0, 1);
		drifts [2] = new Vector3 (10, 0, 5);
		drifts [3] = new Vector3 (-10, 0, 5);
		Level level = new Level ();
		level.objects = positions;
		level.drifts = drifts;
		level.playerPosition = new Vector3 (0, 0.5f, 0);
		level.sunPosition = new Vector3 (10, 10, 10);
		level.name = "Slingshot";
		level.level = 2;
		level.locked = IsLevelLocked (level.level);
		return level;
	}

	public static Level Throwback() {
		Vector3[] positions = new Vector3[1];
		positions [0] = new Vector3 (0, 5, 10);
		Vector3[] drifts = new Vector3[4];
		drifts [0] = new Vector3 (10, 0, 1);
		drifts [1] = new Vector3 (-10, 0, 1);
		drifts [2] = new Vector3 (10, 0, 5);
		drifts [3] = new Vector3 (-10, 0, 5);
		Level level = new Level ();
		level.objects = positions;
		level.drifts = drifts;
		level.playerPosition = new Vector3 (0, 0.5f, 0);
		level.sunPosition = new Vector3 (0, 2, 5);
		level.name = "Throwback";
		level.level = 3;
		level.locked = IsLevelLocked (level.level);
		return level;
	}

	public static Level SplitShot() {
		Vector3[] positions = new Vector3[2];
		positions [0] = new Vector3 (5, 0, 5);
		positions [1] = new Vector3 (-5, 0, 5);
		Vector3[] drifts = new Vector3[3];
		drifts [0] = new Vector3 (0, 10, 0);
		drifts [0] = new Vector3 (0, -10, 0);
		drifts [2] = new Vector3 (0, 0, 15);
		Level level = new Level ();
		level.objects = positions;
		level.drifts = drifts;
		level.playerPosition = new Vector3 (0, 0.5f, 0);
		level.sunPosition = new Vector3 (0, 0, 10);
		level.name = "Splitter";
		level.level = 4;
		level.locked = IsLevelLocked (level.level);
		return level;
	}

	public static Level Ring() {
		Vector3[] positions = new Vector3[4];
		positions [0] = new Vector3 (10, 10, 10);
		positions [1] = new Vector3 (-10, 10, 10);
		positions [2] = new Vector3 (-10, 10, -10);
		positions [3] = new Vector3 (10, 10, -10);
		Vector3[] drifts = new Vector3[5];
		drifts [0] = new Vector3 (5, 5, 5);
		drifts [1] = new Vector3 (15, 15, 15);
		drifts [2] = new Vector3 (-15, 15, 15);
		drifts [3] = new Vector3 (-15, 15, -15);
		drifts [4] = new Vector3 (15, 15, -15);
		Level level = new Level ();
		level.objects = positions;
		level.drifts = drifts;
		level.playerPosition = new Vector3 (0, 0.5f, 0);
		level.sunPosition = new Vector3 (2, 0, 2);
		level.name = "Ring";
		level.level = 1;
		level.locked = IsLevelLocked (level.level);
		return level;
	}

	public static Level BigRing() {
		Vector3[] positions = new Vector3[4];
		positions [0] = new Vector3 (20, 20, 20);
		positions [1] = new Vector3 (-20, 20, 20);
		positions [2] = new Vector3 (-20, 20, -20);
		positions [3] = new Vector3 (20, 20, -20);
		Vector3[] drifts = new Vector3[4];
		drifts [0] = new Vector3 (25, 25, 25);
		drifts [1] = new Vector3 (-25, 25, 25);
		drifts [2] = new Vector3 (-25, 25, -25);
		drifts [3] = new Vector3 (25, 25, -25);
		Level level = new Level ();
		level.objects = positions;
		level.drifts = drifts;
		level.playerPosition = new Vector3 (0, 0.5f, 0);
		level.sunPosition = new Vector3 (2, 0, 2);
		level.name = "BigRing";
		level.level = 5;
		level.locked = IsLevelLocked (level.level);
		return level;
	}

	public static Level TopHat() {
		Vector3[] positions = new Vector3[1];
		positions [0] = new Vector3 (0, 10, 2);
		Vector3[] drifts = new Vector3[2];
		drifts [0] = new Vector3 (2, 1, 2);
		drifts [1] = new Vector3 (-2, 1, 2);
		Level level = new Level ();
		level.objects = positions;
		level.drifts = drifts;
		level.sunPosition = new Vector3 (0, 5, 2);
		level.playerPosition = new Vector3(0, 0, 0);
		level.name = "TopHat";
		level.level = 6;
		level.locked = IsLevelLocked(level.level);
		return level;
	}

	public static Level Wall() {
		Vector3[] positions = new Vector3[9];
		positions [0] = new Vector3 (-2, -2, 10);
		positions [1] = new Vector3 (0, -2, 10);
		positions [2] = new Vector3 (2, -2, 10);
		positions [3] = new Vector3 (-2, 0, 10);
		positions [4] = new Vector3 (0, 0, 10);
		positions [5] = new Vector3 (2, 0, 10);
		positions [6] = new Vector3 (-2, 2, 10);
		positions [7] = new Vector3 (0, 2, 10);
		positions [8] = new Vector3 (2, 2, 10);

		Vector3[] drifts = new Vector3[4];
		drifts [0] = new Vector3 (3, 3, 10);
		drifts [1] = new Vector3 (-3, 3, 10);
		drifts [2] = new Vector3 (-3, -3, 10);
		drifts [3] = new Vector3 (3, -3, 10);
		Level level = new Level ();
		level.objects = positions;
		level.drifts = drifts;
		level.sunPosition = new Vector3 (0, 0, 15);
		level.playerPosition = new Vector3(0, 0, 0);
		level.name = "Wall";
		level.level = 7;
		level.locked = IsLevelLocked(level.level);
		return level;
	}

	public static Level Backboard() {
		Vector3[] positions = new Vector3[9];
		positions [0] = new Vector3 (-2, -2, 15);
		positions [1] = new Vector3 (0, -2, 15);
		positions [2] = new Vector3 (2, -2, 15);
		positions [3] = new Vector3 (-2, 0, 15);
		positions [4] = new Vector3 (0, 0, 15);
		positions [5] = new Vector3 (2, 0, 15);
		positions [6] = new Vector3 (-2, 2, 15);
		positions [7] = new Vector3 (0, 2, 15);
		positions [8] = new Vector3 (2, 2, 15);

		Vector3[] drifts = new Vector3[4];
		drifts [0] = new Vector3 (3, 3, 10);
		drifts [1] = new Vector3 (-3, 3, 10);
		drifts [2] = new Vector3 (-3, -3, 10);
		drifts [3] = new Vector3 (3, -3, 10);
		Level level = new Level ();
		level.objects = positions;
		level.drifts = drifts;
		level.sunPosition = new Vector3 (0, 0, 10);
		level.playerPosition = new Vector3(0, 0, 0);
		level.name = "Backboard";
		level.level = 8;
		level.locked = IsLevelLocked(level.level);
		return level;
	}


	public static Level LevelZero() {
		return null;
	}
}
