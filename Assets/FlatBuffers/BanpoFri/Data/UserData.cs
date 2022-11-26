// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace BanpoFri.Data
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct UserData : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static UserData GetRootAsUserData(ByteBuffer _bb) { return GetRootAsUserData(_bb, new UserData()); }
  public static UserData GetRootAsUserData(ByteBuffer _bb, UserData obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public UserData __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public string Money { get { int o = __p.__offset(4); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetMoneyBytes() { return __p.__vector_as_span<byte>(4, 1); }
#else
  public ArraySegment<byte>? GetMoneyBytes() { return __p.__vector_as_arraysegment(4); }
#endif
  public byte[] GetMoneyArray() { return __p.__vector_as_array<byte>(4); }
  public string Storemoney { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetStoremoneyBytes() { return __p.__vector_as_span<byte>(6, 1); }
#else
  public ArraySegment<byte>? GetStoremoneyBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetStoremoneyArray() { return __p.__vector_as_array<byte>(6); }
  public int Cash { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public bool MutateCash(int cash) { int o = __p.__offset(8); if (o != 0) { __p.bb.PutInt(o + __p.bb_pos, cash); return true; } else { return false; } }
  public int Material { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetInt(o + __p.bb_pos) : (int)0; } }
  public bool MutateMaterial(int material) { int o = __p.__offset(10); if (o != 0) { __p.bb.PutInt(o + __p.bb_pos, material); return true; } else { return false; } }
  public BanpoFri.Data.FacilityData? Facilitydatas(int j) { int o = __p.__offset(12); return o != 0 ? (BanpoFri.Data.FacilityData?)(new BanpoFri.Data.FacilityData()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int FacilitydatasLength { get { int o = __p.__offset(12); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<BanpoFri.Data.UserData> CreateUserData(FlatBufferBuilder builder,
      StringOffset moneyOffset = default(StringOffset),
      StringOffset storemoneyOffset = default(StringOffset),
      int cash = 0,
      int material = 0,
      VectorOffset facilitydatasOffset = default(VectorOffset)) {
    builder.StartTable(5);
    UserData.AddFacilitydatas(builder, facilitydatasOffset);
    UserData.AddMaterial(builder, material);
    UserData.AddCash(builder, cash);
    UserData.AddStoremoney(builder, storemoneyOffset);
    UserData.AddMoney(builder, moneyOffset);
    return UserData.EndUserData(builder);
  }

  public static void StartUserData(FlatBufferBuilder builder) { builder.StartTable(5); }
  public static void AddMoney(FlatBufferBuilder builder, StringOffset moneyOffset) { builder.AddOffset(0, moneyOffset.Value, 0); }
  public static void AddStoremoney(FlatBufferBuilder builder, StringOffset storemoneyOffset) { builder.AddOffset(1, storemoneyOffset.Value, 0); }
  public static void AddCash(FlatBufferBuilder builder, int cash) { builder.AddInt(2, cash, 0); }
  public static void AddMaterial(FlatBufferBuilder builder, int material) { builder.AddInt(3, material, 0); }
  public static void AddFacilitydatas(FlatBufferBuilder builder, VectorOffset facilitydatasOffset) { builder.AddOffset(4, facilitydatasOffset.Value, 0); }
  public static VectorOffset CreateFacilitydatasVector(FlatBufferBuilder builder, Offset<BanpoFri.Data.FacilityData>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateFacilitydatasVectorBlock(FlatBufferBuilder builder, Offset<BanpoFri.Data.FacilityData>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartFacilitydatasVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<BanpoFri.Data.UserData> EndUserData(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<BanpoFri.Data.UserData>(o);
  }
  public static void FinishUserDataBuffer(FlatBufferBuilder builder, Offset<BanpoFri.Data.UserData> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedUserDataBuffer(FlatBufferBuilder builder, Offset<BanpoFri.Data.UserData> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
