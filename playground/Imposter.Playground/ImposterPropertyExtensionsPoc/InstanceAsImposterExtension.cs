/*namespace Imposter.Playground.ImposterPropertyExtensionsPoc
{



    public class Usage
    {
        static void Test()
        {
            var imposter = IOrderService.Imposter();
        }
    }

    public static class IOrderServiceImposterExtensions
    {
        extension(IOrderService imposter)
        {
            public static OrderServiceImposter Imposter()
                => new OrderServiceImposter();
        }
    }

    public class OrderServiceImposter
    {
        public class OrderServiceImposterInstance : IOrderService
        {
        }
    }

    public interface IOrderService
    {
    }
}*/