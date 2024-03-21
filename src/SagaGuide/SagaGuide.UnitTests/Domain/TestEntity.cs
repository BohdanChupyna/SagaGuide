using System;
using SagaGuide.Core.Domain.Common;
using MediatR;

namespace SagaGuide.UnitTests.Domain;

public class TestEntity : Entity<int>
{
    public TestEntity()
    {
        Id = new Random().Next();

        RegisterEvent(new TestEntityCreated(this));
    }
}

public class TestEntityCreated : INotification
{
    public TestEntityCreated(TestEntity entity) => Id = entity.Id;

    public int Id { get; }
}