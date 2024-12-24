namespace LK.LKTool;
internal class Program
{
    public static void Main(string[] _args)
    {
        try
        {
            Queue<string> args = new();
            foreach (string item in _args)
            {
                args.Enqueue(item.ToLower());
            }
            ITool tool = ToolFactory.GetTool(args);
            Arguments arguments = new(args);
            tool.Execute(arguments);
        }
        catch (Exception ex)
        {
            Console.WriteLine("运行错误！");
            Console.Error.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            Console.WriteLine("输入 help 以获取更多帮助。");
        }
    }
}

