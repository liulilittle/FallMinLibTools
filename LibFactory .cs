using System;
using System.Runtime.InteropServices;

namespace FallMinLibTools
{
    public abstract partial class LibFactory
    {
        private static readonly IntPtr NULL = IntPtr.Zero;

        public static IntPtr OpenLibFile(string strLibFileName) 
        {
            IntPtr hFileBase = Win32Native._lopen(strLibFileName, Win32Native.OF_SHARE_COMPAT);
            if (hFileBase == Win32Native.INVALID_HANDLE_VALUE)
                throw new Exception("Unable to open the DLL file, the file does not exist or occupied.");
            return hFileBase;
        }

        public static IntPtr CreateMapFile(IntPtr hFileBase)
        {
            IntPtr hFileMapping = Win32Native.CreateFileMapping(hFileBase, Win32Native.NULL, Win32Native.PAGE_READONLY, 0, 0, null);
            if (hFileBase == Win32Native.INVALID_HANDLE_VALUE)
                throw new Exception("Unable to map file, the file does not exist or is invalid.");
            return hFileMapping;
        }

        public static IntPtr MapFileOfView(IntPtr hFileMapping)
        {
            IntPtr hMapViewOfFile = Win32Native.MapViewOfFile(hFileMapping, Win32Native.FILE_MAP_READ, 0, 0, Win32Native.NULL);
            if (hMapViewOfFile == NULL)
                throw new Exception("Unable to view the map file handle is invalid.");
            return hMapViewOfFile;
        }

        public static void UnmapViewOfFile(IntPtr hMapViewOfFile)
        {
            Win32Native.UnmapViewOfFile(hMapViewOfFile);
        }

        public static void CloseLibFile(IntPtr hFileBase)
        {
            Win32Native._lclose(hFileBase);
        }

        public static void CloseMapFile(IntPtr hFileMapping)
        {
            Win32Native.CloseHandle(hFileMapping);
        }
    }

    public abstract partial class LibFactory
    {
        public static IMAGE_DOS_HEADER GetDosHearder(IntPtr ptr)
        {
            IMAGE_DOS_HEADER sDosHearder = (IMAGE_DOS_HEADER)Marshal.PtrToStructure(ptr, typeof(IMAGE_DOS_HEADER));
            if (new string(sDosHearder.e_magic) != "MZ")
                throw new Exception("Unable to get the DOS image header, DLL file is invalid.");
            return sDosHearder;
        }

        public static IntPtr GetNTHeader(IntPtr ptr, IMAGE_DOS_HEADER sDosHearder)
        {
            return (IntPtr)(sDosHearder.e_lfanew + (long)ptr); // e_lfanew 248     
        }

        public static IMAGE_NT_HEADERS GetNTHeader(IntPtr ptr)
        {
            IMAGE_NT_HEADERS sNTHeader = (IMAGE_NT_HEADERS)Marshal.PtrToStructure(ptr, typeof(IMAGE_NT_HEADERS));
            if (new string(sNTHeader.Signature) != "PE\0\0")
                throw new Exception("Unable to get PE NT image header, DLL file is invalid.");
            if (sNTHeader.OptionalHeader.Magic == Win32Native.IMAGE_NT_OPTIONAL_HDR64_MAGIC)
                throw new Exception("It does not support x64 Dll to Lib.");
            return sNTHeader;
        }

        public static IntPtr GetExportDirectory(IntPtr pNTHeader, IntPtr pDosHeader, IMAGE_NT_HEADERS sNTHeader)
        {
            // 63 63 72 75 6E 2E 63 6F 6D
            IntPtr pExportDirectory = Win32Native.ImageRvaToVa(pNTHeader, pDosHeader, sNTHeader.OptionalHeader.ExportTable.VirtualAddress, Win32Native.NULL);
            if (pExportDirectory == NULL)
                throw new Exception("Unable to get the Image Export Directory.");
            return pExportDirectory;
        }

        public static IMAGE_EXPORT_DIRECTORY GetExportDirectory(IntPtr ptr)
        {
            IMAGE_EXPORT_DIRECTORY sExportDirectory = (IMAGE_EXPORT_DIRECTORY)Marshal.PtrToStructure(ptr, typeof(IMAGE_EXPORT_DIRECTORY));
            if (sExportDirectory.AddressOfFunctions == Win32Native.NULL)
                throw new Exception("Image Export Directory did not export any function.");
            return sExportDirectory;
        }

        public static IntPtr GetExportOfNames(IntPtr pNTHeader, IntPtr pDosHeader, IMAGE_EXPORT_DIRECTORY sExportDirectory)
        {
            IntPtr ppExportOfNames = Win32Native.ImageRvaToVa(pNTHeader, pDosHeader, sExportDirectory.AddressOfNames, Win32Native.NULL);
            if (ppExportOfNames == NULL)
                throw new Exception("Image Export Directory did not export any function.");
            return ppExportOfNames;
        }

        public static string GetExportOfNames(IntPtr pNTHeader, IntPtr pDosHeader, IntPtr ppExportOfNames, uint nNoOfExport)
        {
            uint rvaExportOfName = (uint)Marshal.ReadInt32(ppExportOfNames, (int)(nNoOfExport * sizeof(int)));
            IntPtr pstrExportOfName = Win32Native.ImageRvaToVa(pNTHeader, pDosHeader, rvaExportOfName, Win32Native.NULL);
            if (ppExportOfNames == NULL)
                throw new Exception("Image Export Directory did not export any function.");
            return Marshal.PtrToStringAnsi(pstrExportOfName);
        }
    }
}
