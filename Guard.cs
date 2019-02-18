public static class Guard
    {
        public static void AssertArgumentNotNull(object value, string argumentName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void AssertArgumentNotNullOrEmptyOrWhitespace(string value, string argumentName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(argumentName);
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Value cannot be an empty string.", argumentName);
            }
        }

        public static void AssertArgumentNotLessThanOrEqualToZero(int? value, string argumentName)
        {
            if (value == null)
                throw new ArgumentNullException(argumentName);

            if (value <= 0)
                throw new ArgumentException("Value cannot be less than or equal to zero.", argumentName);
        }

        public static void AssertArgumentNotLessThanOrEqualToZero(long? value, string argumentName)
        {
            if (value == null)
                throw new ArgumentNullException(argumentName);

            if (value <= 0)
                throw new ArgumentException("Value cannot be less than or equal to zero.", argumentName);
        }

        public static void AssertArgumentNotLessThanOrEqualToZero(double? value, string argumentName)
        {
            if (value == null)
                throw new ArgumentNullException(argumentName);

            if (value <= 0)
                throw new ArgumentException("Value cannot be less than or equal to zero.", argumentName);
        }

        public static void AssertArgumentNotLessThanOrEqualToZero(decimal? value, string argumentName)
        {
            if (value == null)
                throw new ArgumentNullException(argumentName);

            if (value <= 0)
                throw new ArgumentException("Value cannot be less than or equal to zero.", argumentName);
        }

        public static void AssertArgumentEquals<T>(T object1, T object2, string message = null)
        {
            if (!object1.Equals(object2))
            {
                message = message ?? $"{object1} should equal to {object2}";
                throw new InvalidOperationException(message);
            }
        }

        public static void AssertArgumentNotEquals<T>(T object1, T object2, string message = null)
        {
            if (object1.Equals(object2))
            {
                message = message ?? $"{object1} shouldn't equal to {object2}";
                throw new InvalidOperationException(message);
            }
        }
        
        public static void AssertArgumentFalse(bool boolValue, string argumentName)
        {
            if (boolValue)
            {
                throw new ArgumentException("Value should be false", argumentName);
            }
        }

        public static void AssertArgumentTrue(bool boolValue, string argumentName)
        {
            if (!boolValue)
                throw new ArgumentException("Value should be true", argumentName);
        }

        public static void AssertArgumentLength(string stringValue, int maximum, string argumentName)
        {
            int length = stringValue.Trim().Length;
            if (length > maximum)
                throw new ArgumentException($"Value cannot be greater than {maximum}.", argumentName);
        }

        public static void AssertArgumentLength(string stringValue, int minimum, int maximum, string argumentName)
        {
            if (string.IsNullOrEmpty(stringValue))
                stringValue = string.Empty;

            int length = stringValue.Trim().Length;
            if (length < minimum || length > maximum)
                throw new ArgumentException($"Value cannot be less than {minimum} or greater than {maximum}.",
                    argumentName);
        }

        public static void AssertArgumentMatches(string pattern, string stringValue, string argumentName, string validationMessage = null)
        {
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(stringValue))
            {
                if (!string.IsNullOrWhiteSpace(validationMessage))
                    throw new UserFriendlyException(validationMessage);

                throw new ArgumentException($"Value should match pattern {pattern}", argumentName);
            }
        }

        public static void AssertArgumentRange(double value, double minimum, double maximum, string argumentName)
        {
            if (value < minimum || value > maximum)
            {
                throw new ArgumentException($"Value cannot be less than {minimum} or greater than {maximum}.",
                    argumentName);
            }
        }
        public static void AssertArgumentRange(double value, double minimum, double maximum, string argumentName, string validationMessage)
        {
            if (value < minimum || value > maximum)
            {
                if (!string.IsNullOrEmpty(validationMessage))
                    throw new UserFriendlyException(validationMessage);

                throw new ArgumentException($"Value cannot be less than {minimum} or greater than {maximum}.",
                    argumentName);
            }
        }
        public static void AssertArgumentRange(decimal value, decimal minimum, decimal maximum, string argumentName)
        {
            if (value < minimum || value > maximum)
            {
                throw new ArgumentException($"Value cannot be less than {minimum} or greater than {maximum}.",
                    argumentName);
            }
        }
        public static void AssertArgumentRange(DateTime value, DateTime minimum, DateTime maximum, string argumentName)
        {
            if (value < minimum || value > maximum)
            {
                throw new ArgumentException($"Value cannot be less than {minimum} or greater than {maximum}.",
                    argumentName);
            }
        }

        public static void AssertArgumentRange(float value, float minimum, float maximum, string argumentName)
        {
            if (value < minimum || value > maximum)
            {
                throw new ArgumentException($"Value cannot be less than {minimum} or greater than {maximum}.",
                    argumentName);
            }
        }

        public static void AssertArgumentRange(int value, int minimum, int maximum, string argumentName)
        {
            if (value < minimum || value > maximum)
            {
                throw new ArgumentException($"Value cannot be less than {minimum} or greater than {maximum}.",
                    argumentName);
            }
        }

        public static void AssertArgumentRange(long value, long minimum, long maximum, string argumentName)
        {
            if (value < minimum || value > maximum)
            {
                throw new ArgumentException($"Value cannot be less than {minimum} or greater than {maximum}.",
                    argumentName);
            }
        }

        public static void AssertEmailFormat(string email, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Value cannot be null or an empty string.", argumentName);

            if (!Regex.IsMatch(email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                throw new ArgumentException($"Invalid Email {email} Format.", argumentName);
        }

        public static void AssertArgumentDateTime(string dateTimeValue, string argumentName)
        {
            DateTime dateTime = DateTime.Now;
            if (!DateTime.TryParse(dateTimeValue, out dateTime))
                throw new ArgumentException($"Invalid DateTime {dateTime} Format.", argumentName);
        }

        public static void AssertGuidNotEmpty(Guid value, string argumentName)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void AssertGuidNotNullOrEmpty(Guid? value, string argumentName)
        {
            if (value == null || value == Guid.Empty)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void AssertDocumentIsValid(Document document)
        {
            if (document.Size > 0 && (document.Bytes == null || document.Bytes.Length == 0))
                throw new ArgumentException($"Invalid or Empty document {document.Name}");
        }

        //public static void AssertPropertyValueIsAlphabetical(string text, string propertyName)
        //{
        //    if(Regex.IsMatch(text, @"^[\p{L}\s]*$") == false)
        //    {
        //        throw new ArgumentException($"{propertyName} does not accept numbers and special characters.");
        //    }
        //}

        public static void AssertPropertyValueIsDigits(string text, string propertyName)
        {
            if (text.All(char.IsDigit) == false)
            {
                throw new ArgumentException($"{propertyName} accepts only numbers");
            }
        }

        public static void AssertLocalizedTextIsValid(LocalizedText localizedText,
            string argumentName, string pattern, string validationMessage = null)
        {
            var parsedName = JObject.Parse(localizedText.StringValue);

            foreach (var property in parsedName.Properties())
            {
                Guard.AssertArgumentMatches(pattern, property.Value.ToString(), $"{property.Name} Name", validationMessage);
            }
        }

        public static void AssertArgumentNotLessThan(int value, int compareToValue, string errorMessage)
        {
            if (value < compareToValue)
            {
                throw new UserFriendlyException(errorMessage);
            }
        }

        public static void AssertArgumentNotLessThan(decimal value, decimal compareToValue, string errorMessage)
        {
            if (value < compareToValue)
            {
                throw new UserFriendlyException(errorMessage);
            }
        }

        public static void AssertArgumentNotGreaterThan(int value, int compareToValue, string errorMessage)
        {
            if (value > compareToValue)
            {
                throw new UserFriendlyException(errorMessage);
            }
        }

        public static void AssertPrecedesDate(DateTime value, DateTime dateToPrecede, string parameterName, string validationMessage = null)
        {
            if (value >= dateToPrecede)
            {
                if (!string.IsNullOrWhiteSpace(validationMessage))
                    throw new UserFriendlyException(validationMessage);

                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        public static void AssertEnumValueIsValid(int value, Type enumType, string parameterName) {
            if (!Enum.IsDefined(enumType, value)) {
                throw new ArgumentException($"The value {value} does not represent an enum of type {enumType.Name} for parameter {parameterName}");
            }
        }

        public static void AssertArgumentContains(object enumValue, Type enumType, string parameterName)
        {
            if (!Enum.IsDefined(enumType, Convert.ToInt32(enumValue)))
            {
                throw new ArgumentException($"The value {enumValue} does not represent an enum of type {enumType.Name} for parameter {parameterName}");
            }
        }

        public static void AssertArgumentContains<T>(IEnumerable<T> collection, T value, string message = null)
        {
            if (!collection.Contains(value))
            {
                message = message ?? $"{collection} must contain {value}";
                throw new InvalidOperationException(message);
            }
        }

        public static void AssertArgumentNotContains<T>(IEnumerable<T> collection, T value, string message = null)
        {
            if (collection.Contains(value))
            {
                message = message ?? $"{collection} must not contain {value}";
                throw new InvalidOperationException(message);
            }
        }
    }
