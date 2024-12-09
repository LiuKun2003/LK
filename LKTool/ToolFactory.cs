namespace LK.LKTool;
internal class ToolFactory
{
    public static ITool GetTool(Queue<string> args)
    {
        string s = args.Dequeue();
        return s switch
        {
            "help" => new Help(),
            "excel2xml" => new Excel2Xml(),
            _ => throw new ArgumentException($"无效的工具名 [{s}]。")
        };
    }
}
