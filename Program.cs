global using GraphQL;
global using GraphQL.Types;

using GQL.Demo.Interfaces;
using GQL.Demo.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddApiServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();//.UseAuthorization(); no auth added currently

app.MapGet("/api/products", ([FromServices] IProductProvider productProvider) =>
{
    return productProvider.GetProducts();
})
.WithName("GetProducts");

app.UseGraphQLAltair();
app.UseGraphQL<ISchema>();

app.Run();
