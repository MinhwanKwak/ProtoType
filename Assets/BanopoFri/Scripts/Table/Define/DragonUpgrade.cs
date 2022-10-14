using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class DragonUpgradeData
    {
        [SerializeField]
		private int _type;
		public int type
		{
			get { return _type;}
			set { _type = value;}
		}
		[SerializeField]
		private int _level;
		public int level
		{
			get { return _level;}
			set { _level = value;}
		}
		[SerializeField]
		private int _silverstone_value;
		public int silverstone_value
		{
			get { return _silverstone_value;}
			set { _silverstone_value = value;}
		}
		[SerializeField]
		private int _goldstone_value;
		public int goldstone_value
		{
			get { return _goldstone_value;}
			set { _goldstone_value = value;}
		}
		[SerializeField]
		private int _skill_buff;
		public int skill_buff
		{
			get { return _skill_buff;}
			set { _skill_buff = value;}
		}
		[SerializeField]
		private int _skill_atk;
		public int skill_atk
		{
			get { return _skill_atk;}
			set { _skill_atk = value;}
		}

    }

    [System.Serializable]
    public class DragonUpgrade : Table<DragonUpgradeData, KeyValuePair<int,int>>
    {
    }
}

