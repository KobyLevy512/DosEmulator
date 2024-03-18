

namespace DosEmulator.Hardware.OpCodes
{
    public class Call : Opcode
    {
        public override void MapTo(Dictionary<byte, MakeOpcode> map)
        {
            map.Add(154, (memory, cpu, reader) =>
            {
                //push the code segment to the top of the stack
                memory.SetDw(cpu.Sp, cpu.Cs);
                cpu.Sp -= 2;

                //push the current instruction pointer to the top of the stack
                memory.SetDw(cpu.Sp, cpu.Ip);
                cpu.Sp -= 2;

                cpu.Cs = reader.ReadUInt16();
                cpu.Ip = (ushort)(cpu.Ip + reader.ReadInt16());
            });
            map.Add(232, (memory, cpu, reader) =>
            {
                //push the current instruction pointer to the top of the stack
                memory.SetDw(cpu.Sp, cpu.Ip);
                cpu.Sp -= 2;

                //set the instruction pointer to jump to the call
                cpu.Ip = (ushort)(cpu.Ip + reader.ReadInt16());
            });
        }
    }
}
