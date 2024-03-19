

namespace DosEmulator.Hardware.OpCodes
{
    public class Cmpw : Opcode
    {
        public override void MapTo(Dictionary<byte, MakeOpcode> map)
        {
            map.Add(167, (memory, cpu, reader) =>
            {
                int ptrL = cpu.Ds + cpu.Si;
                int ptrR = cpu.Es + cpu.Di;

                // Calculate the difference and interpret as signed byte
                int res = memory.GetDw(ptrL) - memory.GetDw(ptrR);

                // Set flags based on the result
                cpu.Zf = res == 0;
                cpu.Sf = res < 0;
                cpu.Of = res < short.MinValue || res > short.MaxValue;

                //Increment to the pointers
                cpu.Si += 2;
                cpu.Di += 2;
            });
        }
    }
}
