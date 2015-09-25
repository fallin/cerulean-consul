using System;
using FluentAssertions;
using NUnit.Framework;

namespace Cerulean.Consul.Tests
{
    [TestFixture]
    public class ParameterTests
    {
        [Test]
        public void ToQueryStringShouldReturnEmptyStringWhenEmptyCollection()
        {
            // Arrange
            Parameters parameters = new TestableParameters();

            // Act
            string queryString = parameters.ToQueryString();

            // Assert
            queryString.Should().BeEmpty();
        }

        [Test]
        public void ToQueryStringShouldReturnQueryStringWhenSingleValueCollection()
        {
            // Arrange
            Parameters parameters = new TestableParameters
            {
                {"flags", 42}
            };

            // Act
            string queryString = parameters.ToQueryString();

            // Assert
            queryString.Should().Be("flags=42");
        }

        [Test]
        public void ToQueryStringShouldReturnConcatenatedValuesWhenMultipleValueCollection()
        {
            // Arrange
            Parameters parameters = new TestableParameters
            {
                {"flags", 42},
                {"foo", "bar"}
            };

            // Act
            string queryString = parameters.ToQueryString();

            // Assert
            queryString.Should().Be("flags=42&foo=bar");
        }

        [Test]
        public void ToQueryStringShouldReturnUnaryValueWhenNameHasEmptyValue()
        {
            // Arrange
            Parameters parameters = new TestableParameters
            {
                {"recurse", ""}
            };

            // Act
            string queryString = parameters.ToQueryString();

            // Assert
            queryString.Should().Be("recurse");
        }

        [Test]
        public void ToQueryStringShouldReturnUnaryValueWhenNameHasNullValue()
        {
            // Arrange
            Parameters parameters = new TestableParameters
            {
                {"recurse", null}
            };

            // Act
            string queryString = parameters.ToQueryString();

            // Assert
            queryString.Should().Be("recurse");
        }

        [Test]
        public void ToQueryStringShouldIgnoreEmptyNames()
        {
            // Arrange
            Parameters parameters = new TestableParameters
            {
                {"flags", 42},
                {"", "a"}
            };

            // Act
            string queryString = parameters.ToQueryString();

            // Assert
            queryString.Should().Be("flags=42");
        }

        [Test, Ignore("Dictionary does not allow null key")]
        public void ToQueryStringShouldIgnoreNullNames()
        {
            // Arrange
            Parameters parameters = new TestableParameters
            {
                {"flags", 42},
                {null, "a"}
            };

            // Act
            string queryString = parameters.ToQueryString();

            // Assert
            queryString.Should().Be("flags=42");
        }
    }
}