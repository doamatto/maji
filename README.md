# マージ
マージ (romanised: Māji, English translation: merge) is a dirt-simple tool to merge JSON strings and files.

## Building from source
1. Install [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/install/) (developed using SDK 5.0.400)
2. Build the project (`dotnet build -v d`) (see [here](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-build))

## Usage
- `maji files`: Merge one or more files into the first file mentioned.
  - **Warning:** You should back-up both files prior to ensure no data is lost in the event that the merge isn't how you expected it to be.
  - Example: `maji files original.json patch1.json patch2.json`
- `maji strings`: Merge one or more strings of JSON into the first string.
  - Example: `maji strings {"1": "yay"} {"2": "yayuh"}`
  - Response: `{"1": "yay", "2": "yayuh"}`

## FAQ
**Q: Why did you make this?** Two reasons: needed to merge rules for [Ella](https://github.com/doamatto/ella-filters) and wanted to try out Newtonsoft after all these years. The last time I played with Visual Studio was 2013, and before that I used Visual Studio 2008 (I feel old).<br/>
**Q: Is this Windows exclusive? Does it work in Wine? Why?** Technically: yes, this only works on Windows. I have seen mentions that installing .NET Framework 4.7.1 (see [here](https://www.reddit.com/r/wine_gaming/comments/8r6low)) can resolve issues with Newtonsoft in Wine, but I can't guarantee it. As far as I'm aware, there aren't any other well-crafted JSON libraries outside of Json.NET for C#.
**Q: Will you create a native version for macOS, Linux, and BSD?** If it does not work at all on Linux, then I likely will write a v2 of マージ for that reason. What it would be written in is beyond me, as I want to keep experimenting with random tools and languages.
