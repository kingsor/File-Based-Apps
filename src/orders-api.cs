#:sdk Microsoft.NET.Sdk.Web

#:property PublishAot=false

#:package Microsoft.EntityFrameworkCore.Sqlite@9.0.0
#:package Microsoft.AspNetCore.OpenApi@9.0.11
#:package Swashbuckle.AspNetCore.SwaggerUI@10.0.1

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();

builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseSqlite("Data Source=orders.db");
});

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", app.Environment.ApplicationName);
});

app.MapGet("/orders", async (OrderDbContext db) =>
{
    return await db.Orders.ToListAsync();
});

app.MapGet("/orders/{id}", async (int id, OrderDbContext db) =>
    await db.Orders.FindAsync(id)
        is Order order
            ? Results.Ok(order)
            : Results.NotFound());

app.MapPost("/orders", async (Order order, OrderDbContext db) =>
{
    db.Orders.Add(order);
    await db.SaveChangesAsync();

    return Results.Created($"/orders/{order.Id}", order);
});


app.Run();
return;

public record Order(Guid Id, string OrderNumber, decimal Amount);

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }
    public DbSet<Order> Orders { get; set; }
}