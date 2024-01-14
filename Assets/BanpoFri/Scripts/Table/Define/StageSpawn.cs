using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class StageSpawnData
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
		private int _enemy_idx;
		public int enemy_idx
		{
			get { return _enemy_idx;}
			set { _enemy_idx = value;}
		}
		[SerializeField]
		private int _spwan_time;
		public int spwan_time
		{
			get { return _spwan_time;}
			set { _spwan_time = value;}
		}
		[SerializeField]
		private int _spawn_count;
		public int spawn_count
		{
			get { return _spawn_count;}
			set { _spawn_count = value;}
		}

    }

    [System.Serializable]
    public class StageSpawn : Table<StageSpawnData, int>
    {
    }
}

