﻿namespace Ordering.Domain.Abstractions;

public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
{
	private readonly List<IDomainEvent> _domainEvents = new();
	public IReadOnlyList<IDomainEvent> Events => throw new NotImplementedException();

	public void AddDomainEvent(IDomainEvent domainEvent)
	{
		_domainEvents.Add(domainEvent);
	}

	public IDomainEvent[] ClearDomainEvents()
	{
		var dequeuedDomainEvents = _domainEvents.ToArray();

		_domainEvents.Clear();

		return dequeuedDomainEvents;
	}
}
