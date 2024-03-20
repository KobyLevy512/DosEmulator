
using DosEmulator.Hardware.OpCodes;
using System.Reflection;

namespace DosEmulator.Hardware
{
    public delegate void MakeOpcode(Memory memory, Cpu cpu, BinaryReader reader);
    public class Pc
    {
        //reference to the pc memory
        Memory memory;

        //reference to the pc cpu
        Cpu cpu;

        //wrapper map for opcodes to appropiate  c# function
        Dictionary<byte, MakeOpcode> map = new Dictionary<byte, MakeOpcode>();

        public Pc():this(new Memory(), new Cpu()) { }
        public Pc(Memory memory):this(memory, new Cpu()) { }
        public Pc(Memory memory, Cpu cpu)
        {
            this.memory = memory;
            this.cpu = cpu;

            // Get all subtypes of opcode
            List<Type> opcodes = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type => type.IsSubclassOf(typeof(Opcode)))
                .ToList();

            // Map each opcode to the map object.
            foreach (Type? opcode in opcodes)
            {
                //Get an instance of this opcode.
                Opcode? ins =  (Opcode?)opcode?.GetConstructor(Type.EmptyTypes)?.Invoke(null);
                
                //Map it to the map object.
                ins?.MapTo(map);
            }
        }
    }
}
