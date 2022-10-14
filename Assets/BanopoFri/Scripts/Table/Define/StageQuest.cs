using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class StageQuestData
    {
        [SerializeField]
		private int _stage;
		public int stage
		{
			get { return _stage;}
			set { _stage = value;}
		}
		[SerializeField]
		private int _order;
		public int order
		{
			get { return _order;}
			set { _order = value;}
		}
		[SerializeField]
		private int _quest_slot;
		public int quest_slot
		{
			get { return _quest_slot;}
			set { _quest_slot = value;}
		}
		[SerializeField]
		private int _quest_type;
		public int quest_type
		{
			get { return _quest_type;}
			set { _quest_type = value;}
		}
		[SerializeField]
		private int _quest_object_idx;
		public int quest_object_idx
		{
			get { return _quest_object_idx;}
			set { _quest_object_idx = value;}
		}
		[SerializeField]
		private byte[] _quest_value;
		public System.Numerics.BigInteger quest_value
		{
			get { return new System.Numerics.BigInteger(_quest_value);}
			set { _quest_value = value.ToByteArray();}
		}
		[SerializeField]
		private int _reward_type;
		public int reward_type
		{
			get { return _reward_type;}
			set { _reward_type = value;}
		}
		[SerializeField]
		private int _reward_idx;
		public int reward_idx
		{
			get { return _reward_idx;}
			set { _reward_idx = value;}
		}
		[SerializeField]
		private int _reward_value;
		public int reward_value
		{
			get { return _reward_value;}
			set { _reward_value = value;}
		}
		[SerializeField]
		private int _shortcut;
		public int shortcut
		{
			get { return _shortcut;}
			set { _shortcut = value;}
		}

    }

    [System.Serializable]
    public class StageQuest : Table<StageQuestData, KeyValuePair<int,int>>
    {
    }
}

