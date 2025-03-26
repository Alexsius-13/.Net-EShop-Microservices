namespace Ordering.Domain.ValueObjects;

public record ProductId
{
	public Guid Value { get; }
	private ProductId(Guid value) => Value = value;
	public static ProductId Of(Guid value)
	{
		ArgumentNullException.ThrowIfNull(value);

		if (value == Guid.Empty)
			throw new DomainException("ProductId cannot be empty");

		return new ProductId(value);
	}

	public static implicit operator Guid(ProductId productId) => productId.Value;

	public static implicit operator ProductId(Guid value) => Of(value);

	public override string ToString() => Value.ToString();
}
