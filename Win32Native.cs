using System;
using System.Runtime.InteropServices;

namespace FallMinLibTools
{
    public static partial class Win32Native
    {
        [DllImport("dbghelp", SetLastError = true)] // PIMAGE_SECTION_HEADER LastRvaSection
        public static extern IntPtr ImageRvaToVa(IntPtr NtHeaders, IntPtr Base, uint Rva, int LastRvaSection);

        [DllImport("kernel32", SetLastError = true)]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFileMapping(IntPtr hFile, int lpFileMappingAttributes, int flProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, int dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, int dwNumberOfBytesToMap);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int UnmapViewOfFile(IntPtr hMapFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int _lclose(IntPtr hFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int CloseHandle(IntPtr hObject);
    }

    public static partial class Win32Native
    {
        public const int NULL = 0;
        public const int OF_SHARE_COMPAT = 0;
        public const int PAGE_READONLY = 2;
        public const int FILE_MAP_READ = 4;
        public const int IMAGE_DIRECTORY_ENTRY_EXPORT = 0;
        public const int IMAGE_NT_OPTIONAL_HDR64_MAGIC = 0x20b;

        public static readonly IntPtr INVALID_HANDLE_VALUE = (IntPtr)(-1);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IMAGE_DOS_HEADER
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public char[] e_magic;       // Magic number
        public ushort e_cblp;    // Bytes on last page of file
        public ushort e_cp;      // Pages in file
        public ushort e_crlc;    // Relocations
        public ushort e_cparhdr;     // Size of header in paragraphs
        public ushort e_minalloc;    // Minimum extra paragraphs needed
        public ushort e_maxalloc;    // Maximum extra paragraphs needed
        public ushort e_ss;      // Initial (relative) SS value
        public ushort e_sp;      // Initial SP value
        public ushort e_csum;    // Checksum
        public ushort e_ip;      // Initial IP value
        public ushort e_cs;      // Initial (relative) CS value
        public ushort e_lfarlc;      // File address of relocation table
        public ushort e_ovno;    // Overlay number
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public ushort[] e_res1;    // Reserved words
        public ushort e_oemid;       // OEM identifier (for e_oeminfo)
        public ushort e_oeminfo;     // OEM information; e_oemid specific
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public ushort[] e_res2;    // Reserved words
        public int e_lfanew;      // File address of new exe header
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct IMAGE_OPTIONAL_HEADERS
    {
        [FieldOffset(0)]
        public int Magic;

        [FieldOffset(2)]
        public byte MajorLinkerVersion;

        [FieldOffset(3)]
        public byte MinorLinkerVersion;

        [FieldOffset(4)]
        public uint SizeOfCode;

        [FieldOffset(8)]
        public uint SizeOfInitializedData;

        [FieldOffset(12)]
        public uint SizeOfUninitializedData;

        [FieldOffset(16)]
        public uint AddressOfEntryPoint;

        [FieldOffset(20)]
        public uint BaseOfCode;

        // PE32 contains this additional field
        [FieldOffset(24)]
        public uint BaseOfData;

        [FieldOffset(28)]
        public uint ImageBase;

        [FieldOffset(32)]
        public uint SectionAlignment;

        [FieldOffset(36)]
        public uint FileAlignment;

        [FieldOffset(40)]
        public ushort MajorOperatingSystemVersion;

        [FieldOffset(42)]
        public ushort MinorOperatingSystemVersion;

        [FieldOffset(44)]
        public ushort MajorImageVersion;

        [FieldOffset(46)]
        public ushort MinorImageVersion;

        [FieldOffset(48)]
        public ushort MajorSubsystemVersion;

        [FieldOffset(50)]
        public ushort MinorSubsystemVersion;

        [FieldOffset(52)]
        public uint Win32VersionValue;

        [FieldOffset(56)]
        public uint SizeOfImage;

        [FieldOffset(60)]
        public uint SizeOfHeaders;

        [FieldOffset(64)]
        public uint CheckSum;

        [FieldOffset(68)]
        public int Subsystem;

        [FieldOffset(70)]
        public int DllCharacteristics;

        [FieldOffset(72)]
        public uint SizeOfStackReserve;

        [FieldOffset(76)]
        public uint SizeOfStackCommit;

        [FieldOffset(80)]
        public uint SizeOfHeapReserve;

        [FieldOffset(84)]
        public uint SizeOfHeapCommit;

        [FieldOffset(88)]
        public uint LoaderFlags;

        [FieldOffset(92)]
        public uint NumberOfRvaAndSizes;

        [FieldOffset(96)]
        public IMAGE_DATA_DIRECTORY ExportTable;

        [FieldOffset(104)]
        public IMAGE_DATA_DIRECTORY ImportTable;

        [FieldOffset(112)]
        public IMAGE_DATA_DIRECTORY ResourceTable;

        [FieldOffset(120)]
        public IMAGE_DATA_DIRECTORY ExceptionTable;

        [FieldOffset(128)]
        public IMAGE_DATA_DIRECTORY CertificateTable;

        [FieldOffset(136)]
        public IMAGE_DATA_DIRECTORY BaseRelocationTable;

        [FieldOffset(144)]
        public IMAGE_DATA_DIRECTORY Debug;

        [FieldOffset(152)]
        public IMAGE_DATA_DIRECTORY Architecture;

        [FieldOffset(160)]
        public IMAGE_DATA_DIRECTORY GlobalPtr;

        [FieldOffset(168)]
        public IMAGE_DATA_DIRECTORY TLSTable;

        [FieldOffset(176)]
        public IMAGE_DATA_DIRECTORY LoadConfigTable;

        [FieldOffset(184)]
        public IMAGE_DATA_DIRECTORY BoundImport;

        [FieldOffset(192)]
        public IMAGE_DATA_DIRECTORY IAT;

        [FieldOffset(200)]
        public IMAGE_DATA_DIRECTORY DelayImportDescriptor;

        [FieldOffset(208)]
        public IMAGE_DATA_DIRECTORY CLRRuntimeHeader;

        [FieldOffset(216)]
        public IMAGE_DATA_DIRECTORY Reserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IMAGE_FILE_HEADER
    {
        public ushort Machine;
        public ushort NumberOfSections;
        public uint TimeDateStamp;
        public uint PointerToSymbolTable;
        public uint NumberOfSymbols;
        public ushort SizeOfOptionalHeader;
        public ushort Characteristics;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IMAGE_DATA_DIRECTORY
    {
        public uint VirtualAddress;
        public uint Size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IMAGE_EXPORT_DIRECTORY
    {
        public uint Characteristics;
        public uint TimeDateStamp;
        public ushort MajorVersion;
        public ushort MinorVersion;
        public uint Name;
        public uint Base;
        public uint NumberOfFunctions;
        public uint NumberOfNames;
        public uint AddressOfFunctions;     // RVA from base of image
        public uint AddressOfNames;     // RVA from base of image
        public uint AddressOfNameOrdinals;  // RVA from base of image
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct IMAGE_NT_HEADERS
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public char[] Signature;

        [FieldOffset(4)]
        public IMAGE_FILE_HEADER FileHeader;

        [FieldOffset(24)]
        public IMAGE_OPTIONAL_HEADERS OptionalHeader;
    }
}
