
namespace DosEmulator.Hardware.OpCodes
{
    public abstract class Opcode
    {
        protected const int last_db = 128;
        protected const int last_dw = 32768;

        /// <summary>
        /// Map all the opcodes of this instruction to a map object
        /// </summary>
        /// <param name="map"></param>
        public abstract void MapTo(Dictionary<byte, MakeOpcode> map);
    }
}
