


namespace DosEmulator.Hardware.OpCodes
{
    public class Add : Opcode
    {
        public override void MapTo(Dictionary<byte, MakeOpcode> map)
        {
            //-----------------------------------------
            //                 ADD
            map.Add(0, (memory, cpu, reader) =>
            {
                byte value = reader.ReadByte();

                //Update the carry flag
                byte r = cpu.GetRegisterByte(value);
                byte l = cpu.GetRegisterByte((byte)(value >> 3));
                cpu.Cf = (r + l) > byte.MaxValue;

                //Set the value
                cpu.SetRegister(value, (byte)(l + r));

                //Update others flags
                r = cpu.GetRegisterByte(value);
                cpu.Zf = r == 0;
                cpu.Sf = (r & last_db) != 0;
                cpu.Of = ((r & last_db) == (l & last_db)) && ((r & last_db) != ((r + l) & last_db));

            });
            map.Add(1, (memory, cpu, reader) =>
            {
                byte value = reader.ReadByte();

                //Update the carry flag
                ushort r = cpu.GetRegister(value);
                ushort l = cpu.GetRegister((byte)(value >> 3));
                cpu.Cf = (l + r) > ushort.MaxValue;

                //Set the value
                cpu.SetRegister(value, (ushort)(l + r));

                //Update others flags
                r = cpu.GetRegister(value);
                cpu.Zf = r == 0;
                cpu.Sf = (r & last_dw) != 0;
                cpu.Of = ((r & last_dw) == (l & last_dw)) && ((r & last_dw) != ((r + l) & last_dw));

            });
            map.Add(2, (memory, cpu, reader) =>
            {
                map[0].Invoke(memory, cpu, reader);
            });
            map.Add(3, (memory, cpu, reader) =>
            {
                map[1].Invoke(memory, cpu, reader);
            });
            map.Add(4, (memory, cpu, reader) =>
            {
                //Update the carry flag
                byte add = reader.ReadByte();
                cpu.Cf = cpu.Al + add > byte.MaxValue;

                //set the value
                cpu.Al += add;

                //Update others flags
                cpu.Zf = cpu.Al == 0;
                cpu.Sf = (cpu.Al & last_db) != 0;
                cpu.Of = ((cpu.Al & last_db) == (add & last_db)) && ((cpu.Al & last_db) != ((cpu.Al + add) & last_db));
            });
            map.Add(5, (memory, cpu, reader) =>
            {
                //Check for overflow
                ushort add = reader.ReadUInt16();
                cpu.Cf = cpu.Ax + add > ushort.MaxValue;

                //set the value
                cpu.Ax += add;

                //Update others flags
                cpu.Zf = cpu.Ax == 0;
                cpu.Sf = (cpu.Ax & last_dw) != 0;
                cpu.Of = ((cpu.Ax & last_dw) == (add & last_dw)) && ((cpu.Ax & last_dw) != ((cpu.Ax + add) & last_dw));
            });
            map.Add(128, (memory, cpu, reader) =>
            {
                byte reg = reader.ReadByte();
                byte value = reader.ReadByte();
                byte regValue = cpu.GetRegisterByte(reg);
                ushort result = 0;

                //Check which instruction is it
                byte ins = (byte)(reg & 56);
                switch (ins)
                {
                    //Its add
                    case 0:
                        result = (ushort)(regValue + value);
                        break;
                    //Its or
                    case 1:
                        result = (ushort)(regValue | value);
                        break;
                    //Its adc
                    case 2:
                        result = (ushort)(regValue + value + (cpu.Cf ? 1 : 0));
                        break;
                    //Its sbb
                    case 3:
                        result = (ushort)(regValue - value - (cpu.Cf ? 1 : 0));
                        break;
                    //Its and
                    case 4:
                        result = (ushort)(regValue & value);
                        break;
                    //Its sub
                    case 5:
                        result = (ushort)(regValue - value);
                        break;
                    //Its xor
                    case 6:
                        result = (ushort)(regValue ^ value);
                        break;
                    //Its cmp
                    case 7:
                        short res = (short)(value - regValue);
                        if (res == 0)
                        {
                            cpu.Zf = true;
                        }
                        else if (res < 0)
                        {
                            cpu.Sf = true;
                        }
                        else if (res > sbyte.MaxValue)
                        {
                            cpu.Of = true;
                        }
                        return;
                }

                //Update the carry flag
                cpu.Cf = result > byte.MaxValue;

                //Set the value
                cpu.SetRegister(reg, (byte)result);

                //Update others flags
                regValue = cpu.GetRegisterByte(reg);
                cpu.Zf = regValue == 0;
                cpu.Sf = (regValue & last_db) != 0;
                cpu.Of = ((regValue & last_db) == (value & last_db)) && ((regValue & last_db) != (result & last_db));
            });
            map.Add(129, (memory, cpu, reader) =>
            {
                byte reg = reader.ReadByte();
                ushort value = reader.ReadUInt16();
                ushort regValue = cpu.GetRegister(reg);
                uint result = 0;

                //Check which instruction is it
                byte ins = (byte)(reg & 56);
                switch (ins)
                {
                    //Its add
                    case 0:
                        result = (uint)(regValue + value);
                        break;
                    //Its or
                    case 1:
                        result = (uint)(regValue | value);
                        break;
                    //Its adc
                    case 2:
                        result = (uint)(regValue + value + (cpu.Cf ? 1 : 0));
                        break;
                    //Its sbb
                    case 3:
                        result = (uint)(regValue - value - (cpu.Cf ? 1 : 0));
                        break;
                    //Its and
                    case 4:
                        result = (uint)(regValue & value);
                        break;
                    //Its sub
                    case 5:
                        result = (uint)(regValue - value);
                        break;
                    //Its xor
                    case 6:
                        result = (uint)(regValue ^ value);
                        break;
                    //Its cmp
                    case 7:
                        int res = value - regValue;
                        if (res == 0)
                        {
                            cpu.Zf = true;
                        }
                        else if (res < 0)
                        {
                            cpu.Sf = true;
                        }
                        else if (res > short.MaxValue)
                        {
                            cpu.Of = true;
                        }
                        return;
                }

                //Update the carry flag
                cpu.Cf = result > ushort.MaxValue;

                //Set the value
                cpu.SetRegister(reg, (ushort)result);

                //Update others flags
                regValue = cpu.GetRegisterByte(reg);
                cpu.Zf = regValue == 0;
                cpu.Sf = (regValue & last_dw) != 0;
                cpu.Of = ((regValue & last_dw) == (value & last_dw)) && ((regValue & last_dw) != (result & last_dw));
            });
            map.Add(131, (memory, cpu, reader) =>
            {
                byte reg = reader.ReadByte();
                ushort value = reader.ReadByte();
                ushort regValue = cpu.GetRegister(reg);
                uint result = 0;

                //Check which instruction is it
                byte ins = (byte)(reg & 56);
                switch (ins)
                {
                    //Its add
                    case 0:
                        result = (uint)(regValue + value);
                        break;
                    //Its or
                    case 1:
                        result = (uint)(regValue | value);
                        break;
                    //Its adc
                    case 2:
                        result = (uint)(regValue + value + (cpu.Cf ? 1 : 0));
                        break;
                    //Its sbb
                    case 3:
                        result = (uint)(regValue - value - (cpu.Cf ? 1 : 0));
                        break;
                    //Its and
                    case 4:
                        result = (uint)(regValue & value);
                        break;
                    //Its sub
                    case 5:
                        result = (uint)(regValue - value);
                        break;
                    //Its xor
                    case 6:
                        result = (uint)(regValue ^ value);
                        break;
                    //Its cmp
                    case 7:
                        int res = value - regValue;
                        if (res == 0)
                        {
                            cpu.Zf = true;
                        }
                        else if (res < 0)
                        {
                            cpu.Sf = true;
                        }
                        else if (res > short.MaxValue)
                        {
                            cpu.Of = true;
                        }
                        return;
                }

                //Update the carry flag
                cpu.Cf = result > ushort.MaxValue;

                //Set the value
                cpu.SetRegister(reg, (ushort)result);

                //Update others flags
                regValue = cpu.GetRegisterByte(reg);
                cpu.Zf = regValue == 0;
                cpu.Sf = (regValue & last_dw) != 0;
                cpu.Of = ((regValue & last_dw) == (value & last_dw)) && ((regValue & last_dw) != (result & last_dw));
            });
            //-----------------------------------------
        }
    }
}
