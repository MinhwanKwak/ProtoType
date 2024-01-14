using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class StageBuildData
    {
        [SerializeField]
		private int _stage;
		public int stage
		{
			get { return _stage;}
			set { _stage = value;}
		}
		[SerializeField]
		private int _ally_build_hp;
		public int ally_build_hp
		{
			get { return _ally_build_hp;}
			set { _ally_build_hp = value;}
		}
		[SerializeField]
		private int _enemy_build_hp;
		public int enemy_build_hp
		{
			get { return _enemy_build_hp;}
			set { _enemy_build_hp = value;}
		}

    }

    [System.Serializable]
    public class StageBuild : Table<StageBuildData, int>
    {
    }
}

