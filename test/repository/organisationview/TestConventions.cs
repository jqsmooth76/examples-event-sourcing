using AutoFixture;
using MongoDB.Bson;

namespace organisationview;

internal class CompositeTestingCustomization : CompositeCustomization
{
    public CompositeTestingCustomization() :
        base(
            new MongoObjectIdCustomization())
    {

    }

    private class MongoObjectIdCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Register(ObjectId.GenerateNewId);
        }
    }
}