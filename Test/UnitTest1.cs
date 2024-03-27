using System.Diagnostics;

namespace Test
{
    public class Tests
    {

        [Test]
        public void createProduct()
        {
            OrderProcessorRepositoryImpl orderProcessor = new OrderProcessorRepositoryImpl();
            Products products = new Products();
            {
                ProductName = "Oven";
                Price = 5000.00;
                Description = "Oven for cooking";
                StockQuantity = 2;
            }
            bool result = orderProcessor.CreateProduct(products);
            Assert.That(result, Is.True);
        }
        [Test]
        public void addToCart()
        {
            OrderProcessorRepositoryImpl orderProcessor = new OrderProcessorRepositoryImpl();
            Products products = new Products();
            {
                ProductId = 6;
            }
            Customer customer = new Customer();
            {
                CustomerId = 6;
            }
            Cart cart = new Cart();
            {
                Quantity = 1;
            }
            bool result = orderProcessor.AddToCart(products);
            Assert.That(result, Is.True);

        }
        public class ProductIdNotFoundExceptionTest
        {
            [SetUp]
            public void Setup()
            {
            }


            [Test]

            [TestCase(43, ExpectedResult = 0)]
            [TestCase(45, ExpectedResult = 0)]
            public int ProductIdNotFoundExecptionTest(int ProductId)
            {
                OrderProcessorService orderProcessorService = new OrderProcessorService();
                OrderProcessorRepositoryImpl orderProcessorRepository = new OrderProcessorRepositoryImpl();


                bool Pnotfound = orderProcessorRepository.ProductNotExist(ProductId);
                return Pnotfound ? 1 : 0;
            }
        }
    }
}