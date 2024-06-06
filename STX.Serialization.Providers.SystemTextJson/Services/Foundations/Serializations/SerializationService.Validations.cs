// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System;
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
