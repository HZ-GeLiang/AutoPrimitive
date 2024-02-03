namespace AutoPrimitive.Types
{
    /// <summary>
    /// enum的implicit conversions
    /// </summary>
    public readonly struct PrimitiveEnum
    {
        //参考: https://stackoverflow.com/q/261663/3563013 

        public PrimitiveEnum(Enum val)
        {
            Value = val;
        }
        public Enum Value { get; }

        public static implicit operator PrimitiveEnum(Enum val) => new PrimitiveEnum(val);
        public static implicit operator Enum(PrimitiveEnum primitive) => primitive.Value;

        public static implicit operator byte(PrimitiveEnum primitive) => Convert.ToByte(primitive.Value);
        public static implicit operator sbyte(PrimitiveEnum primitive) => Convert.ToSByte(primitive.Value);
        public static implicit operator short(PrimitiveEnum primitive) => Convert.ToInt16(primitive.Value);
        public static implicit operator ushort(PrimitiveEnum primitive) => Convert.ToUInt16(primitive.Value);
        public static implicit operator int(PrimitiveEnum primitive) => Convert.ToInt32(primitive.Value);
        public static implicit operator uint(PrimitiveEnum primitive) => Convert.ToUInt32(primitive.Value);
        public static implicit operator long(PrimitiveEnum primitive) => Convert.ToInt64(primitive.Value);
        public static implicit operator ulong(PrimitiveEnum primitive) => Convert.ToUInt64(primitive.Value);

        public static implicit operator string(PrimitiveEnum primitive) => primitive.Value.ToString();

        //测试发现即使这块代码没有也能对 nullable 进行比较, 原因是 nullable<int> 在和 int 做比较了.
        public static implicit operator byte?(PrimitiveEnum primitive) => new byte?(Convert.ToByte(primitive.Value));
        public static implicit operator sbyte?(PrimitiveEnum primitive) => new sbyte?(Convert.ToSByte(primitive.Value));
        public static implicit operator short?(PrimitiveEnum primitive) => new short?(Convert.ToInt16(primitive.Value));
        public static implicit operator ushort?(PrimitiveEnum primitive) => new ushort?(Convert.ToUInt16(primitive.Value));
        public static implicit operator int?(PrimitiveEnum primitive) => new int?(Convert.ToInt32(primitive.Value));
        public static implicit operator uint?(PrimitiveEnum primitive) => new uint?(Convert.ToUInt32(primitive.Value));
        public static implicit operator long?(PrimitiveEnum primitive) => new long?(Convert.ToInt64(primitive.Value));
        public static implicit operator ulong?(PrimitiveEnum primitive) => new ulong?(Convert.ToUInt64(primitive.Value));


        //操作符/方法的重写
        public static bool operator ==(PrimitiveEnum a, PrimitiveEnum b) => a.Value.Equals(b.Value);
        public static bool operator !=(PrimitiveEnum a, PrimitiveEnum b) => !a.Value.Equals(b.Value);

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is PrimitiveEnum other)
            {
                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                return Equals(Value, other.Value);
            }

            if (Value == null)
            {
                return obj == null;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }


    }

    /*
if (mod.ResetState == (int)ResetStateEnum.正常)
对比
if (mod.ResetState == (PrimitiveEnum)ResetStateEnum.申请重开)

// if (ticket2.ResetState == 0)
IL_020d: ldloc.s 6
IL_020f: callvirt instance int32 HR.CRM.Application.CRM_Ticket.Dto.v_CRM_TicketDto::get_ResetState()
IL_0214: ldc.i4.0
IL_0215: ceq
IL_0217: stloc.s 21
IL_0219: ldloc.s 21
IL_021b: brfalse.s IL_028c


// else if (ticket2.ResetState == (int)(PrimitiveEnum)ResetStateEnum.申请重开)
IL_0287: br IL_04ae
IL_028c: ldloc.s 6
IL_028e: callvirt instance int32 HR.CRM.Application.CRM_Ticket.Dto.v_CRM_TicketDto::get_ResetState()
IL_0293: ldc.i4.1
会多一步骤:先装箱,然后通过方法表调用隐式转换来进行比较
IL_0294: box [HR.CRM.Core]HR.CRM.Core.Enums.ResetStateEnum
IL_0299: call valuetype HR.CRM.Application.PrimitiveEnum HR.CRM.Application.PrimitiveEnum::op_Implicit(class [System.Runtime]System.Enum)
IL_029e: call int32 HR.CRM.Application.PrimitiveEnum::op_Implicit(valuetype HR.CRM.Application.PrimitiveEnum)
IL_02a3: ceq
IL_02a5: stloc.s 26
IL_02a7: ldloc.s 26
IL_02a9: brfalse.s IL_0314



compareEnum2是  (int)(PrimitiveEnum)ResetStateEnum.申请重开) 这种


Method	        Mean	    Error	    StdDev  	Median  	Rank	Gen 0	Gen 1	Gen 2	Allocated
FilterByString	39.63 μs	0.767 μs	0.970 μs	39.20 μs	1	    0.2441	-	    -	    1,384 B
compareEnum2	119.99 μs	2.386 μs	4.422 μs	119.35 μs	2   	-	    -	    -	    24 B
compareEnum 	122.20 μs	2.424 μs	5.472 μs	121.33 μs	2	    -	    -	    -	    -

*/
}
