using Dapr.Client;

namespace DaprCounter
{
    using Dapr.Client;
    public class Program
    {
        const string storeName = "statestore";
        const string key = "counter";

        public static async Task Main(string[] args)
        {
            var daprClient = new DaprClientBuilder().Build();
            var counter = await daprClient.GetStateAsync<int>(storeName, key);

            while (true)
            {
                Console.WriteLine($"Counter = {counter++}");

                await daprClient.SaveStateAsync(storeName, key, counter);
                await Task.Delay(1000);
            }
        }
    }
}