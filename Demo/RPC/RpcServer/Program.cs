using System;
using Grpc.Core;
using Teacherrpcservice;

namespace RpcServer
{
    class Program
    {
        private static void Main(string[] args)
        {
            const int Port = 50012;

            Server server = new Server
            {
                Services = { TeacherRpcService.BindService(new TeacherService()) },
                Ports = { new ServerPort("192.168.1.78", Port, ServerCredentials.Insecure) }
            };

            server.Start();

            Console.WriteLine("grpc server listening on port " + Port);
            Console.WriteLine("Press any key to stop server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
