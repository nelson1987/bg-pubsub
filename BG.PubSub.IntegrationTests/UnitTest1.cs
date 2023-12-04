// namespace BG.PubSub.IntegrationTests;

using AutoFixture;
using AutoFixture.AutoMoq;

public class UnitTest1
{
    [Fact]
    public async void Test1()
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        //Arrange
        // var evento = fixture.Create<CriaAlunoEvent>();
        // await RabbitFixture.Produce(evento);
        // //Act

        // // Assert
        // var criaAlunoEvent = RabbitFixture
        //     .Consume<CriaAlunoEvent>();
    }
}