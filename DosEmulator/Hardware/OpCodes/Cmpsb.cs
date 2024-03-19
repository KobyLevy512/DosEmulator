

namespace DosEmulator.Hardware.OpCodes
{
    public class Cmpsb : Opcode
    {
        public override void MapTo(Dictionary<byte, MakeOpcode> map)
        {
            map.Add(166, (memory, cpu, reader) =>
            {
                int ptrL = cpu.Ds + cpu.Si;
                int ptrR = cpu.Es + cpu.Di;
                
                // Calculate the difference and interpret as signed byte
                short res = (short)(memory.GetDb(ptrL) - memory.GetDb(ptrR));

                // Set flags based on the result
                cpu.Zf = res == 0;
                cpu.Sf = res < 0;
                cpu.Of = res < sbyte.MinValue || res > sbyte.MaxValue;

                //Increment to the pointers
                cpu.Si++;
                cpu.Di++;
            });
        }
    }
}
