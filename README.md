[![.NET CI](https://github.com/axdelafuen/maui-xale-toolkit/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/axdelafuen/maui-xale-toolkit/actions/workflows/dotnet.yml)

# Maui.XaleToolkit

Many native UI controls are still missing for * FREE * in .NET MAUI or the CommunityToolkit.

This repository aims to fill that gap by implementing some of them during my free time.  

## Implemented controls

| Status | Control | Platforms | Comment |
| - | - | - | - |
| 🟢 | ComboBox | Android & Windows | _I dont have Mac/Apple device to develop for Maccalyste & iOS platforms._ |

**Legend:**  
🟢 Fully functional  
🟠 Some issues, but still usable  
🔴 Critical issues or not yet implemented

## Installation

_Soon on NuGet.org_

_A NuGet package is currently available on my GitHub account, but an access token is required to pull it remotely. Until the NuGet.org registry is set up, you will need to download it manually._

## Getting started

In order to use the `Maui.XaleToolkit` you need to call the extension method in your `MauiProgram.cs` file as follows:

```cs
using CommunityToolkit.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiXaleToolkit(); // Initialize the Maui.XaleToolkit by adding this line of code
			
		// Continue initializing your .NET MAUI App here

		return builder.Build();
	}
}
```

## XAML Usage

```xaml
xmlns:xale="clr-namespace:Maui.XaleToolkit.Views;assembly=Maui.XaleToolkit"
```
