using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class RandomCardData
    {
        [SerializeField]
		private int _random_idx;
		public int random_idx
		{
			get { return _random_idx;}
			set { _random_idx = value;}
		}
		[SerializeField]
		private int _order;
		public int order
		{
			get { return _order;}
			set { _order = value;}
		}
		[SerializeField]
		private int _type;
		public int type
		{
			get { return _type;}
			set { _type = value;}
		}
		[SerializeField]
		private int _manage_type;
		public int manage_type
		{
			get { return _manage_type;}
			set { _manage_type = value;}
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
		private string _name;
		public string name
		{
			get { return _name;}
			set { _name = value;}
		}
		[SerializeField]
		private string _icon;
		public string icon
		{
			get { return _icon;}
			set { _icon = value;}
		}

    }

    [System.Serializable]
    public class RandomCard : Table<RandomCardData, int>
    {
    }
}

