using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class TutorialData
    {
        [SerializeField]
		private int _idx;
		public int idx
		{
			get { return _idx;}
			set { _idx = value;}
		}
		[SerializeField]
		private int _order;
		public int order
		{
			get { return _order;}
			set { _order = value;}
		}
		[SerializeField]
		private int _open_stage;
		public int open_stage
		{
			get { return _open_stage;}
			set { _open_stage = value;}
		}
		[SerializeField]
		private int _select_tuto;
		public int select_tuto
		{
			get { return _select_tuto;}
			set { _select_tuto = value;}
		}
		[SerializeField]
		private string _name;
		public string name
		{
			get { return _name;}
			set { _name = value;}
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
		private int _use_place;
		public int use_place
		{
			get { return _use_place;}
			set { _use_place = value;}
		}

    }

    [System.Serializable]
    public class Tutorial : Table<TutorialData, int>
    {
    }
}

