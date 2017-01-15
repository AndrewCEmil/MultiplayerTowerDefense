using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;

namespace AssemblyCSharp
{
	[System.Serializable]
	public class LevelLockState
	{
		Dictionary<int, bool> levelLocks;
		public LevelLockState ()
		{

			levelLocks = new Dictionary<int, bool>();
		}

		public LevelLockState(SerializationInfo info, StreamingContext ctxt)
		{
			levelLocks = (Dictionary<int, bool>)info.GetValue("levelLocks", typeof(Dictionary<int, bool>));
		}

		public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
		{
			info.AddValue("levelLocks", levelLocks);
		}
	}
}

