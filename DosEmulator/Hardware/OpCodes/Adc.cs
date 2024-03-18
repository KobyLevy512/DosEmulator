﻿

namespace DosEmulator.Hardware.OpCodes
{
    public class Adc : Opcode
    {
        public override void MapTo(Dictionary<byte, MakeOpcode> map)
        {
            //-----------------------------------------
            //                 ADC
            map.Add(16, (memory, cpu, reader) =>
            {
                byte value = reader.ReadByte();

                //Update the carry flag
                byte r = cpu.GetRegisterByte(value);
                byte l = cpu.GetRegisterByte((byte)(value >> 3));
                cpu.Cf = (r + l + (cpu.Cf ? 1 : 0)) > byte.MaxValue;

                //Set the value
                cpu.SetRegister(value, (byte)(l + r));

                //Update others flags
                r = cpu.GetRegisterByte(value);
                cpu.Zf = r == 0;
                cpu.Sf = (r & last_db) != 0;
                cpu.Of = ((r & last_db) == (l & last_db)) && ((r & last_db) != ((r + l) & last_db));

            });
            map.Add(17, (memory, cpu, reader) =>
            {
                byte value = reader.ReadByte();

                //Update the carry flag
                ushort r = cpu.GetRegister(value);
                ushort l = cpu.GetRegister((byte)(value >> 3));
                cpu.Cf = (l + r + (cpu.Cf ? 1 : 0)) > ushort.MaxValue;

                //Set the value
                cpu.SetRegister(value, (ushort)(l + r));

                //Update others flags
                r = cpu.GetRegister(value);
                cpu.Zf = r == 0;
                cpu.Sf = (r & last_dw) != 0;
                cpu.Of = ((r & last_dw) == (l & last_dw)) && ((r & last_dw) != ((r + l) & last_dw));
            });
            map.Add(18, (memory, cpu, reader) =>
            {
                map[16].Invoke(memory, cpu, reader);
            });
            map.Add(19, (memory, cpu, reader) =>
            {
                map[17].Invoke(memory, cpu, reader);
            });
            map.Add(20, (memory, cpu, reader) =>
            {
                //Update the carry flag
                byte add = reader.ReadByte();
                cpu.Cf = cpu.Al + add + (cpu.Cf ? 1 : 0) > byte.MaxValue;

                //set the value
                cpu.Al += add;

                //Update others flags
                cpu.Zf = cpu.Al == 0;
                cpu.Sf = (cpu.Al & last_db) != 0;
                cpu.Of = ((cpu.Al & last_db) == (add & last_db)) && ((cpu.Al & last_db) != ((cpu.Al + add) & last_db));
            });
            map.Add(21, (memory, cpu, reader) =>
            {
                //Check for overflow
                ushort add = reader.ReadUInt16();
                cpu.Cf = cpu.Ax + add + (cpu.Cf ? 1 : 0) > ushort.MaxValue;

                //set the value
                cpu.Ax += add;

                //Update others flags
                cpu.Zf = cpu.Ax == 0;
                cpu.Sf = (cpu.Ax & last_dw) != 0;
                cpu.Of = ((cpu.Ax & last_dw) == (add & last_dw)) && ((cpu.Ax & last_dw) != ((cpu.Ax + add) & last_dw));
            });
            //-----------------------------------------
        }
    }
}
