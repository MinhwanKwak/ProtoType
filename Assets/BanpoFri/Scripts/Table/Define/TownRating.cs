using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class TownRatingData
    {
        [SerializeField]
		private int _grade;
		public int grade
		{
			get { return _grade;}
			set { _grade = value;}
		}
		[SerializeField]
		private string _name;
		public string name
		{
			get { return _name;}
			set { _name = value;}
		}
		[SerializeField]
		private int _rating_point_min;
		public int rating_point_min
		{
			get { return _rating_point_min;}
			set { _rating_point_min = value;}
		}

    }

    [System.Serializable]
    public class TownRating : Table<TownRatingData, int>
    {
    }
}

