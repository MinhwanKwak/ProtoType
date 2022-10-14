using UnityEngine;
using System.Collections.Generic;

namespace BanpoFri
{
    [System.Serializable]
    public class FacilityBasicData
    {
        [SerializeField]
		private int _idx;
		public int idx
		{
			get { return _idx;}
			set { _idx = value;}
		}
		[SerializeField]
		private string _name;
		public string name
		{
			get { return _name;}
			set { _name = value;}
		}
		[SerializeField]
		private string _event_name;
		public string event_name
		{
			get { return _event_name;}
			set { _event_name = value;}
		}
		[SerializeField]
		private string _desc;
		public string desc
		{
			get { return _desc;}
			set { _desc = value;}
		}
		[SerializeField]
		private int _facility_type;
		public int facility_type
		{
			get { return _facility_type;}
			set { _facility_type = value;}
		}
		[SerializeField]
		private int _manage_type;
		public int manage_type
		{
			get { return _manage_type;}
			set { _manage_type = value;}
		}
		[SerializeField]
		private int _value;
		public int value
		{
			get { return _value;}
			set { _value = value;}
		}
		[SerializeField]
		private int _cost;
		public int cost
		{
			get { return _cost;}
			set { _cost = value;}
		}
		[SerializeField]
		private int _production_time;
		public int production_time
		{
			get { return _production_time;}
			set { _production_time = value;}
		}
		[SerializeField]
		private int _slot2_producion_time;
		public int slot2_producion_time
		{
			get { return _slot2_producion_time;}
			set { _slot2_producion_time = value;}
		}
		[SerializeField]
		private int _slot3_producion_time;
		public int slot3_producion_time
		{
			get { return _slot3_producion_time;}
			set { _slot3_producion_time = value;}
		}
		[SerializeField]
		private int _slot2_incre_value;
		public int slot2_incre_value
		{
			get { return _slot2_incre_value;}
			set { _slot2_incre_value = value;}
		}
		[SerializeField]
		private int _slot3_incre_value;
		public int slot3_incre_value
		{
			get { return _slot3_incre_value;}
			set { _slot3_incre_value = value;}
		}
		[SerializeField]
		private int _synergy_buff_idx_2count;
		public int synergy_buff_idx_2count
		{
			get { return _synergy_buff_idx_2count;}
			set { _synergy_buff_idx_2count = value;}
		}
		[SerializeField]
		private int _synergy_buff_idx_3count;
		public int synergy_buff_idx_3count
		{
			get { return _synergy_buff_idx_3count;}
			set { _synergy_buff_idx_3count = value;}
		}
		[SerializeField]
		private string _resource_facility_icon;
		public string resource_facility_icon
		{
			get { return _resource_facility_icon;}
			set { _resource_facility_icon = value;}
		}
		[SerializeField]
		private string _resource_event_facility_icon;
		public string resource_event_facility_icon
		{
			get { return _resource_event_facility_icon;}
			set { _resource_event_facility_icon = value;}
		}
		[SerializeField]
		private string _resource_attribute_icon;
		public string resource_attribute_icon
		{
			get { return _resource_attribute_icon;}
			set { _resource_attribute_icon = value;}
		}
		[SerializeField]
		private string _resource_facility_prefab;
		public string resource_facility_prefab
		{
			get { return _resource_facility_prefab;}
			set { _resource_facility_prefab = value;}
		}
		[SerializeField]
		private string _resource_facilityevent_prefab;
		public string resource_facilityevent_prefab
		{
			get { return _resource_facilityevent_prefab;}
			set { _resource_facilityevent_prefab = value;}
		}
		[SerializeField]
		private string _resource_facility_image;
		public string resource_facility_image
		{
			get { return _resource_facility_image;}
			set { _resource_facility_image = value;}
		}

    }

    [System.Serializable]
    public class FacilityBasic : Table<FacilityBasicData, int>
    {
    }
}

