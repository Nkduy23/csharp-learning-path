using ConsoleFundamentals.Models;
using ConsoleFundamentals.Services;
using ConsoleFundamentals.Utils;

var service = new StudentService();
service.LoadFromFile(FileHelper.Load());

while (true)
{
    Console.WriteLine("\n===== 📚 QUẢN LÝ SINH VIÊN =====");
    Console.WriteLine("1. Thêm sinh viên");
    Console.WriteLine("2. Thêm điểm");
    Console.WriteLine("3. Danh sách sinh viên");
    Console.WriteLine("4. Tìm kiếm");
    Console.WriteLine("5. Xếp hạng theo điểm TB");
    Console.WriteLine("6. Xóa sinh viên");
    Console.WriteLine("7. Lưu & Thoát");
    Console.Write("👉 Chọn: ");

    string choice = Console.ReadLine() ?? "";

    switch (choice)
    {
        case "1":
            Console.Write("Tên: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("Tuổi: ");
            int age = int.Parse(Console.ReadLine() ?? "0");
            service.Add(new Student(name, age));
            break;

        case "2":
            Console.Write("Tên sinh viên: ");
            string sName = Console.ReadLine() ?? "";
            Console.Write("Môn học: ");
            string subject = Console.ReadLine() ?? "";
            Console.Write("Điểm: ");
            double score = double.Parse(Console.ReadLine() ?? "0");
            service.AddScore(sName, subject, score);
            break;

        case "3":
            var all = service.GetAll();
            if (all.Count == 0) { Console.WriteLine("Chưa có sinh viên."); break; }
            Console.WriteLine("\n--- Danh sách ---");
            all.ForEach(s => Console.WriteLine(s));
            break;

        case "4":
            Console.Write("Nhập tên cần tìm: ");
            string keyword = Console.ReadLine() ?? "";
            var found = service.FindByName(keyword);
            Console.WriteLine(found != null ? found.ToString() : "❌ Không tìm thấy.");
            break;

        case "5":
            Console.WriteLine("\n--- 🏆 Xếp hạng ---");
            var ranked = service.SortByAverage();
            for (int i = 0; i < ranked.Count; i++)
                Console.WriteLine($"{i + 1}. {ranked[i]}");
            break;

        case "6":
            Console.Write("Tên sinh viên cần xóa: ");
            string delName = Console.ReadLine() ?? "";
            service.Remove(delName);
            break;

        case "7":
            FileHelper.Save(service.GetAll());
            Console.WriteLine("👋 Tạm biệt!");
            return;

        default:
            Console.WriteLine("❌ Lựa chọn không hợp lệ.");
            break;
    }
}