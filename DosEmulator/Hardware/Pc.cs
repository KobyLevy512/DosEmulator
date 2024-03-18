
namespace DosEmulator.Hardware
{
    public delegate void MakeOpcode(Memory memory, Cpu cpu, BinaryReader reader);
    public class Pc
    {
        const int last_db = 128;
        const int last_dw = 32768;
        //reference to the pc memory
        Memory memory;
        //reference to the pc cpu
        Cpu cpu;
        //reference to the reader
        BinaryReader reader;
        //map opcodes to c# function
        Dictionary<byte, MakeOpcode> map = new Dictionary<byte, MakeOpcode>();

        public Pc(Memory memory, Cpu cpu)
        {
            this.memory = memory;
            this.cpu = cpu;

            new OpCodes.Add().MapTo(map);
            new OpCodes.Adc().MapTo(map);
            //-----------------------------------------
            //                 AND

        }
    }
}
