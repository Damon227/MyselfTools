using System;
using System.Threading.Tasks;
using Grpc.Core;
using Teacherrpcservice;

namespace RpcClient
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            try
            {
                Channel channel = new Channel("192.168.1.78:50012", ChannelCredentials.Insecure);
                TeacherRpcService.TeacherRpcServiceClient client = new TeacherRpcService.TeacherRpcServiceClient(channel);

                Teacher teacher1 = client.GetName(new IdRequest { Id = 1 });
                Console.WriteLine($"Teacher1: {teacher1.Id},{teacher1.Name},{teacher1.Age}");

                Teacher teacher2 = await client.GetNameAsync(new IdRequest { Id = 1 });
                Console.WriteLine($"Teacher2: {teacher2.Id},{teacher2.Name},{teacher2.Age}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
          

            Console.ReadKey();
        }
    }
}
