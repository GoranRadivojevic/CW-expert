//=================================================================
// Windows.cs
//=================================================================
// Copyright (C) 2011 S56A YT7PWR
//
// This program is free software; you can redistribute it and/or
// modify it under the terms of the GNU General Public License
// as published by the Free Software Foundation; either version 2
// of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
//=================================================================

using System;
using System.Runtime.InteropServices;

namespace CWExpert
{
    unsafe class Windows
    {
        [DllImport("kernel32.dll", EntryPoint = "EnterCriticalSection")]
        public static extern void EnterCriticalSection(IntPtr* cs_ptr);

        [DllImport("kernel32.dll", EntryPoint = "LeaveCriticalSection")]
        public static extern void LeaveCriticalSection(IntPtr* cs_ptr);

        [DllImport("kernel32.dll", EntryPoint = "InitializeCriticalSection")]
        public static extern void InitializeCriticalSection(IntPtr* cs_ptr);

        [DllImport("kernel32.dll", EntryPoint = "InitializeCriticalSectionAndSpinCount")]
        public static extern int InitializeCriticalSectionAndSpinCount(IntPtr* cs_ptr, uint spincount);

        [DllImport("kernel32.dll", EntryPoint = "DeleteCriticalSection")]
        public static extern byte DeleteCriticalSection(IntPtr* cs_ptr);

    }
}
