using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using FlatBuffers;
using BanpoFri.Data;
using BanpoFri;

public partial class UserDataSystem
{
	private readonly string dataFileName = "Master.dat";

	private BanpoFri.Data.UserData flatBufferUserData;
	private float saveWaitStandardTime = 0.3f;
	private float deltaTime = 0f;
	private bool saving = false;

	public void Update()
	{
		if(saving)
		{
			if(deltaTime > saveWaitStandardTime)
			{
				saving = false;
				SaveFile();
				return;
			}
			deltaTime += Time.deltaTime;
		}
		
	}
	
	public string GetSaveFilePath()
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return $"{Application.persistentDataPath}/{dataFileName}";
		}
		else
		{
			return $"{Application.dataPath}/{dataFileName}";
		}
	}



	public string GetBackUpSaveFilePath()
	{
		var path = GetSaveFilePath();
		return $"{path}.backup";
	}


	public void Load()
    {
			

		var filePath = GetBackUpSaveFilePath();

			if (File.Exists(filePath))
			{
				mainData = new UserDataMain();
				eventData = new UserDataEvent();
				CurMode = mainData;
				var data = File.ReadAllBytes(filePath);
				ByteBuffer bb = new ByteBuffer(data);
				flatBufferUserData = BanpoFri.Data.UserData.GetRootAsUserData(bb);
				ConnectReadOnlyDatas();
				File.Delete(filePath);
				return;
			}
        






	     filePath = GetSaveFilePath();
		
		if(File.Exists(filePath))
		{
			var data = File.ReadAllBytes(filePath);
			ByteBuffer bb = new ByteBuffer(data);
			flatBufferUserData = BanpoFri.Data.UserData.GetRootAsUserData(bb);
			ConnectReadOnlyDatas();
		}
		else
		{
			ChangeDataMode(DataState.Main);
		}
    }

	public void Save(bool Immediately = false)
	{
		if (Immediately)
		{
			SaveFile();
			return;
		}
		if (saving)
		{
			deltaTime = 0f;
			return;
		}

		saving = true;
		deltaTime = 0f;		
	}

	private void SaveFile()
	{
		TpLog.Log("save file");
		var builder = new FlatBufferBuilder(1);
		int dataIdx = 0;
		var money = builder.CreateString(mainData.Money.Value.ToString());


		//insert start
		BanpoFri.Data.UserData.StartUserData(builder);

		BanpoFri.Data.UserData.AddMoney(builder, money);
		BanpoFri.Data.UserData.AddCash(builder, Cash.Value);
		BanpoFri.Data.UserData.AddMaterial(builder, mainData.Material.Value);

		var orc = BanpoFri.Data.UserData.EndUserData(builder);
		builder.Finish(orc.Value);

		var dataBuf = builder.DataBuffer;
		flatBufferUserData = BanpoFri.Data.UserData.GetRootAsUserData(dataBuf);

		var filePath = GetSaveFilePath();
		using (var ms = new MemoryStream(flatBufferUserData.ByteBuffer.ToFullArray(), dataBuf.Position, builder.Offset))
		{
			File.WriteAllBytes(filePath, ms.ToArray());
		}
	}


	public void ChangeDataMode(DataState state)
	{
		if (state == DataState)
			return;

		TpLog.Log($"ChangeDataMode:{state.ToString()}");
		switch (state)
		{
			case DataState.Main:
				CurMode = mainData;
				break;
			case DataState.Event:
				CurMode = eventData;
				break;
		}

		DataState = state;
	}
}
