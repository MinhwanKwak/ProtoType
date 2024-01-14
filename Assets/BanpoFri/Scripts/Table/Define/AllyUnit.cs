using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class AllyUnitData
    {
        [SerializeField]
		private int _ally_idx;
		public int ally_idx
		{
			get { return _ally_idx;}
			set { _ally_idx = value;}
		}
		[SerializeField]
		private int _hp;
		public int hp
		{
			get { return _hp;}
			set { _hp = value;}
		}
		[SerializeField]
		private int _move_speed;
		public int move_speed
		{
			get { return _move_speed;}
			set { _move_speed = value;}
		}
		[SerializeField]
		private int _attack_speed;
		public int attack_speed
		{
			get { return _attack_speed;}
			set { _attack_speed = value;}
		}
		[SerializeField]
		private int _attack;
		public int attack
		{
			get { return _attack;}
			set { _attack = value;}
		}
		[SerializeField]
		private int _attack_range;
		public int attack_range
		{
			get { return _attack_range;}
			set { _attack_range = value;}
		}
		[SerializeField]
		private int _price_buildcoin;
		public int price_buildcoin
		{
			get { return _price_buildcoin;}
			set { _price_buildcoin = value;}
		}

    }

    [System.Serializable]
    public class AllyUnit : Table<AllyUnitData, int>
    {
    }
}

