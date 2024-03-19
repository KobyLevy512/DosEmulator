
using System.Reflection;

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

            // Get all subtypes of opcode
            List<Type> subclasses = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.IsSubclassOf(typeof(OpCodes.Opcode)))
                .ToList();

            // Print the names of the subclasses
            foreach (var subclass in subclasses)
            {
                ((OpCodes.Opcode)subclass?.GetConstructor(null)?.Invoke(null)).MapTo(map);
            }
            //new OpCodes.Add().MapTo(map);
            //new OpCodes.Adc().MapTo(map);
            //new OpCodes.And().MapTo(map);
            //new OpCodes.Call().MapTo(map);
            //map.Add(252, (memory, cpu, reader) => cpu.Df = false);//Direction flag reset
            //new OpCodes.Cmp().MapTo(map);
        }
    }
}
