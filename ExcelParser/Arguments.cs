namespace LK.LKTool;
/// <summary>
/// 表示用户启动工具时的参数。
/// </summary>
internal class Arguments
{
    /// <summary>
    /// 指示是否应该填入缺省参数
    /// </summary>
    private bool _defaultArg;

    /// <summary>
    /// 获取用户指定的输入文件。
    /// </summary>
    public FileInfo? InFileInfo { get; private set; }

    /// <summary>
    /// 获取用户指定的输入目录。
    /// </summary>
    public DirectoryInfo? InDirectory { get; private set; }

    /// <summary>
    /// 获取用户指定的输出文件。
    /// </summary>
    public FileInfo? OutFileInfo { get; private set; }

    /// <summary>
    /// 获取用户指定的输出目录。
    /// </summary>
    public DirectoryInfo? OutDirectory { get; private set; }

    /// <summary>
    /// 获取一个值，此值指示是否打印帮助信息。
    /// </summary>
    public bool PrintHelp { get; private set; }

    /// <summary>
    /// 创建一个参数对象。
    /// </summary>
    /// <param name="args">用户输入的字符串队列。</param>
    public Arguments(Queue<string> args)
    {
        _defaultArg = false;
        InFileInfo = null;
        InDirectory = null;
        OutFileInfo = null;
        OutDirectory = null;
        PrintHelp = false;

        while (args.Count != 0)
        {
            Analyze(args);
        }

        if (_defaultArg)
        {
            Default();
        }
    }

    /// <summary>
    /// 解析用户输入的字符串。
    /// </summary>
    private void Analyze(Queue<string> args)
    {
        string s = args.Dequeue();
        switch (s)
        {
            case "-h":
                Help(args);
                break;
            case "-i":
                In(args);
                break;
            case "-o":
                Out(args);
                break;
            case "-d":
                _defaultArg = true;
                break;
            default:
                throw new ArgumentException($"无效的参数[{s}]。");
        }
    }

    /// <summary>
    /// 解析 [-i] 参数。
    /// </summary>
    private void In(Queue<string> args)
    {
        if (args.Count == 0) throw new ArgumentException("[-i] 未指定路径。");
        string s = args.Dequeue();
        if (File.Exists(s)) InFileInfo = new FileInfo(s);
        else if (Directory.Exists(s)) InDirectory = new DirectoryInfo(s);
        else throw new ArgumentException("[-i] 未指定路径或路径错误。");
    }

    /// <summary>
    /// 解析 [-o] 参数。
    /// </summary>
    private void Out(Queue<string> args)
    {
        if (args.Count == 0) throw new ArgumentException("[-o] 未指定路径。");
        string s = args.Dequeue();
        if (File.Exists(s)) OutFileInfo = new FileInfo(s);
        else if (Directory.Exists(s)) OutDirectory = new DirectoryInfo(s);
        else throw new ArgumentException("[-o] 未指定路径或路径错误。");
    }

    /// <summary>
    /// 解析 [-h] 参数。
    /// </summary>
    private void Help(Queue<string> _) => PrintHelp = true;

    /// <summary>
    /// 填入缺省参数。
    /// </summary>
    private void Default()
    {
        InDirectory ??= new DirectoryInfo(".");
        OutDirectory ??= new DirectoryInfo(".");
    }
}
