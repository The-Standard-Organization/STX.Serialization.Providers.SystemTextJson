// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
using System.IO;
using STX.Serialization.Providers.SystemTextJson.Models.Foundations.Serializations;

namespace STX.Serialization.Providers.SystemTextJson.Services.Foundations.Serializations
{
    internal partial class SerializationService
    {
        private static void ValidateInputIsNotNull<TInput>(TInput @object)
        {
            if (@object is null)
            {
                throw new NullSerializationException(message: $"Input is null.");
            }
        }

        private static void ValidateInput<TInput>(TInput json)
        {
            switch (typeof(TInput))
            {
                case Type _ when typeof(TInput) == typeof(string):
                    Validate(
                        (Rule: IsInvalid($"{json}"), Parameter: nameof(json)));
                    break;

                case Type _ when typeof(TInput) == typeof(byte[]):
                    Validate(
                        (Rule: IsInvalid(json as byte[]), Parameter: nameof(json)));
                    break;

                case Type _ when typeof(TInput) == typeof(Stream):
                    Validate(
                        (Rule: IsInvalid(json as Stream), Parameter: nameof(json)));
                    break;

                default:
                    ValidateInputIsNotNull(json);
                    break;
            }
        }

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(byte[] bytes) => new
        {
            Condition = bytes is null || bytes.Length == 0,
            Message = "Bytes is required"
        };

        private static dynamic IsInvalid(Stream stream) => new
        {
            Condition = stream is null || stream.Length == 0,
            Message = "Stream is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidSerializationException =
                new InvalidSerializationException(
                    message: "Invalid input. Please correct the errors and try again.");

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidSerializationException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidSerializationException.ThrowIfContainsErrors();
        }
    }
}
