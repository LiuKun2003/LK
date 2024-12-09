using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace LK.LKTool;

internal class Excel2Xml : ITool
{
    /// <summary>
    /// 将excel文件转换为json字符串并写入txt文件
    /// </summary>
    public void Execute(Arguments args)
    {
        if (args.PrintHelp)
        {
            PrintHelp();
        }
        if (args.InDirectory == null)
        {
            throw new NullReferenceException("工具 [Excel2Json] 需要指定 excel 文件目录。");
        }
        if (args.OutDirectory == null)
        {
            throw new NullReferenceException("工具 [Excel2Json] 需要指定 xml 文件目录。");
        }

        List<FileInfo> excels = new();
        excels.AddRange(args.InDirectory.EnumerateFiles("*.xlsx"));
        excels.AddRange(args.InDirectory.EnumerateFiles("*.xls"));
        Console.WriteLine($"共找到{excels.Count}个文件在 {args.InDirectory.FullName}。");
        foreach (FileInfo e in excels)
        {
            Console.WriteLine($"开始解析{e.FullName}。");
            DataTable dataTable = CreateDataTable(e);
            WriteXml(dataTable, args.OutDirectory);
            Console.WriteLine("完成。");
        }
    }

    /// <summary>
    /// 创建数据表。
    /// </summary>
    private static DataTable CreateDataTable(FileInfo info)
    {
        DataTable result = new(info.Name.Substring(0, info.Name.LastIndexOf('.')));
        using (FileStream fs = info.Open(FileMode.Open, FileAccess.Read))
        {
            ISheet sheet = CreateWorkbook(info.Extension, fs).GetSheetAt(0);
            int Rows = sheet.LastRowNum;//行长
            int Cols = sheet.GetRow(0).LastCellNum;//列宽

            //初始化表头
            {
                IRow row1 = sheet.GetRow(0);
                IRow row2 = sheet.GetRow(1);
                for (int i = 0; i < Cols; i++)
                {
                    Type type = JudgeType(row1.GetCell(i));
                    string name = row2.GetCell(i).StringCellValue;
                    result.Columns.Add(new DataColumn(name, type));
                }
            }
            DataFormatter dataFormatter = new();
            //读取数据
            {
                for(int i = 2; i <= Rows; i++)
                {
                    DataRow dr = result.NewRow();
                    IRow cells = sheet.GetRow(i);
                    for(int j = 0; j < Cols; j++)
                    {
                        ICell cell = cells.GetCell(j);
                        dr[j] = GetCellData(dataFormatter.FormatCellValue(cell), result.Columns[j].DataType);
                    }
                    result.Rows.Add(dr);
                }
            }
        }
        return result;
    }

    /// <summary>
    /// 创建工作表。
    /// </summary>
    private static IWorkbook CreateWorkbook(string extension, FileStream fs)
    {
        if(extension == ".xlsx")
        {
            return new XSSFWorkbook(fs);
        }
        else
        {
            return new HSSFWorkbook(fs);
        }
    }

    /// <summary>
    /// 判断表行类型。
    /// </summary>
    private static Type JudgeType(ICell cell)
    {
        if(cell.CellType != CellType.String)
        {
            throw new NotSupportedException("检测到无法访问的单元格类型，请检查excel第一行是否为文本类型单元格。");
        }

        return cell.StringCellValue.ToLower() switch
        {
            "boolean" or "bool" => typeof(bool),
            "byte" => typeof(byte),
            "char" => typeof(char),
            "datetime" or "date" => typeof(DateTime),
            "decimal" => typeof(decimal),
            "double" => typeof(double),
            "int16" or "short" => typeof(short),
            "int32" or "int" => typeof(int),
            "int64" or "long" => typeof(long),
            "sbyte" => typeof(sbyte),
            "single" or "float" => typeof(float),
            "string" => typeof(string),
            "timespan" or "time" => typeof(TimeSpan),
            "uint16" or "ushort" => typeof(ushort),
            "uint32" or "uint" => typeof(uint),
            "uint64" or "ulong" => typeof(ulong),
            _ => throw new NotSupportedException("检测到无法访问的类型;可用类型为：Boolean，Byte，Char，DateTime，Decimal，Double，Int16，Int32，Int64，SByte，Single，String，TimeSpan，UInt16，UInt32，UInt64。"),
        };
    }

    /// <summary>
    /// 获取单元格的值。
    /// </summary>
    private static object? GetCellData(string s, Type type)
    {
        return type.Name.ToLower() switch
        {
            "boolean" => Convert.ToBoolean(s),
            "byte" => Convert.ToByte(s),
            "char" => Convert.ToChar(s),
            "datetime" => Convert.ToDateTime(s),
            "decimal" => Convert.ToDecimal(s),
            "double" => Convert.ToDouble(s),
            "int16" => Convert.ToInt16(s),
            "int32" => Convert.ToInt32(s),
            "int64" => Convert.ToInt64(s),
            "sbyte" => Convert.ToSByte(s),
            "single" => Convert.ToSingle(s),
            "string" => Convert.ToString(s),
            "timespan" => TimeSpan.Parse(s),
            "uint16" => Convert.ToUInt16(s),
            "uint32" => Convert.ToUInt32(s),
            "uint64" => Convert.ToUInt64(s),
            _ => null
        };
    }

    /// <summary>
    /// 写入数据表到Xml文件中。
    /// </summary>
    private static void WriteXml(DataTable dataTable, DirectoryInfo outDirectory)
    {
        FileStream outStream = File.Open(outDirectory.FullName + "\\" + dataTable.TableName + ".xml", FileMode.Create, FileAccess.Write);
        dataTable.WriteXml(outStream, XmlWriteMode.WriteSchema);
    }

    /// <summary>
    /// 打印关于 Excel2Xml 的帮助。
    /// </summary>
    private static void PrintHelp()
    {
        Console.WriteLine("Excel2Xml工具：将excel文件转换为Xml字符串并写入Xml文件。使用此工具必须附带参数 [-i] 和 [-o] 来指定excel文件目录和输出xml文件目录。");
    }
}
