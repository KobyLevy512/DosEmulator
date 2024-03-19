


namespace DosEmulator.Hardware.OpCodes
{
    public class Cmp : Opcode
    {
        public override void MapTo(Dictionary<byte, MakeOpcode> map)
        {
            map.Add(56, (memory, cpu, reader) =>
            {
                byte value = reader.ReadByte();

                // Get the registers values
                byte l = cpu.GetRegisterByte((byte)(value >> 3));
                byte r = cpu.GetRegisterByte(value);

                // Calculate the difference and interpret as signed byte
                short res = (short)(l - r);

                // Set flags based on the result
                cpu.Zf = res == 0;
                cpu.Sf = res < 0;
                cpu.Of = res < sbyte.MinValue || res > sbyte.MaxValue;
            });
            map.Add(57, (memory, cpu, reader) =>
            {
                byte value = reader.ReadByte();

                // Get the registers values
                ushort l = cpu.GetRegister((byte)(value >> 3));
                ushort r = cpu.GetRegister(value);

                // Calculate the difference and interpret as signed byte
                int res = (l - r);

                // Set flags based on the result
                cpu.Zf = res == 0;
                cpu.Sf = res < 0;
                cpu.Of = res < short.MinValue || res > short.MaxValue;
            });
            map.Add(58, (memory, cpu, reader) =>
            {
                map[56].Invoke(memory, cpu, reader);
            });
            map.Add(59, (memory, cpu, reader) =>
            {
                map[57].Invoke(memory, cpu, reader);
            });
            map.Add(60, (memory, cpu, reader) =>
            {
                //Read constant value.
                byte value = reader.ReadByte();
                //compute with al.
                short res = (short)(value - cpu.Al);

                // Set flags based on the result.
                cpu.Zf = res == 0;
                cpu.Sf = res < 0;
                cpu.Of = res < sbyte.MinValue || res > sbyte.MaxValue;
            });
            map.Add(61, (memory, cpu, reader) =>
            {
                //Read constant value.
                ushort value = reader.ReadUInt16();
                //compute with al.
                int res = (value - cpu.Ax);

                // Set flags based on the result.
                cpu.Zf = res == 0;
                cpu.Sf = res < 0;
                cpu.Of = res < short.MinValue || res > short.MaxValue;
            });
        }
    }
}
