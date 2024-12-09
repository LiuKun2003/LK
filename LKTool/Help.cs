namespace LK.LKTool;
internal class Help : ITool
{
    public void Execute(Arguments args)
    {
        Console.WriteLine("工具列表：");
        Console.WriteLine("[Help]：打印帮助信息。");
        Console.WriteLine("[Excel2Xml]：将Excel文件转换为XML文件。");
        Console.WriteLine("参数列表：");
        Console.WriteLine("[-h]：获取工具的帮助信息。");
        Console.WriteLine("[-i]：指定输入文件或输入目录。");
        Console.WriteLine("[-o]：指定输出文件或输出目录。");
        Console.WriteLine("[-d]：将允许有默认值的参数设置为默认值。");
    }
}
