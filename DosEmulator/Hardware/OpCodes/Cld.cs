
namespace DosEmulator.Hardware.OpCodes
{
    public class Cld : Opcode
    {
        public override void MapTo(Dictionary<byte, MakeOpcode> map)
        {
            //Direction flag reset
            map.Add(252, (memory, cpu, reader) => cpu.Df = false);
        }
    }
}
