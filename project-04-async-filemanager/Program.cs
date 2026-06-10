using AsyncFileManager.Services;
using AsyncFileManager.Utils;

var service = new NoteService();
await service.LoadAsync();

while (true)
{
    Console.WriteLine("\n===== 📝 NOTE MANAGER =====");
    Console.WriteLine("1.  Xem tất cả ghi chú");
    Console.WriteLine("2.  Xem chi tiết");
    Console.WriteLine("3.  Thêm ghi chú");
    Console.WriteLine("4.  Sửa ghi chú");
    Console.WriteLine("5.  Xóa ghi chú");
    Console.WriteLine("6.  Tìm kiếm (full-text)");
    Console.WriteLine("7.  Lọc theo tag");
    Console.WriteLine("8.  Import từ file .txt");
    Console.WriteLine("9.  Export ghi chú");
    Console.WriteLine("10. Backup");
    Console.WriteLine("11. Xem log");
    Console.WriteLine("12. Lưu");
    Console.WriteLine("0.  Lưu & Thoát");
    Console.Write("👉 Chọn: ");

    switch (Console.ReadLine())
    {
        case "1":
            service.PrintAll();
            break;

        case "2":
            Console.Write("ID ghi chú: ");
            service.PrintDetail(int.Parse(Console.ReadLine() ?? "0"));
            break;

        case "3":
            Console.Write("Tiêu đề: ");
            string title = Console.ReadLine() ?? "";
            Console.Write("Nội dung: ");
            string content = Console.ReadLine() ?? "";
            Console.Write("Tags (cách nhau bằng dấu phẩy): ");
            var tags = (Console.ReadLine() ?? "")
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .ToList();
            await service.AddAsync(title, content, tags);
            break;

        case "4":
            Console.Write("ID cần sửa: ");
            int editId = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Tiêu đề mới: ");
            string newTitle = Console.ReadLine() ?? "";
            Console.Write("Nội dung mới: ");
            string newContent = Console.ReadLine() ?? "";
            await service.UpdateAsync(editId, newTitle, newContent);
            break;

        case "5":
            Console.Write("ID cần xóa: ");
            await service.DeleteAsync(int.Parse(Console.ReadLine() ?? "0"));
            break;

        case "6":
            Console.Write("Từ khóa: ");
            var found = service.Search(Console.ReadLine() ?? "");
            if (found.Count == 0) Console.WriteLine("Không tìm thấy.");
            else found.ForEach(n => Console.WriteLine(n));
            break;

        case "7":
            Console.Write("Tag: ");
            var byTag = service.FilterByTag(Console.ReadLine() ?? "");
            if (byTag.Count == 0) Console.WriteLine("Không có ghi chú nào với tag này.");
            else byTag.ForEach(n => Console.WriteLine(n));
            break;

        case "8":
            Console.Write("Nhập đường dẫn file (cách nhau bằng dấu phẩy): ");
            string[] paths = (Console.ReadLine() ?? "")
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(p => p.Trim())
                .ToArray();
            await service.ImportFromFilesAsync(paths);
            break;

        case "9":
            Console.Write("ID ghi chú: ");
            int exportId = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Format (txt / json): ");
            string format = Console.ReadLine() ?? "txt";
            await service.ExportToFileAsync(exportId, format);
            break;

        case "10":
            await service.BackupAsync();
            break;

        case "11":
            await Logger.PrintLogsAsync();
            break;

        case "12":
            await service.SaveAsync();
            break;

        case "0":
            await service.SaveAsync();
            Console.WriteLine("👋 Tạm biệt!");
            return;

        default:
            Console.WriteLine("❌ Lựa chọn không hợp lệ.");
            break;
    }
}