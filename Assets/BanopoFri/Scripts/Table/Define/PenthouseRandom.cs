using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class PenthouseRandomData
    {
        [SerializeField]
		private int _random_idx;
		public int random_idx
		{
			get { return _random_idx;}
			set { _random_idx = value;}
		}
		[SerializeField]
		private List<int> _random_type;
		public List<int> random_type
		{
			get { return _random_type;}
			set { _random_type = value;}
		}
		[SerializeField]
		private int _grade;
		public int grade
		{
			get { return _grade;}
			set { _grade = value;}
		}
		[SerializeField]
		private int _ratio;
		public int ratio
		{
			get { return _ratio;}
			set { _ratio = value;}
		}
		[SerializeField]
		private string _info_name;
		public string info_name
		{
			get { return _info_name;}
			set { _info_name = value;}
		}

    }

    [System.Serializable]
    public class PenthouseRandom : Table<PenthouseRandomData, int>
    {
    }
}

