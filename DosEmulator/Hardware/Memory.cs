
namespace DosEmulator.Hardware
{
    public class Memory
    {
        byte[] data;

        public Memory() : this(64000) { }
        public Memory(int sizeInBytes)
        {
            data = new byte[sizeInBytes];
        }

        /// <summary>
        /// Clear all the data
        /// </summary>
        public void Clear()
        {
            Array.Clear(data);
        }
        /// <summary>
        /// Return a byte(db) value from the memory.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte GetDb(int index)
        {
            if (index < 0 || index >= data.Length) return 0;
            return data[index];
        }

        /// <summary>
        /// Return a ushort(dw) value from the memory.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ushort GetDw(int index)
        {
            if (index < 0 || index >= data.Length - 1) return 0;
            return (ushort)(data[index] | (data[index + 1] << 8));
        }

        /// <summary>
        /// Set a byte(db) value at the ram. 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetDb(int index, byte value)
        {
            if (index < 0 || index >= data.Length) return;
            data[index] = value;
        }
        /// <summary>
        /// Set a byte(db) value at the ram. 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetDw(int index, ushort value)
        {
            if (index < 0 || index >= data.Length - 1) return;
            data[index] = (byte)value;
            data[index + 1] = (byte)(value >> 8);
        }
    }
}
