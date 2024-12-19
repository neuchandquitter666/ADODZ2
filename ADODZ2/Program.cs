using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp
{
    class Program
    {
        private static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Вставка нового товара");
                Console.WriteLine("2. Вставка нового типа товара");
                Console.WriteLine("3. Вставка нового поставщика");
                Console.WriteLine("4. Обновление товара");
                Console.WriteLine("5. Обновление поставщика");
                Console.WriteLine("6. Обновление типа товара");
                Console.WriteLine("7. Удаление товара");
                Console.WriteLine("8. Удаление поставщика");
                Console.WriteLine("9. Удаление типа товара");
                Console.WriteLine("10. Показать поставщика с наибольшим количеством товаров");
                Console.WriteLine("11. Показать поставщика с наименьшим количеством товаров");
                Console.WriteLine("12. Показать тип товара с наибольшим количеством товаров");
                Console.WriteLine("13. Показать тип товара с наименьшим количеством товаров");
                Console.WriteLine("14. Показать товары с поставки, которых прошло заданное количество дней");
                Console.WriteLine("0. Выйти");

                Console.Write("Введите номер действия: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": InsertProduct(); break;
                    case "2": InsertProductType(); break;
                    case "3": InsertSupplier(); break;
                    case "4": UpdateProduct(); break;
                    case "5": UpdateSupplier(); break;
                    case "6": UpdateProductType(); break;
                    case "7": DeleteProduct(); break;
                    case "8": DeleteSupplier(); break;
                    case "9": DeleteProductType(); break;
                    case "10": ShowSupplierWithMostProducts(); break;
                    case "11": ShowSupplierWithLeastProducts(); break;
                    case "12": ShowTypeWithMostProducts(); break;
                    case "13": ShowTypeWithLeastProducts(); break;
                    case "14": ShowProductsFromLastNDays(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный ввод. Попробуйте еще раз."); break;
                }
            }
        }

        private static void InsertProduct()
        {
            Console.Write("Введите название товара: ");
            string name = Console.ReadLine();
            Console.Write("Введите цену товара: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Введите количество товара: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("Введите ID поставщика: ");
            int supplierId = int.Parse(Console.ReadLine());
            Console.Write("Введите ID типа товара: ");
            int typeId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Products (Name, Price, Quantity, SupplierID, TypeID) VALUES (@Name, @Price, @Quantity, @SupplierID, @TypeID)", connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@SupplierID", supplierId);
                command.Parameters.AddWithValue("@TypeID", typeId);
                command.ExecuteNonQuery();
                Console.WriteLine("Товар успешно добавлен.");
            }
        }

        private static void InsertProductType()
        {
            Console.Write("Введите название типа товара: ");
            string typeName = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO ProductTypes (TypeName) VALUES (@TypeName)", connection);
                command.Parameters.AddWithValue("@TypeName", typeName);
                command.ExecuteNonQuery();
                Console.WriteLine("Тип товара успешно добавлен.");
            }
        }

        private static void InsertSupplier()
        {
            Console.Write("Введите название поставщика: ");
            string supplierName = Console.ReadLine();
            Console.Write("Введите контактную информацию поставщика: ");
            string contactInfo = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Suppliers (SupplierName, ContactInfo) VALUES (@SupplierName, @ContactInfo)", connection);
                command.Parameters.AddWithValue("@SupplierName", supplierName);
                command.Parameters.AddWithValue("@ContactInfo", contactInfo);
                command.ExecuteNonQuery();
                Console.WriteLine("Поставщик успешно добавлен.");
            }
        }

        private static void UpdateProduct()
        {
            Console.Write("Введите ID товара для обновления: ");
            int productId = int.Parse(Console.ReadLine());
            Console.Write("Введите новое название товара: ");
            string name = Console.ReadLine();
            Console.Write("Введите новую цену товара: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Введите новое количество товара: ");
            int quantity = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Products SET Name = @Name, Price = @Price, Quantity = @Quantity WHERE ProductID = @ProductID", connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@ProductID", productId);
                command.ExecuteNonQuery();
                Console.WriteLine("Информация о товаре успешно обновлена.");
            }
        }

        private static void UpdateSupplier()
        {
            Console.Write("Введите ID поставщика для обновления: ");
            int supplierId = int.Parse(Console.ReadLine());
            Console.Write("Введите новое название поставщика: ");
            string supplierName = Console.ReadLine();
            Console.Write("Введите новую контактную информацию: ");
            string contactInfo = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Suppliers SET SupplierName = @SupplierName, ContactInfo = @ContactInfo WHERE SupplierID = @SupplierID", connection);
                command.Parameters.AddWithValue("@SupplierName", supplierName);
                command.Parameters.AddWithValue("@ContactInfo", contactInfo);
                command.Parameters.AddWithValue("@SupplierID", supplierId);
                command.ExecuteNonQuery();
                Console.WriteLine("Информация о поставщике успешно обновлена.");
            }
        }

        private static void UpdateProductType()
        {
            Console.Write("Введите ID типа товара для обновления: ");
            int typeId = int.Parse(Console.ReadLine());
            Console.Write("Введите новое название типа товара: ");
            string typeName = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE ProductTypes SET TypeName = @TypeName WHERE TypeID = @TypeID", connection);
                command.Parameters.AddWithValue("@TypeName", typeName);
                command.Parameters.AddWithValue("@TypeID", typeId);
                command.ExecuteNonQuery();
                Console.WriteLine("Информация о типе товара успешно обновлена.");
            }
        }

        private static void DeleteProduct()
        {
            Console.Write("Введите ID товара для удаления: ");
            int productId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Products WHERE ProductID = @ProductID", connection);
                command.Parameters.AddWithValue("@ProductID", productId);
                command.ExecuteNonQuery();
                Console.WriteLine("Товар успешно удален.");
            }
        }

        private static void DeleteSupplier()
        {
            Console.Write("Введите ID поставщика для удаления: ");
            int supplierId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Suppliers WHERE SupplierID = @SupplierID", connection);
                command.Parameters.AddWithValue("@SupplierID", supplierId);
                command.ExecuteNonQuery();
                Console.WriteLine("Поставщик успешно удален.");
            }
        }

        private static void DeleteProductType()
        {
            Console.Write("Введите ID типа товара для удаления: ");
            int typeId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM ProductTypes WHERE TypeID = @TypeID", connection);
                command.Parameters.AddWithValue("@TypeID", typeId);
                command.ExecuteNonQuery();
                Console.WriteLine("Тип товара успешно удален.");
            }
        }

        private static void ShowSupplierWithMostProducts()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT s.SupplierID, s.SupplierName, COUNT(p.ProductID) as ProductCount FROM Suppliers s LEFT JOIN Products p ON s.SupplierID = p.SupplierID GROUP BY s.SupplierID, s.SupplierName ORDER BY ProductCount DESC OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine($"Поставщик с наибольшим количеством товаров: {reader["SupplierName"]} ({reader["ProductCount"]} товаров)");
                }
                reader.Close();
            }
        }

        private static void ShowSupplierWithLeastProducts()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT s.SupplierID, s.SupplierName, COUNT(p.ProductID) as ProductCount FROM Suppliers s LEFT JOIN Products p ON s.SupplierID = p.SupplierID GROUP BY s.SupplierID, s.SupplierName ORDER BY ProductCount ASC OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine($"Поставщик с наименьшим количеством товаров: {reader["SupplierName"]} ({reader["ProductCount"]} товаров)");
                }
                reader.Close();
            }
        }

        private static void ShowTypeWithMostProducts()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT pt.TypeID, pt.TypeName, COUNT(p.ProductID) as ProductCount FROM ProductTypes pt LEFT JOIN Products p ON pt.TypeID = p.TypeID GROUP BY pt.TypeID, pt.TypeName ORDER BY ProductCount DESC OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine($"Тип товара с наибольшим количеством товаров: {reader["TypeName"]} ({reader["ProductCount"]} товаров)");
                }
                reader.Close();
            }
        }

        private static void ShowTypeWithLeastProducts()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT pt.TypeID, pt.TypeName, COUNT(p.ProductID) as ProductCount FROM ProductTypes pt LEFT JOIN Products p ON pt.TypeID = p.TypeID GROUP BY pt.TypeID, pt.TypeName ORDER BY ProductCount ASC OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine($"Тип товара с наименьшим количеством товаров: {reader["TypeName"]} ({reader["ProductCount"]} товаров)");
                }
                reader.Close();
            }
        }

        private static void ShowProductsFromLastNDays()
        {
            Console.Write("Введите количество дней: ");
            int days = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Products WHERE DATEDIFF(day, DeliveryDate, GETDATE()) <= @Days", connection);
                command.Parameters.AddWithValue("@Days", days);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Название: {reader["Name"]}, Дата поставки: {reader["DeliveryDate"]}");
                }
                reader.Close();
            }
        }
    }
}