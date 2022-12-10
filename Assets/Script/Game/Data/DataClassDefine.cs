using System;
using System.Numerics;
using UniRx;
using System.Collections.Generic;
using UnityEngine;
using BanpoFri;
using System.Linq;

public interface IReadOnlyData : ICloneable { 
	void Create();
}
public interface IClientData { }


public class FacilityData : IReadOnlyData
{
	public int GroundIndex { get; set; } = 0;
	public int FacilityGradeIdx { get; protected set; } = 0;
	public int LandStatusEventIdx { get; protected set; } = 0;
	public bool IsEventGround { get; protected set; } = false;

	public DateTime BenefitTime;

	public IReactiveProperty<int> FacilityGradeProperty = new ReactiveProperty<int>();
	public IReactiveProperty<int> LandStatusProperty = new ReactiveProperty<int>();

	public IReactiveProperty<bool> IsBenefit = new ReactiveProperty<bool>(false);

	public void Create()
	{
		FacilityGradeProperty.Value = FacilityGradeIdx;
		FacilityGradeProperty.Subscribe(x => { FacilityGradeIdx = x; });

		LandStatusProperty.Value = LandStatusEventIdx;
		LandStatusProperty.Subscribe(x => { LandStatusEventIdx = x; });

	}


	public FacilityData(int groundidx, int facilitygradeidx, int landstatuseventidx, bool iseventground, long benefittime , bool isbenefit)
	{
		GroundIndex = groundidx;
		FacilityGradeIdx = facilitygradeidx;
		LandStatusEventIdx = landstatuseventidx;
		IsEventGround = iseventground;
		BenefitTime = new System.DateTime(benefittime);
		IsBenefit.Value = isbenefit;

		Create();
	}

	public virtual object Clone()
	{
		var clone = new FacilityData(GroundIndex, FacilityGradeIdx, LandStatusEventIdx, IsEventGround , BenefitTime.Ticks, IsBenefit.Value);
		return clone;
	}


}




