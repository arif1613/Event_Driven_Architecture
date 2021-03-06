using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Microsoft.OpenApi.Models;
using OrderService.Api.Model.Event;
using OrderService.Api.Model.Notification;
using OrderService.Api.Model.Request;
using OrderService.Api.Services;
using OrderService.Api.Utils.OrderActions;
using OrderService.Api.Utils.ReceiptGenerator;
using OrderService.Data.Context;
using OrderService.Data.Repo;
using OrderService.Data.UnitOfWork;

namespace OrderService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });


            //Register context
            services.AddTransient<IOrderContext, OrderContext>();

            //register repos
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //register services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductEventService, ProductEventService>();
            services.AddScoped<IOrderService, Services.OrderService>();
            services.AddScoped<IOrderEventService, OrderEventService>();
            services.AddScoped<IReceiptService, ReceiptService>();
            services.AddScoped<IReceiptGenerator, ReceiptGenerator>();
            services.AddScoped<IOrderCalculation,OrderCalculation>();


            //MediatR
            BuildMediator(services);

            AddSwagger(services);
            services.AddControllers();
        }

        private static IMediator BuildMediator(IServiceCollection services)
        {
            //Event
            services.AddMediatR(typeof(ProductCreated));
            services.AddMediatR(typeof(OrderCreated));
            //Request
            services.AddMediatR(typeof(CreateProduct));
            services.AddMediatR(typeof(CreateOrder));
            //Notification
            services.AddMediatR(typeof(SendemailNotification));
            var provider = services.BuildServiceProvider();
            return provider.GetRequiredService<IMediator>();
        }


        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "OrderService api",
                    Version = "V1",
                    Description = $"Order api endpoints"

                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
                c.DisplayRequestDuration();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
