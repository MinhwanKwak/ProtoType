using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class MonsterBuffData
    {
        [SerializeField]
		private int _level;
		public int level
		{
			get { return _level;}
			set { _level = value;}
		}
		[SerializeField]
		private int _need_monster_kill;
		public int need_monster_kill
		{
			get { return _need_monster_kill;}
			set { _need_monster_kill = value;}
		}
		[SerializeField]
		private int _buff_idx;
		public int buff_idx
		{
			get { return _buff_idx;}
			set { _buff_idx = value;}
		}
		[SerializeField]
		private int _gauge_reduce_time;
		public int gauge_reduce_time
		{
			get { return _gauge_reduce_time;}
			set { _gauge_reduce_time = value;}
		}
		[SerializeField]
		private int _gauge_reduce_value;
		public int gauge_reduce_value
		{
			get { return _gauge_reduce_value;}
			set { _gauge_reduce_value = value;}
		}
		[SerializeField]
		private int _buff_gem_value;
		public int buff_gem_value
		{
			get { return _buff_gem_value;}
			set { _buff_gem_value = value;}
		}

    }

    [System.Serializable]
    public class MonsterBuff : Table<MonsterBuffData, int>
    {
    }
}

