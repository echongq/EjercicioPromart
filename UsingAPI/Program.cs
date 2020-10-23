using UsingAPI.Access;
using System;
using System.Threading.Tasks;
namespace UsingAPI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            var url = "http://dummy.restapiexample.com/api/v1/employees";

            var response = APIAccess.getApi(url);

            foreach (var employee in response.Result.data)
            {
                Console.WriteLine("{");
                Console.WriteLine($"'id' : { employee.id} " + ",");
            }
            //Console.ReadLine();
            var url2 = "https://localhost:44356/api/Employees";
            for (int i = 0; i < response.Result.data.Count - 1; i++)
            {
                var result = await APIAccess.postApi(url2, response.Result.data[i]);

                Console.WriteLine("AQUI RESPUESTA POST: " + result.ToString());

            }
            Console.ReadLine();
        }
    }
}
