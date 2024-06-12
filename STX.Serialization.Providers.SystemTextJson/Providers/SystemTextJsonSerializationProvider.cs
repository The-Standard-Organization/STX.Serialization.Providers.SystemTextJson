// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using STX.Serialization.Providers.Abstractions;
using STX.Serialization.Providers.SystemTextJson.Brokers;
using STX.Serialization.Providers.SystemTextJson.Models.Foundations.Serializations;
using STX.Serialization.Providers.SystemTextJson.Models.Providers.Serializations.SystemTextJson.Exceptions;
using STX.Serialization.Providers.SystemTextJson.Services.Foundations.Serializations;
using Xeptions;

namespace STX.Serialization.Providers.SystemTextJson.Providers
{
    public class SystemTextJsonSerializationProvider : ISerializationProvider
    {
        private readonly ISerializationService serializationService;

        public SystemTextJsonSerializationProvider()
        {
            IHost host = RegisterServices(jsonSerializerOptions: null);
            this.serializationService = host.Services.GetRequiredService<ISerializationService>();
        }

        public SystemTextJsonSerializationProvider(JsonSerializerOptions jsonSerializerOptions)
        {
            IHost host = RegisterServices(jsonSerializerOptions);
            this.serializationService = host.Services.GetRequiredService<ISerializationService>();
        }

        public ValueTask<TOutput> SerializeAsync<TInput, TOutput>(TInput @object)
        {
            try
            {
                return this.serializationService.SerializeAsync<TInput, TOutput>(@object);
            }
            catch (SerializationValidationException serializationValidationException)
            {
                throw new SystemTextJsonSerializationProviderValidationException(
                    message: "Serialization provider validation error(s) occurred, fix the error(s) and try again.",
                    innerException: serializationValidationException.InnerException as Xeption,
                    data: serializationValidationException.InnerException.Data);
            }
            catch (SerializationDependencyValidationException serializationDependencyValidationException)
            {
                throw new SystemTextJsonSerializationProviderValidationException(
                    message: "Serialization provider validation error(s) occurred, fix the error(s) and try again.",
                    serializationDependencyValidationException.InnerException as Xeption,
                    data: serializationDependencyValidationException.InnerException.Data);
            }
            catch (SerializationDependencyException serializationDependencyException)
            {
                throw new SystemTextJsonSerializationProviderDependencyException(
                    message: "Serialization provider dependency error occurred, please contact support.",
                    serializationDependencyException.InnerException as Xeption);
            }
            catch (SerializationServiceException serializationServiceException)
            {
                throw new SystemTextJsonSerializationProviderServiceException(
                    message: "Serialization provider service error occurred, please contact support.",
                    serializationServiceException.InnerException as Xeption);
            }
        }

        public ValueTask<TOutput> DeserializeAsync<TInput, TOutput>(TInput json)
        {
            try
            {
                return this.serializationService.DeserializeAsync<TInput, TOutput>(json);
            }
            catch (SerializationValidationException serializationValidationException)
            {
                throw new SystemTextJsonSerializationProviderValidationException(
                    message: "Serialization provider validation error(s) occurred, fix the error(s) and try again.",
                    innerException: serializationValidationException.InnerException as Xeption,
                    data: serializationValidationException.InnerException.Data);
            }
            catch (SerializationDependencyValidationException serializationDependencyValidationException)
            {
                throw new SystemTextJsonSerializationProviderValidationException(
                    message: "Serialization provider validation error(s) occurred, fix the error(s) and try again.",
                    serializationDependencyValidationException.InnerException as Xeption,
                    data: serializationDependencyValidationException.InnerException.Data);
            }
            catch (SerializationDependencyException serializationDependencyException)
            {
                throw new SystemTextJsonSerializationProviderDependencyException(
                    message: "Serialization provider dependency error occurred, please contact support.",
                    serializationDependencyException.InnerException as Xeption);
            }
            catch (SerializationServiceException serializationServiceException)
            {
                throw new SystemTextJsonSerializationProviderServiceException(
                    message: "Serialization provider service error occurred, please contact support.",
                    serializationServiceException.InnerException as Xeption);
            }
        }

        private static IHost RegisterServices(JsonSerializerOptions jsonSerializerOptions)
        {
            IHostBuilder builder = Host.CreateDefaultBuilder();

            builder.ConfigureServices(configuration =>
            {
                configuration.AddSingleton(options => jsonSerializerOptions);
                configuration.AddTransient<ISystemTextSerializationBroker, SystemTextSerializationBroker>();
                configuration.AddTransient<ISerializationService, SerializationService>();
            });

            IHost host = builder.Build();

            return host;
        }
    }
}
