
namespace DosEmulator.Hardware
{
    public delegate void MakeOpcode(Memory memory, Cpu cpu, BinaryReader reader);
    public class Opcodes
    {
        //reference to the pc memory
        Memory memory;
        //reference to the pc cpu
        Cpu cpu;
        //reference to the reader
        BinaryReader reader;
        //map opcodes to c# function
        Dictionary<byte, MakeOpcode> map = new Dictionary<byte, MakeOpcode>();

        public Opcodes(Memory memory, Cpu cpu)
        {
            this.memory = memory;
            this.cpu = cpu;

            //-----------------------------------------
            //                 ADD
            map.Add(0, (memory, cpu, reader) =>
            {
                byte value = reader.ReadByte();

                //Check for overflow.
                byte r = cpu.GetRegisterByte(value);
                byte l = cpu.GetRegisterByte((byte)(value >> 3));
                cpu.O = l + r > byte.MaxValue;

                //Set the value
                cpu.SetRegister(value, (byte)(l + r));

                //Update zero & carry flags
                r = cpu.GetRegisterByte(value);
                cpu.Z = r == 0;
                cpu.Cy = (r & 128) != 0;

            });
            map.Add(1, (memory, cpu, reader) =>
            {
                byte value = reader.ReadByte();

                //Check for overflow.
                ushort r = cpu.GetRegister(value);
                ushort l = cpu.GetRegister((byte)(value >> 3));
                cpu.O = l + r > ushort.MaxValue;

                //Set the value
                cpu.SetRegister(value, (ushort)(l + r));

                //Update zero & carry flags
                r = cpu.GetRegister(value);
                cpu.Z = r == 0;
                cpu.Cy = (r & 32768) != 0;

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
                //Check for overflow
                byte add = reader.ReadByte();
                cpu.O = cpu.Al + add > byte.MaxValue;

                //set the value
                cpu.Al += add;

                //Update zero & carry flags
                cpu.Z = cpu.Al == 0;
                cpu.Cy = (cpu.Al & 128) != 0;
            });
            map.Add(5, (memory, cpu, reader) =>
            {
                //Check for overflow
                ushort add = reader.ReadUInt16();
                cpu.O = cpu.Ax + add > ushort.MaxValue;

                //set the value
                cpu.Ax += add;

                //Update zero & carry flags
                cpu.Z = cpu.Ax == 0;
                cpu.Cy = (cpu.Ax & 32768) != 0;
            });
            map.Add(128, (memory, cpu, reader) =>
            {
                byte reg = reader.ReadByte();
                byte value = reader.ReadByte();

                //Check for overflow.
                byte regValue = cpu.GetRegisterByte(reg);
                cpu.O = regValue + value > byte.MaxValue;

                //Set the value
                cpu.SetRegister(reg, (byte)(regValue + value));

                //Update zero & carry flags
                regValue = cpu.GetRegisterByte(reg);
                cpu.Z = regValue == 0;
                cpu.Cy = (regValue & 128) != 0;
            });
            map.Add(129, (memory, cpu, reader) =>
            {
                byte reg = reader.ReadByte();
                ushort value = reader.ReadUInt16();

                //Check for overflow.
                ushort regValue = cpu.GetRegister(reg);
                cpu.O = regValue + value > ushort.MaxValue;

                //Set the value
                cpu.SetRegister(reg, (ushort)(regValue + value));

                //Update zero & carry flags
                regValue = cpu.GetRegister(reg);
                cpu.Z = regValue == 0;
                cpu.Cy = (regValue & 32768) != 0;
            });
            map.Add(131, (memory, cpu, reader) =>
            {
                byte reg = reader.ReadByte();
                byte value = reader.ReadByte();

                //Check for overflow.
                ushort regValue = cpu.GetRegister(reg);
                cpu.O = regValue + value > ushort.MaxValue;

                //Set the value
                cpu.SetRegister(reg, (ushort)(regValue + value));

                //Update zero & carry flags
                regValue = cpu.GetRegister(reg);
                cpu.Z = regValue == 0;
                cpu.Cy = (regValue & 32768) != 0;
            });
            //-----------------------------------------
        }
    }
}
