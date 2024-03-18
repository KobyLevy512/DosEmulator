
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

        #region GeneralPurposeRegisters
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
        /// <summary>
        /// si (Source Index) : Used as a pointer to access memory indirectly
        /// </summary>
        public ushort Si
        {
            get => si;
            set => si = value;
        }
        /// <summary>
        /// di (Dest. Index) : Like the si register, this is also used for indirectly accessing the memory
        /// </summary>
        public ushort Di
        {
            get => di;
            set => di = value;
        }
        /// <summary>
        /// bp (Base Pointer) : Like the bx register, this is also used to store base addresses. Generally, this register is used to access local variables in a procedure.
        /// </summary>
        public ushort Bp
        {
            get => bp;
            set => bp = value;
        }
        /// <summary>
        /// sp (Stack Pointer) : A very important register. Maintains the program stack, and so should be used carefully.
        /// </summary>
        public ushort Sp
        {
            get => sp;
            set => sp = value;
        }
        #endregion

        #region SegmentsRegisters
        /// <summary>
        /// The cs (Code Segment) register points at the segment containing the currently executing machine instructions. Since you can change the value of the cs register, you can switch to a new code segment when you want to execute the code located there. 
        /// </summary>
        public ushort Cs
        {
            get => cs;
            set => cs = value;
        }
        /// <summary>
        /// The ds (Data Segment) register generally points at global variables for the program. You can change the value of the ds register to access additional data in other segments. 
        /// </summary>
        public ushort Ds
        {
            get => ds;
            set => ds = value;
        }
        /// <summary>
        /// The es (Extra Segment) register is an extra segment register. 8086 programs often use this segment register to gain access to segments when it is difficult or impossible to modify the other segment registers. 
        /// </summary>
        public ushort Es
        {
            get => es;
            set => es = value;
        }
        /// <summary>
        /// The ss (Stack Segment) register points at the segment containing the 8086 stack. The stack is where the 8086 stores important machine state information, subroutine return addresses, procedure parameters, and local variables. In general, you do not modify the stack segment register because too many things in the system depend upon it. 
        /// </summary>
        public ushort Ss
        {
            get => ss;
            set => ss = value;
        }
        #endregion

        #region SpecialRegisters
        /// <summary>
        /// The ip is sometimes referred to as the pc (program counter). These registers cannot be accessed directly in real mode, they are modified by the cpu during execution.
        /// </summary>
        public ushort Ip
        {
            get => ip;
            set => ip = value;
        }

        /// <summary>
        /// Sign Flag
        /// </summary>
        public bool S
        {
            get => (flag & 128) != 0;
            set
            {
                flag |= value ? (ushort)128 : (ushort)0;
            }
        }
        /// <summary>
        /// Zero Flag
        /// </summary>
        public bool Z
        {
            get => (flag & 64) != 0;
            set
            {
                flag |= value ? (ushort)64 : (ushort)0;
            }
        }
        /// <summary>
        /// Auxiliary Carry Flag
        /// </summary>
        public bool Ac
        {
            get => (flag & 16) != 0;
            set
            {
                flag |= value ? (ushort)16 : (ushort)0;
            }
        }
        /// <summary>
        /// Parity Flag
        /// </summary>
        public bool P
        {
            get => (flag & 4) != 0;
            set
            {
                flag |= value ? (ushort)4 : (ushort)0;
            }
        }
        /// <summary>
        /// Carry Flag 
        /// </summary>
        public bool Cy
        {
            get => (flag & 1) != 0;
            set
            {
                flag |= value ? (ushort)1 : (ushort)0;
            }
        }
        /// <summary>
        /// Overflow Flag
        /// </summary>
        public bool O
        {
            get => (flag & 2048) != 0;
            set
            {
                flag |= value ? (ushort)2048 : (ushort)0;
            }
        }
        /// <summary>
        /// Directional Flag 
        /// </summary>
        public bool D
        {
            get => (flag & 1024) != 0;
            set
            {
                flag |= value ? (ushort)1024 : (ushort)0;
            }
        }
        /// <summary>
        /// Interrupt Flag
        /// </summary>
        public bool I
        {
            get => (flag & 512) != 0;
            set
            {
                flag |= value ? (ushort)512 : (ushort)0;
            }
        }
        /// <summary>
        /// Trap Flag
        /// </summary>
        public bool T
        {
            get => (flag & 256) != 0;
            set
            {
                flag |= value ? (ushort)256 : (ushort)0;
            }
        }
        #endregion

        /// <summary>
        /// Set register value by his address.
        /// </summary>
        /// <param name="adr"></param>
        /// <param name="value"></param>
        public void SetRegister(byte adr, byte value)
        {
            byte val = (byte)(adr & 7);
            switch(val)
            {
                case 0:
                    Al = value; 
                    break;
                case 1:
                    Cl = value;
                    break;
                case 2:
                    Dl = value;
                    break;
                case 3:
                    Bl = value;
                    break;
                case 4:
                    Ah = value;
                    break;
                case 5:
                    Ch = value;
                    break;
                case 6:
                    Dh = value;
                    break;
                case 7:
                    Bh = value;
                    break;
            }
        }
        /// <summary>
        /// Set register value by his address.
        /// </summary>
        /// <param name="adr"></param>
        /// <param name="value"></param>
        public void SetRegister(byte adr, ushort value)
        {
            byte val = (byte)(adr & 7);
            switch (val)
            {
                case 0:
                    Ax = value;
                    break;
                case 1:
                    Cx = value;
                    break;
                case 2:
                    Dx = value;
                    break;
                case 3:
                    Bx = value;
                    break;
                case 4:
                    Sp = value;
                    break;
                case 5:
                    Bp = value;
                    break;
                case 6:
                    Si = value;
                    break;
                case 7:
                    Di = value;
                    break;
            }
        }
        /// <summary>
        /// Set segment value by his address.
        /// </summary>
        /// <param name="adr"></param>
        /// <param name="value"></param>
        public void SetSegment(byte adr, ushort value)
        {
            byte val = (byte)(adr & 3);
            switch (val)
            {
                case 0:
                    Es = value;
                    break;
                case 1:
                    Cs = value;
                    break;
                case 2:
                    Ss = value;
                    break;
                case 3:
                    Ds = value;
                    break;
            }
        }

        /// <summary>
        /// Get register byte value by his address.
        /// </summary>
        /// <param name="adr"></param>
        /// <param name="value"></param>
        public byte GetRegisterByte(byte adr)
        {
            byte val = (byte)(adr & 7);
            switch (val)
            {
                case 0:
                    return Al;
                case 1:
                    return Cl;
                case 2:
                    return Dl;
                case 3:
                    return Bl;
                case 4:
                    return Ah;
                case 5:
                    return Ch;
                case 6:
                    return Dh;
                default:
                    return Bh;
            }
        }

        /// <summary>
        /// Get register ushort value by his address.
        /// </summary>
        /// <param name="adr"></param>
        /// <param name="value"></param>
        public ushort GetRegister(byte adr)
        {
            byte val = (byte)(adr & 7);
            switch (val)
            {
                case 0:
                    return Ax;
                case 1:
                    return Cx;
                case 2:
                    return Dx;
                case 3:
                    return Bx;
                case 4:
                    return Sp;
                case 5:
                    return Bp;
                case 6:
                    return Si;
                default:
                    return Di;
            }
        }
    }
}
