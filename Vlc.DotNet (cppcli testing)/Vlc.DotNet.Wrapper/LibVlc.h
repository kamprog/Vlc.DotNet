#pragma once
#pragma comment(lib, "libvlc.lib")

#include "stdafx.h"
#include "wchar.h"
#include <vlc/vlc.h>
#using <System.dll>

using namespace System;

namespace Vlc
{
	namespace DotNet
	{
		namespace Wrapper
		{
			public ref class LibVlc
			{
			public:
				property static String ^ Version
				{
					String ^ get()
					{
						return gcnew String(libvlc_get_version());
					}
				}
				property static String ^ Compiler
				{
					String ^ get()
					{
						return gcnew String(libvlc_get_compiler());
					}
				}
				property static String ^ Changeset
				{
					String ^ get()
					{
						return gcnew String(libvlc_get_changeset());
					}
				}
			};
		}
	}
}

