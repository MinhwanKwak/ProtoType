using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class FacilityStageData
    {
        [SerializeField]
		private int _stage;
		public int stage
		{
			get { return _stage;}
			set { _stage = value;}
		}
		[SerializeField]
		private int _facility_floor;
		public int facility_floor
		{
			get { return _facility_floor;}
			set { _facility_floor = value;}
		}
		[SerializeField]
		private int _facility_idx;
		public int facility_idx
		{
			get { return _facility_idx;}
			set { _facility_idx = value;}
		}
		[SerializeField]
		private int _build_condition_idx;
		public int build_condition_idx
		{
			get { return _build_condition_idx;}
			set { _build_condition_idx = value;}
		}
		[SerializeField]
		private int _build_condition_level;
		public int build_condition_level
		{
			get { return _build_condition_level;}
			set { _build_condition_level = value;}
		}
		[SerializeField]
		private byte[] _build_cost;
		public System.Numerics.BigInteger build_cost
		{
			get { return new System.Numerics.BigInteger(_build_cost);}
			set { _build_cost = value.ToByteArray();}
		}
		[SerializeField]
		private byte[] _floor_value_increment;
		public System.Numerics.BigInteger floor_value_increment
		{
			get { return new System.Numerics.BigInteger(_floor_value_increment);}
			set { _floor_value_increment = value.ToByteArray();}
		}
		[SerializeField]
		private byte[] _floor_cost_increment;
		public System.Numerics.BigInteger floor_cost_increment
		{
			get { return new System.Numerics.BigInteger(_floor_cost_increment);}
			set { _floor_cost_increment = value.ToByteArray();}
		}
		[SerializeField]
		private int _slot_condition_idx1;
		public int slot_condition_idx1
		{
			get { return _slot_condition_idx1;}
			set { _slot_condition_idx1 = value;}
		}
		[SerializeField]
		private int _slot_condition_level1;
		public int slot_condition_level1
		{
			get { return _slot_condition_level1;}
			set { _slot_condition_level1 = value;}
		}
		[SerializeField]
		private int _slot_cost_ratio1;
		public int slot_cost_ratio1
		{
			get { return _slot_cost_ratio1;}
			set { _slot_cost_ratio1 = value;}
		}
		[SerializeField]
		private int _slot_condition_idx2;
		public int slot_condition_idx2
		{
			get { return _slot_condition_idx2;}
			set { _slot_condition_idx2 = value;}
		}
		[SerializeField]
		private int _slot_condition_level2;
		public int slot_condition_level2
		{
			get { return _slot_condition_level2;}
			set { _slot_condition_level2 = value;}
		}
		[SerializeField]
		private int _slot_cost_ratio2;
		public int slot_cost_ratio2
		{
			get { return _slot_cost_ratio2;}
			set { _slot_cost_ratio2 = value;}
		}
		[SerializeField]
		private List<int> _auto_manager_lv;
		public List<int> auto_manager_lv
		{
			get { return _auto_manager_lv;}
			set { _auto_manager_lv = value;}
		}
		[SerializeField]
		private List<int> _auto_manager_count;
		public List<int> auto_manager_count
		{
			get { return _auto_manager_count;}
			set { _auto_manager_count = value;}
		}

    }

    [System.Serializable]
    public class FacilityStage : Table<FacilityStageData, KeyValuePair<int,int>>
    {
    }
}

