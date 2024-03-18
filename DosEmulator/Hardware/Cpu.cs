
namespace DosEmulator.Hardware
{
    public class Cpu
    {
        //General purpose registers
        ushort ax, bx, cx, dx, si, di, bp, sp;
        //Segment registers
        ushort cs, ds, es, ss;
        //Special registers
        ushort ip, flag;

        /// <summary>
        ///ax (Accumulator) : Most arithmetic and logical computations use this register
        /// </summary>
        public ushort Ax
        {
            get => ax;
            set => ax = value;
        }
        /// <summary>
        /// bx (Base Reg) : Normally used to store base addresses (later)
        /// </summary>
        public ushort Bx
        {
            get => bx;
            set => bx = value;
        }
        /// <summary>
        /// cx (Count Reg) : Used for counting purposes, like number of iterations while looping, number of characters in a string, etc.
        /// </summary>
        public ushort Cx
        {
            get => cx;
            set => cx = value;
        }
        /// <summary>
        /// dx (Data Reg) : a true general purpose register.
        /// </summary>
        public ushort Dx
        {
            get => dx;
            set => dx = value;
        }

        /// <summary>
        /// The lower 8 bits of ax.
        /// </summary>
        public byte Al
        {
            get => (byte)(ax & 0x00FF);
            set => ax = (ushort)((ax & 0xFF00) | value);
        }
        /// <summary>
        /// The higher 8 bits of ax.
        /// </summary>
        public byte Ah
        {
            get => (byte)(ax & 0xFF00);
            set => ax = (ushort)((ax & 0x00FF) | value);
        }
        /// <summary>
        /// The lower 8 bits of bx.
        /// </summary>
        public byte Bl
        {
            get => (byte)(bx & 0x00FF);
            set => bx = (ushort)((bx & 0xFF00) | value);
        }
        /// <summary>
        /// The higher 8 bits of bx.
        /// </summary>
        public byte Bh
        {
            get => (byte)(bx & 0xFF00);
            set => bx = (ushort)((bx & 0x00FF) | value);
        }
        /// <summary>
        /// The lower 8 bits of cx.
        /// </summary>
        public byte Cl
        {
            get => (byte)(cx & 0x00FF);
            set => cx = (ushort)((cx & 0xFF00) | value);
        }
        /// <summary>
        /// The higher 8 bits of cx.
        /// </summary>
        public byte Ch
        {
            get => (byte)(cx & 0xFF00);
            set => cx = (ushort)((cx & 0x00FF) | value);
        }
        /// <summary>
        /// The lower 8 bits of dx.
        /// </summary>
        public byte Dl
        {
            get => (byte)(dx & 0x00FF);
            set => dx = (ushort)((dx & 0xFF00) | value);
        }
        /// <summary>
        /// The higher 8 bits of dx.
        /// </summary>
        public byte Dh
        {
            get => (byte)(dx & 0xFF00);
            set => dx = (ushort)((dx & 0x00FF) | value);
        }
    }
}
