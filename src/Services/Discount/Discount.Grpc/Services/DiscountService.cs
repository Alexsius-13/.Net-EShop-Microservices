using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext) 
	: DiscountProtoService.DiscountProtoServiceBase
{
	public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
	{
		var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

		if (coupon is null)
			coupon = new Coupon { Amount = 0, ProductName = "No Discount", Description = "No Discount" };

		var couponModel = coupon.Adapt<CouponModel>();

		return couponModel;
	}
	public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
	{
		var coupon = request.Coupon.Adapt<Coupon>();
		if (coupon is null)
			throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid object"));

		dbContext.Coupons.Add(coupon);
		await dbContext.SaveChangesAsync();

		var couponModel = coupon.Adapt<CouponModel>();
		return couponModel;
	}

	public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
	{
		var coupon = request.Coupon.Adapt<Coupon>();
		if(coupon is null)
			throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid object"));

		dbContext.Coupons.Update(coupon);
		await dbContext.SaveChangesAsync();

		var couponModel = coupon.Adapt<CouponModel>();
		return couponModel;
	}

	public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
	{
		var coupon = await dbContext.Coupons
						.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
		if(coupon is null)
			throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid object"));

		dbContext.Coupons.Remove(coupon);
		await dbContext.SaveChangesAsync();

		return new DeleteDiscountResponse { Success = true };
	}
}
