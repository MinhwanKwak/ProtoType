using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class ContentsOpenData
    {
        [SerializeField]
		private string _idx;
		public string idx
		{
			get { return _idx;}
			set { _idx = value;}
		}
		[SerializeField]
		private int _condition_stage;
		public int condition_stage
		{
			get { return _condition_stage;}
			set { _condition_stage = value;}
		}
		[SerializeField]
		private int _condition_quest;
		public int condition_quest
		{
			get { return _condition_quest;}
			set { _condition_quest = value;}
		}

    }

    [System.Serializable]
    public class ContentsOpen : Table<ContentsOpenData, string>
    {
    }
}

